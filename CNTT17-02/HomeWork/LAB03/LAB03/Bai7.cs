using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai7
    {
        class Nguoi
        {
            public string HoTen { get; set; } = string.Empty;
            public int NamSinh { get; set; }
            public string QueQuan { get; set; } = string.Empty;
            public string CMND { get; set; } = string.Empty;

            public virtual void NhapThongTin()
            {
                Console.Write("Nhap ho ten: ");
                HoTen = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap nam sinh: ");
                NamSinh = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap que quan: ");
                QueQuan = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap CMND: ");
                CMND = Console.ReadLine() ?? string.Empty;
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Nam sinh: {NamSinh}");
                Console.WriteLine($"Que quan: {QueQuan}");
                Console.WriteLine($"CMND: {CMND}");
            }
        }

        class CBGV : Nguoi
        {
            public decimal LuongCung { get; set; }
            public decimal Thuong { get; set; }
            public decimal Phat { get; set; }
            public decimal LuongThucLinh => LuongCung + Thuong - Phat;

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.Write("Nhap luong cung: ");
                LuongCung = decimal.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap thuong: ");
                Thuong = decimal.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap phat: ");
                Phat = decimal.Parse(Console.ReadLine() ?? "0");
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"Luong cung: {LuongCung:N0} VND");
                Console.WriteLine($"Thuong: {Thuong:N0} VND");
                Console.WriteLine($"Phat: {Phat:N0} VND");
                Console.WriteLine($"Luong thuc linh: {LuongThucLinh:N0} VND");
            }
        }

        class QuanLyCBGV
        {
            private List<CBGV> danhSachCBGV = new List<CBGV>();

            public void NhapDanhSach()
            {
                Console.Write("Nhap so luong can bo giao vien: ");
                int soLuong = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soLuong; i++)
                {
                    Console.WriteLine($"\nNhap thong tin CBGV thu {i + 1}:");
                    var cbgv = new CBGV();
                    cbgv.NhapThongTin();
                    danhSachCBGV.Add(cbgv);
                }
            }

            public void TimKiemTheoQueQuan()
            {
                Console.Write("Nhap que quan can tim: ");
                string queQuan = Console.ReadLine() ?? string.Empty;

                var ketQua = danhSachCBGV
                    .Where(cb => cb.QueQuan.Contains(queQuan, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                HienThiKetQua(ketQua, "KET QUA TIM KIEM THEO QUE QUAN:");
            }

            public void HienThiLuongTren5Trieu()
            {
                var ketQua = danhSachCBGV
                    .Where(cb => cb.LuongThucLinh > 5000000)
                    .OrderByDescending(cb => cb.LuongThucLinh)
                    .ToList();

                HienThiKetQua(ketQua, "DANH SACH CBGV CO LUONG TREN 5 TRIEU:");
            }

            private void HienThiKetQua(List<CBGV> ketQua, string tieuDe)
            {
                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong tim thay ket qua nao!");
                    return;
                }

                Console.WriteLine($"\n{tieuDe}");
                foreach (var cbgv in ketQua)
                {
                    cbgv.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B7()
        {
            var quanLy = new QuanLyCBGV();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY CAN BO GIAO VIEN");
                Console.WriteLine("1. Nhap danh sach CBGV");
                Console.WriteLine("2. Tim kiem theo que quan");
                Console.WriteLine("3. Hien thi CBGV co luong tren 5 trieu");
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
                        quanLy.NhapDanhSach();
                        break;
                    case 2:
                        quanLy.TimKiemTheoQueQuan();
                        break;
                    case 3:
                        quanLy.HienThiLuongTren5Trieu();
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
