using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _08MD5Learning
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetMD5("123"));
            Console.ReadKey();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] md5buf = md5.ComputeHash(Encoding.Default.GetBytes(str));
            string md5str = "";
            for (int i = 0; i < md5buf.Length; i++)
                md5str += md5buf[i].ToString("x2");
            return md5str;
        }
    }
}
