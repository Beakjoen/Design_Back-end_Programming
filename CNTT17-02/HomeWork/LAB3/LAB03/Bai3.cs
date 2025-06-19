using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai3
    {
        class ThiSinh
        {
            public string SoBaoDanh { get; set; } = string.Empty;
            public string HoTen { get; set; } = string.Empty;
            public string DiaChi { get; set; } = string.Empty;
            public int UuTien { get; set; }
            public double DiemToan { get; set; }
            public double DiemMon2 { get; set; }
            public double DiemMon3 { get; set; }

            public virtual string KhoiThi => "";
            public virtual double DiemChuan => 0;

            public virtual void NhapThongTin()
            {
                Console.Write("Nhap so bao danh: ");
                SoBaoDanh = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ho ten: ");
                HoTen = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap dia chi: ");
                DiaChi = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap diem uu tien (1-3): ");
                UuTien = int.Parse(Console.ReadLine() ?? "0");
                NhapDiem();
            }

            protected virtual void NhapDiem()
            {
                Console.Write("Nhap diem Toan: ");
                DiemToan = double.Parse(Console.ReadLine() ?? "0");
                Console.Write($"Nhap diem {MonThi2}: ");
                DiemMon2 = double.Parse(Console.ReadLine() ?? "0");
                Console.Write($"Nhap diem {MonThi3}: ");
                DiemMon3 = double.Parse(Console.ReadLine() ?? "0");
            }

            protected virtual string MonThi2 => "";
            protected virtual string MonThi3 => "";

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"So bao danh: {SoBaoDanh}");
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Dia chi: {DiaChi}");
                Console.WriteLine($"Uu tien: {UuTien}");
                Console.WriteLine($"Khoi thi: {KhoiThi}");
                Console.WriteLine($"Diem Toan: {DiemToan}");
                Console.WriteLine($"Diem {MonThi2}: {DiemMon2}");
                Console.WriteLine($"Diem {MonThi3}: {DiemMon3}");
                Console.WriteLine($"Tong diem: {TinhTongDiem()}");
            }

            public double TinhTongDiem() => DiemToan + DiemMon2 + DiemMon3 + UuTien;
        }

        class ThiSinhKhoiA : ThiSinh
        {
            public override string KhoiThi => "A";
            public override double DiemChuan => 15;
            protected override string MonThi2 => "Ly";
            protected override string MonThi3 => "Hoa";
        }

        class ThiSinhKhoiB : ThiSinh
        {
            public override string KhoiThi => "B";
            public override double DiemChuan => 16;
            protected override string MonThi2 => "Hoa";
            protected override string MonThi3 => "Sinh";
        }

        class ThiSinhKhoiC : ThiSinh
        {
            public override string KhoiThi => "C";
            public override double DiemChuan => 13.5;
            protected override string MonThi2 => "Van";
            protected override string MonThi3 => "Su";
        }

        class TuyenSinh
        {
            private List<ThiSinh> danhSachThiSinh = new List<ThiSinh>();

            public void ThemThiSinh()
            {
                Console.WriteLine("Chon khoi thi (A/B/C): ");
                string khoi = Console.ReadLine()?.ToUpper() ?? "A";

                ThiSinh thiSinh = khoi switch
                {
                    "A" => new ThiSinhKhoiA(),
                    "B" => new ThiSinhKhoiB(),
                    "C" => new ThiSinhKhoiC(),
                    _ => new ThiSinhKhoiA()
                };

                thiSinh.NhapThongTin();
                danhSachThiSinh.Add(thiSinh);
                Console.WriteLine("Them thi sinh thanh cong!");
            }

            public void HienThiDanhSachTrungTuyen()
            {
                var dsTrungTuyen = danhSachThiSinh
                    .Where(ts => ts.TinhTongDiem() >= ts.DiemChuan)
                    .OrderByDescending(ts => ts.TinhTongDiem());

                if (!dsTrungTuyen.Any())
                {
                    Console.WriteLine("Khong co thi sinh trung tuyen!");
                    return;
                }

                Console.WriteLine("\nDANH SACH THI SINH TRUNG TUYEN:");
                foreach (var ts in dsTrungTuyen)
                {
                    ts.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void TimKiemTheoSoBaoDanh()
            {
                Console.Write("Nhap so bao danh can tim: ");
                string sbd = Console.ReadLine() ?? string.Empty;

                var thiSinh = danhSachThiSinh.FirstOrDefault(ts => ts.SoBaoDanh == sbd);
                if (thiSinh == null)
                {
                    Console.WriteLine("Khong tim thay thi sinh!");
                    return;
                }

                thiSinh.HienThiThongTin();
            }
        }

        public static void B3()
        {
            var tuyenSinh = new TuyenSinh();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY TUYEN SINH");
                Console.WriteLine("1. Them thi sinh");
                Console.WriteLine("2. Hien thi danh sach trung tuyen");
                Console.WriteLine("3. Tim kiem theo so bao danh");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang (1-4): ");

                if (!int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le!");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        tuyenSinh.ThemThiSinh();
                        break;
                    case 2:
                        tuyenSinh.HienThiDanhSachTrungTuyen();
                        break;
                    case 3:
                        tuyenSinh.TimKiemTheoSoBaoDanh();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}
