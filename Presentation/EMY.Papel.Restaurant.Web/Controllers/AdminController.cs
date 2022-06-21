using EMY.Papel.Restaurant.Core.Application.Abstract;
using EMY.Papel.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;
using EMY.Papel.Restaurant.Infrastructure.Persistence;
using EMY.Papel.Restaurant.Web.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace EMY.Papel.Restaurant.Web.Controllers
{
    public class AdminController : Controller
    {

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        private readonly IMailListReadRepository _mailListRead;
        private readonly IReservationReadRepository _reservationRead;
        private readonly IOrderReadRepository _basketRead;
        private readonly IPhotoWriteRepository _photoWrite;
        private readonly IMenuReadRepository _menuRead;
        private readonly IMenuWriteRepository _menuWrite;
        private readonly IReservationWriteRepository _reservationWrite;
        private readonly IEmailService _mailSystem;
        private readonly IMenuCategoryReadRepository _menuCategoryRead;
        private readonly IMenuCategoryWriteRepository _menuCategoryWrite;
        private readonly IMailListWriteRepository _mailListWrite;

        public AdminController(IReservationReadRepository reservationRead, IOrderReadRepository basketRead, IPhotoWriteRepository photoWrite, IMenuReadRepository menuRead, IMenuWriteRepository menuWrite, IReservationWriteRepository reservationWrite, IMenuCategoryReadRepository menuCategoryRead, IEmailService mailSystem, IMenuCategoryWriteRepository menuCategoryWrite, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IMailListWriteRepository mailListWrite, IMailListReadRepository mailListRead)
        {
            _reservationRead = reservationRead;
            _basketRead = basketRead;
            _photoWrite = photoWrite;
            _menuRead = menuRead;
            _menuWrite = menuWrite;
            _reservationWrite = reservationWrite;
            _menuCategoryRead = menuCategoryRead;
            _mailSystem = mailSystem;
            _menuCategoryWrite = menuCategoryWrite;
            Environment = environment;
            _mailListWrite = mailListWrite;
            _mailListRead = mailListRead;
        }

        public async Task<IActionResult> Index()
        {

            var reservationstats = _reservationRead.GetReservationStats();
            ViewBag.ReservationStats = reservationstats;

            var basketstats = _basketRead.BasketStats();
            ViewBag.BasketStats = basketstats;

            return View();
        }

        public async Task<IActionResult> Orders()
        {

            return View();
        }
        public async Task<IActionResult> MenuDesign(string menuCategoryID)
        {

            return View();
        }


        async Task<Photo> UploadImage(IFormFile file, int thumbWidth = 128, int thumbHeight = 128, bool generateThumb = true)
        {
            if (file.ContentType.StartsWith("image"))
            {
                Photo photo = new Photo();
                photo.PhotoID = Guid.NewGuid();
                photo.FileName = file.FileName;
                photo.Extention = System.IO.Path.GetExtension(file.FileName);
                await _photoWrite.AddAsync(photo, this.ActiveUserID());

                MemoryStream memory = new MemoryStream();

                await file.CopyToAsync(memory);

                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads/Photos/");
                string filePath = Path.Combine(path, photo.PhotoID + photo.Extention);
                string filePathThumb = Path.Combine(path, photo.PhotoID + "_thumb" + photo.Extention);

                Image image = Image.FromStream(memory);
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                if (generateThumb)
                {
                    var thumb = image.ResizeImage(new Size(thumbWidth, thumbHeight));
                    thumb.Save(filePathThumb, System.Drawing.Imaging.ImageFormat.Png);
                }

                return photo;
            }
            return new Photo();
        }

        [HttpGet]
        public async Task<IActionResult> MenuCategory()
        {
            List<MenuCategory> menuCategory = await _menuCategoryRead.GetAllMenuCategoryWithMenus();
            return View(menuCategory);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(string MenuCategoryID, string name, string description, IFormFile headerPhoto, IFormFile logoPhoto)
        {
            string HeaderPhotoUrl = string.Empty;
            Guid HeaderPhotoID = Guid.Empty;
            if (headerPhoto != null)
            {
                var uploadedHeaderPhoto = await UploadImage(headerPhoto);
                HeaderPhotoUrl = uploadedHeaderPhoto.PhotoID.ToString() + uploadedHeaderPhoto.Extention; ;
                HeaderPhotoID = uploadedHeaderPhoto.PhotoID;
            }

            string LogoPhotoUrl = string.Empty;
            Guid LogoPhotoID = Guid.Empty;
            if (logoPhoto != null)
            {
                var uploadedLogoPhoto = await UploadImage(logoPhoto);
                LogoPhotoUrl = uploadedLogoPhoto.PhotoID.ToString() + uploadedLogoPhoto.Extention; ;
                LogoPhotoID = uploadedLogoPhoto.PhotoID;
            }

            if (MenuCategoryID.ToGuid() == Guid.Empty)
            {
                MenuCategory menuCategory = new MenuCategory()
                {
                    Description = description,
                    Name = name,
                    Active = true,
                    LogoPhotoID = LogoPhotoID,
                    HeaderPhotoID = HeaderPhotoID,
                    HeaderPhotoURL = HeaderPhotoUrl,
                    LogoPhotoURL = LogoPhotoUrl
                };
                await _menuCategoryWrite.AddAsync(menuCategory, this.ActiveUserID());
                return Ok();
            }
            else
            {
                MenuCategory menuCategory = await _menuCategoryRead.GetByIdAsync(MenuCategoryID.ToGuid());
                if (menuCategory == null)
                {
                    return NotFound("Menu Category not found");
                }

                menuCategory.Description = description;
                menuCategory.Name = name;
                if (headerPhoto != null)
                {
                    menuCategory.HeaderPhotoID = HeaderPhotoID;
                    menuCategory.HeaderPhotoURL = HeaderPhotoUrl;
                }


                if (logoPhoto != null)
                {
                    menuCategory.LogoPhotoID = LogoPhotoID;
                    menuCategory.LogoPhotoURL = LogoPhotoUrl;
                }
                await _menuCategoryWrite.UpdateAsync(menuCategory, this.ActiveUserID());
                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(string MenuCategoryID)
        {
            MenuCategory menuCategory = await _menuCategoryRead.GetByIdAsync(MenuCategoryID.ToGuid());
            if (menuCategory == null)
            {
                return NotFound("Menu Category not found");
            }
            await _menuCategoryWrite.RemoveAsync(menuCategory, this.ActiveUserID());
            return Ok($"{menuCategory.Name} has been removed!");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenu(string MenuID)
        {
            Menu menu = await _menuRead.GetByIdAsync(MenuID.ToGuid());
            if (menu == null)
            {
                return NotFound("Menu not found");
            }
            await _menuWrite.RemoveAsync(menu, this.ActiveUserID());
            return Ok($"{menu.Name} has been removed!");
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEditCategory(string categoryid)
        {
            if (!string.IsNullOrEmpty(categoryid))
            {
                MenuCategory menuCategory = await _menuCategoryRead.GetByIdAsync(categoryid.ToGuid()) ?? new MenuCategory();
                return PartialView(menuCategory);
            }


            return PartialView(new MenuCategory());
        }
        [HttpGet]
        public async Task<IActionResult> GetMenu(string MenuCategoryID)
        {
            IList<Menu> menuItems = _menuRead.GetMenuFromCategory(Guid.Parse(MenuCategoryID));
            return View(menuItems);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMenu(Menu menu, IFormFile photoContent)
        {
            int result = 0;
            if (photoContent != null)
            {
                var photo = await UploadImage(photoContent);
                menu.PhotoID = photo.PhotoID;
                menu.PhotoFileName = photo.PhotoID.ToString() + photo.Extention;
                menu.PhotoThumbFileName = photo.PhotoID.ToString() + "_thumb" + photo.Extention;
            }
            if (menu.MenuID == Guid.Empty)
                result = await _menuWrite.AddAsync(menu, this.ActiveUserID());
            else
                result = await _menuWrite.UpdateAsync(menu, this.ActiveUserID());


            if (result > 0)
            {
                return Ok(menu);
            }
            else
            {
                return BadRequest("Samething went wrong!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEditMenu(string MenuCategoryID, string MenuID = "")
        {
            var menu = new Menu();
            menu.MenuCategoryID = Guid.Parse(MenuCategoryID);
            if (!string.IsNullOrEmpty(MenuID))
            {
                menu = await _menuRead.GetByIdAsync(Guid.Parse(MenuID));
            }
            return PartialView("CreateOrEditMenu", menu);
        }

        public async Task<IActionResult> GetMenuDetailsAsync(string MenuID)
        {
            var menu = await _menuRead.GetByIdAsync(Guid.Parse(MenuID));
            return View("CreateOrEdit", menu);
        }
        public async Task<IActionResult> Reservations()
        {
            DateTime
                Begindt = DateTime.Today.AddDays(-1),
                Enddt = DateTime.Today.AddDays(1);

            List<Reservation> reservations = await _reservationRead.Table.AsNoTracking().Where(o => !o.IsDeleted && o.Date >= Begindt && o.Date <= Enddt).ToListAsync();
            ReservationPageResultViewModel result = new ReservationPageResultViewModel();
            result.AuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Confirmed).ToList();
            result.UnAuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Rejected).ToList();
            result.Pendings = await _reservationRead.GetWhere(o => !o.IsDeleted && o.ConfirmationStatus == ReservationConfirmationStatus.Authorized).ToListAsync();
            result.Begin = Begindt;
            result.End = Enddt;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Reservations(DateTime Begin, DateTime End)
        {
            List<Reservation> reservations = await _reservationRead.Table.AsNoTracking().Where(o => !o.IsDeleted && o.Date >= Begin && o.Date <= End).ToListAsync();
            ReservationPageResultViewModel result = new ReservationPageResultViewModel();
            result.AuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Confirmed).ToList();
            result.UnAuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Rejected).ToList();
            result.Pendings = await _reservationRead.GetWhere(o => !o.IsDeleted && o.ConfirmationStatus == ReservationConfirmationStatus.Authorized).ToListAsync();
            result.Begin = Begin;
            result.End = End;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetReservationStatusSettings(string reservationid, ReservationConfirmationStatus status)
        {
            var reservation = await _reservationRead.GetByIdAsync(reservationid.ToGuid());
            if (reservation == null)
            {
                return NotFound("Reservation not found!");
            }
            if (reservation.ConfirmationStatus == status && status == ReservationConfirmationStatus.Confirmed)
            {
                return BadRequest("Reservation already confirmed!");
            }
            if (reservation.ConfirmationStatus == status && status == ReservationConfirmationStatus.Rejected)
            {
                return BadRequest("Reservation already rejected!");
            }

            if (reservation.ConfirmationStatus == ReservationConfirmationStatus.Pending && status != ReservationConfirmationStatus.Authorized)
            {
                return BadRequest("Reservation is pending, you can only change to authorized!");
            }
            if (
                (reservation.ConfirmationStatus == ReservationConfirmationStatus.Confirmed ||
                reservation.ConfirmationStatus == ReservationConfirmationStatus.Rejected) &&
                status != ReservationConfirmationStatus.Authorized)
            {
                return BadRequest("Reservation is confirmed or rejected, you can not change to authorized!");
            }



            reservation.ConfirmationStatus = status;
            await _reservationWrite.UpdateAsync(reservation, this.ActiveUserID());
            string confirmationMessage = "";
            switch (status)
            {
                case ReservationConfirmationStatus.Confirmed:
                    confirmationMessage = "confirmed!";
                    break;
                case ReservationConfirmationStatus.Rejected:
                    confirmationMessage = "rejected!";
                    break;
                case ReservationConfirmationStatus.Authorized:
                    confirmationMessage = "authorized!";
                    break;
            }

            await _mailSystem.SendEmail(reservation.Email, $"{Configuration.SystemName} Reservation Confirmation", $"Your reservation has been {confirmationMessage}", System.Net.Mail.MailPriority.High);
            return View(reservation);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMailList(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required!");
            }
            var mail = _mailListRead.Get(o => o.Email == email && !o.IsDeleted);
            if (mail == null)
            {
                await _mailListWrite.AddAsync(new MailList() { Email = email }, Guid.Empty);

                return Ok();
            }
            else
                return BadRequest("Email already exists!");
        }
    }
}
