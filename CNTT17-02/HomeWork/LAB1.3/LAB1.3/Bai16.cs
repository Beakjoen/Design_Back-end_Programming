using System;

namespace QLApp
{
    internal class Diem
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Diem() { X = 0; Y = 0; }
        public Diem(double x, double y) { X = x; Y = y; }

        public void Nhap()
        {
            Console.Write("Nhap hoanh do: ");
            X = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap tung do: ");
            Y = double.Parse(Console.ReadLine() ?? "0");
        }

        public void HienThi()
        {
            Console.WriteLine($"({X}, {Y})");
        }

        public double KhoangCach(Diem d)
        {
            return Math.Sqrt((X - d.X) * (X - d.X) + (Y - d.Y) * (Y - d.Y));
        }
    }

    internal class TamGiacDiem
    {
        public Diem D1 { get; set; } = new Diem();
        public Diem D2 { get; set; } = new Diem();
        public Diem D3 { get; set; } = new Diem();

        public void Nhap()
        {
            Console.WriteLine("Nhap diem 1:");
            D1.Nhap();
            Console.WriteLine("Nhap diem 2:");
            D2.Nhap();
            Console.WriteLine("Nhap diem 3:");
            D3.Nhap();
        }

        public void HienThi()
        {
            Console.Write("Tam giac voi cac diem: ");
            D1.HienThi();
            D2.HienThi();
            D3.HienThi();
        }

        public double TinhChuVi()
        {
            double a = D1.KhoangCach(D2);
            double b = D2.KhoangCach(D3);
            double c = D3.KhoangCach(D1);
            return a + b + c;
        }

        public double TinhDienTich()
        {
            double a = D1.KhoangCach(D2);
            double b = D2.KhoangCach(D3);
            double c = D3.KhoangCach(D1);
            double p = (a + b + c) / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }

    internal static class Bai16
    {
        public static void B16()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 16: Diem & Tam Giac ------");
            Console.Write("Nhap so luong tam giac: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            TamGiacDiem[] dsTamGiac = new TamGiacDiem[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap tam giac thu {i + 1}:");
                dsTamGiac[i] = new TamGiacDiem();
                dsTamGiac[i].Nhap();
            }
            double tongChuVi = 0;
            double tongDienTich = 0;
            foreach (var tg in dsTamGiac)
            {
                tongChuVi += tg.TinhChuVi();
                tongDienTich += tg.TinhDienTich();
            }
            Console.WriteLine("Tong chu vi cua cac tam giac: " + tongChuVi);
            Console.WriteLine("Tong dien tich cua cac tam giac: " + tongDienTich);
            Console.WriteLine("Nhan phim bat ky de thoat Bai 16...");
            Console.ReadKey();
        }
    }
}
