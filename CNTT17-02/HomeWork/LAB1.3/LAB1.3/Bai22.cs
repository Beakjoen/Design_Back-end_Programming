using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QLApp
{
    internal class HocSinh22
    {
        public string HoTen { get; set; } = string.Empty;
        public int NamSinh { get; set; }
        public double TongDiem { get; set; }

        public void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap tong diem: ");
            TongDiem = double.Parse(Console.ReadLine() ?? "0");
        }

        // Chuyển đổi tên sao cho mỗi chữ cái đầu của từ được viết hoa.
        public void ChuanHoaTen()
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            HoTen = ti.ToTitleCase(HoTen.ToLower());
        }

        public void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Nam sinh: {NamSinh}, Tong diem: {TongDiem}");
        }
    }

    internal class QuanLyHocSinh22
    {
        private List<HocSinh22> dsHS = new List<HocSinh22>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong hoc sinh: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap hoc sinh thu {i + 1}:");
                HocSinh22 hs = new HocSinh22();
                hs.Nhap();
                dsHS.Add(hs);
            }
        }

        public void SapXepVaHienThi()
        {
            // Sắp xếp theo tổng diem giảm dần; nếu bằng thì hoc sinh co nam sinh nho hon (tuổi cao hơn).
            var dsSapXep = dsHS.OrderByDescending(hs => hs.TongDiem)
                               .ThenBy(hs => hs.NamSinh)
                               .ToList();
            // Chuẩn hóa tên
            foreach (var hs in dsSapXep)
            {
                hs.ChuanHoaTen();
            }
            Console.WriteLine("Danh sach hoc sinh sau khi sap xep:");
            foreach (var hs in dsSapXep)
            {
                hs.HienThi();
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH SAP XEP HOC SINH (Bai 22)");
                Console.WriteLine("1. Nhap danh sach hoc sinh");
                Console.WriteLine("2. Sap xep va hien thi danh sach hoc sinh");
                Console.WriteLine("3. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        SapXepVaHienThi();
                        break;
                    case 3:
                        Console.WriteLine("Thoat chuong trinh Bai 22.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
                if (luaChon != 3)
                {
                    Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            } while (luaChon != 3);
        }
    }

    internal static class Bai22
    {
        public static void B22()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 22: Sap xep danh sach hoc sinh ------");
            QuanLyHocSinh22 ql = new QuanLyHocSinh22();
            ql.Menu();
        }
    }
}
