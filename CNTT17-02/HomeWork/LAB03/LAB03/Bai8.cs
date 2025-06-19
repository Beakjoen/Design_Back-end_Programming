using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB03
{
    internal class Bai8
    {
        class SinhVien
        {
            public string HoTen { get; set; } = string.Empty;
            public int NamSinh { get; set; }
            public string Lop { get; set; } = string.Empty;
            public string MaSV { get; set; } = string.Empty;

            public virtual void NhapThongTin()
            {
                Console.Write("Nhap ho ten: ");
                HoTen = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap nam sinh: ");
                NamSinh = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Nhap lop: ");
                Lop = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ma sinh vien: ");
                MaSV = Console.ReadLine() ?? string.Empty;
            }

            public virtual void HienThiThongTin()
            {
                Console.WriteLine($"Ho ten: {HoTen}");
                Console.WriteLine($"Nam sinh: {NamSinh}");
                Console.WriteLine($"Lop: {Lop}");
                Console.WriteLine($"Ma sinh vien: {MaSV}");
            }
        }

        class TheMuon : SinhVien
        {
            public string SoPhieuMuon { get; set; } = string.Empty;
            public DateTime NgayMuon { get; set; }
            public DateTime HanTra { get; set; }
            public string SoHieuSach { get; set; } = string.Empty;

            public override void NhapThongTin()
            {
                base.NhapThongTin();
                Console.Write("Nhap so phieu muon: ");
                SoPhieuMuon = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap so hieu sach: ");
                SoHieuSach = Console.ReadLine() ?? string.Empty;
                Console.Write("Nhap ngay muon (dd/MM/yyyy): ");
                NgayMuon = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd/MM/yyyy"));
                Console.Write("Nhap han tra (dd/MM/yyyy): ");
                HanTra = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"));
            }

            public override void HienThiThongTin()
            {
                base.HienThiThongTin();
                Console.WriteLine($"So phieu muon: {SoPhieuMuon}");
                Console.WriteLine($"So hieu sach: {SoHieuSach}");
                Console.WriteLine($"Ngay muon: {NgayMuon:dd/MM/yyyy}");
                Console.WriteLine($"Han tra: {HanTra:dd/MM/yyyy}");
            }
        }

        class QuanLyThuVien
        {
            private List<TheMuon> danhSachTheMuon = new List<TheMuon>();

            public void NhapDanhSach()
            {
                Console.Write("Nhap so luong sinh vien muon sach: ");
                int soLuong = int.Parse(Console.ReadLine() ?? "0");

                for (int i = 0; i < soLuong; i++)
                {
                    Console.WriteLine($"\nNhap thong tin the muon thu {i + 1}:");
                    var theMuon = new TheMuon();
                    theMuon.NhapThongTin();
                    danhSachTheMuon.Add(theMuon);
                }
            }

            public void TimKiemTheoMaSV()
            {
                Console.Write("Nhap ma sinh vien can tim: ");
                string maSV = Console.ReadLine() ?? string.Empty;

                var ketQua = danhSachTheMuon
                    .Where(tm => tm.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                HienThiKetQua(ketQua, "KET QUA TIM KIEM:");
            }

            public void HienThiDenHanTra()
            {
                var today = DateTime.Now.Date;
                var ketQua = danhSachTheMuon
                    .Where(tm => tm.HanTra.Date <= today)
                    .OrderBy(tm => tm.HanTra)
                    .ToList();

                HienThiKetQua(ketQua, "DANH SACH SINH VIEN DEN HAN TRA SACH:");
            }

            private void HienThiKetQua(List<TheMuon> ketQua, string tieuDe)
            {
                if (!ketQua.Any())
                {
                    Console.WriteLine("Khong tim thay ket qua nao!");
                    return;
                }

                Console.WriteLine($"\n{tieuDe}");
                foreach (var theMuon in ketQua)
                {
                    theMuon.HienThiThongTin();
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void B8()
        {
            var quanLy = new QuanLyThuVien();
            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY THU VIEN");
                Console.WriteLine("1. Nhap danh sach muon sach");
                Console.WriteLine("2. Tim kiem theo ma sinh vien");
                Console.WriteLine("3. Hien thi danh sach den han tra");
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
                        quanLy.TimKiemTheoMaSV();
                        break;
                    case 3:
                        quanLy.HienThiDenHanTra();
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
