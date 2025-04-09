using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp KhachHang: lưu thông tin hộ sử dụng điện
    internal class KhachHang
    {
        public string HoTenChuHo { get; set; } = string.Empty;
        public string SoNha { get; set; } = string.Empty;
        public string MaCongTo { get; set; } = string.Empty;

        public void Nhap()
        {
            Console.Write("Nhap ho ten chu ho: ");
            HoTenChuHo = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so nha: ");
            SoNha = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ma cong to: ");
            MaCongTo = Console.ReadLine() ?? string.Empty;
        }

        public void HienThi()
        {
            Console.WriteLine($"Ho ten chu ho: {HoTenChuHo}, So nha: {SoNha}, Ma cong to: {MaCongTo}");
        }
    }

    // Lớp BienLai: quản lý chỉ số cũ, chỉ số mới và tính tiền điện
    internal class BienLai
    {
        public KhachHang Kh { get; set; } = new KhachHang();
        public int ChiSoCu { get; set; }
        public int ChiSoMoi { get; set; }

        public void Nhap()
        {
            Console.WriteLine("Nhap thong tin bien lai:");
            Kh.Nhap();
            Console.Write("Nhap chi so cu: ");
            ChiSoCu = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap chi so moi: ");
            ChiSoMoi = int.Parse(Console.ReadLine() ?? "0");
        }

        public int TinhTienDien()
        {
            int soDien = ChiSoMoi - ChiSoCu;
            int giaTien;
            if (soDien < 50)
                giaTien = 1250;
            else if (soDien < 100)
                giaTien = 1500;
            else
                giaTien = 2000;
            return soDien * giaTien;
        }

        public void HienThi()
        {
            Kh.HienThi();
            Console.WriteLine($"Chi so cu: {ChiSoCu}, Chi so moi: {ChiSoMoi}");
            Console.WriteLine("Tien dien phai tra: " + TinhTienDien());
        }
    }

    // Lớp QuanLyBienLai: quản lý danh sách biên lai thu tiền điện
    internal class QuanLyBienLai
    {
        private List<BienLai> dsBienLai = new List<BienLai>();

        public void NhapBienLai()
        {
            Console.Write("Nhap so luong bien lai: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin bien lai thu {i + 1}:");
                BienLai bl = new BienLai();
                bl.Nhap();
                dsBienLai.Add(bl);
            }
        }

        public void HienThiBienLai()
        {
            foreach (var bl in dsBienLai)
            {
                bl.HienThi();
                Console.WriteLine("-----");
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY BIEN LAI DIEN");
                Console.WriteLine("1. Nhap thong tin bien lai");
                Console.WriteLine("2. Hien thi tat ca bien lai");
                Console.WriteLine("3. Tinh tien dien (hien thi chi tiet)");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapBienLai();
                        break;
                    case 2:
                        HienThiBienLai();
                        break;
                    case 3:
                        foreach (var bl in dsBienLai)
                        {
                            Console.WriteLine("Bien lai:");
                            bl.HienThi();
                            Console.WriteLine("-----");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 9.");
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

    internal static class Bai9
    {
        public static void B9()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 9: Quan ly bien lai tien dien ------");
            QuanLyBienLai ql = new QuanLyBienLai();
            ql.Menu();
        }
    }
}
