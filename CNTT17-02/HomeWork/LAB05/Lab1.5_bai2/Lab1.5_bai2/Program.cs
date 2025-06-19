using System;
using System.Collections.Generic;

abstract class Hinh
{
    public abstract double TinhChuVi();
    public abstract double TinhDienTich();
}

class HinhTron : Hinh
{
    public double BanKinh { get; set; }

    public HinhTron(double banKinh)
    {
        BanKinh = banKinh;
    }

    public override double TinhChuVi()
    {
        return 2 * Math.PI * BanKinh;
    }

    public override double TinhDienTich()
    {
        return Math.PI * BanKinh * BanKinh;
    }
}

class HinhVuong : Hinh
{
    public double Canh { get; set; }

    public HinhVuong(double canh)
    {
        Canh = canh;
    }

    public override double TinhChuVi()
    {
        return 4 * Canh;
    }

    public override double TinhDienTich()
    {
        return Canh * Canh;
    }
}

class HinhTamGiac : Hinh
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public HinhTamGiac(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public override double TinhChuVi()
    {
        return A + B + C;
    }

    public override double TinhDienTich()
    {
        double p = (A + B + C) / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }
}

class HinhChuNhat : Hinh
{
    public double ChieuDai { get; set; }
    public double ChieuRong { get; set; }

    public HinhChuNhat(double dai, double rong)
    {
        ChieuDai = dai;
        ChieuRong = rong;
    }

    public override double TinhChuVi()
    {
        return 2 * (ChieuDai + ChieuRong);
    }

    public override double TinhDienTich()
    {
        return ChieuDai * ChieuRong;
    }
}

class Program
{
    static void Main()
    {
        List<Hinh> danhSachHinh = new List<Hinh>();
        string luaChon;

        do
        {
            Console.WriteLine("\nChọn loại hình:");
            Console.WriteLine("1. Hình tròn");
            Console.WriteLine("2. Hình vuông");
            Console.WriteLine("3. Hình tam giác");
            Console.WriteLine("4. Hình chữ nhật");
            Console.WriteLine("0. Kết thúc");
            Console.Write("Nhập lựa chọn: ");
            luaChon = Console.ReadLine();

            switch (luaChon)
            {
                case "1":
                    Console.Write("Nhập bán kính: ");
                    if (double.TryParse(Console.ReadLine(), out double r))
                        danhSachHinh.Add(new HinhTron(r));
                    else
                        Console.WriteLine("Giá trị không hợp lệ.");
                    break;
                case "2":
                    Console.Write("Nhập cạnh: ");
                    if (double.TryParse(Console.ReadLine(), out double canh))
                        danhSachHinh.Add(new HinhVuong(canh));
                    else
                        Console.WriteLine("Giá trị không hợp lệ.");
                    break;
                case "3":
                    Console.Write("Nhập cạnh a: ");
                    if (double.TryParse(Console.ReadLine(), out double a))
                    {
                        Console.Write("Nhập cạnh b: ");
                        if (double.TryParse(Console.ReadLine(), out double b))
                        {
                            Console.Write("Nhập cạnh c: ");
                            if (double.TryParse(Console.ReadLine(), out double c))
                            {
                                if (a + b > c && a + c > b && b + c > a)
                                    danhSachHinh.Add(new HinhTamGiac(a, b, c));
                                else
                                    Console.WriteLine("Ba cạnh không hợp lệ.");
                            }
                        }
                    }
                    break;
                case "4":
                    Console.Write("Nhập chiều dài: ");
                    if (double.TryParse(Console.ReadLine(), out double dai))
                    {
                        Console.Write("Nhập chiều rộng: ");
                        if (double.TryParse(Console.ReadLine(), out double rong))
                            danhSachHinh.Add(new HinhChuNhat(dai, rong));
                        else
                            Console.WriteLine("Giá trị không hợp lệ.");
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }

        } while (luaChon != "0");

        double tongChuVi = 0, tongDienTich = 0;
        foreach (var hinh in danhSachHinh)
        {
            tongChuVi += hinh.TinhChuVi();
            tongDienTich += hinh.TinhDienTich();
        }

        Console.WriteLine($"\nTổng chu vi: {tongChuVi}");
        Console.WriteLine($"Tổng diện tích: {tongDienTich}");
        Console.ReadKey();
    }
}
