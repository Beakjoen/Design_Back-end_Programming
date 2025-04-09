using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp HoiVien: thông tin cơ bản của hội viên.
    internal class HoiVien
    {
        public string HoTen { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;

        public virtual void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap dia chi: ");
            DiaChi = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Dia chi: {DiaChi}");
        }
    }

    // Lớp HoiVienCoGiaDinh: hội viên có gia đình -> thêm ten vo va ngay cuoi
    internal class HoiVienCoGiaDinh : HoiVien
    {
        public string TenVo { get; set; } = string.Empty;
        public string NgayCuoi { get; set; } = string.Empty; // định dạng dd.mm.yyyy

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap ten vo: ");
            TenVo = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ngay cuoi (dd.mm.yyyy): ");
            NgayCuoi = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThi()
        {
            base.HienThi();
            Console.WriteLine($"Ten vo: {TenVo}, Ngay cuoi: {NgayCuoi}");
        }
    }

    // Lớp HoiVienCoNguoiYeu: hội viên có người yêu -> thêm ten nguoi yeu va sdt
    internal class HoiVienCoNguoiYeu : HoiVien
    {
        public string TenNguoiYeu { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap ten nguoi yeu: ");
            TenNguoiYeu = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so dien thoai: ");
            SoDienThoai = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThi()
        {
            base.HienThi();
            Console.WriteLine($"Ten nguoi yeu: {TenNguoiYeu}, So dien thoai: {SoDienThoai}");
        }
    }

    // Lớp QuanLyHoiVien: quản lý danh sách hội viên và các chức năng theo đề.
    internal class QuanLyHoiVien
    {
        private List<HoiVien> dsHoiVien = new List<HoiVien>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong hoi vien: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nNhap thong tin hoi vien thu {i + 1}:");
                Console.WriteLine("Chon loai (1: Co gia dinh, 2: Co nguoi yeu, 3: Khong co gi): ");
                int loai = int.Parse(Console.ReadLine() ?? "0");
                HoiVien hv = loai switch
                {
                    1 => new HoiVienCoGiaDinh(),
                    2 => new HoiVienCoNguoiYeu(),
                    3 => new HoiVien(),
                    _ => new HoiVien()
                };
                hv.Nhap();
                dsHoiVien.Add(hv);
            }
        }

        public void TimKiemNgayCuoi()
        {
            Console.Write("Nhap ngay cuoi can tim (vd: 11.11.2011): ");
            string ngay = Console.ReadLine() ?? string.Empty;
            bool found = false;
            Console.WriteLine("Danh sach hoi vien co ngay cuoi {0}:", ngay);
            foreach (var hv in dsHoiVien)
            {
                if (hv is HoiVienCoGiaDinh hvgd)
                {
                    if (hvgd.NgayCuoi.Equals(ngay))
                    {
                        hvgd.HienThi();
                        Console.WriteLine("-----");
                        found = true;
                    }
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay hoi vien co ngay cuoi " + ngay);
        }

        public void HienThiCoNguoiYeuChuaGiaDinh()
        {
            Console.WriteLine("Danh sach hoi vien co nguoi yeu (nhung khong co gia dinh):");
            bool found = false;
            foreach (var hv in dsHoiVien)
            {
                // Nếu là HoiVienCoNguoiYeu, tức có người yêu nhưng không là HoiVienCoGiaDinh
                if (hv is HoiVienCoNguoiYeu)
                {
                    hv.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong co hoi vien nao co nguoi yeu ma chua co gia dinh.");
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HOI VIEN");
                Console.WriteLine("1. Nhap danh sach hoi vien");
                Console.WriteLine("2. Tim kiem hoi vien co ngay cuoi 11.11.2011");
                Console.WriteLine("3. Hien thi hoi vien co nguoi yeu (chua co gia dinh)");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        TimKiemNgayCuoi();
                        break;
                    case 3:
                        HienThiCoNguoiYeuChuaGiaDinh();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 20.");
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

    internal static class Bai20
    {
        public static void B20()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 20: Quan ly hoi vien ------");
            QuanLyHoiVien ql = new QuanLyHoiVien();
            ql.Menu();
        }
    }
}
