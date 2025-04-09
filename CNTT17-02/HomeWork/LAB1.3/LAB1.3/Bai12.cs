using System;

namespace QLApp
{
    internal class MaTran
    {
        public int SoDong { get; set; }
        public int SoCot { get; set; }
        public double[,] MT { get; set; }

        public MaTran()
        {
            SoDong = 0;
            SoCot = 0;
            MT = new double[0, 0];
        }

        public MaTran(int n, int m)
        {
            SoDong = n;
            SoCot = m;
            MT = new double[n, m];
        }

        public void Nhap()
        {
            Console.Write("Nhap so dong: ");
            SoDong = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap so cot: ");
            SoCot = int.Parse(Console.ReadLine() ?? "0");
            MT = new double[SoDong, SoCot];
            for (int i = 0; i < SoDong; i++)
            {
                for (int j = 0; j < SoCot; j++)
                {
                    Console.Write($"Nhap phan tu [{i}][{j}]: ");
                    MT[i, j] = double.Parse(Console.ReadLine() ?? "0");
                }
            }
        }

        public void HienThi()
        {
            for (int i = 0; i < SoDong; i++)
            {
                for (int j = 0; j < SoCot; j++)
                {
                    Console.Write(MT[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static MaTran Cong(MaTran A, MaTran B)
        {
            if (A.SoDong != B.SoDong || A.SoCot != B.SoCot)
            {
                Console.WriteLine("Khong the cong, ma tran khong cung cap.");
                return null;
            }
            MaTran C = new MaTran(A.SoDong, A.SoCot);
            for (int i = 0; i < A.SoDong; i++)
            {
                for (int j = 0; j < A.SoCot; j++)
                {
                    C.MT[i, j] = A.MT[i, j] + B.MT[i, j];
                }
            }
            return C;
        }

        public static MaTran Tru(MaTran A, MaTran B)
        {
            if (A.SoDong != B.SoDong || A.SoCot != B.SoCot)
            {
                Console.WriteLine("Khong the tru, ma tran khong cung cap.");
                return null;
            }
            MaTran C = new MaTran(A.SoDong, A.SoCot);
            for (int i = 0; i < A.SoDong; i++)
            {
                for (int j = 0; j < A.SoCot; j++)
                {
                    C.MT[i, j] = A.MT[i, j] - B.MT[i, j];
                }
            }
            return C;
        }

        public static MaTran Tich(MaTran A, MaTran B)
        {
            if (A.SoCot != B.SoDong)
            {
                Console.WriteLine("Khong the nhan, so cot cua A phai bang so dong cua B.");
                return null;
            }
            MaTran C = new MaTran(A.SoDong, B.SoCot);
            for (int i = 0; i < A.SoDong; i++)
            {
                for (int j = 0; j < B.SoCot; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < A.SoCot; k++)
                    {
                        sum += A.MT[i, k] * B.MT[k, j];
                    }
                    C.MT[i, j] = sum;
                }
            }
            return C;
        }

        public static MaTran Thuong(MaTran A, MaTran B)
        {
            // Chia theo từng phần tử (element-wise). Yêu cầu 2 ma trận phải cùng cấp
            if (A.SoDong != B.SoDong || A.SoCot != B.SoCot)
            {
                Console.WriteLine("Khong the chia theo phan tu, ma tran khong cung cap.");
                return null;
            }
            MaTran C = new MaTran(A.SoDong, A.SoCot);
            for (int i = 0; i < A.SoDong; i++)
            {
                for (int j = 0; j < A.SoCot; j++)
                {
                    if (B.MT[i, j] == 0)
                    {
                        Console.WriteLine($"Phan tu [{i}][{j}] cua ma tran B bang 0, khong the chia.");
                        return null;
                    }
                    C.MT[i, j] = A.MT[i, j] / B.MT[i, j];
                }
            }
            return C;
        }
    }

    internal static class Bai12
    {
        public static void B12()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 12: Ma Tran ------");
            Console.WriteLine("Nhap ma tran A:");
            MaTran A = new MaTran();
            A.Nhap();
            Console.WriteLine("Nhap ma tran B (cung cap voi A):");
            MaTran B = new MaTran();
            B.Nhap();
            Console.WriteLine("Chon thao tac:");
            Console.WriteLine("1. Cong hai ma tran");
            Console.WriteLine("2. Tru hai ma tran (A - B)");
            Console.WriteLine("3. Tich hai ma tran (A x B)");
            Console.WriteLine("4. Thuong hai ma tran (phan tu chia phan tu)");
            Console.Write("Lua chon: ");
            int chon = int.Parse(Console.ReadLine() ?? "0");
            MaTran ketQua = null;
            switch (chon)
            {
                case 1:
                    ketQua = MaTran.Cong(A, B);
                    Console.WriteLine("Ket qua cong: ");
                    break;
                case 2:
                    ketQua = MaTran.Tru(A, B);
                    Console.WriteLine("Ket qua tru: ");
                    break;
                case 3:
                    ketQua = MaTran.Tich(A, B);
                    Console.WriteLine("Ket qua tich: ");
                    break;
                case 4:
                    ketQua = MaTran.Thuong(A, B);
                    Console.WriteLine("Ket qua thuong (phan tu chia phan tu): ");
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le.");
                    return;
            }
            if (ketQua != null)
            {
                ketQua.HienThi();
            }
            Console.WriteLine("Nhan phim bat ky de thoat Bai 12...");
            Console.ReadKey();
        }
    }
}
