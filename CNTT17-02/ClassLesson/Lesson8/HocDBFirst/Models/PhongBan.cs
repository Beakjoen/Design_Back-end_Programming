using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HocDBFirst.Models;

public partial class PhongBan
{
    [DisplayName("Mã phòng")]
    [Required(ErrorMessage = "Mã phòng không được để trống")]
    public string MaPhong { get; set; } = null!;
    [DisplayName("Tên phòng")]
    [Required(ErrorMessage = "Tên phòng không được để trống")]
    public string TenPhong { get; set; } = null!;
    [DisplayName("Điện thoại")]
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [MaxLength(20, ErrorMessage = "Số điện thoại không được quá 20 ký tự")]
    public string? DienThoai { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
