//VD1: viet chuog trinh nhap 1 so nguyen x tu ban phim
//Tinh 1/x

//float a;
//int x;
//Console.Write("Nhap so nguyen x = ");
//try
//{
//    x = int.Parse(Console.ReadLine() ?? "");
//    a = 1 / (float)x;
//    Console.WriteLine($"a = " + a);
//}
//catch (DivideByZeroException e)
//{
//    Console.WriteLine("Loi: " + e.Message);
//}
//catch (FormatException e)
//{
//    Console.WriteLine("Loi: " + e.Message);
//}
//catch (Exception e)
//{
//    Console.WriteLine("Loi: " + e.Message);
//}

//Vd 2 : Nhap 1 so nguyen duong tu ban phim
//Xuat so nguyen duong vua nhap len man hinh
void Nhap()
{
    int a;
    Console.Write("Nhap so nguyen duong a = ");
    try
    {
        a = int.Parse(Console.ReadLine() ?? "");
        if (a <= 0)
            throw new Lesson2.SoAmException("a phai la so nguyen duong ");
        Console.WriteLine($"So nguyen duong vua nhap la: {a}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Loi nhap lieu: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Loi: {ex.Message}");
    }
    finally
    {
        Console.WriteLine("Ket thuc chuong trinh");
    }
}
Nhap();
