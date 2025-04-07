using System;
namespace LAB1._2;
class Program
{
    //public static void NhapMang(int[] a, int n)
    //{
    //    for (int i = 0; i < n; i++)
    //    {
    //        Console.Write($"a[{i}]: ");
    //        a[i] = int.Parse(Console.ReadLine());
    //    }
    //}
    //public static int TinhTong(int[] a, int n)
    //{
    //    int tong = 0;
    //    for (int i = 0; i < n; i++)
    //    {
    //        tong += a[i];
    //    }
    //    return tong;
    //}
    //static void Main(string[] args)
    //{
    //    Console.OutputEncoding = System.Text.Encoding.UTF8;
    //    //Khai báo biến n 
    //    int n;
    //    //Nhập giá trị cho biến n 
    //    Console.Write("Nhập n: ");
    //    n = int.Parse(Console.ReadLine());
    //    //Khai báo và khởi tạo mảng số nguyên có n phần tử 
    //    int[] a = new int[n];
    //    //Gọi hàm nhập mảng 
    //    NhapMang(a, n);
    //    //Gọi hàm Tính Tổng các phần tử trong mảng và hiển thị kết quả ra màn hình 
    //    Console.WriteLine($"Tổng = {TinhTong(a, n)}");
    //}

    static void Main()
    {
        bool tiepTuc = true;

        while (tiepTuc)
        {
            Console.WriteLine("Chon bai (1 - 6): ");
            int chon;
            while (!int.TryParse(Console.ReadLine(), out chon) || chon < 1 || chon > 6)
            {
                Console.WriteLine("Vui long chon mot so tu 1 den 6.");
            }

            switch (chon)
            {
                case 1: Bai1(); break;
                case 2: Bai2(); break;
                case 3: Bai3(); break;
                case 4: Bai4(); break;
                case 5: Bai5(); break;
                case 6: Bai6(); break;
                default: Console.WriteLine("Khong co bai nay"); break;
            }

            // Ask if user wants to continue yes or no ?:>
            Console.WriteLine("Ban co muon tiep tuc? (y/n): ");
            string tiepTucInput = Console.ReadLine().ToLower();
            tiepTuc = tiepTucInput == "y" || tiepTucInput == "yes";
        }
    }

    // Bài 1: Tính tổng các số chẵn trong mảng
    static void Bai1()
    {
        Console.WriteLine("Nhap so phan tu mang: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Vui long nhap mot so nguyen duong hop le.");
        }

        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}]: ");
            while (!int.TryParse(Console.ReadLine(), out a[i]))
            {
                Console.WriteLine("Vui long nhap mot so nguyen hop le.");
            }
        }

        int tong = 0;
        for (int i = 0; i < n; i++)
        {
            if (a[i] % 2 == 0) tong += a[i];
        }

        Console.WriteLine($"Tong cac so chan: {tong}");
    }

    // Bài 2: Kiểm tra và hiển thị số nguyên tố trong mảng
    static void Bai2()
    {
        Console.WriteLine("Nhap so phan tu mang: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Vui long nhap mot so nguyen duong hop le.");
        }

        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}]: ");
            while (!int.TryParse(Console.ReadLine(), out a[i]))
            {
                Console.WriteLine("Vui long nhap mot so nguyen hop le.");
            }
        }

        Console.WriteLine("Cac so nguyen to trong mang:");
        for (int i = 0; i < n; i++)
        {
            if (KiemTraNguyenTo(a[i]))
            {
                Console.WriteLine($"a[{i}] = {a[i]}");
            }
        }
    }

    static bool KiemTraNguyenTo(int num)
    {
        if (num < 2) return false;
        for (int i = 2; i <= Math.Sqrt(num); i++)
        {
            if (num % i == 0) return false;
        }
        return true;
    }

    // Bài 3: Đếm số âm và số dương trong mảng
    static void Bai3()
    {
        Console.WriteLine("Nhap so phan tu mang: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Vui long nhap mot so nguyen duong hop le.");
        }

        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}]: ");
            while (!int.TryParse(Console.ReadLine(), out a[i]))
            {
                Console.WriteLine("Vui long nhap mot so nguyen hop le.");
            }
        }

        int soAm = 0, soDuong = 0;
        foreach (int num in a)
        {
            if (num < 0) soAm++;
            else if (num > 0) soDuong++;
        }

        Console.WriteLine($"So am: {soAm}, So duong: {soDuong}");
    }

    // Bài 4: Tìm số lớn thứ hai trong mảng
    static void Bai4()
    {
        Console.WriteLine("Nhap so phan tu mang: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 1)
        {
            Console.WriteLine("Vui long nhap so phan tu mang lon hon 1.");
        }

        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}]: ");
            while (!int.TryParse(Console.ReadLine(), out a[i]))
            {
                Console.WriteLine("Vui long nhap mot so nguyen hop le.");
            }
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
            Console.WriteLine("Khong co so lon thu hai");
        else
            Console.WriteLine($"So lon thu hai: {max2}");
    }

    // Bài 5: Hoán vị 2 số nguyên
    static void Bai5()
    {
        Console.WriteLine("Nhap so a: ");
        int a = int.Parse(Console.ReadLine());
        Console.WriteLine("Nhap so b: ");
        int b = int.Parse(Console.ReadLine());

        // Thực hiện hoán vị
        int temp = a;
        a = b;
        b = temp;

        Console.WriteLine($"Sau hoan vi: a = {a}, b = {b}");
    }

    // Bài 6: Sắp xếp mảng số thực tăng dần
    static void Bai6()
    {
        Console.WriteLine("Nhap so phan tu mang: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Vui long nhap mot so nguyen duong hop le.");
        }

        double[] a = new double[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}]: ");
            while (!double.TryParse(Console.ReadLine(), out a[i]))
            {
                Console.WriteLine("Vui long nhap mot so thuc hop le.");
            }
        }

        Array.Sort(a);

        Console.WriteLine("Mang sau khi sap xep:");
        foreach (double item in a)
        {
            Console.Write($"{item} ");
        }

        // Giữ màn hình mở để user có thể xem kết quả
        Console.WriteLine("\nNhan bat ky phim nao de thoat...");
        Console.ReadKey();
    }
}
