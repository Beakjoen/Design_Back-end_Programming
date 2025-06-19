using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LAB03
{
    internal class Bai10
    {
        class VanBan
        {
            private string NoiDung { get; set; }

            public VanBan()
            {
                NoiDung = string.Empty;
            }

            public VanBan(string st)
            {
                NoiDung = st;
            }

            public void NhapVanBan()
            {
                Console.WriteLine("Nhap van ban:");
                NoiDung = Console.ReadLine() ?? string.Empty;
            }

            public int DemSoTu()
            {
                return Regex.Split(NoiDung.Trim(), @"\s+").Length;
            }

            public int DemKyTuH()
            {
                return NoiDung.Count(c => c.ToString().Equals("h", StringComparison.OrdinalIgnoreCase));
            }

            public string ChuanHoaXau()
            {
                // Xóa kho?ng tr?ng ??u, cu?i và thay th? nhi?u kho?ng tr?ng liên ti?p b?ng m?t kho?ng tr?ng
                NoiDung = Regex.Replace(NoiDung.Trim(), @"\s+", " ");
                return NoiDung;
            }

            public override string ToString()
            {
                return NoiDung;
            }
        }

        public static void B10()
        {
            var vanBan = new VanBan();
            bool daCoVanBan = false;

            while (true)
            {
                Console.WriteLine("\nCHUONG TRINH XU LY VAN BAN");
                Console.WriteLine("1. Nhap van ban");
                Console.WriteLine("2. Dem so tu");
                Console.WriteLine("3. Dem so ky tu 'h'");
                Console.WriteLine("4. Chuan hoa van ban");
                Console.WriteLine("5. Hien thi van ban");
                Console.WriteLine("6. Thoat");
                Console.Write("Chon chuc nang (1-6): ");

                if (!int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le!");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        vanBan.NhapVanBan();
                        daCoVanBan = true;
                        break;
                    case 2:
                        if (!daCoVanBan)
                        {
                            Console.WriteLine("Vui long nhap van ban truoc!");
                            break;
                        }
                        Console.WriteLine($"So tu trong van ban: {vanBan.DemSoTu()}");
                        break;
                    case 3:
                        if (!daCoVanBan)
                        {
                            Console.WriteLine("Vui long nhap van ban truoc!");
                            break;
                        }
                        Console.WriteLine($"So ky tu 'h' trong van ban: {vanBan.DemKyTuH()}");
                        break;
                    case 4:
                        if (!daCoVanBan)
                        {
                            Console.WriteLine("Vui long nhap van ban truoc!");
                            break;
                        }
                        Console.WriteLine($"Van ban sau khi chuan hoa: {vanBan.ChuanHoaXau()}");
                        break;
                    case 5:
                        if (!daCoVanBan)
                        {
                            Console.WriteLine("Chua co van ban nao!");
                            break;
                        }
                        Console.WriteLine($"Van ban hien tai: {vanBan}");
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}
