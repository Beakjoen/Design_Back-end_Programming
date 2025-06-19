using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    internal class Bai2
    {
        class TaiLieu
        {
            public string MaTaiLieu { get; set; } = string.Empty;
            public string NhaXuatBan { get; set; } = string.Empty;
            public int SoBanPhatHanh { get; set; }

            public virtual void NhapThongTin()
            {
                Console.WriteLine("Nhap ma tai lieu: ");
                MaTaiLieu = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Nhap ten nha xuat ban: ");
                NhaXuatBan = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Nhap so ban phat hanh: ");
                SoBanPhatHanh = int.Parse(Console.ReadLine() ?? "0");
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ma tai lieu: {MaTaiLieu}");
                Console.WriteLine($"Nha xuat ban: {NhaXuatBan}");
                Console.WriteLine($"So ban phat hanh: {SoBanPhatHanh}");
            }
        }

        class Sach : TaiLieu
        {
            public string TacGia { get; set; } = string.Empty;
            public int SoTrang { get; set; }

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.WriteLine("Nhap ten tac gia: ");
                TacGia = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Nhap so trang: ");
                SoTrang = int.Parse(Console.ReadLine() ?? "0");
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"Tac gia: {TacGia}");
                Console.WriteLine($"So trang: {SoTrang}");
            }
        }

        class TapChi : TaiLieu
        {
            public int SoPhatHanh { get; set; }
            public int ThangPhatHanh { get; set; }

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.WriteLine("Nhap so phat hanh: ");
                SoPhatHanh = int.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine("Nhap thang phat hanh: ");
                ThangPhatHanh = int.Parse(Console.ReadLine() ?? "0");
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"So phat hanh: {SoPhatHanh}");
                Console.WriteLine($"Thang phat hanh: {ThangPhatHanh}");
            }
        }

        class Bao : TaiLieu
        {
            public DateTime NgayPhatHanh { get; set; }

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.WriteLine("Nhap ngay phat hanh (dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime ngayPH))
                {
                    NgayPhatHanh = ngayPH;
                }
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"Ngay phat hanh: {NgayPhatHanh:dd/MM/yyyy}");
            }
        }

        class QuanLyTaiLieu
        {
            private List<TaiLieu> danhSachTaiLieu = new List<TaiLieu>();

            public void NhapTaiLieu()
            {
                Console.WriteLine("Chon loai tai lieu (1: Sach, 2: Tap chi, 3: Bao): ");
                if (!int.TryParse(Console.ReadLine(), out int loai))
                {
                    Console.WriteLine("Loai tai lieu khong hop le!");
                    return;
                }

                TaiLieu taiLieu = loai switch
                {
                    1 => new Sach(),
                    2 => new TapChi(),
                    3 => new Bao(),
                    _ => throw new ArgumentException("Loai tai lieu khong hop le")
                };

                taiLieu.NhapThongTin();
                danhSachTaiLieu.Add(taiLieu);
                Console.WriteLine("Them tai lieu thanh cong!");
            }

            public void HienThiDanhSach()
            {
                if (danhSachTaiLieu.Count == 0)
                {
                    Console.WriteLine("Danh sach trong!");
                    return;
                }

                foreach (var taiLieu in danhSachTaiLieu)
                {
                    taiLieu.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void TimKiemTheoLoai()
            {
                Console.WriteLine("Nhap loai tai lieu can tim (1: Sach, 2: Tap chi, 3: Bao): ");
                if (!int.TryParse(Console.ReadLine(), out int loai))
                {
                    Console.WriteLine("Loai tai lieu khong hop le!");
                    return;
                }

                var ketQua = loai switch
                {
                    1 => danhSachTaiLieu.OfType<Sach>(),
                    2 => danhSachTaiLieu.OfType<TapChi>(),
                    3 => danhSachTaiLieu.OfType<Bao>(),
                    _ => Enumerable.Empty<TaiLieu>()
                };

                foreach (var taiLieu in ketQua)
                {
                    taiLieu.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B2()
        {
            var quanLy = new QuanLyTaiLieu();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY TAI LIEU");
                Console.WriteLine("1. Nhap tai lieu moi");
                Console.WriteLine("2. Hien thi danh sach tai lieu");
                Console.WriteLine("3. Tim kiem theo loai");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang (1-4): ");

                if (!int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le!");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        quanLy.NhapTaiLieu();
                        break;
                    case 2:
                        quanLy.HienThiDanhSach();
                        break;
                    case 3:
                        quanLy.TimKiemTheoLoai();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}
