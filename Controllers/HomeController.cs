// Controllers/HomeController.cs
using System;
using System.Web.Mvc;
using HotelBooking.DAL;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class HomeController : Controller
    {
        private RoomTypeDAL roomTypeDAL = new RoomTypeDAL();

        public ActionResult Index()
        {
            var roomTypes = roomTypeDAL.GetAllRoomTypes();
            return View(roomTypes);
        }

        public ActionResult RoomDetails(int id)
        {
            var roomType = roomTypeDAL.GetRoomTypeByID(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvailableRooms = roomTypeDAL.GetAvailableRoomCount(id);
            return View(roomType);
        }
    }
}