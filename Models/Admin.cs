// Models/Admin.cs
using System;
using System.ComponentModel.DataAnnotations;

public class Admin
{
    public int AdminID { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }

    [Display(Name = "Họ tên")]
    public string FullName { get; set; }

    public DateTime CreatedDate { get; set; }
}