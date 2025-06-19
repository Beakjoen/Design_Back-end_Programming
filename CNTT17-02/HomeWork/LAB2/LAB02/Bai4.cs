using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai4
    {
        public static void B4()
        {
            int[] a; int n;
            (a, n) = NhapMang();
            TimSoLonNhat(a, n);
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
        public static void TimSoLonNhat(int[] a, int n)
        {
            if (n < 2)
            {
                Console.WriteLine("Mảng phải có ít nhất 2 phần tử.");
                return;
            }
            int max1 = int.MinValue;
            int max2 = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (a[i] > max1)
                {
                    max2 = max1;
                    max1 = a[i];
                }
                else if (a[i] > max2 && a[i] != max1)
                {
                    max2 = a[i];
                }
            }
            if (max2 == int.MinValue)
            {
                Console.WriteLine("Không có số lớn thứ hai trong mảng.");
            }
            else
            {
                Console.WriteLine($"Số lớn thứ hai trong mảng là: {max2}");
            }
        }
    }
}
