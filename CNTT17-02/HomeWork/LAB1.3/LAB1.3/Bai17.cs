using System;

namespace QLApp
{
    // Đổi tên lớp Diem thành DiemB17 trong bài 17 để tránh xung đột với định nghĩa Diem ở bài khác.
    internal class DiemB17
    {
        public double X { get; set; }
        public double Y { get; set; }

        public DiemB17() { X = 0; Y = 0; }
        public DiemB17(double x, double y) { X = x; Y = y; }

        public void Nhap()
        {
            Console.Write("Nhap hoanh do: ");
            X = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap tung do: ");
            Y = double.Parse(Console.ReadLine() ?? "0");
        }

        public void HienThi()
        {
            Console.Write($"({X}, {Y})");
        }

        public double KhoangCach(DiemB17 d)
        {
            return Math.Sqrt((X - d.X) * (X - d.X) + (Y - d.Y) * (Y - d.Y));
        }
    }

    // Lớp HinhTron sử dụng lớp DiemB17 ở đây.
    internal class HinhTron
    {
        public DiemB17 Tam { get; set; }
        public int BanKinh { get; set; }

        public HinhTron()
        {
            Tam = new DiemB17();
            BanKinh = 0;
        }

        public HinhTron(DiemB17 d, int bk)
        {
            Tam = d;
            BanKinh = bk;
        }

        public void Nhap()
        {
            Console.WriteLine("Nhap thong tin hinh tron:");
            Tam = new DiemB17();
            Tam.Nhap();
            Console.Write("Nhap ban kinh: ");
            BanKinh = int.Parse(Console.ReadLine() ?? "0");
        }

        public void HienThi()
        {
            Console.Write("Hinh tron: Tam = ");
            Tam.HienThi();
            Console.Write($", Ban kinh = {BanKinh}");
        }

        public double ChuVi() => 2 * Math.PI * BanKinh;
        public double DienTich() => Math.PI * BanKinh * BanKinh;

        // Kiểm tra xem hai hình tròn giao nhau không.
        public bool GiaoNhau(HinhTron other)
        {
            double d = Tam.KhoangCach(other.Tam);
            return d <= (this.BanKinh + other.BanKinh) && d >= Math.Abs(this.BanKinh - other.BanKinh);
        }
    }

    // Lớp QuanLyHinhTron với các chức năng nhập danh sách và hiển thị hình tròn giao nhau nhiều nhất.
    internal class QuanLyHinhTron
    {
        private System.Collections.Generic.List<HinhTron> dsHinhTron = new System.Collections.Generic.List<HinhTron>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong hinh tron: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nNhap hinh tron thu {i + 1}:");
                HinhTron ht = new HinhTron();
                ht.Nhap();
                dsHinhTron.Add(ht);
            }
        }

        public void HienThiHinhTronGiaoNhieuNhat()
        {
            if (dsHinhTron.Count == 0)
            {
                Console.WriteLine("Danh sach trong.");
                return;
            }

            int maxCount = -1;
            System.Collections.Generic.List<HinhTron> result = new System.Collections.Generic.List<HinhTron>();

            // Đếm số hình giao nhau cho mỗi hình
            for (int i = 0; i < dsHinhTron.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < dsHinhTron.Count; j++)
                {
                    if (i == j) continue;
                    if (dsHinhTron[i].GiaoNhau(dsHinhTron[j]))
                        count++;
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    result.Clear();
                    result.Add(dsHinhTron[i]);
                }
                else if (count == maxCount)
                {
                    result.Add(dsHinhTron[i]);
                }
            }

            Console.WriteLine($"\nHinh tron giao voi {maxCount} hinh khac (nhung hinh sau co so giao nhau lon nhat):");
            foreach (var ht in result)
            {
                ht.HienThi();
                Console.WriteLine($", Chu vi = {ht.ChuVi():F2}, Dien tich = {ht.DienTich():F2}");
                Console.WriteLine("-----");
            }
        }

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH QUAN LY HINH TRON");
                Console.WriteLine("1. Nhap danh sach hinh tron");
                Console.WriteLine("2. Hien thi hinh tron giao nhieu nhat");
                Console.WriteLine("3. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        NhapDanhSach();
                        break;
                    case 2:
                        HienThiHinhTronGiaoNhieuNhat();
                        break;
                    case 3:
                        Console.WriteLine("Thoat chuong trinh Bai 17.");
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

    internal static class Bai17
    {
        public static void B17()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 17: Diem & Hinh Tron ------");
            QuanLyHinhTron ql = new QuanLyHinhTron();
            ql.Menu();
        }
    }
}
