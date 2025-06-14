using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HocValkidations.Models
{
    public class NhanVien
    {
        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MinLength(5, ErrorMessage = "Họ tên phải có ít nhất 5 ký tự")]
        public string HoTen { get; set; }
        [DisplayName("Tuổi")]
        [Range(18, 60, ErrorMessage = "Tuổi phải từ 18 đến 60")]
        public int Tuoi { get; set; }
        [DisplayName("Địa chỉ Email")]
        [Required(ErrorMessage = "Địa chỉ Email không được để trống")]
        [DataType(DataType.EmailAddress)][EmailAddress(ErrorMessage = "Sai định dạng Email!")]
        public string Email { get; set; }
    }
}
