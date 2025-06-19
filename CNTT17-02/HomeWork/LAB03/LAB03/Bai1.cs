internal class Bai1
{
    class CanBo
    {
        public string HoTen { get; set; } = string.Empty;
        public int NamSinh { get; set; }
        public string DiaChi { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;

        public virtual void NhapThongTin()
        {
            Console.WriteLine("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Nhap dia chi: ");
            DiaChi = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Nhap gioi tinh: ");
            GioiTinh = Console.ReadLine() ?? string.Empty;
        }

        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Ho ten: {HoTen}, Nam sinh: {NamSinh}, Dia chi: {DiaChi}, Gioi tinh: {GioiTinh}");
        }
    }

    class NhanVien : CanBo
    {
        public string CongViec { get; set; } = string.Empty;

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.WriteLine("Nhap cong viec: ");
            CongViec = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Cong viec: {CongViec}");
        }
    }

    class KySu : CanBo
    {
        public string Nganh { get; set; } = string.Empty;

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.WriteLine("Nhap nganh dao tao: ");
            Nganh = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Nganh dao tao: {Nganh}");
        }
    }

    class CongNhan : CanBo
    {
        public string Bac { get; set; } = string.Empty;

        public override void NhapThongTin()
        {
            base.NhapThongTin();
            Console.WriteLine("Nhap bac (Vi du: 3/7): ");
            Bac = Console.ReadLine() ?? string.Empty;
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Bac: {Bac}");
        }
    }

    class QLCB
    {
        private List<CanBo> danhSachCanBo = new List<CanBo>();

        public void NhapThongTinMoi()
        {
            Console.WriteLine("Nhap loai can bo (1: Nhan Vien, 2: Ky Su, 3: Cong Nhan): ");
            if (!int.TryParse(Console.ReadLine(), out int loaiCanBo))
            {
                Console.WriteLine("Loai can bo khong hop le.");
                return;
            }

            CanBo canBo = loaiCanBo switch
            {
                1 => new NhanVien(),
                2 => new KySu(),
                3 => new CongNhan(),
                _ => throw new ArgumentException("Loai can bo khong hop le")
            };

            canBo.NhapThongTin();
            danhSachCanBo.Add(canBo);
        }

        public void TimKiemTheoHoTen(string hoTen)
        {
            var ketQua = danhSachCanBo.Where(cb => cb.HoTen.Contains(hoTen, StringComparison.OrdinalIgnoreCase));
            foreach (var canBo in ketQua)
            {
                canBo.HienThiThongTin();
            }
        }

        public void HienThiDanhSach()
        {
            if (danhSachCanBo.Count == 0)
            {
                Console.WriteLine("Danh sach trong!");
                return;
            }

            foreach (var canBo in danhSachCanBo)
            {
                canBo.HienThiThongTin();
            }
        }
    }

        public static void B1()
    {
        var quanLy = new QLCB();
        while (true)
        {
            Console.WriteLine("\nCHUONG TRINH QUAN LY CAN BO");
            Console.WriteLine("1. Nhap thong tin moi");
            Console.WriteLine("2. Tim kiem theo ho ten");
            Console.WriteLine("3. Hien thi danh sach");
            Console.WriteLine("4. Thoat");
            Console.Write("Chon chuc nang (1-4): ");

            if (!int.TryParse(Console.ReadLine(), out int luaChon))
            {
                Console.WriteLine("Lua chon khong hop le!");
                continue;
            }

            switch (luaChon)
            {
                case 1:
                    quanLy.NhapThongTinMoi();
                    break;
                case 2:
                    Console.Write("Nhap ho ten can tim: ");
                    string hoTen = Console.ReadLine() ?? string.Empty;
                    quanLy.TimKiemTheoHoTen(hoTen);
                    break;
                case 3:
                    quanLy.HienThiDanhSach();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }
    }
}
