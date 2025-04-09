using System;

namespace QLApp
{
    internal class PhanSo
    {
        public int TuSo { get; set; }
        public int MauSo { get; set; }

        public PhanSo()
        {
            TuSo = 0;
            MauSo = 1;
        }

        public PhanSo(int tu, int mau)
        {
            TuSo = tu;
            if (mau == 0)
            {
                Console.WriteLine("Mau so phai khac 0. Set Mau = 1.");
                MauSo = 1;
            }
            else
                MauSo = mau;
        }

        public void Nhap()
        {
            Console.Write("Nhap tu so: ");
            TuSo = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap mau so: ");
            MauSo = int.Parse(Console.ReadLine() ?? "1");
            if (MauSo == 0)
            {
                Console.WriteLine("Mau so phai khac 0. Set Mau = 1.");
                MauSo = 1;
            }
        }

        public void HienThi()
        {
            Console.WriteLine($"{TuSo}/{MauSo}");
        }

        private int UCLN(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }

        public void RutGon()
        {
            int u = UCLN(TuSo, MauSo);
            TuSo /= u;
            MauSo /= u;
        }

        public static PhanSo Cong(PhanSo A, PhanSo B)
        {
            int tu = A.TuSo * B.MauSo + B.TuSo * A.MauSo;
            int mau = A.MauSo * B.MauSo;
            PhanSo ketQua = new PhanSo(tu, mau);
            ketQua.RutGon();
            return ketQua;
        }

        public static PhanSo Tru(PhanSo A, PhanSo B)
        {
            int tu = A.TuSo * B.MauSo - B.TuSo * A.MauSo;
            int mau = A.MauSo * B.MauSo;
            PhanSo ketQua = new PhanSo(tu, mau);
            ketQua.RutGon();
            return ketQua;
        }

        public static PhanSo Nhan(PhanSo A, PhanSo B)
        {
            int tu = A.TuSo * B.TuSo;
            int mau = A.MauSo * B.MauSo;
            PhanSo ketQua = new PhanSo(tu, mau);
            ketQua.RutGon();
            return ketQua;
        }

        public static PhanSo Chia(PhanSo A, PhanSo B)
        {
            if (B.TuSo == 0)
            {
                Console.WriteLine("Khong the chia, phan so B = 0");
                return null;
            }
            int tu = A.TuSo * B.MauSo;
            int mau = A.MauSo * B.TuSo;
            PhanSo ketQua = new PhanSo(tu, mau);
            ketQua.RutGon();
            return ketQua;
        }
    }

    internal static class Bai14
    {
        public static void B14()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 14: Phan So ------");
            Console.WriteLine("Nhap phan so A:");
            PhanSo A = new PhanSo();
            A.Nhap();
            Console.WriteLine("Nhap phan so B:");
            PhanSo B = new PhanSo();
            B.Nhap();
            Console.WriteLine("Chon thao tac:");
            Console.WriteLine("1. Cong A + B");
            Console.WriteLine("2. Tru A - B");
            Console.WriteLine("3. Nhan A * B");
            Console.WriteLine("4. Chia A / B");
            Console.Write("Lua chon: ");
            int chon = int.Parse(Console.ReadLine() ?? "0");
            PhanSo ketQua = null;
            switch (chon)
            {
                case 1:
                    ketQua = PhanSo.Cong(A, B);
                    Console.Write("Ket qua A + B: ");
                    break;
                case 2:
                    ketQua = PhanSo.Tru(A, B);
                    Console.Write("Ket qua A - B: ");
                    break;
                case 3:
                    ketQua = PhanSo.Nhan(A, B);
                    Console.Write("Ket qua A * B: ");
                    break;
                case 4:
                    ketQua = PhanSo.Chia(A, B);
                    if (ketQua == null) return;
                    Console.Write("Ket qua A / B: ");
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le.");
                    return;
            }
            ketQua.HienThi();
            Console.WriteLine("Nhan phim bat ky de thoat Bai 14...");
            Console.ReadKey();
        }
    }
}
