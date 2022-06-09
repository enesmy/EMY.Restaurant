using EMY.Papel.Restaurant.Core.Application.Abstract;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Infrastructure.Persistence;
using EMY.Papel.Restaurant.Web.Statics;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EMY.Papel.Restaurant.Web.Controllers
{
    public class AdminController : Controller
    {
       

        private readonly IReservationReadRepository _reservationRead;
        private readonly IBasketReadRepository _basketRead;
        private readonly IPhotoWriteRepository _photoWrite;
        private readonly IMenuReadRepository _menuRead;
        private readonly IMenuWriteRepository _menuWrite;
        private readonly IReservationWriteRepository _reservationWrite;
        private readonly IEmailService _mailSystem;
        private readonly IMenuCategoryReadRepository _menuCategoryRead;

        public AdminController(IReservationReadRepository reservationRead, IBasketReadRepository basketRead, IPhotoWriteRepository photoWrite, IMenuReadRepository menuRead, IMenuWriteRepository menuWrite, IReservationWriteRepository reservationWrite, IMenuCategoryReadRepository menuCategoryRead)
        {
            _reservationRead = reservationRead;
            _basketRead = basketRead;
            _photoWrite = photoWrite;
            _menuRead = menuRead;
            _menuWrite = menuWrite;
            _reservationWrite = reservationWrite;
            _menuCategoryRead = menuCategoryRead;
        }

        public async Task<IActionResult> Dashboard()
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


        async Task<Photo> UploadImage(IFormFile file)
        {
            if (file.ContentType == "image/jpeg")
            {
                Image image = Image.FromStream(file.OpenReadStream());

                Photo photo = new Photo();
                photo.PhotoID = Guid.NewGuid();
                photo.FileName = file.FileName;
                photo.Extention = System.IO.Path.GetExtension(file.FileName);
                await _photoWrite.AddAsync(photo, this.ActiveUserID());

                image.Save(photo.GetOrginalLocation);
                Image thumb = image.GetThumbnailImage(128, 128, () => false, IntPtr.Zero);
                thumb.Save(photo.GetThumbnailLocation);
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
        [HttpGet]
        public async Task<IActionResult> GetMenu(string MenuCategoryID)
        {
            IList<Menu> menuItems = _menuRead.GetMenuFromCategory(Guid.Parse(MenuCategoryID));
            return View(menuItems);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMenu(Menu menu, IFormFile file)
        {

            if (file != null)
                menu.PhotoID = (await UploadImage(file)).PhotoID;

            var result = await _menuWrite.AddAsync(menu, this.ActiveUserID());
            if (result > 0)
            {
                return Ok(menu);
            }
            else
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> GetMenuDetailsAsync(string MenuID)
        {
            var menu = await _menuRead.GetByIdAsync(Guid.Parse(MenuID));
            return View("CreateOrEdit", menu);
        }
        public async Task<IActionResult> Reservations()
        {
            List<Reservation> reservations = _reservationRead.GetReservations();
            return View();
        }

        public async Task<IActionResult> SetReservationStatusSettings(string ReservationID, ReservationConfirmationStatus status)
        {
            var reservation = await _reservationRead.GetByIdAsync(Guid.Parse(ReservationID));
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
            if(
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
    }
}
