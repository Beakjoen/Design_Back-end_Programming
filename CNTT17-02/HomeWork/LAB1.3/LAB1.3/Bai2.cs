using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp cơ sở: TaiLieu
    internal class TaiLieu
    {
        public string MaTaiLieu { get; set; } = string.Empty;
        public string TenNhaXuatBan { get; set; } = string.Empty;
        public int SoBanPhatHanh { get; set; }

        public virtual void NhapThongTin()
        {
            Console.Write("Nhap ma tai lieu: ");
            MaTaiLieu = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ten nha xuat ban: ");
            TenNhaXuatBan = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so ban phat hanh: ");
            SoBanPhatHanh = int.Parse(Console.ReadLine() ?? "0");
        }

        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Ma tai lieu: {MaTaiLieu}, Ten Nha Xuat Ban: {TenNhaXuatBan}, So Ban Phat Hanh: {SoBanPhatHanh}");
        }
    }

    // Lớp Sach: kế thừa từ TaiLieu, thêm thuộc tính tác giả và số trang
    internal class Sach : TaiLieu
    {
        public string TenTacGia { get; set; } = string.Empty;
        public int SoTrang { get; set; }

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.Write("Nhap ten tac gia: ");
            TenTacGia = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so trang: ");
            SoTrang = int.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Ten Tac Gia: {TenTacGia}, So Trang: {SoTrang}");
        }
    }

    // Lớp TapChi: kế thừa từ TaiLieu, thêm thuộc tính số phát hành và tháng phát hành
    internal class TapChi : TaiLieu
    {
        public int SoPhatHanh { get; set; }
        public string ThangPhatHanh { get; set; } = string.Empty;

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.Write("Nhap so phat hanh: ");
            SoPhatHanh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap thang phat hanh: ");
            ThangPhatHanh = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"So Phat Hanh: {SoPhatHanh}, Thang Phat Hanh: {ThangPhatHanh}");
        }
    }

    // Lớp Bao: kế thừa từ TaiLieu, thêm thuộc tính ngày phát hành
    internal class Bao : TaiLieu
    {
        public string NgayPhatHanh { get; set; } = string.Empty;

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.Write("Nhap ngay phat hanh (dd/mm/yyyy): ");
            NgayPhatHanh = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Ngay Phat Hanh: {NgayPhatHanh}");
        }
    }

    // Lớp QuanLyTaiLieu: quản lý các tài liệu
    internal class QuanLyTaiLieu
    {
        private List<TaiLieu> danhSachTaiLieu = new List<TaiLieu>();

        public void NhapThongTinMoi()
        {
            Console.WriteLine("Nhap loai tai lieu (1: Sach, 2: Tap Chi, 3: Bao): ");
            if (!int.TryParse(Console.ReadLine(), out int loaiTaiLieu))
            {
                Console.WriteLine("Loai tai lieu khong hop le.");
                return;
            }

            TaiLieu taiLieu = loaiTaiLieu switch
            {
                1 => new Sach(),
                2 => new TapChi(),
                3 => new Bao(),
                _ => throw new ArgumentException("Loai tai lieu khong hop le")
            };

            taiLieu.NhapThongTin();
            danhSachTaiLieu.Add(taiLieu);
        }

        public void TimKiemTheoLoai(int loaiTaiLieu)
        {
            var ketQua = danhSachTaiLieu.FindAll(tl =>
                (loaiTaiLieu == 1 && tl is Sach) ||
                (loaiTaiLieu == 2 && tl is TapChi) ||
                (loaiTaiLieu == 3 && tl is Bao));

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Khong tim thay tai lieu cua loai: " + loaiTaiLieu);
                return;
            }

            foreach (var taiLieu in ketQua)
            {
                taiLieu.HienThiThongTin();
            }
        }

        public void HienThiDanhSach()
        {
            if (danhSachTaiLieu.Count == 0)
            {
                Console.WriteLine("Danh sach tai lieu trong!");
                return;
            }

            foreach (var taiLieu in danhSachTaiLieu)
            {
                taiLieu.HienThiThongTin();
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY TAI LIEU");
                Console.WriteLine("1. Nhap thong tin moi");
                Console.WriteLine("2. Tim kiem theo loai tai lieu");
                Console.WriteLine("3. Hien thi danh sach");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                int.TryParse(Console.ReadLine(), out luaChon);
                switch (luaChon)
                {
                    case 1:
                        NhapThongTinMoi();
                        break;
                    case 2:
                        Console.Write("Nhap loai tai lieu can tim (1: Sach, 2: Tap Chi, 3: Bao): ");
                        int loaiTaiLieu = int.Parse(Console.ReadLine() ?? "0");
                        TimKiemTheoLoai(loaiTaiLieu);
                        break;
                    case 3:
                        HienThiDanhSach();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 2.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
                if (luaChon != 4)
                {
                    Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            } while (luaChon != 4);
        }
    }

    // Lớp tĩnh Bai2 chứa hàm xử lý giao diện riêng, được gọi từ file Program.cs
    internal static class Bai2
    {
        public static void B2()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 2: Quan ly tai lieu ------");
            QuanLyTaiLieu ql = new QuanLyTaiLieu();
            ql.Menu();
        }
    }
}
