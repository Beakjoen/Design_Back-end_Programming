using System;
using System.Runtime.Intrinsics.X86;
using System.Text;
using QLApp;

namespace LAB1._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Hien thi tieng viet
            Console.OutputEncoding = Encoding.UTF8;

            int choice = -1;
            bool tiepTuc = true;

            do
            {
                Console.Clear();
                Console.WriteLine("======= MENU CHINH =======");
                Console.WriteLine("1.  Bai 1");
                Console.WriteLine("2.  Bai 2");
                Console.WriteLine("3.  Bai 3");
                Console.WriteLine("4.  Bai 4");
                Console.WriteLine("5.  Bai 5");
                Console.WriteLine("6.  Bai 6");
                Console.WriteLine("7.  Bai 7");
                Console.WriteLine("8.  Bai 8");
                Console.WriteLine("9.  Bai 9");
                Console.WriteLine("10. Bai 10");
                Console.WriteLine("11. Bai 11");
                Console.WriteLine("12. Bai 12");
                Console.WriteLine("13. Bai 13");
                Console.WriteLine("14. Bai 14");
                Console.WriteLine("15. Bai 15");
                Console.WriteLine("16. Bai 16");
                Console.WriteLine("17. Bai 17");
                Console.WriteLine("18. Bai 18");
                Console.WriteLine("19. Bai 19");
                Console.WriteLine("20. Bai 20");
                Console.WriteLine("21. Bai 21");
                Console.WriteLine("22. Bai 22");
                Console.WriteLine("0. Thoat chuong trinh");
                Console.Write("Nhap lua chon: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Nhap sai dinh dang, vui long nhap lai!");
                    Console.ReadKey();
                    continue;
                }

                // Gọi hàm của bài tương ứng
                switch (choice)
                {
                    case 1:
                        Bai1.B1(); // Gọi hàm B1() trong Bai1.cs
                        break;
                    case 2:
                        Bai2.B2();
                        break;
                    case 3:
                        Bai3.B3();
                        break;
                    case 4:
                        Bai4.B4();
                        break;
                    case 5:
                        Bai5.B5();
                        break;
                    case 6:
                        Bai6.B6();
                        break;
                    case 7:
                        Bai7.B7();
                        break;
                    case 8:
                        Bai8.B8();
                        break;
                    case 9:
                        Bai9.B9();
                        break;
                    case 10:
                        Bai10.B10();
                        break;
                    case 11:
                        Bai11.B11();
                        break;
                    case 12:
                        Bai12.B12();
                        break;
                    case 13:
                        Bai13.B13();
                        break;
                    case 14:
                        Bai14.B14();
                        break;
                    case 15:
                        Bai15.B15();
                        break;
                    case 16:
                        Bai16.B16();
                        break;
                    case 17:
                        Bai17.B17();
                        break;
                    case 18:
                        Bai18.B18();
                        break;
                    case 19:
                        Bai19.B19();
                        break;
                    case 20:
                        Bai20.B20();
                        break;
                    case 21:
                        Bai21.B21();
                        break;
                    case 22:
                        Bai22.B22();
                        break;
                    case 0:
                        Console.WriteLine("Cam on da su dung chuong trinh. Tam biet!");
                        tiepTuc = false;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le! Vui long nhap lai.");
                        break;
                }

                // Hỏi người dùng có muốn tiếp tục không
                if (choice != 0)
                {
                    Console.WriteLine("\nBan co muon chay chuong trinh khac? (Y/N) : ");
                    string input = Console.ReadLine();
                    if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        tiepTuc = false;
                    }
                }
            } while (tiepTuc);
        }
    }
}
