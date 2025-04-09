using System;
using System.Collections.Generic;

namespace QLApp
{
    // Đổi tên lớp Nguoi thành NguoiB4
    internal class NguoiB4
    {
        public string SoCMND { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public int Tuoi { get; set; }
        public int NamSinh { get; set; }
        public string NgheNghiep { get; set; } = string.Empty;

        public void Nhap()
        {
            Console.Write("Nhap so CMND: ");
            SoCMND = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap tuoi: ");
            Tuoi = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap nghe nghiep: ");
            NgheNghiep = Console.ReadLine() ?? string.Empty;
        }

        public void HienThi()
        {
            Console.WriteLine($"CMND: {SoCMND}, Ho ten: {HoTen}, Tuoi: {Tuoi}, Nam sinh: {NamSinh}, Nghe nghiep: {NgheNghiep}");
        }
    }

    // Lớp HoDan: quản lý thông tin 1 hộ dân gồm số nhà, số thành viên và danh sách NguoiB4
    internal class HoDan
    {
        public int SoThanhVien { get; set; }
        public string SoNha { get; set; } = string.Empty;
        public List<NguoiB4> DsNguoi { get; set; } = new List<NguoiB4>();

        public void Nhap()
        {
            Console.Write("Nhap so nha: ");
            SoNha = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so thanh vien: ");
            SoThanhVien = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < SoThanhVien; i++)
            {
                Console.WriteLine($"Nhap thong tin nguoi thu {i + 1}:");
                NguoiB4 n = new NguoiB4();
                n.Nhap();
                DsNguoi.Add(n);
            }
        }

        public void HienThi()
        {
            Console.WriteLine($"So nha: {SoNha}, So thanh vien: {SoThanhVien}");
            foreach (var n in DsNguoi)
            {
                n.HienThi();
                Console.WriteLine("-----");
            }
        }

        public bool TimKiemTheoHoTen(string ten)
        {
            foreach (var n in DsNguoi)
            {
                if (n.HoTen.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }
            return false;
        }
    }

    // Lớp KhuPho: quản lý danh sách các hộ dân
    internal class KhuPho
    {
        private List<HoDan> dsHoDan = new List<HoDan>();

        public void NhapHoDan()
        {
            Console.Write("Nhap so luong ho dan: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin ho dan thu {i + 1}:");
                HoDan hd = new HoDan();
                hd.Nhap();
                dsHoDan.Add(hd);
            }
        }

        public void TimKiem()
        {
            Console.Write("Nhap ho ten hoac so nha can tim: ");
            string input = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var hd in dsHoDan)
            {
                if (hd.SoNha.Equals(input, StringComparison.OrdinalIgnoreCase) || hd.TimKiemTheoHoTen(input))
                {
                    hd.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay ho dan voi key: " + input);
        }

        public void HienThiTatCa()
        {
            foreach (var hd in dsHoDan)
            {
                hd.HienThi();
                Console.WriteLine("-----");
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HOA DAN");
                Console.WriteLine("1. Nhap danh sach ho dan");
                Console.WriteLine("2. Tim kiem ho dan theo ho ten hoac so nha");
                Console.WriteLine("3. Hien thi tat ca ho dan");
                Console.WriteLine("4. Thoat chuong trinh");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapHoDan();
                        break;
                    case 2:
                        TimKiem();
                        break;
                    case 3:
                        HienThiTatCa();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 4.");
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

    // Lớp tĩnh Bai4 chứa hàm giao diện của bài 4
    internal static class Bai4
    {
        public static void B4()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 4: Quan ly ho dan ------");
            KhuPho kp = new KhuPho();
            kp.Menu();
        }
    }
}
