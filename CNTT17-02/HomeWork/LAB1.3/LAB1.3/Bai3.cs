using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp cơ sở ThiSinh
    internal class ThiSinh
    {
        public string SoBaoDanh { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string UuTien { get; set; } = string.Empty;

        public virtual void Nhap()
        {
            Console.Write("Nhap so bao danh: ");
            SoBaoDanh = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap dia chi: ");
            DiaChi = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap uu tien: ");
            UuTien = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThi()
        {
            Console.WriteLine("So bao danh: " + SoBaoDanh);
            Console.WriteLine("Ho ten: " + HoTen);
            Console.WriteLine("Dia chi: " + DiaChi);
            Console.WriteLine("Uu tien: " + UuTien);
        }

        public virtual double TongDiem() => 0;
        public virtual string GetKhoi() => "";
    }

    // ThiSinh khoi A: Thi Toán, Lý, Hoá (điểm chuẩn 15)
    internal class ThiSinhKhoiA : ThiSinh
    {
        public double Toan { get; set; }
        public double Ly { get; set; }
        public double Hoa { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap diem Toan: ");
            Toan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Ly: ");
            Ly = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Hoa: ");
            Hoa = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            Console.WriteLine("Khoi A");
            base.HienThi();
            Console.WriteLine("Diem Toan: " + Toan);
            Console.WriteLine("Diem Ly: " + Ly);
            Console.WriteLine("Diem Hoa: " + Hoa);
            Console.WriteLine("Tong diem: " + TongDiem());
        }

        public override double TongDiem() => Toan + Ly + Hoa;
        public override string GetKhoi() => "A";
    }

    // ThiSinh khoi B: Thi Toán, Hoá, Sinh (điểm chuẩn 16)
    internal class ThiSinhKhoiB : ThiSinh
    {
        public double Toan { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap diem Toan: ");
            Toan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Hoa: ");
            Hoa = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Sinh: ");
            Sinh = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            Console.WriteLine("Khoi B");
            base.HienThi();
            Console.WriteLine("Diem Toan: " + Toan);
            Console.WriteLine("Diem Hoa: " + Hoa);
            Console.WriteLine("Diem Sinh: " + Sinh);
            Console.WriteLine("Tong diem: " + TongDiem());
        }

        public override double TongDiem() => Toan + Hoa + Sinh;
        public override string GetKhoi() => "B";
    }

    // ThiSinh khoi C: Thi Văn, Sử, Địa (điểm chuẩn 13.5)
    internal class ThiSinhKhoiC : ThiSinh
    {
        public double Van { get; set; }
        public double Su { get; set; }
        public double Dia { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap diem Van: ");
            Van = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Su: ");
            Su = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Dia: ");
            Dia = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            Console.WriteLine("Khoi C");
            base.HienThi();
            Console.WriteLine("Diem Van: " + Van);
            Console.WriteLine("Diem Su: " + Su);
            Console.WriteLine("Diem Dia: " + Dia);
            Console.WriteLine("Tong diem: " + TongDiem());
        }

        public override double TongDiem() => Van + Su + Dia;
        public override string GetKhoi() => "C";
    }

    // Lớp TuyenSinh: quản lý danh sách thí sinh và các chức năng nhập, tìm kiếm, hiển thị
    internal class TuyenSinh
    {
        private List<ThiSinh> dsThiSinh = new List<ThiSinh>();

        public void NhapThiSinh()
        {
            Console.WriteLine("Chon khoi thi:");
            Console.WriteLine("1. Khoi A");
            Console.WriteLine("2. Khoi B");
            Console.WriteLine("3. Khoi C");
            Console.Write("Nhap lua chon: ");
            int luaChon = int.Parse(Console.ReadLine() ?? "0");
            ThiSinh ts = luaChon switch
            {
                1 => new ThiSinhKhoiA(),
                2 => new ThiSinhKhoiB(),
                3 => new ThiSinhKhoiC(),
                _ => null
            };
            if (ts == null)
            {
                Console.WriteLine("Lua chon khong hop le.");
                return;
            }
            ts.Nhap();
            dsThiSinh.Add(ts);
        }

        public void HienThiTrungTuyen()
        {
            Console.WriteLine("Danh sach thi sinh trung tuyen:");
            foreach (var ts in dsThiSinh)
            {
                bool trungTuyen = false;
                if (ts is ThiSinhKhoiA && ts.TongDiem() >= 15)
                    trungTuyen = true;
                if (ts is ThiSinhKhoiB && ts.TongDiem() >= 16)
                    trungTuyen = true;
                if (ts is ThiSinhKhoiC && ts.TongDiem() >= 13.5)
                    trungTuyen = true;

                if (trungTuyen)
                {
                    ts.HienThi();
                    Console.WriteLine("-----");
                }
            }
        }

        public void TimKiemTheoSoBaoDanh()
        {
            Console.Write("Nhap so bao danh can tim: ");
            string soBD = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var ts in dsThiSinh)
            {
                if (ts.SoBaoDanh.Equals(soBD, StringComparison.OrdinalIgnoreCase))
                {
                    ts.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Khong tim thay thi sinh voi so bao danh: " + soBD);
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH TUYEN SINH");
                Console.WriteLine("1. Nhap thong tin thi sinh");
                Console.WriteLine("2. Hien thi thi sinh trung tuyen");
                Console.WriteLine("3. Tim kiem theo so bao danh");
                Console.WriteLine("4. Thoat chuong trinh");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapThiSinh();
                        break;
                    case 2:
                        HienThiTrungTuyen();
                        break;
                    case 3:
                        TimKiemTheoSoBaoDanh();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 3.");
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

    // Lớp tĩnh Bai3 chứa hàm giao diện của bài 3
    internal static class Bai3
    {
        public static void B3()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 3: Tuyen sinh ------");
            TuyenSinh ts = new TuyenSinh();
            ts.Menu();
        }
    }
}
