using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp Nguoi dùng chung (nếu đã có, hãy đảm bảo không trùng lặp nếu cần)
    // Nếu gặp xung đột, bạn có thể đổi tên lớp ở bài này (ví dụ: NguoiB18)
    internal class NguoiB18
    {
        public string HoTen { get; set; } = string.Empty;
        public bool GioiTinh { get; set; }  // true: Nam, false: Nu
        public int Tuoi { get; set; }

        public NguoiB18() { }
        public NguoiB18(string hoTen, bool gioiTinh, int tuoi)
        {
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            Tuoi = tuoi;
        }

        public virtual void Nhap()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap gioi tinh (Nam=true, Nu=false): ");
            GioiTinh = bool.Parse(Console.ReadLine() ?? "true");
            Console.Write("Nhap tuoi: ");
            Tuoi = int.Parse(Console.ReadLine() ?? "0");
        }

        public virtual void In()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Gioi tinh: {(GioiTinh ? "Nam" : "Nu")}, Tuoi: {Tuoi}");
        }
    }

    // Lớp CoQuan kế thừa từ NguoiB18
    internal class CoQuan : NguoiB18
    {
        public string DonVi { get; set; } = string.Empty;
        public double HeSoLuong { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap don vi cong tac: ");
            DonVi = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap he so luong: ");
            HeSoLuong = double.Parse(Console.ReadLine() ?? "1");
        }

        public override void In()
        {
            base.In();
            Console.WriteLine($"Don vi: {DonVi}, He so luong: {HeSoLuong}");
        }

        public double TinhLuong()
        {
            const double luongCoBan = 1050000;
            return HeSoLuong * luongCoBan;
        }
    }

    internal class QuanLyCoQuan
    {
        private List<CoQuan> dsCoQuan = new List<CoQuan>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong can bo: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap thong tin can bo thu {i + 1}:");
                CoQuan cq = new CoQuan();
                cq.Nhap();
                dsCoQuan.Add(cq);
            }
        }

        public void HienThiPhongTaiChinh()
        {
            Console.WriteLine("Danh sach cac can bo thuoc Phong tai chinh:");
            bool found = false;
            foreach (var cq in dsCoQuan)
            {
                if (cq.DonVi.Equals("Phong tai chinh", StringComparison.OrdinalIgnoreCase))
                {
                    cq.In();
                    Console.WriteLine($"Luong thuc linh: {cq.TinhLuong():F0}");
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong co can bo nao thuoc Phong tai chinh.");
        }

        public void TimKiemTheoHoTen()
        {
            Console.Write("Nhap ho ten can tim: ");
            string name = Console.ReadLine() ?? string.Empty;
            bool found = false;
            foreach (var cq in dsCoQuan)
            {
                if (cq.HoTen.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    cq.In();
                    Console.WriteLine($"Luong: {cq.TinhLuong():F0}");
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay can bo voi ho ten: " + name);
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY CO QUAN");
                Console.WriteLine("1. Nhap danh sach co quan");
                Console.WriteLine("2. Hien thi thong tin cac can bo thuoc Phong tai chinh");
                Console.WriteLine("3. Tim kiem theo ho ten");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        HienThiPhongTaiChinh();
                        break;
                    case 3:
                        TimKiemTheoHoTen();
                        break;
                    case 4:
                        Console.WriteLine("Thoat chuong trinh Bai 18.");
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

    internal static class Bai18
    {
        public static void B18()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 18: Nguoi & Co Quan ------");
            QuanLyCoQuan ql = new QuanLyCoQuan();
            ql.Menu();
        }
    }
}
