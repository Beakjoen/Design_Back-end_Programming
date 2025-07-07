using System.ComponentModel.DataAnnotations;

namespace BaiTapLon.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 ký tự")]
        public string? UserName { get; set; }
        
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-100 ký tự")]
        public string? Password { get; set; }
        
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự")]
        public string? FullName { get; set; }
        
        [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự")]
        public string? Address { get; set; }
        
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? Phone { get; set; }
        
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Avatar { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
} 