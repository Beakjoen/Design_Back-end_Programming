using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp SinhVien lưu thông tin riêng của sinh viên
    internal class SinhVien
    {
        public string HoTen { get; set; } = string.Empty;
        public int NamSinh { get; set; }
        public string Lop { get; set; } = string.Empty;
        public string MaSV { get; set; } = string.Empty;

        public void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap lop: ");
            Lop = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ma so sinh vien: ");
            MaSV = Console.ReadLine() ?? string.Empty;
        }

        public void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Nam sinh: {NamSinh}, Lop: {Lop}, MaSV: {MaSV}");
        }
    }

    // Lớp TheMuon lưu thông tin các thẻ mượn
    internal class TheMuon
    {
        public string SoPhieuMuon { get; set; } = string.Empty;
        public string NgayMuon { get; set; } = string.Empty;
        public string HanTra { get; set; } = string.Empty;
        public string SoHieuSach { get; set; } = string.Empty;
        public SinhVien SV { get; set; } = new SinhVien();

        public void Nhap()
        {
            Console.Write("Nhap so phieu muon: ");
            SoPhieuMuon = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap ngay muon (dd/mm/yyyy): ");
            NgayMuon = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap han tra (dd/mm/yyyy): ");
            HanTra = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap so hieu sach: ");
            SoHieuSach = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Nhap thong tin sinh vien:");
            SV.Nhap();
        }

        public void HienThi()
        {
            Console.WriteLine("So phieu muon: " + SoPhieuMuon);
            Console.WriteLine("Ngay muon: " + NgayMuon);
            Console.WriteLine("Han tra: " + HanTra);
            Console.WriteLine("So hieu sach: " + SoHieuSach);
            Console.WriteLine("Thong tin sinh vien:");
            SV.HienThi();
        }
    }

    // Lớp QuanLyTheMuon quản lý danh sách thẻ mượn
    internal class QuanLyTheMuon
    {
        private List<TheMuon> dsTheMuon = new List<TheMuon>();

        public void NhapTheMuon()
        {
            Console.Write("Nhap so luong the muon: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin the muon thu {i + 1}:");
                TheMuon tm = new TheMuon();
                tm.Nhap();
                dsTheMuon.Add(tm);
            }
        }

        public void TimKiemTheoMaSV()
        {
            Console.Write("Nhap ma so sinh vien can tim: ");
            string maSV = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var tm in dsTheMuon)
            {
                if (tm.SV.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase))
                {
                    tm.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay the muon voi ma SV: " + maSV);
        }

        public void HienThiTheMuonQuaHan()
        {
            Console.Write("Nhap ngay hien tai (dd/mm/yyyy): ");
            string ngayHienTai = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Danh sach the muon qua han:");
            bool found = false;
            foreach (var tm in dsTheMuon)
            {
                // So sánh theo chuỗi: nếu han tra < ngay hien tai thì coi là qua han.
                if (string.Compare(tm.HanTra, ngayHienTai) < 0)
                {
                    tm.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong co the muon nao qua han.");
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY THE MUON");
                Console.WriteLine("1. Nhap thong tin the muon");
                Console.WriteLine("2. Tim kiem the muon theo ma so sinh vien");
                Console.WriteLine("3. Hien thi the muon qua han");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapTheMuon();
                        break;
                    case 2:
                        TimKiemTheoMaSV();
                        break;
                    case 3:
                        HienThiTheMuonQuaHan();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 8.");
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

    internal static class Bai8
    {
        public static void B8()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 8: Quan ly the muon ------");
            QuanLyTheMuon ql = new QuanLyTheMuon();
            ql.Menu();
        }
    }
}
