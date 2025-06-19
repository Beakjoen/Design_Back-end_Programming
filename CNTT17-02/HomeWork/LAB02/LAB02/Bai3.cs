using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai3
    {
        public static void B3()
        {
            int[] a; int n;
            (a, n) = NhapMang();
            KiemTra(a, n);
        }
        public static (int[]a ,int n) NhapMang()
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

        public static void KiemTra(int[] a, int n)
        {
            int am = 0;
            int duong = 0;
            for(int i = 0; i < n; i++)
            {
                if (a[i] < 0)
                {
                    am++;
                }
                else if (a[i] > 0)
                {
                    duong++;
                }
            }
            
            Console.WriteLine($"Số dương trong mảng là {duong}");
            Console.WriteLine($"Số âm trong mảng là {am}");
        }
    }
}
