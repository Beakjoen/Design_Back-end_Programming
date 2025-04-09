using System;
using System.Collections.Generic;

namespace QLApp
{
    // Lớp cơ sở PTGT
    internal class PTGT
    {
        public string HangSanXuat { get; set; } = string.Empty;
        public int NamSanXuat { get; set; }
        public double GiaBan { get; set; }
        public string Mau { get; set; } = string.Empty;

        public virtual void Nhap()
        {
            Console.Write("Nhap hang san xuat: ");
            HangSanXuat = Console.ReadLine() ?? string.Empty;
            Console.Write("Nhap nam san xuat: ");
            NamSanXuat = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap gia ban: ");
            GiaBan = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap mau: ");
            Mau = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThi()
        {
            Console.WriteLine($"Hang: {HangSanXuat}, Nam: {NamSanXuat}, Gia ban: {GiaBan}, Mau: {Mau}");
        }
    }

    // Lớp OTo
    internal class OTo : PTGT
    {
        public int SoChoNgoi { get; set; }
        public string KieuDongCo { get; set; } = string.Empty;

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap so cho ngoi: ");
            SoChoNgoi = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap kieu dong co: ");
            KieuDongCo = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThi()
        {
            Console.WriteLine("Loai: OTo");
            base.HienThi();
            Console.WriteLine($"So cho ngoi: {SoChoNgoi}, Kieu dong co: {KieuDongCo}");
        }
    }

    // Lớp XeMay
    internal class XeMay : PTGT
    {
        public double CongSuat { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap cong suat: ");
            CongSuat = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            Console.WriteLine("Loai: XeMay");
            base.HienThi();
            Console.WriteLine($"Cong suat: {CongSuat}");
        }
    }

    // Lớp XeTai
    internal class XeTai : PTGT
    {
        public double TrongTai { get; set; }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhap trong tai: ");
            TrongTai = double.Parse(Console.ReadLine() ?? "0");
        }

        public override void HienThi()
        {
            Console.WriteLine("Loai: XeTai");
            base.HienThi();
            Console.WriteLine($"Trong tai: {TrongTai}");
        }
    }

    // Lớp QLPTGT
    internal class QLPTGT
    {
        private List<PTGT> dsPTGT = new List<PTGT>();

        public void NhapPhuongTien()
        {
            Console.WriteLine("Chon loai phuong tien:");
            Console.WriteLine("1. OTo");
            Console.WriteLine("2. XeMay");
            Console.WriteLine("3. XeTai");
            Console.Write("Nhap lua chon: ");
            int chon = int.Parse(Console.ReadLine() ?? "0");
            PTGT pt = chon switch
            {
                1 => new OTo(),
                2 => new XeMay(),
                3 => new XeTai(),
                _ => null
            };
            if (pt == null)
            {
                Console.WriteLine("Lua chon khong hop le.");
                return;
            }
            pt.Nhap();
            dsPTGT.Add(pt);
        }

        public void TimKiemTheoMauHoacNam()
        {
            Console.Write("Nhap mau hoac nam san xuat can tim: ");
            string key = Console.ReadLine() ?? "";
            bool found = false;
            foreach (var pt in dsPTGT)
            {
                if (pt.Mau.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0 || pt.NamSanXuat.ToString() == key)
                {
                    pt.HienThi();
                    Console.WriteLine("-----");
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay phuong tien voi key: " + key);
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY PTGT");
                Console.WriteLine("1. Nhap dang ky phuong tien");
                Console.WriteLine("2. Tim phuong tien theo mau hoac nam san xuat");
                Console.WriteLine("3. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapPhuongTien();
                        break;
                    case 2:
                        TimKiemTheoMauHoacNam();
                        break;
                    case 3:
                        Console.WriteLine("Thoat chuong trinh Bai 13.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
                if (luaChon != 3)
                {
                    Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            } while (luaChon != 3);
        }
    }

    internal static class Bai13
    {
        public static void B13()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 13: Quan ly phuong tien giao thong ------");
            QLPTGT ql = new QLPTGT();
            ql.Menu();
        }
    }
}
