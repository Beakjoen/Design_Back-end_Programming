//int[] numbers = { 3, 2, 8, 5, 4 };
//~Query syntax
//var result = from n in numbers
//             where n % 2 == 0
//             select n;

//~Method syntax
//var result = numbers.Where(n => n % 2 == 0).Select(n => n);

//foreach (var item in result)
//{
//    Console.Write(item + " ");
//}

using HocLinQ;

List<Brand> brands = new List<Brand>()
{
    new Brand(){BrandId=1,BrandName="Công ty may 10"},
    new Brand(){BrandId=2,BrandName="Công ty CMC"},
    new Brand(){BrandId=3,BrandName="Nhà sách Trí Tuệ"},
    new Brand(){BrandId=4,BrandName="Công ty Xuân Hòa"}
};
List<Product> products = new List<Product>()
{
    new Product(){ProductId=1,ProductName="Laptop Dell",
        Price=15000000,Colors = new string[]{"Trắng","Đen"},BrandId=2},
    new Product(){ProductId=2,ProductName="Áo đồng phục",
        Price=250000,Colors = new string[]{"Trắng","Xanh"},BrandId=1},
    new Product(){ProductId=3,ProductName="Bàn học",
        Price=800000,Colors = new string[]{"Trắng","Nâu"},BrandId=4},
    new Product(){ProductId=4,ProductName="Đèn bàn",
        Price=400000,Colors = new string[]{"Xanh","Hồng"},BrandId=3},
    new Product(){ProductId=5,ProductName="Máy chiếu",
        Price=12000000,Colors = new string[]{"Trắng","Đen"},BrandId=2},
    new Product(){ProductId=6,ProductName="Cặp học sinh",
        Price=600000,Colors = new string[]{"Trắng","Hồng"},BrandId=3},
};

Console.OutputEncoding = System.Text.Encoding.UTF8;
//Các toán tử truy vấn cơ bản
//1. Toán tử Select
//~Query syntax
//var result = from p in brands
//             select p;

//~Method syntax
//var result = brands.Select(p => p);

//foreach (var item in result)
//{
//    Console.WriteLine($"{item.BrandId},{item.BrandName}");
//}

//trả về đối tượng vô danh
//~Query syntax
//var result = from p in products
//             select new
//             {
//                 MaSP = p.ProductId,
//                 TenSP = p.ProductName,
//             };

//~Method syntax

//var result = products.Select(p => new
//{
//    MaSP = p.ProductId,
//    TenSP = p.ProductName,
//});

//foreach (var item in result)
//{
//    Console.WriteLine($"{item.MaSP},{item.TenSP}");
//}

//~Query syntax
//var result = from p in products
//             where p.Price > 1000000
//             select new
//                    {
//                        MaSP = p.ProductId,
//                        TenSP = p.ProductName,
//                    };

//~Method syntax
//var result = products.Where(p => p.Price > 1000000)
//                    .Select(p => new
//                    {
//                        MaSP = p.ProductId,
//                        TenSP = p.ProductName,
//                        GiaSP = p.Price,
//                    });

//foreach (var item in result)
//{
//    Console.WriteLine($"{item.MaSP},{item.TenSP}.{item.GiaSP}");
//}

//3. Sắp xếp orderby
//~Query syntax
//var result = from b in brands
//             orderby b.BrandName ascending
//             select b;

//~Method syntax

//var result = brands.OrderBy(b => b.BrandName).Select(b => b);

//foreach (var item in brands)
//{
//    Console.WriteLine($"{item.BrandId},{item.BrandName}");
//}

//4. Phép nối
//~Query syntax
//var result = from p in products
//             join b in brands
//             on p.BrandId equals b.BrandId
//             select new
//             {
//                 MaSP = p.ProductId,
//                 TenSP = p.ProductName,
//                 TenTH = b.BrandName
//             };

//~Method syntax

//var result = products.Join(brands,p => p.BrandId, b => b.BrandId,(p, b) => new
//{
//    MaSP = p.ProductId,
//    TenSP = p.ProductName,
//    TenTH = b.BrandName
//});

//foreach (var item in result)
//{
//    Console.WriteLine($"{item.MaSP},{item.TenSP},{item.TenTH}");
//}

//5. Gộp nhóm: goup by
//~Query syntax
//var result = from p in products
//             group p by p.BrandId;

//~Method syntax

//var result = products.GroupBy(p => p.BrandId);

//foreach (var brandGroup in result)
//{
//    Console.WriteLine($"Thương hiệu: {brandGroup.Key}");
//    foreach (var item in brandGroup)
//    {
//        Console.WriteLine($"\t+{item.ProductId}, {item.ProductName}");
//    }
//}

//6. Toán tử tổng hợp
//var avgPrice = products.Average(p => p.Price);
//Console.WriteLine($"Giá trung bình: {avgPrice}");
//var countProduct = products.Count(p=>p.Price > 1000000);
//Console.WriteLine($"Số sản phẩm có giá lớn hơn 1 triệu: {countProduct}");

// toán tử phân vùng
List<string> list = new List<string>()
{
    "Nguyễn Văn A","Nguyễn Văn B","Nguyễn Văn C","Nguyễn Văn D","Nguyễn Văn E","Nguyễn Văn F"
};
var result = list.Skip(3).Take(5);
foreach (var item in result)
{
    Console.WriteLine(item + " ");
}
