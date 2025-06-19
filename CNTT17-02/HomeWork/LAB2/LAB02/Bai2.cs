using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai2
    {
        public static void B2()
        {
            int[] a; int n;
            (a, n) = NhapMang();
            for (int i = 0; i < n; i++)
            {
                if (kiemTraNguyenTo(a[i]))
                {
                    Console.WriteLine($"a[{i}] có giá trị {a[i]} là số nguyên tố");
                }
                else
                {
                    Console.WriteLine($"a[{i}] có giá trị {a[i]} không phải là số nguyên tố");
                }
            }
        }

        public static (int[] a, int n) NhapMang()
        {
            Console.WriteLine("Nhap phan tu mang: ");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"a[{i}] = ");
                a[i] = int.Parse(Console.ReadLine() ?? "0");
            }
            return (a, n);
        }

        public static bool kiemTraNguyenTo(int number)
        {
            if (number < 2) return false;
            for (int j = 2; j <= Math.Sqrt(number); j++)
            {
                if (number % j == 0) return false;
            }
            return true;
        }
    }
}
