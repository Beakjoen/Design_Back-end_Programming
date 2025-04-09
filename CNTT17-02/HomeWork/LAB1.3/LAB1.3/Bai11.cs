using System;

namespace QLApp
{
    internal class SoPhuc
    {
        public double PhanThuc { get; set; }
        public double PhanAo { get; set; }

        public SoPhuc()
        {
            PhanThuc = 0;
            PhanAo = 0;
        }

        public SoPhuc(double a, double b)
        {
            PhanThuc = a;
            PhanAo = b;
        }

        public void Nhap()
        {
            Console.Write("Nhap phan thuc: ");
            PhanThuc = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhap phan ao: ");
            PhanAo = double.Parse(Console.ReadLine() ?? "0");
        }

        public void HienThi()
        {
            Console.WriteLine($"{PhanThuc} + {PhanAo}i");
        }

        public static SoPhuc Cong(SoPhuc A, SoPhuc B)
        {
            return new SoPhuc(A.PhanThuc + B.PhanThuc, A.PhanAo + B.PhanAo);
        }

        public static SoPhuc Tru(SoPhuc A, SoPhuc B)
        {
            return new SoPhuc(A.PhanThuc - B.PhanThuc, A.PhanAo - B.PhanAo);
        }

        public static SoPhuc Nhan(SoPhuc A, SoPhuc B)
        {
            double real = A.PhanThuc * B.PhanThuc - A.PhanAo * B.PhanAo;
            double imag = A.PhanThuc * B.PhanAo + A.PhanAo * B.PhanThuc;
            return new SoPhuc(real, imag);
        }

        public static SoPhuc Chia(SoPhuc A, SoPhuc B)
        {
            double denominator = B.PhanThuc * B.PhanThuc + B.PhanAo * B.PhanAo;
            if (denominator == 0)
            {
                Console.WriteLine("Khong the chia, so phuc B = 0");
                return null;
            }
            double real = (A.PhanThuc * B.PhanThuc + A.PhanAo * B.PhanAo) / denominator;
            double imag = (A.PhanAo * B.PhanThuc - A.PhanThuc * B.PhanAo) / denominator;
            return new SoPhuc(real, imag);
        }
    }

    internal static class Bai11
    {
        public static void B11()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 11: So Phuc ------");
            Console.WriteLine("Nhap so phuc A:");
            SoPhuc A = new SoPhuc();
            A.Nhap();
            Console.WriteLine("Nhap so phuc B:");
            SoPhuc B = new SoPhuc();
            B.Nhap();
            Console.WriteLine("Chon thao tac:");
            Console.WriteLine("1. Cong hai so phuc");
            Console.WriteLine("2. Tru hai so phuc (A - B)");
            Console.WriteLine("3. Nhan hai so phuc");
            Console.WriteLine("4. Chia hai so phuc (A / B)");
            Console.Write("Lua chon: ");
            int chon = int.Parse(Console.ReadLine() ?? "0");
            SoPhuc ketQua = null;
            switch (chon)
            {
                case 1:
                    ketQua = SoPhuc.Cong(A, B);
                    Console.Write("Ket qua cong: ");
                    break;
                case 2:
                    ketQua = SoPhuc.Tru(A, B);
                    Console.Write("Ket qua tru (A - B): ");
                    break;
                case 3:
                    ketQua = SoPhuc.Nhan(A, B);
                    Console.Write("Ket qua nhan: ");
                    break;
                case 4:
                    ketQua = SoPhuc.Chia(A, B);
                    if (ketQua == null) return;
                    Console.Write("Ket qua chia (A / B): ");
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le.");
                    return;
            }
            ketQua.HienThi();
            Console.WriteLine("Nhan phim bat ky de thoat Bai 11...");
            Console.ReadKey();
        }
    }
}
