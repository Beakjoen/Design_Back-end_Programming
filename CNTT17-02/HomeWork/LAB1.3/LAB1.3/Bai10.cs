using System;

namespace QLApp
{
    // Lớp VanBan chứa thuộc tính xâu và các phương thức xử lý
    internal class VanBan
    {
        public string NoiDung { get; set; } = string.Empty;

        public VanBan() { NoiDung = string.Empty; }
        public VanBan(string st) { NoiDung = st; }

        public int DemSoTu()
        {
            if (string.IsNullOrWhiteSpace(NoiDung))
                return 0;
            string[] words = NoiDung.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public int DemSoKyTuH()
        {
            int count = 0;
            foreach (char ch in NoiDung)
            {
                if (char.ToLower(ch) == 'h')
                    count++;
            }
            return count;
        }

        public void ChuanHoa()
        {
            NoiDung = NoiDung.Trim();
            string[] words = NoiDung.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            NoiDung = string.Join(" ", words);
        }

        public void HienThi()
        {
            Console.WriteLine("Noi dung: " + NoiDung);
        }
    }

    // Lớp QuanLyVanBan với menu cho các chức năng xử lý xâu
    internal class QuanLyVanBan
    {
        private VanBan vb = new VanBan();

        public void Menu()
        {
            int luaChon = -1;
            do
            {
                Console.WriteLine("\nCHUONG TRINH XU LY VAN BAN");
                Console.WriteLine("1. Nhap xau");
                Console.WriteLine("2. Dem so tu trong xau");
                Console.WriteLine("3. Dem so ky tu 'H/h' trong xau");
                Console.WriteLine("4. Chuan hoa xau");
                Console.WriteLine("5. Hien thi xau");
                Console.WriteLine("6. Thoat");
                Console.Write("Chon chuc nang: ");
                luaChon = int.Parse(Console.ReadLine() ?? "0");
                switch (luaChon)
                {
                    case 1:
                        Console.Write("Nhap xau: ");
                        vb.NoiDung = Console.ReadLine() ?? string.Empty;
                        break;
                    case 2:
                        Console.WriteLine("So tu trong xau: " + vb.DemSoTu());
                        break;
                    case 3:
                        Console.WriteLine("So ky tu 'H/h' trong xau: " + vb.DemSoKyTuH());
                        break;
                    case 4:
                        vb.ChuanHoa();
                        Console.WriteLine("Xau sau khi chuan hoa:");
                        vb.HienThi();
                        break;
                    case 5:
                        vb.HienThi();
                        break;
                    case 6:
                        Console.WriteLine("Thoat chuong trinh Bai 10.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
                if (luaChon != 6)
                {
                    Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            } while (luaChon != 6);
        }
    }

    internal static class Bai10
    {
        public static void B10()
        {
            Console.Clear();
            Console.WriteLine("------ Bai 10: Xu ly van ban ------");
            QuanLyVanBan ql = new QuanLyVanBan();
            ql.Menu();
        }
    }
}
