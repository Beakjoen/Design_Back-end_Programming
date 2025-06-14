using System;
using System.Collections.Generic;

namespace HocDBFirst.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? MaPhong { get; set; }

    public virtual PhongBan? MaPhongNavigation { get; set; }
}
