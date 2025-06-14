namespace FluentAPI.Models
{
    public class PhongBan
    {
        public string? MaPhong { get; set; }
        public string? TenPhong { get; set; }
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
