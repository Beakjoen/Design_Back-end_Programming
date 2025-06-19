using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai5
    {
        class Nguoi
        {
            public string HoTen { get; set; } = string.Empty;
            public int NamSinh { get; set; }
            public string CMND { get; set; } = string.Empty;

            public virtual void NhapThongTin()
            {
                Console.Write("Nhap ho ten: ");
                HoTen = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap nam sinh: ");
                NamSinh = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap CMND: ");
                CMND = Console.ReadLine() ?? string.Empty;
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Nam sinh: {NamSinh}");
                Console.WriteLine($"CMND: {CMND}");
            }
        }

        class KhachTro : Nguoi
        {
            public int SoNgayTro { get; set; }
            public string LoaiPhong { get; set; } = string.Empty;
            public decimal GiaPhong { get; set; }

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.Write("Nhap so ngay tro: ");
                SoNgayTro = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap loai phong (A/B/C): ");
                LoaiPhong = Console.ReadLine()?.ToUpper() ?? "A";
                GiaPhong = LoaiPhong switch
                {
                    "A" => 500000, // 500,000 VND
                    "B" => 300000, // 300,000 VND
                    "C" => 100000, // 100,000 VND
                    _ => 100000
                };
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"So ngay tro: {SoNgayTro}");
                Console.WriteLine($"Loai phong: {LoaiPhong}");
                Console.WriteLine($"Gia phong: {GiaPhong:N0} VND/ngay");
            }

            public decimal TinhTien()
            {
                return SoNgayTro * GiaPhong;
            }
        }

        class KhachSan
        {
            private List<KhachTro> danhSachKhach = new List<KhachTro>();

            public void NhapDanhSachKhach()
            {
                Console.Write("Nhap so luong khach tro: ");
                int soKhach = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soKhach; i++)
                {
                    Console.WriteLine($"\nNhap thong tin khach tro thu {i + 1}:");
                    var khachTro = new KhachTro();
                    khachTro.NhapThongTin();
                    danhSachKhach.Add(khachTro);
                }
            }

            public void HienThiDanhSach()
            {
                if (!danhSachKhach.Any())
                {
                    Console.WriteLine("Khong co khach tro nao!");
                    return;
                }

                Console.WriteLine("\nDANH SACH KHACH TRO:");
                foreach (var khach in danhSachKhach)
                {
                    khach.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void TimKiemTheoTen()
            {
                Console.Write("Nhap ten khach can tim: ");
                string tenKhach = Console.ReadLine() ?? string.Empty;

                var ketQua = danhSachKhach
                    .Where(k => k.HoTen.Contains(tenKhach, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong tim thay khach hang!");
                    return;
                }

                Console.WriteLine("\nKET QUA TIM KIEM:");
                foreach (var khach in ketQua)
                {
                    khach.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void TinhTienPhong()
            {
                Console.Write("Nhap CMND khach can tinh tien: ");
                string cmnd = Console.ReadLine() ?? string.Empty;

                var khach = danhSachKhach.FirstOrDefault(k => k.CMND == cmnd);
                if (khach == null)
                {
                    Console.WriteLine("Khong tim thay khach hang!");
                    return;
                }

                decimal tongTien = khach.TinhTien();
                Console.WriteLine("\nTHONG TIN THANH TOAN:");
                khach.HienThiThongTin();
                Console.WriteLine($"Tong tien: {tongTien:N0} VND");
                
                // Xóa khách sau khi thanh toán
                danhSachKhach.Remove(khach);
                Console.WriteLine("Da thanh toan va tra phong thanh cong!");
            }
        }

        public static void B5()
        {
            var khachSan = new KhachSan();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY KHACH SAN");
                Console.WriteLine("1. Nhap danh sach khach tro");
                Console.WriteLine("2. Hien thi danh sach khach tro");
                Console.WriteLine("3. Tim kiem khach theo ten");
                Console.WriteLine("4. Tinh tien phong");
                Console.WriteLine("5. Thoat");
                Console.Write("Chon chuc nang (1-5): ");

                if (!int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le!");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        khachSan.NhapDanhSachKhach();
                        break;
                    case 2:
                        khachSan.HienThiDanhSach();
                        break;
                    case 3:
                        khachSan.TimKiemTheoTen();
                        break;
                    case 4:
                        khachSan.TinhTienPhong();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}
