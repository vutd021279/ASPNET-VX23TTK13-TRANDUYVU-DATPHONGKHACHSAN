// Models/RoomType.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }

        [Required(ErrorMessage = "Tên loại phòng là bắt buộc")]
        [Display(Name = "Tên loại phòng")]
        public string TypeName { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá phòng là bắt buộc")]
        [Display(Name = "Giá (VNĐ)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Sức chứa là bắt buộc")]
        [Display(Name = "Sức chứa")]
        public int Capacity { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}