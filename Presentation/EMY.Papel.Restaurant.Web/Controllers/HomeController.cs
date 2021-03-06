using EMY.Papel.Restaurant.Core.Application.Abstract;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;
using EMY.Papel.Restaurant.Infrastructure.Persistence;
using EMY.Papel.Restaurant.Web.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EMY.Papel.Restaurant.Web.Controllers
{
    public class HomeController : Controller
    {
        IDatabaseFactory databaseFactory;
        IEmailService _emailService;
        public HomeController(IDatabaseFactory databaseFactory, IEmailService emailService)
        {
            this.databaseFactory = databaseFactory;
            _emailService = emailService;
        }



        public IActionResult Index()
        {
            var Categories = databaseFactory.MenuCategoryRead.Table.Include(o => o.Menus).Where(o => !o.IsDeleted && o.Menus.Count > 0).ToList();
            ViewBag.Categories = Categories;
            return PartialView();
        }
        public async Task<IActionResult> Menu(string categoryname, string categoryid)
        {
            var menuCategory = await databaseFactory.MenuCategoryRead.Table.Include(o => o.Menus).
                FirstOrDefaultAsync(o => o.MenuCategoryID == categoryid.ToGuid() && !o.IsDeleted);

            return View(menuCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AuthorizeReservation(string id)
        {
            var reservation = await databaseFactory.ReservationRead.GetByIdAsync(id.ToGuid());
            if (reservation == null) return NotFound();
            reservation.ConfirmationStatus = ReservationConfirmationStatus.Authorized;
            await databaseFactory.ReservationWrite.UpdateAsync(reservation, this.ActiveUserID());
            ViewBag.reservation = reservation;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AuthorizeSubscribe(string id)
        {
            var reservation = await databaseFactory.ReservationRead.GetByIdAsync(id.ToGuid());
            if (reservation == null) return NotFound();
            if (string.IsNullOrEmpty(reservation.Email))
            {
                return BadRequest("Email is required!");
            }
            var mail = databaseFactory.MailListRead.Get(o => o.Email == reservation.Email && !o.IsDeleted);

            ViewBag.reservation = reservation;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(string datepicker_field, string time, int people, string name_reserve, string email_reserve, string telephone_reserve, string opt_message_reserve, string terms, bool subscribemaillist)
        {
            datepicker_field = datepicker_field.Substring(4);
            DateTime dtPick = DateTime.Parse(datepicker_field + " " + time.Replace('.', ':'));
            Guid id = Guid.NewGuid();
            var result = await databaseFactory.ReservationWrite.AddAsync(new Reservation()
            {
                ReservationID = id,
                Date = dtPick,
                NumberOfPeople = people,
                Name = name_reserve,
                Email = email_reserve ?? "",
                Phone = telephone_reserve ?? "",
                Message = opt_message_reserve ?? "",
                ConfirmationStatus = ReservationConfirmationStatus.Pending
            }, this.ActiveUserID());
            var mail = await _emailService.SendEmail(email_reserve, "Authorization", $"<h2>Please authorize your reservation!</h2> <a href='https://localhost:44383/Home/AuthorizeReservation?id={id.ToString()}'>For authorization please click here!</a><br><br><br>" +
                $"" +
               (subscribemaillist ? $"<h2>Please authorize your mail adres!</h2> <a href='https://localhost:44383/Home/AuthorizeSubscribe?id={id.ToString()}'>For authorization please click here!</a>" : ""), System.Net.Mail.MailPriority.Normal);

            if (!mail.IsSuccess)
            {
                return Problem(mail.Message);
            }



            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShopCart() => View();
        public async Task<IActionResult> CheckOut() => View();
        public async Task<IActionResult> Search(string q)
        {
            var result = await databaseFactory.MenuRead.Table.AsNoTracking().Include(o => o.Category).Where(o => !o.IsDeleted && !o.Category.IsDeleted && (o.Name.Contains(q) || o.Description.Contains(q) || o.Category.Name.Contains(q))).ToListAsync();
            ViewBag.q = q;
            return View(result);
        }

        public async Task<IActionResult> CreateOrder(
            string productsJson, string payment_method, string notes,
            string fullName, string email, string phone,
            string fullAdress, string city, int postalCode
            )
        {
            List<OrderProductViewModel> products = JsonSerializer.Deserialize<List<OrderProductViewModel>>(productsJson);
            Guid orderID = Guid.NewGuid();
            if (products == null || products.Count == 0) return NoContent();
            List<string> productsids = products.Select(o => o.productid).ToList();
            var dbproducts = databaseFactory.MenuRead.GetWhere(o => productsids.Contains(o.MenuID.ToString().ToLower()));
            decimal TotalPrice = 0;
            var orderItems = new List<OrderItem>();
            foreach (var product in products)
            {
                var curprod = dbproducts.FirstOrDefault(o => o.MenuID.ToString().ToLower() == product.productid.ToLower());
                if (curprod == null) return NotFound("Same products are does not exists in system!");
                OrderItem oi = new OrderItem()
                {
                    MenuID = curprod.MenuID,
                    ItemPrice = curprod.Price,
                    ItemCount = product.count,
                    MenuText = $"({curprod.Code}) {curprod.Name}",
                    OrderID = orderID,
                    OrderItemID = Guid.NewGuid()
                };
                TotalPrice += oi.ItemPrice * oi.ItemCount;
                orderItems.Add(oi);
            }
            var count = databaseFactory.OrderRead.Table.Count();
            string orderNumber = ((long)count).CreateOrderNumber();

            var order = new Order()
            {
                OrderID = orderID,
                OrderNumber = orderNumber,
                PaymentMethod = payment_method,
                FullName = fullName,
                EmailAdress = email,
                PhoneNumber = phone,
                FullAdress = fullAdress,
                City = city,
                PostalCode = postalCode,
                AfterDiscountPrice = TotalPrice,
                RealPrice = TotalPrice,
                PaymentAuthorizationToken = "",
                Discount = 0,
                Notes = notes ?? ""
            };
            _emailService.SendEmail(email, "Order", $"<h2>Your order number is {orderNumber}</h2> <a href='https://localhost:44383/Home/OrderAuthorize?id={orderID.ToString()}'>For order authorize please click here!</a><br><br><br>", System.Net.Mail.MailPriority.Normal);
            await databaseFactory.OrderWrite.AddAsync(order, this.ActiveUserID());
            await databaseFactory.OrderItemWrite.AddRangeAsync(orderItems, this.ActiveUserID());
            return Ok(orderID.ToString());

        }
        public async Task<IActionResult> Confirmation(string id)
        {
            var result = await databaseFactory.OrderRead.GetByIdAsync(id.ToGuid());

            if (result == null)
            {
                return NotFound("Order not found!");
            }
            ViewBag.id = result.OrderNumber;
            return View();
        }

        public async Task<IActionResult> OrderAuthorize(string id)
        {
            var result = await databaseFactory.OrderRead.GetByIdAsync(id.ToGuid());

            if (result == null)
            {
                return NotFound("Order not found!");
            }
            result.OrderStatus = OrderStatus.Authorized;
            await databaseFactory.OrderWrite.UpdateAsync(result, this.ActiveUserID());                               
            return View(result);
        }        
    }
}