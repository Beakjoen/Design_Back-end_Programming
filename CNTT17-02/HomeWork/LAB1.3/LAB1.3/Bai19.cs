using System;
using System.Collections.Generic;
using System.Linq;

namespace QLApp
{
    // Lớp THISINH
    internal class Thisinh
    {
        // Có thể tách thành cấu trúc nhưng để đơn giản, gom tất cả vào thuộc tính dạng string và số
        public string HoTen { get; set; } = string.Empty;  // Bao gồm: Ho, ten dem, ten
        public string QueQuan { get; set; } = string.Empty;  // Bao gồm: xa, huyen, tinh
        public string Truong { get; set; } = string.Empty;
        public int Tuoi { get; set; }
        public int SoBaoDanh { get; set; }
        public int NamSinh { get; set; }  // Thêm để phục vụ sắp xếp
        public double DiemToan { get; set; }
        public double DiemLy { get; set; }
        public double DiemHoa { get; set; }

        public void Nhap()
        {
            Console.Write("Nhap ho ten (ho, ten dem, ten): ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap que quan (xa, huyen, tinh): ");
            QueQuan = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap truong: ");
            Truong = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap tuoi: ");
            Tuoi = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap so bao danh: ");
            SoBaoDanh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Toan: ");
            DiemToan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Ly: ");
            DiemLy = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Hoa: ");
            DiemHoa = double.Parse(Console.ReadLine() ?? "0");
        }

        public double TongDiem() => DiemToan + DiemLy + DiemHoa;

        public void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Que quan: {QueQuan}, So bao danh: {SoBaoDanh}");
            Console.WriteLine($"Diem Toan: {DiemToan}, Diem Ly: {DiemLy}, Diem Hoa: {DiemHoa}");
            Console.WriteLine("Tong diem: " + TongDiem());
        }
    }

    // Lớp QuanLyThisinh quản lý danh sách và các thao tác
    internal class QuanLyThisinh
    {
        private List<Thisinh> dsThisinh = new List<Thisinh>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong thi sinh: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin thi sinh thu {i + 1}:");
                Thisinh ts = new Thisinh();
                ts.Nhap();
                dsThisinh.Add(ts);
            }
        }

        public void HienThiTongDiemLonHon15()
        {
            Console.WriteLine("Danh sach thi sinh co tong diem lon hon 15:");
            foreach (var ts in dsThisinh)
            {
                if (ts.TongDiem() > 15)
                {
                    ts.HienThi();
                    Console.WriteLine("-----");
                }
            }
        }

        public void SapXepVaHienThi()
        {
            // Sắp xếp theo tổng điểm giảm dần; nếu bằng, thi sinh co nam sinh nho hon (i.e. cu hon) đứng trước.
            var dsSapXep = dsThisinh.OrderByDescending(ts => ts.TongDiem())
                                      .ThenBy(ts => ts.NamSinh);
            Console.WriteLine("Danh sach thi sinh sau khi sap xep:");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,8} {4,8} {5,8}", "Ho ten", "Que quan", "So bao danh", "Toan", "Ly", "Hoa");
            Console.WriteLine("------------------------------------------------------");
            foreach (var ts in dsSapXep)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,8} {4,8} {5,8}",
                    ts.HoTen, ts.QueQuan, ts.SoBaoDanh, ts.DiemToan, ts.DiemLy, ts.DiemHoa);
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY THISINH");
                Console.WriteLine("1. Nhap danh sach thi sinh");
                Console.WriteLine("2. Hien thi thi sinh co tong diem > 15");
                Console.WriteLine("3. Sap xep va hien thi danh sach");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        HienThiTongDiemLonHon15();
                        break;
                    case 3:
                        SapXepVaHienThi();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 19.");
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

    internal static class Bai19
    {
        public static void B19()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 19: THISINH ------");
            QuanLyThisinh ql = new QuanLyThisinh();
            ql.Menu();
        }
    }
}
