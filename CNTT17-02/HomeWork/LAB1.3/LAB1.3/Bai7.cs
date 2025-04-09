using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp Nguoi cho CBGV với thông tin cá nhân
    internal class Nguoi7
    {
        public string HoTen { get; set; } = string.Empty;
        public int NamSinh { get; set; }
        public string QueQuan { get; set; } = string.Empty;
        public string SoCMND { get; set; } = string.Empty;

        public virtual void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap que quan: ");
            QueQuan = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so CMND: ");
            SoCMND = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Nam sinh: {NamSinh}, Que quan: {QueQuan}, So CMND: {SoCMND}");
        }
    }

    // Lớp CBGV kế thừa từ Nguoi7, thêm các thuộc tính lương
    internal class CBGV : Nguoi7
    {
        public double LuongCoDinh { get; set; }
        public double Thuong { get; set; }
        public double Phat { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap luong co dinh: ");
            LuongCoDinh = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap thuong: ");
            Thuong = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap phat: ");
            Phat = double.Parse(Console.ReadLine() ?? "0");
        }

        public double TinhLuongThucLinh() => LuongCoDinh + Thuong - Phat;

        public override void HienThi()
        {
            base.HienThi();
            Console.WriteLine($"Luong co dinh: {LuongCoDinh}, Thuong: {Thuong}, Phat: {Phat}");
            Console.WriteLine("Luong thuc linh: " + TinhLuongThucLinh());
        }
    }

    // Lớp QuanLyCBGV quản lý danh sách CBGV
    internal class QuanLyCBGV
    {
        private List<CBGV> dsGV = new List<CBGV>();

        public void NhapCBGV()
        {
            Console.Write("Nhap so luong CBGV: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin CBGV thu {i + 1}:");
                CBGV gv = new CBGV();
                gv.Nhap();
                dsGV.Add(gv);
            }
        }

        public void TimKiemTheoQueQuan()
        {
            Console.Write("Nhap que quan can tim: ");
            string que = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var gv in dsGV)
            {
                if (gv.QueQuan.IndexOf(que, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    gv.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay CBGV voi que quan: " + que);
        }

        public void HienThiLuongTren5Trieu()
        {
            Console.WriteLine("Danh sach CBGV co luong thuc linh tren 5 trieu:");
            bool found = false;
            foreach (var gv in dsGV)
            {
                if (gv.TinhLuongThucLinh() > 5000000)
                {
                    gv.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong co CBGV nao co luong thuc linh tren 5 trieu");
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY CBGV");
                Console.WriteLine("1. Nhap thong tin CBGV");
                Console.WriteLine("2. Tim kiem theo que quan");
                Console.WriteLine("3. Hien thi CBGV co luong tren 5 trieu");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapCBGV();
                        break;
                    case 2:
                        TimKiemTheoQueQuan();
                        break;
                    case 3:
                        HienThiLuongTren5Trieu();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 7.");
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

    internal static class Bai7
    {
        public static void B7()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 7: Quan ly thanh toan luong cho CBGV ------");
            QuanLyCBGV ql = new QuanLyCBGV();
            ql.Menu();
        }
    }
}
