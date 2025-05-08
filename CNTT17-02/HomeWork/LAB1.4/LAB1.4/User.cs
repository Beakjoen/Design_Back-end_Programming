using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB2;

namespace LAB2
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public string ToString()
        {
            return $"{id}\t\t{username}\t\t{password}\t\t{email}\t\t{phone}";
        }
    }
}
//Bước 3: Tạo Mock API 
//Truy cập vào https://mockapi.io/ => Thêm resource User gồm các trường thông tin: id, username, password, email, phone => Copy url get data 
