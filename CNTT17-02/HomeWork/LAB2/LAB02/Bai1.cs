using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai1
    {
        public static void B1()
        {
            int[] a; int n;

            (a, n) = NhapMang();
            TinhTong(a, n);
        }

        public static (int[] a, int n) NhapMang()
        {
            Console.WriteLine("Nhap phan tu mang: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"a[{i}] = ");
                a[i] = int.Parse(Console.ReadLine() ?? "0");
            }
            return (a, n);
        }

        public static void TinhTong(int[] a, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (a[i] % 2 == 0)
                {
                    sum += a[i];
                }
            }
            Console.WriteLine($"Tong cac phan tu trong mang la: {sum}");
        }
    }
}
