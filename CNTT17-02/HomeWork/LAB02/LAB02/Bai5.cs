using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02
{
    internal class Bai5
    {
        public static void B5()
        {
            int a, b;
            Console.WriteLine("Nhap a: ");
            a = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Nhap b: ");
            b = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine($"Truoc khi hoan vi: a = {a} ,b = {b}");
            HoanVi( ref a, ref b);
            Console.WriteLine($"Sau khi hoan vi: a = {a} ,b = {b}");
        }

        public static void HoanVi(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
