using System.Collections;

internal class Program
{
    public delegate int MyDelegate(int a, int b);
    public static int Tong(int a, int b)
    {
        return a + b;
    }
    private static async Task Main(string[] args)
    {
        ////1. Collections
        ////1.1. ArrayList
        ////Khai bao va khoi tao ArrayList
        //ArrayList arrayList = new ArrayList();
        ////Themphan tu vao trong ArrayList
        ////cu phap arraylist.Add(value)
        //string s = "CNTT 17-02";
        //arrayList.Add(s);
        ////Truy cap den phan tu
        //Console.WriteLine(arrayList[0]);
        ////AddRange(mang gia tri);
        ////Insert(value): Them phan tu vao vij tri bat li trong arraylist
        ////InsertRange(mang gia tri): Them nhieu phan tu vao vij tri bat ki trong arraylist
        //ArrayList arr = [1, 2, 3];
        //arrayList.AddRange(arr);
        ////Xoa phan tu
        //arrayList.RemoveAt(0);//Xoa theo index
        //arrayList.Remove("CNTT 17 - 02");//xoa theo gia tri
        //arrayList.RemoveRange(0, 2);//xoa nhieu phan tu tu vi tri va xoa 2 phan tu
        //arrayList.Clear();//xoa toan bo phan tu trong arraylist
        ////Duyet arraylist
        ////Cach 1 dung foR
        //arrayList.AddRange(arr);
        //Console.WriteLine("FOR");
        //for (int i = 0; i < arrayList.Count; i++)
        //{
        //    Console.WriteLine(arrayList[i]);
        //}
        ////Cach2 dung Enumerator
        //Console.WriteLine("enumerator");
        //IEnumerator enumerator = arrayList.GetEnumerator();
        //while (enumerator.MoveNext())
        //{
        //    Console.WriteLine(enumerator.Current);
        //}
        ////Cach 3 dung foreach
        //Console.WriteLine("foreach");
        //foreach (var item in arrayList)
        //{
        //    Console.WriteLine(item);
        //}
        ////Mot so phuong thuc hay su dung
        //arrayList.Add(-3);
        //Console.WriteLine("before");
        //foreach (var item in arrayList)
        //{
        //    Console.WriteLine(item);
        //}
        //Console.WriteLine("After");
        //arrayList.Sort();//sap xep danh sach
        //foreach (var item in arrayList)
        //{
        //    Console.WriteLine(item);
        //}
        ////IndexOf(value): Tra ve vi tri cau phantu  dau tien co gia tri bang value
        //ArrayList array = new ArrayList() { "Dao", "Cuc", "Truc" };
        //int index = array.IndexOf("Truc");
        //Console.WriteLine($"Index: {index}");
        ////LastIndexOf(VALUE): tra ve vi tri cua phan tu cuoi cung co gia tri = voi value
        ////Contains(value): tra ve true neu phan tu co ton tai trong arraylist
        //bool isExist = array.Contains("Dao");
        //Console.WriteLine($"Is exist: {isExist}");
        ////1.2 HashTable
        //// key la gia tri duy nhat khong duowc trung lap
        //Hashtable hashtable = new Hashtable();
        ////them phan tu
        //hashtable.Add(1, "CNTT 17-02");
        //hashtable.Add(2, "CNTT 17-02");
        //hashtable.Add("key", "value");
        ////duyet
        //foreach (var item in hashtable)
        //{
        //    Console.WriteLine(item);
        //}
        //Console.WriteLine("Key: 2" + hashtable[2]);
        ////2. Generic
        //GenericClass<int> genericClass = new GenericClass<int>(10);
        //GenericClass<char> genericClass1 = new GenericClass<char>('a');
        //GenericClass<string> genericClass2 = new GenericClass<string>("Hello");
        //GenericClass<double> genericClass3 = new GenericClass<double>(3.14);

        //4. Delegate
        //Khoi tao delegate
        //MyDelegate myDelegate = new MyDelegate(Tong);
        //MyDelegate myDelegate1 = Tong;
        ////Thuc thi delegate
        //int ketqua = myDelegate(1, 2);
        //Console.WriteLine($"Ket qua: " + ketqua);
        ////trong C# co 3 loai delegate
        ////Action : tra ve la void
        ////Predicate : tra ve la bool
        ////Func : tra ve la gia tri
        //Func<int, int, int> func = (a, b) => a + b; //Lamba Expression
        //Console.WriteLine($"Ket qua: {func(1, 2)}");
        //Action<string> action = (s) => Console.WriteLine(s);
        //action("ABCD");
        ////Predicate
        //Predicate<int> predicate = (a) => a > 0;
        //Console.WriteLine(predicate(1));

        //5. Asynchoronous Programming (Lap trinh bat dong bo)
        //5.1 Async/Await
        //Console.WriteLine("Main Method started ... ");
        //SomeMethod();
        //Console.WriteLine("Main Method end!");
        //Console.ReadKey();

        //5.2 Task
        Console.WriteLine("Main Started....");
        int result = await TinhTong(1, 2);
        //chay nhieu task cung mot luc
        Task<int> task1 = TinhTong(1, 2);
        Task<int> task2 = TinhTong(3, 4);
        int[] results = await Task.WhenAll(task1, task2);
        Console.WriteLine($"Ket qua: {results[0]} , Ket qua 2: {results[1]}");
        Console.WriteLine("Main end!");

        Console.WriteLine("Press enter to exit");
    }
    public static async Task<int> TinhTong(int a, int b)
    {
        Console.WriteLine("Tinh Tong .... ");
        await Task.Delay(5000);
        return a + b;
    }
    public async static Task DoWorkAsyn()
    {
        Console.WriteLine("Working....");
        await Task.Delay(5000);
        Console.WriteLine("Done!");
    }
    public async static void SomeMethod()
    {
        Console.WriteLine("SomeMethod started .... ");
        //Thread.Sleep(10000);
        await Task.Delay(10000);
        Console.WriteLine("SomeMethod end!");
    }
}