using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01learning
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str;
            //Person1 p1 = new Person1();
            Person2 p2 = new Person2();
            student st = new student();
            st.kou();
            //Process.Start(@"C:\Windows\System32\telnet.exe");
            Console.WriteLine("Please input one's information:");
            Console.Write("Name:");
            p2.name = Console.ReadLine();
            Console.Write("Age:");
            try
            {
                p2.age = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error input,please try again:");
                Console.Write("Age:");
                p2.age = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Gender:");
            try
            {


                p2.Gender = Convert.ToChar(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error input,please try again:");
                Console.Write("Gender:");
                p2.Gender = Convert.ToChar(Console.ReadLine());
            }
            Console.Write("Birthday:");
            p2.Birthday = Console.ReadLine();
            p2.Introduction();
            Console.ReadKey();
            Console.Clear();
            using (FileStream fsread = new FileStream(@"E:\GZY\README.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];//定义文件缓存区
                int r = fsread.Read(buffer, 0, buffer.Length);
                string str = Encoding.UTF8.GetString(buffer,0,r);
                Console.WriteLine(str);
                using (FileStream fswrite = new FileStream(@"E:\GZY\RE.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fswrite.Write(buffer, 0, r);
                }
                Console.ReadKey();
            }
        }
        public struct Person1
        {
            public string name;
            public int age;
            public string short_num;
            public void show()
            {
                Console.WriteLine("我叫{0}，今年{1}岁，我的短号是{2}",this.name,this.age,this.short_num);
            }
        }
    }

    public class Person2
    {
        public string name;
        public int age;
        private char gender;
        private string birthday;
        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public void Introduction()
        {
            Console.WriteLine("Hello,大家好，我叫{0}，我是一名{1}生，我今年{2}岁了，我是{3}出生的。", this.name, this.Gender, this.age, this.Birthday);
        }
    }

    public class NBA
    {
        public void kou()
        {
            Console.WriteLine("koulan");
        }
    }

    public interface Ikoulanable
    {
        void kou();
    }
    public class student : Person2, Ikoulanable
    {
        public void kou()
        {
            Console.WriteLine("koulan");
        }
    }
}
