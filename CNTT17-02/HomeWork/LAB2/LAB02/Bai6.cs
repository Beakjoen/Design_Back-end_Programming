using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai6
    {
        public static void B6()
        {
            int[] a; int n;
            TangDan();
        }
        public static (int []a,int n) NhapMang()
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

        public static void TangDan()
        {
            int[] a; int n;
            (a, n) = NhapMang();
            Console.WriteLine("Mang da nhap la: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
            Array.Sort(a);
            Console.WriteLine("Mang sau khi sap xep tang dan la: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(a[i] + " ");
            }
        }
    }
}
