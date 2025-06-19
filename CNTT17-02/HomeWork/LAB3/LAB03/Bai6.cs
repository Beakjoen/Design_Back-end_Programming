using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai6
    {
        class Nguoi
        {
            public string HoTen { get; set; } = string.Empty;
            public int Tuoi { get; set; }
            public int NamSinh { get; set; }
            public string QueQuan { get; set; } = string.Empty;
            public string GioiTinh { get; set; } = string.Empty;

            public virtual void NhapThongTin()
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
                GioiTinh = Console.ReadLine()?.Trim().ToUpper() ?? "NAM";
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Tuoi: {Tuoi}");
                Console.WriteLine($"Nam sinh: {NamSinh}");
                Console.WriteLine($"Que quan: {QueQuan}");
                Console.WriteLine($"Gioi tinh: {GioiTinh}");
            }
        }

        class HSHocSinh : Nguoi
        {
            public string Lop { get; set; } = string.Empty;
            public string KhoaHoc { get; set; } = string.Empty;
            public int KyHoc { get; set; }

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.Write("Nhap lop: ");
                Lop = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap khoa hoc: ");
                KhoaHoc = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ky hoc (1-2): ");
                KyHoc = int.Parse(Console.ReadLine() ?? "1");
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"Lop: {Lop}");
                Console.WriteLine($"Khoa hoc: {KhoaHoc}");
                Console.WriteLine($"Ky hoc: {KyHoc}");
            }
        }

        class QuanLyHocSinh
        {
            private List<HSHocSinh> danhSachHocSinh = new List<HSHocSinh>();

            public void NhapDanhSachHocSinh()
            {
                Console.Write("Nhap so luong hoc sinh: ");
                int soLuong = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soLuong; i++)
                {
                    Console.WriteLine($"\nNhap thong tin hoc sinh thu {i + 1}:");
                    var hocSinh = new HSHocSinh();
                    hocSinh.NhapThongTin();
                    danhSachHocSinh.Add(hocSinh);
                }
            }

            public void HienThiHocSinhNu1985()
            {
                var ketQua = danhSachHocSinh
                    .Where(hs => hs.GioiTinh.Equals("NU", StringComparison.OrdinalIgnoreCase) 
                           && hs.NamSinh == 1985)
                    .ToList();

                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong co hoc sinh nu sinh nam 1985!");
                    return;
                }

                Console.WriteLine("\nDANH SACH HOC SINH NU SINH NAM 1985:");
                foreach (var hocSinh in ketQua)
                {
                    hocSinh.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void TimKiemTheoQueQuan()
            {
                Console.Write("Nhap que quan can tim: ");
                string queQuan = Console.ReadLine() ?? string.Empty;

                var ketQua = danhSachHocSinh
                    .Where(hs => hs.QueQuan.Contains(queQuan, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong tim thay hoc sinh nao!");
                    return;
                }

                Console.WriteLine("\nKET QUA TIM KIEM:");
                foreach (var hocSinh in ketQua)
                {
                    hocSinh.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B6()
        {
            var quanLy = new QuanLyHocSinh();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HO SO HOC SINH");
                Console.WriteLine("1. Nhap danh sach hoc sinh");
                Console.WriteLine("2. Hien thi hoc sinh nu sinh nam 1985");
                Console.WriteLine("3. Tim kiem theo que quan");
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
                        quanLy.NhapDanhSachHocSinh();
                        break;
                    case 2:
                        quanLy.HienThiHocSinhNu1985();
                        break;
                    case 3:
                        quanLy.TimKiemTheoQueQuan();
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
