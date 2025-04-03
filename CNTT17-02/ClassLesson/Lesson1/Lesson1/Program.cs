//1.Khai bao bien
int x;
x = 3;
Console.WriteLine("x = " + x);


int? x; //x co the chua null
x = null;
Console.WriteLine("x = " + x);

//2. Toan tu hop nhat null
int? a = null;
int b = a ?? 10;// neu a = null, thi b = 10
Console.WriteLine("x = " + x);


//3. Chuoi noi suy
string ten = "Vuong";
int tuoi = 18;
string s = $"Chao ban {ten}, ban da {tuoi} tuoi";
Console.WriteLine(s);
