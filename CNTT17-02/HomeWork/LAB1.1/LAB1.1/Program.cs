using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Chon bai (1 - 10): ");
        int chon = int.Parse(Console.ReadLine());

        switch (chon)
        {
            case 1: Bai1(); break;
            case 2: Bai2(); break;
            case 3: Bai3(); break;
            case 4: Bai4(); break;
            case 5: Bai5(); break;
            case 6: Bai6(); break;
            case 7: Bai7(); break;
            case 8: Bai8(); break;
            case 9: Bai9(); break;
            case 10: Bai10(); break;
            default: Console.WriteLine("Khong co bai nay"); break;
        }
    }

    // Bài 1: Nhập tên và tuổi
    static void Bai1()
    {
        Console.Write("Nhap ten: ");
        string ten = Console.ReadLine();
        Console.Write("Nhap tuoi: ");
        int tuoi = int.Parse(Console.ReadLine());
        Console.WriteLine($"Xin chao {ten}, ban {tuoi} tuoi!");
    }

    // Bài 2: Tính diện tích hình chữ nhật
    static void Bai2()
    {
        float dai, rong;
        Console.Write("Nhap chieu dai: ");
        try
        {
            dai = float.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.Write("Nhap chieu rong: ");
        try
        {
            rong = float.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.WriteLine($"Dien tich: {dai * rong}");
    }

    // Bài 3: Chuyển độ C sang độ F
    static void Bai3()
    {
        float c;
        Console.WriteLine("Nhap nhiet do (C): ");
        try
        {
            c = float.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.WriteLine($"Nhiet do F: {(c * 9 / 5) + 32}");
    }

    // Bài 4: Kiểm tra số chẵn lẻ
    static void Bai4()
    {
        int x;
        Console.WriteLine("Nhap so: ");
        try
        {
            x = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.WriteLine(x % 2 == 0 ? "Chan" : "Le");
    }

    // Bài 5: Tính tổng và tích
    static void Bai5()
    {
        int a, b;
        Console.WriteLine("Nhap so a: ");
        try
        {
            a = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.WriteLine("Nhap so b: ");
        try
        {
            b = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Hay nhap so!");
            return;
        }
        Console.WriteLine($"Tong: {a + b}, Tich: {a * b}");
    }

    // Bài 6: Kiểm tra âm, dương hoặc bằng 0
    static void Bai6()
    {
        Console.WriteLine("Nhap so: ");
        int n = int.Parse(Console.ReadLine());
        if (n > 0) Console.WriteLine("Duong");
        else if (n < 0) Console.WriteLine("Am");
        else Console.WriteLine("Bang 0");
    }

    // Bài 7: Kiểm tra năm nhuận
    static void Bai7()
    {
        Console.WriteLine("Nhap nam: ");
        int y = int.Parse(Console.ReadLine());
        if ((y % 4 == 0 && y % 100 != 0) || (y % 400 == 0))
            Console.WriteLine("Nam nhuan");
        else
            Console.WriteLine("Khong phai nam nhuan");
    }

    // Bài 8: In bảng cửu chương từ 1 đến 10
    static void Bai8()
    {
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                Console.WriteLine($"{i} x {j} = {i * j}
            }
        }
    }

    // Bài 9: Tính giai thừa
    static void Bai9()
    {
        Console.WriteLine("Nhap so nguyen duong n: ");
        int m = int.Parse(Console.ReadLine());
        long gt = 1;
        for (int i = 1; i <= m; i++)
        {
            gt *= i;
        }
        Console.WriteLine(gt);
    }

    // Bài 10: Kiểm tra số nguyên tố
    static void Bai10()
    {
        Console.WriteLine("Nhap so: ");
        int p = int.Parse(Console.ReadLine());
        bool isPrime = true;
        for (int i = 2; i <= Math.Sqrt(p); i++)
        {
            if (p % i == 0)
            {
                isPrime = false;
                break;
            }
        }
        Console.WriteLine(isPrime ? "Nguyen to" : "Khong nguyen to");
    }
}