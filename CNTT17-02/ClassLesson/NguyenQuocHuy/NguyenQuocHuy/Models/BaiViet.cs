using System.ComponentModel.DataAnnotations;

namespace NguyenQuocHuy.Models
{
    public class BaiViet
    {
        [Display(Name = "Mã bài viết")]
        [Required(ErrorMessage = "Mã bài viết không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã bài viết phải lớn hơn 0")]
        public int MaBaiViet { get; set; }
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự")]
        public string TieuDe { get; set; }
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        [StringLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự")]
        public string NoiDung { get; set; }
        [Display(Name = "Ngày Post")]
        [Required(ErrorMessage = "Ngày đăng không được để trống")]
        [Range(20230101, 99991231, ErrorMessage = "Ngày đăng không hợp lệ")]
        public int NgayPost { get; set; }
    }
}
