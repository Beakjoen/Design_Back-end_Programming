using System;
using System.Collections.Generic;
using System.Linq;

namespace QLApp
{
    // Lớp DaGiac
    internal class DaGiac
    {
        public int SoCanh { get; set; }
        public int[] CacCanh { get; set; }

        public virtual void Nhap()
        {
            Console.Write("Nhap so canh cua da giac: ");
            SoCanh = int.Parse(Console.ReadLine() ?? "0");
            CacCanh = new int[SoCanh];
            for (int i = 0; i < SoCanh; i++)
            {
                Console.Write($"Nhap do dai canh {i + 1}: ");
                CacCanh[i] = int.Parse(Console.ReadLine() ?? "0");
            }
        }

        public virtual void HienThi()
        {
            Console.WriteLine("Cac canh cua da giac: " + string.Join(", ", CacCanh));
        }

        public virtual int TinhChuVi()
        {
            return CacCanh.Sum();
        }
    }

    // Lớp TamGiac kế thừa từ DaGiac (với số canh = 3)
    internal class TamGiac : DaGiac
    {
        public override void Nhap()
        {
            SoCanh = 3;
            CacCanh = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Nhap do dai canh {i + 1}: ");
                CacCanh[i] = int.Parse(Console.ReadLine() ?? "0");
            }
        }

        public override int TinhChuVi()
        {
            return CacCanh.Sum();
        }

        public double TinhDienTich()
        {
            double p = TinhChuVi() / 2.0;
            return Math.Sqrt(p * (p - CacCanh[0]) * (p - CacCanh[1]) * (p - CacCanh[2]));
        }

        // Kiểm tra định lý Pitago (sắp xếp các cạnh, kiểm tra a^2 + b^2 == c^2)
        public bool LaTamGiacPitago()
        {
            int[] temp = (int[])CacCanh.Clone();
            Array.Sort(temp);
            return temp[0] * temp[0] + temp[1] * temp[1] == temp[2] * temp[2];
        }
    }

    internal static class Bai15
    {
        public static void B15()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 15: Da Giác & Tam Giác ------");
            Console.Write("Nhap so luong tam giac: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            List<TamGiac> dsTamGiac = new List<TamGiac>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhap tam giac thu {i + 1}:");
                TamGiac tg = new TamGiac();
                tg.Nhap();
                dsTamGiac.Add(tg);
            }
            Console.WriteLine("\nDanh sach tam giac thoa dinh ly Pitago:");
            foreach (var tg in dsTamGiac)
            {
                if (tg.LaTamGiacPitago())
                {
                    tg.HienThi();
                    Console.WriteLine("Chu vi: " + tg.TinhChuVi());
                    Console.WriteLine("Dien tich: " + tg.TinhDienTich());
                    Console.WriteLine("-----");
                }
            }
            Console.WriteLine("Nhan phim bat ky de thoat Bai 15...");
            Console.ReadKey();
        }
    }
}
