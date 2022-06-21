using EMY.Papel.Restaurant.Core.Application.Abstract;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Web.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMenuCategoryReadRepository _menuCategoryReadRepository;
        private readonly IReservationWriteRepository _reservationWrite;
        private readonly IEmailService _emailService;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IOrderItemWriteRepository _orderItemWriteRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public HomeController(ILogger<HomeController> logger, IMenuCategoryReadRepository menuCategoryReadRepository, IReservationWriteRepository reservationWrite, IEmailService emailService, IMenuReadRepository menuReadRepository, IOrderItemWriteRepository orderItemWriteRepository, IOrderWriteRepository orderWriteRepository)
        {
            _logger = logger;
            _menuCategoryReadRepository = menuCategoryReadRepository;
            _reservationWrite = reservationWrite;
            _emailService = emailService;
            _menuReadRepository = menuReadRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public IActionResult Index()
        {
            var Categories = _menuCategoryReadRepository.Table.Include(o => o.Menus).Where(o => !o.IsDeleted && o.Menus.Count > 0).ToList();
            ViewBag.Categories = Categories;
            return PartialView();
        }
        public async Task<IActionResult> Menu(string categoryname, string categoryid)
        {
            var menuCategory = await _menuCategoryReadRepository.Table.Include(o => o.Menus).
                FirstOrDefaultAsync(o => o.MenuCategoryID == categoryid.ToGuid() && !o.IsDeleted);

            return View(menuCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(string datepicker_field, string time, int people, string name_reserve, string email_reserve, string telephone_reserve, string opt_message_reserve, bool terms)
        {
            datepicker_field = datepicker_field.Substring(4);
            DateTime dtPick = DateTime.Parse(datepicker_field + " " + time.Replace('.', ':'));
            var result = await _reservationWrite.AddAsync(new Reservation()
            {
                Date = dtPick,
                NumberOfPeople = people,
                Name = name_reserve,
                Email = email_reserve,
                Phone = telephone_reserve,
                Message = opt_message_reserve,
                ConfirmationStatus = ReservationConfirmationStatus.Pending
            }, this.ActiveUserID());
            var mail = await _emailService.SendEmail(email_reserve, "Authorization", "<h2>Please authorize your mail adres!</h2>", System.Net.Mail.MailPriority.High);
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
            var result = await _menuReadRepository.Table.AsNoTracking().Include(o => o.Category).Where(o => !o.IsDeleted && !o.Category.IsDeleted && (o.Name.Contains(q) || o.Description.Contains(q) || o.Category.Name.Contains(q))).ToListAsync();
            ViewBag.q = q;
            return View(result);
        }

        public async Task<IActionResult> CreateOrder(
            List<(string productid, int count)> products,
            string name_card_order, string card_number, string expire_month,
            string expire_year, string ccv, string payment_method,
            string fullName, string email, string phone,
            string fullAdress, string city, int postalCode
            )
        {
            Guid orderID = Guid.NewGuid();
            var dbproducts = await _menuReadRepository.Table.AsNoTracking().Include(o => o.Category).Where(o => !o.IsDeleted && !o.Category.IsDeleted && products.Any(p => p.productid == o.MenuID.ToString())).ToListAsync();
            decimal TotalPrice = 0;
            foreach (var product in products)
            {
                var curprod = dbproducts.FirstOrDefault(o => o.MenuID.ToString() == product.productid);
                if (curprod == null) return NotFound("Same products are does not exists in system!");
                OrderItem oi = new OrderItem()
                {
                    MenuID = curprod.MenuID,
                    ItemPrice = curprod.Price,
                    ItemCount = product.count,
                    MenuText = curprod.Name,
                    OrderID = orderID,
                    OrderItemID = Guid.NewGuid()
                };
                TotalPrice += oi.ItemPrice * oi.ItemPrice;
                await _orderItemWriteRepository.AddAsync(oi, this.ActiveUserID());
            }

            var order = new Order()
            {
                OrderID = orderID,
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
                CardHolderName = name_card_order,
                CardNumber = card_number,
                ExpireMonth = expire_month,
                ExpireYear = expire_year,
                CCV = ccv,
                Discount = 0
            };
            await _orderWriteRepository.AddAsync(order, this.ActiveUserID());
            return Ok(orderID.ToString());

        }
        public async Task<IActionResult> Confirmation(string id)
        {
            if (_orderReadRepository.GetByIdAsync(id.ToGuid()).Result == null)
            {
                return NotFound("Order not found!");
            }
            ViewBag.id = id;
            return View();
        }
    }
}