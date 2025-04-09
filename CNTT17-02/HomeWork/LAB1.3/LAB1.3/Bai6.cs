using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp Nguoi cho học sinh
    internal class Nguoi6
    {
        public string HoTen { get; set; } = string.Empty;
        public int Tuoi { get; set; }
        public int NamSinh { get; set; }
        public string QueQuan { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;

        public virtual void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap tuoi: ");
            Tuoi = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap que quan: ");
            QueQuan = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap gioi tinh (Nam/Nu): ");
            GioiTinh = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Tuoi: {Tuoi}, Nam sinh: {NamSinh}, Que quan: {QueQuan}, Gioi tinh: {GioiTinh}");
        }
    }

    // Lớp HSHocSinh lưu trữ hồ sơ cá nhân (thêm thông tin: Lớp, Khóa học, Kỳ học)
    internal class HSHocSinh : Nguoi6
    {
        public string Lop { get; set; } = string.Empty;
        public string KhoaHoc { get; set; } = string.Empty;
        public string KyHoc { get; set; } = string.Empty;

        public new void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap lop: ");
            Lop = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap khoa hoc: ");
            KhoaHoc = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ky hoc: ");
            KyHoc = Console.ReadLine() ?? string.Empty;
        }

        public new void HienThi()
        {
            Console.WriteLine("Ho so hoc sinh:");
            base.HienThi();
            Console.WriteLine($"Lop: {Lop}, Khoa hoc: {KhoaHoc}, Ky hoc: {KyHoc}");
        }
    }

    // Lớp QuanLyHocSinh quản lý danh sách hồ sơ học sinh
    internal class QuanLyHocSinh
    {
        private List<HSHocSinh> dsHocSinh = new List<HSHocSinh>();

        public void NhapHocSinh()
        {
            Console.Write("Nhap so luong hoc sinh: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin hoc sinh thu {i + 1}:");
                HSHocSinh hs = new HSHocSinh();
                hs.Nhap();
                dsHocSinh.Add(hs);
            }
        }

        // Hiển thị tất cả các học sinh nữ và sinh năm 1985
        public void HienThiHocSinhNu1985()
        {
            Console.WriteLine("Danh sach hoc sinh nu sinh nam 1985:");
            foreach (var hs in dsHocSinh)
            {
                if (hs.GioiTinh.Equals("Nu", StringComparison.OrdinalIgnoreCase) && hs.NamSinh == 1985)
                {
                    hs.HienThi();
                    Console.WriteLine("-----");
                }
            }
        }

        // Tìm kiếm theo quê quán
        public void TimKiemTheoQueQuan()
        {
            Console.Write("Nhap que quan can tim: ");
            string que = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var hs in dsHocSinh)
            {
                if (hs.QueQuan.IndexOf(que, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hs.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay hoc sinh voi que quan: " + que);
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HO SO HOC SINH");
                Console.WriteLine("1. Nhap thong tin hoc sinh");
                Console.WriteLine("2. Hien thi hoc sinh nu sinh nam 1985");
                Console.WriteLine("3. Tim kiem hoc sinh theo que quan");
                Console.WriteLine("4. Thoat chuong trinh");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapHocSinh();
                        break;
                    case 2:
                        HienThiHocSinhNu1985();
                        break;
                    case 3:
                        TimKiemTheoQueQuan();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 6.");
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

    internal static class Bai6
    {
        public static void B6()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 6: Quan ly ho so hoc sinh ------");
            QuanLyHocSinh ql = new QuanLyHocSinh();
            ql.Menu();
        }
    }
}
