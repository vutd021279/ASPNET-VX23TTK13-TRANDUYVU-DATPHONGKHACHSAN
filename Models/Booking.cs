using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Loại phòng")]
        public int RoomTypeID { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng là bắt buộc")]
        [Display(Name = "Ngày nhận phòng")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng là bắt buộc")]
        [Display(Name = "Ngày trả phòng")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Số khách là bắt buộc")]
        [Display(Name = "Số khách")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Yêu cầu đặc biệt")]
        public string SpecialRequests { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        public DateTime BookingDate { get; set; }

        // Navigation property
        public RoomType RoomType { get; set; }
    }
}