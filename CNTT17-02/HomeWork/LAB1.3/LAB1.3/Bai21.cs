using System;
using System.Collections.Generic;
using System.Linq;

namespace QLApp
{
    // Lớp học sinh chung
    internal abstract class HocSinh
    {
        public string HoTen { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty; // "Nam" or "Nu"
        public double DiemToan { get; set; }
        public double DiemLy { get; set; }
        public double DiemHoa { get; set; }

        public abstract void Nhap();

        public virtual void HienThi()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Gioi tinh: {GioiTinh}, Toan: {DiemToan}, Ly: {DiemLy}, Hoa: {DiemHoa}");
        }
    }

    // Học sinh Nam: có điểm môn Kỹ thuật
    internal class HocSinhNam : HocSinh
    {
        public double DiemKyThuat { get; set; }

        public override void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            GioiTinh = "Nam";
            Console.Write("Nhap diem Toan: ");
            DiemToan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Ly: ");
            DiemLy = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Hoa: ");
            DiemHoa = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Ky thuat: ");
            DiemKyThuat = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            base.HienThi();
            Console.WriteLine($"Diem Ky thuat: {DiemKyThuat}");
        }
    }

    // Học sinh Nữ: có điểm môn Nữ công
    internal class HocSinhNu : HocSinh
    {
        public double DiemNuCong { get; set; }

        public override void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            GioiTinh = "Nu";
            Console.Write("Nhap diem Toan: ");
            DiemToan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Ly: ");
            DiemLy = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Hoa: ");
            DiemHoa = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap diem Nu cong: ");
            DiemNuCong = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            base.HienThi();
            Console.WriteLine($"Diem Nu cong: {DiemNuCong}");
        }
    }

    // Lớp QuanLyHocSinh – nhập danh sách, hiển thị thông tin học sinh nam có điểm kỹ thuật >= 8,
    // và hiển thị danh sách với học sinh nam đứng trước.
    internal class QuanLyHocSinh21
    {
        private List<HocSinh> dsHS = new List<HocSinh>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong hoc sinh: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nNhap thong tin hoc sinh thu {i + 1}:");
                Console.Write("Chon loai hoc sinh (1: Nam, 2: Nu): ");
                int loai = int.Parse(Console.ReadLine() ?? "0");
                HocSinh hs = loai switch
                {
                    1 => new HocSinhNam(),
                    2 => new HocSinhNu(),
                    _ => null
                };
                if (hs == null)
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    i--;
                    continue;
                }
                hs.Nhap();
                dsHS.Add(hs);
            }
        }

        public void HienThiNamKyThuat()
        {
            Console.WriteLine("Danh sach hoc sinh nam co diem ky thuat >= 8:");
            foreach (var hs in dsHS)
            {
                if (hs is HocSinhNam hsNam && hsNam.DiemKyThuat >= 8)
                {
                    hs.HienThi();
                    Console.WriteLine("-----");
                }
            }
        }

        public void HienThiNamTruocNu()
        {
            // Sắp xếp: học sinh nam trước, sau đó nữ. Giữ nguyên thứ tự nhập trong cùng nhóm.
            var dsSapXep = dsHS.OrderBy(h => h.GioiTinh != "Nam").ToList();
            Console.WriteLine("Danh sach hoc sinh (Nam truoc, Nu sau):");
            foreach (var hs in dsSapXep)
            {
                hs.HienThi();
                Console.WriteLine("-----");
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HOC SINH (Bai 21)");
                Console.WriteLine("1. Nhap danh sach hoc sinh");
                Console.WriteLine("2. Hien thi hoc sinh nam co diem Ky thuat >= 8");
                Console.WriteLine("3. Hien thi danh sach hoc sinh (Nam truoc, Nu sau)");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        HienThiNamKyThuat();
                        break;
                    case 3:
                        HienThiNamTruocNu();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 21.");
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

    internal static class Bai21
    {
        public static void B21()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 21: Hoc sinh trung hoc ------");
            QuanLyHocSinh21 ql = new QuanLyHocSinh21();
            ql.Menu();
        }
    }
}
