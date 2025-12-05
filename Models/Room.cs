// Models/Room.cs
using HotelBooking.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Room
{
    public int RoomID { get; set; }

    [Required(ErrorMessage = "Số phòng là bắt buộc")]
    [Display(Name = "Số phòng")]
    public string RoomNumber { get; set; }

    [Display(Name = "Loại phòng")]
    public int RoomTypeID { get; set; }

    [Display(Name = "Trạng thái")]
    public string Status { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation property
    public RoomType RoomType { get; set; }
}