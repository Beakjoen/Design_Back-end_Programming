using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai9
    {
        class KhachHang
        {
            public string HoTenChuHo { get; set; } = string.Empty;
            public string SoNha { get; set; } = string.Empty;
            public string MaCongTo { get; set; } = string.Empty;

            public virtual void NhapThongTin()
            {
                Console.Write("Nhap ho ten chu ho: ");
                HoTenChuHo = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap so nha: ");
                SoNha = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ma cong to: ");
                MaCongTo = Console.ReadLine() ?? string.Empty;
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ho ten chu ho: {HoTenChuHo}");
                Console.WriteLine($"So nha: {SoNha}");
                Console.WriteLine($"Ma cong to: {MaCongTo}");
            }
        }

        class BienLai
        {
            public KhachHang ThongTinKH { get; set; }
            public int ChiSoCu { get; set; }
            public int ChiSoMoi { get; set; }
            public decimal SoTienPhaiTra { get; private set; }

            public BienLai()
            {
                ThongTinKH = new KhachHang();
            }

            public void NhapThongTin()
            {
                ThongTinKH.NhapThongTin();
                Console.Write("Nhap chi so cu: ");
                ChiSoCu = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap chi so moi: ");
                ChiSoMoi = int.Parse(Console.ReadLine() ?? "0");
                TinhTienDien();
            }

            public void HienThiThongTin()
            {
                ThongTinKH.HienThiThongTin();
                Console.WriteLine($"Chi so cu: {ChiSoCu}");
                Console.WriteLine($"Chi so moi: {ChiSoMoi}");
                Console.WriteLine($"So dien tieu thu: {ChiSoMoi - ChiSoCu}");
                Console.WriteLine($"So tien phai tra: {SoTienPhaiTra:N0} VND");
            }

            private void TinhTienDien()
            {
                int soDien = ChiSoMoi - ChiSoCu;
                SoTienPhaiTra = soDien switch
                {
                    < 50 => soDien * 1250,
                    < 100 => soDien * 1500,
                    _ => soDien * 2000
                };
            }
        }

        class QuanLyBienLai
        {
            private List<BienLai> danhSachBienLai = new List<BienLai>();

            public void NhapDanhSach()
            {
                Console.Write("Nhap so luong ho dan: ");
                int soLuong = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soLuong; i++)
                {
                    Console.WriteLine($"\nNhap thong tin bien lai thu {i + 1}:");
                    var bienLai = new BienLai();
                    bienLai.NhapThongTin();
                    danhSachBienLai.Add(bienLai);
                }
            }

            public void HienThiDanhSach()
            {
                if (!danhSachBienLai.Any())
                {
                    Console.WriteLine("Danh sach trong!");
                    return;
                }

                Console.WriteLine("\nDANH SACH BIEN LAI:");
                foreach (var bienLai in danhSachBienLai)
                {
                    bienLai.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B9()
        {
            var quanLy = new QuanLyBienLai();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY TIEN DIEN");
                Console.WriteLine("1. Nhap danh sach bien lai");
                Console.WriteLine("2. Hien thi danh sach bien lai");
                Console.WriteLine("3. Thoat");
                Console.Write("Chon chuc nang (1-3): ");

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
                        quanLy.HienThiDanhSach();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}
