using System;
using System.Collections.Generic;

// Bai 1: Tinh tong cac phan so
class TinhPS
{
    public int TuSo { get; set; }
    public int MauSo { get; set; }

    public TinhPS(int tuSo = 0, int mauSo = 1)
    {
        TuSo = tuSo;
        if (mauSo == 0)
        {
            throw new ArgumentException("Mau so khong the bang 0.");
        }
        MauSo = mauSo;
    }

    public void Nhap()
    {
        Console.Write("Nhap tu so: ");
        int tuSo;
        while (!int.TryParse(Console.ReadLine(), out tuSo))
        {
            Console.WriteLine("Vui long nhap mot so nguyen.");
            Console.Write("Nhap tu so: ");
        }
        TuSo = tuSo;

        Console.Write("Nhap mau so: ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int mauSo))
            {
                if (mauSo == 0)
                {
                    Console.WriteLine("Mau so khong the bang 0. Vui long nhap lai.");
                    Console.Write("Nhap mau so: ");
                }
                else
                {
                    MauSo = mauSo;
                    break;
                }
            }
            else
            {
                Console.WriteLine("Vui long nhap mot so nguyen.");
                Console.Write("Nhap mau so: ");
            }
        }
    }

    public override string ToString()
    {
        return $"{TuSo}/{MauSo}";
    }

    public static TinhPS operator +(TinhPS ps1, TinhPS ps2)
    {
        int tuSoMoi = ps1.TuSo * ps2.MauSo + ps2.TuSo * ps1.MauSo;
        int mauSoMoi = ps1.MauSo * ps2.MauSo;
        return new TinhPS(tuSoMoi, mauSoMoi);
    }
}

class TinhPSCalculator
{
    public static TinhPS TinhTongTinhPS(List<TinhPS> danhSachTinhPS)
    {
        if (danhSachTinhPS == null || danhSachTinhPS.Count == 0)
        {
            return new TinhPS(0, 1);
        }

        TinhPS tong = danhSachTinhPS[0];
        for (int i = 1; i < danhSachTinhPS.Count; i++)
        {
            tong += danhSachTinhPS[i];
        }
        return tong;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<TinhPS> danhSachTinhPS = new List<TinhPS>();
        Console.Write("Nhap so luong phan so: ");
        int soLuong;
        while (!int.TryParse(Console.ReadLine(), out soLuong) || soLuong <= 0)
        {
            Console.WriteLine("Vui long nhap mot so nguyen duong.");
            Console.Write("Nhap so luong phan so: ");
        }

        for (int i = 0; i < soLuong; i++)
        {
            Console.WriteLine($"Nhap phan so thu {i + 1}:");
            TinhPS ps = new TinhPS();
            ps.Nhap();
            danhSachTinhPS.Add(ps);
        }

        TinhPS tong = TinhPSCalculator.TinhTongTinhPS(danhSachTinhPS);
        Console.WriteLine($"Tong cac phan so la: {tong}");
    }
}
