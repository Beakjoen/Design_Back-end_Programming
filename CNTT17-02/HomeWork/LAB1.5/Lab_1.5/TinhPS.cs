using System;
using System.Collections.Generic;

// Bài 1: Tính tổng các phân số
class TinhPS
{
    public int TuSo { get; set; }
    public int MauSo { get; set; }

    public TinhPS(int tuSo = 0, int mauSo = 1)
    {
        TuSo = tuSo;
        if (mauSo == 0)
        {
            throw new ArgumentException("Mẫu số không thể bằng 0.");
        }
        MauSo = mauSo;
    }

    public void Nhap()
    {
        Console.Write("Nhập tử số: ");
        int tuSo; // Declare the variable 'tuSo' here
        while (!int.TryParse(Console.ReadLine(), out tuSo))
        {
            Console.WriteLine("Vui lòng nhập một số nguyên.");
            Console.Write("Nhập tử số: ");
        }
        TuSo = tuSo; // Use the declared variable 'tuSo'

        Console.Write("Nhập mẫu số: ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int mauSo))
            {
                if (mauSo == 0)
                {
                    Console.WriteLine("Mẫu số không thể bằng 0. Vui lòng nhập lại.");
                    Console.Write("Nhập mẫu số: ");
                }
                else
                {
                    MauSo = mauSo;
                    break;
                }
            }
            else
            {
                Console.WriteLine("Vui lòng nhập một số nguyên.");
                Console.Write("Nhập mẫu số: ");
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