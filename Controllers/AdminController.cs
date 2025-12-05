// Controllers/AdminController.cs
using System;
using System.Web.Mvc;
using HotelBooking.DAL;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class AdminController : Controller
    {
        private RoomTypeDAL roomTypeDAL = new RoomTypeDAL();
        private BookingDAL bookingDAL = new BookingDAL();

        // Simple authentication - In production, use proper authentication
        private bool IsAuthenticated()
        {
            return Session["AdminUsername"] != null;
        }

        public ActionResult Login()
        {
            if (IsAuthenticated())
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Simple check - In production, use proper password hashing
            if (username == "admin" && password == "123456")
            {
                Session["AdminUsername"] = username;
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            ViewBag.TotalRoomTypes = roomTypeDAL.GetAllRoomTypes().Count;
            ViewBag.TotalBookings = bookingDAL.GetAllBookings().Count;
            return View();
        }

        // Room Type Management
        public ActionResult RoomTypes()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var roomTypes = roomTypeDAL.GetAllRoomTypes();
            return View(roomTypes);
        }

        public ActionResult CreateRoomType()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoomType(RoomType roomType)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                if (roomTypeDAL.Insert(roomType))
                {
                    TempData["Success"] = "Thêm loại phòng thành công!";
                    return RedirectToAction("RoomTypes");
                }
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return View(roomType);
        }

        public ActionResult EditRoomType(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            var roomType = roomTypeDAL.GetRoomTypeByID(id);
            if (roomType == null) return HttpNotFound();
            return View(roomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoomType(RoomType roomType)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                if (roomTypeDAL.Update(roomType))
                {
                    TempData["Success"] = "Cập nhật loại phòng thành công!";
                    return RedirectToAction("RoomTypes");
                }
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return View(roomType);
        }

        public ActionResult DeleteRoomType(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (roomTypeDAL.Delete(id))
            {
                TempData["Success"] = "Xóa loại phòng thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể xóa loại phòng này";
            }
            return RedirectToAction("RoomTypes");
        }

        // Booking Management
        public ActionResult Bookings()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var bookings = bookingDAL.GetAllBookings();
            return View(bookings);
        }

        public ActionResult BookingDetails(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            var booking = bookingDAL.GetBookingByID(id);
            if (booking == null) return HttpNotFound();
            return View(booking);
        }

        [HttpPost]
        public ActionResult UpdateBookingStatus(int id, string status)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (bookingDAL.UpdateStatus(id, status))
            {
                TempData["Success"] = "Cập nhật trạng thái thành công!";
            }
            else
            {
                TempData["Error"] = "Có lỗi xảy ra";
            }
            return RedirectToAction("Bookings");
        }

        public ActionResult DeleteBooking(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            if (bookingDAL.Delete(id))
            {
                TempData["Success"] = "Xóa đặt phòng thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể xóa đặt phòng này";
            }
            return RedirectToAction("Bookings");
        }
    }
}