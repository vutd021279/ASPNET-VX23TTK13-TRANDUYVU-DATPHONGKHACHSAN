// Controllers/BookingController.cs
using System;
using System.Web.Mvc;
using HotelBooking.DAL;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {
        private BookingDAL bookingDAL = new BookingDAL();
        private RoomTypeDAL roomTypeDAL = new RoomTypeDAL();

        public ActionResult Create(int? roomTypeID)
        {
            ViewBag.RoomTypes = new SelectList(roomTypeDAL.GetAllRoomTypes(), "RoomTypeID", "TypeName", roomTypeID);

            var booking = new Booking
            {
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(2),
                RoomTypeID = roomTypeID ?? 0
            };

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Validate dates
                if (booking.CheckInDate < DateTime.Now.Date)
                {
                    ModelState.AddModelError("CheckInDate", "Ngày nhận phòng phải từ hôm nay trở đi");
                }
                else if (booking.CheckOutDate <= booking.CheckInDate)
                {
                    ModelState.AddModelError("CheckOutDate", "Ngày trả phòng phải sau ngày nhận phòng");
                }
                else
                {
                    // Calculate total price
                    var roomType = roomTypeDAL.GetRoomTypeByID(booking.RoomTypeID);
                    int nights = (booking.CheckOutDate - booking.CheckInDate).Days;
                    booking.TotalPrice = roomType.Price * nights;
                    booking.Status = "Pending";

                    if (bookingDAL.Insert(booking))
                    {
                        TempData["Success"] = "Đặt phòng thành công! Chúng tôi sẽ liên hệ với bạn sớm.";
                        return RedirectToAction("Confirmation");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Có lỗi xảy ra. Vui lòng thử lại.");
                    }
                }
            }

            ViewBag.RoomTypes = new SelectList(roomTypeDAL.GetAllRoomTypes(), "RoomTypeID", "TypeName", booking.RoomTypeID);
            return View(booking);
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}