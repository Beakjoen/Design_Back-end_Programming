using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai4
    {
        class Nguoi
        {
            public string CMND { get; set; } = string.Empty;
            public string HoTen { get; set; } = string.Empty;
            public int Tuoi { get; set; }
            public int NamSinh { get; set; }
            public string NgheNghiep { get; set; } = string.Empty;

            public void NhapThongTin()
            {
                Console.Write("Nhap CMND: ");
                CMND = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ho ten: ");
                HoTen = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap tuoi: ");
                Tuoi = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap nam sinh: ");
                NamSinh = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap nghe nghiep: ");
                NgheNghiep = Console.ReadLine() ?? string.Empty;
            }

            public void HienThiThongTin()
            {
                Console.WriteLine($"CMND: {CMND}");
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Tuoi: {Tuoi}");
                Console.WriteLine($"Nam sinh: {NamSinh}");
                Console.WriteLine($"Nghe nghiep: {NgheNghiep}");
            }
        }

        class HoDan
        {
            public int SoThanhVien { get; set; }
            public string SoNha { get; set; } = string.Empty;
            public List<Nguoi> ThanhVien { get; set; } = new List<Nguoi>();

            public void NhapThongTin()
            {
                Console.Write("Nhap so nha: ");
                SoNha = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap so thanh vien: ");
                SoThanhVien = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < SoThanhVien; i++)
                {
                    Console.WriteLine($"\nNhap thong tin thanh vien thu {i + 1}:");
                    var thanhVien = new Nguoi();
                    thanhVien.NhapThongTin();
                    ThanhVien.Add(thanhVien);
                }
            }

            public void HienThiThongTin()
            {
                Console.WriteLine($"\nThong tin ho dan so nha {SoNha}:");
                Console.WriteLine($"So thanh vien: {SoThanhVien}");
                for (int i = 0; i < ThanhVien.Count; i++)
                {
                    Console.WriteLine($"\nThanh vien thu {i + 1}:");
                    ThanhVien[i].HienThiThongTin();
                }
            }
        }

        class KhuPho
        {
            private List<HoDan> danhSachHoDan = new List<HoDan>();

            public void NhapDanhSachHoDan()
            {
                Console.Write("Nhap so ho dan: ");
                int soHoDan = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soHoDan; i++)
                {
                    Console.WriteLine($"\nNhap thong tin ho dan thu {i + 1}:");
                    var hoDan = new HoDan();
                    hoDan.NhapThongTin();
                    danhSachHoDan.Add(hoDan);
                }
            }

            public void TimKiemHoDan()
            {
                Console.WriteLine("Chon tieu chi tim kiem:");
                Console.WriteLine("1. Theo ho ten");
                Console.WriteLine("2. Theo so nha");
                Console.Write("Lua chon (1-2): ");
                
                int luaChon = int.Parse(Console.ReadLine() ?? "0");
                if (luaChon == 1)
                {
                    Console.Write("Nhap ho ten can tim: ");
                    string hoTen = Console.ReadLine() ?? string.Empty;
                    var ketQua = danhSachHoDan.Where(hd => 
                        hd.ThanhVien.Any(tv => tv.HoTen.Contains(hoTen, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                    HienThiKetQuaTimKiem(ketQua);
                }
                else if (luaChon == 2)
                {
                    Console.Write("Nhap so nha can tim: ");
                    string soNha = Console.ReadLine() ?? string.Empty;
                    var ketQua = danhSachHoDan.Where(hd => hd.SoNha == soNha).ToList();
                    HienThiKetQuaTimKiem(ketQua);
                }
            }

            private void HienThiKetQuaTimKiem(List<HoDan> ketQua)
            {
                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong tim thay ho dan nao!");
                    return;
                }

                Console.WriteLine("\nKet qua tim kiem:");
                foreach (var hoDan in ketQua)
                {
                    hoDan.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }

            public void HienThiTatCaHoDan()
            {
                if (!danhSachHoDan.Any())
                {
                    Console.WriteLine("Chua co ho dan nao trong khu pho!");
                    return;
                }

                Console.WriteLine("\nDANH SACH TAT CA HO DAN TRONG KHU PHO:");
                foreach (var hoDan in danhSachHoDan)
                {
                    hoDan.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B4()
        {
            var khuPho = new KhuPho();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY KHU PHO");
                Console.WriteLine("1. Nhap danh sach ho dan");
                Console.WriteLine("2. Tim kiem ho dan");
                Console.WriteLine("3. Hien thi tat ca ho dan");
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
                        khuPho.NhapDanhSachHoDan();
                        break;
                    case 2:
                        khuPho.TimKiemHoDan();
                        break;
                    case 3:
                        khuPho.HienThiTatCaHoDan();
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
