using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp Nguoi: quản lý thông tin cá nhân của khách trọ
    internal class Nguoi
    {
        public string HoTen { get; set; } = string.Empty;
        public int NamSinh { get; set; }
        public string SoCMND { get; set; } = string.Empty;

        public void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap so CMND: ");
            SoCMND = Console.ReadLine() ?? string.Empty;
        }

        public void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Nam sinh: {NamSinh}, So CMND: {SoCMND}");
        }
    }

    // Lớp KhachTro: quản lý thông tin khách trọ (so ngay tro, loai phong, gia phong, và thông tin khach)
    internal class KhachTro
    {
        public int SoNgayTro { get; set; }
        public string LoaiPhong { get; set; } = string.Empty;
        public double GiaPhong { get; set; }
        public Nguoi Khach { get; set; } = new Nguoi();

        public void Nhap()
        {
            Console.WriteLine("Nhap thong tin khach tro:");
            Khach.Nhap();
            Console.Write("Nhap so ngay tro: ");
            SoNgayTro = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap loai phong: ");
            LoaiPhong = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap gia phong: ");
            GiaPhong = double.Parse(Console.ReadLine() ?? "0");
        }

        public double TinhTien() => SoNgayTro * GiaPhong;

        public void HienThi()
        {
            Console.WriteLine("Thong tin khach tro:");
            Khach.HienThi();
            Console.WriteLine($"So ngay tro: {SoNgayTro}, Loai phong: {LoaiPhong}, Gia phong: {GiaPhong}");
            Console.WriteLine("Tong tien phai thanh toan: " + TinhTien());
        }
    }

    // Lớp QuanLyKhachSan: quản lý danh sách khách trọ
    internal class QuanLyKhachSan
    {
        private List<KhachTro> dsKhachTro = new List<KhachTro>();

        public void NhapKhachTro()
        {
            Console.Write("Nhap so luong khach tro: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin khach tro thu {i + 1}:");
                KhachTro kt = new KhachTro();
                kt.Nhap();
                dsKhachTro.Add(kt);
            }
        }

        public void HienThiKhachTro()
        {
            if (dsKhachTro.Count == 0)
            {
                Console.WriteLine("Danh sach khach tro trong!");
                return;
            }
            foreach (var kt in dsKhachTro)
            {
                kt.HienThi();
                Console.WriteLine("-----");
            }
        }

        public void TimKiemTheoHoTen()
        {
            Console.Write("Nhap ho ten can tim: ");
            string ten = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var kt in dsKhachTro)
            {
                if (kt.Khach.HoTen.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    kt.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay khach tro voi ho ten: " + ten);
        }

        public void TinhTienThanhToan()
        {
            Console.WriteLine("Tien thanh toan cho cac khach tro:");
            foreach (var kt in dsKhachTro)
            {
                Console.WriteLine($"Khach: {kt.Khach.HoTen} - Tien thanh toan: {kt.TinhTien()}");
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY KHACH SAN");
                Console.WriteLine("1. Nhap thong tin khach tro");
                Console.WriteLine("2. Hien thi danh sach khach tro");
                Console.WriteLine("3. Tim kiem theo ho ten");
                Console.WriteLine("4. Tinh tien thanh toan khi tra phong");
                Console.WriteLine("5. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapKhachTro();
                        break;
                    case 2:
                        HienThiKhachTro();
                        break;
                    case 3:
                        TimKiemTheoHoTen();
                        break;
                    case 4:
                        TinhTienThanhToan();
                        break;
                    case 5:
                        Console.WriteLine("Thoat chuong trinh Bai 5.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
                if (luaChon != 5)
                {
                    Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            } while (luaChon != 5);
        }
    }

    // Lớp tĩnh Bai5 chứa hàm giao diện của bài 5
    internal static class Bai5
    {
        public static void B5()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 5: Quan ly khach san ------");
            QuanLyKhachSan ql = new QuanLyKhachSan();
            ql.Menu();
        }
    }
}
