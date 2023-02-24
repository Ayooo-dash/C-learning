




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05飞行棋游戏
{
    class Program
    {
        static int[] Maps = new int[100];
        static Random r = new Random();
        static Player A, B;

        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GameShow();
            Console.Clear();
            Console.SetWindowSize(60, 30);
            PlayerInit();
            Console.Clear();
            Maps_Init();
            DrawMaps();
            while (A.Pos < 99 && B.Pos < 99)
            {
                if (A.Flag == false)
                {
                    A.Playgame(Maps);
                    while (Maps[A.Pos] == 4 && A.Flag == false)
                    {
                        A.Playgame(Maps);
                    }
                }
                else
                {
                    A.Flag = false;
                }
                if (B.Flag == false && A.Winner == false)
                {
                    B.Playgame(Maps);
                    while (Maps[B.Pos] == 4 && B.Flag == false)
                    {
                        B.Playgame(Maps);
                    }
                }
                else
                {
                    B.Flag = false;
                }
            }
            Console.ReadKey();
        }
        /// <summary>
        /// 开始显示
        /// </summary>
        public static void GameShow()
        {
            Console.SetWindowSize(38, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************飞行棋游戏**************");
            Console.WriteLine("***********按任意键开始游戏***********");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**************************************");
            Console.ReadKey();
        }
        /// <summary>
        /// 随机初始化地图参数
        /// </summary>
        public static void Maps_Init()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 0 | i == 99)
                {
                    Maps[i] = (i == 0) ? 0 : 6;
                    continue;
                }
                Maps[i] = r.Next(1, 6);
            }
        }
        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMaps()
        {
            #region 顶部显示
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("************************************************************");
            Console.WriteLine("*                        飞行棋游戏                        *");
            Console.WriteLine("************************************************************");
            Console.WriteLine("|起点<> | 终点※ | 陷阱ㄨ：后退随机格 | 方块■：安全区 |\r\n|地雷▲：暂停一回合 | 传送卐：前进随机格 |\r\n|幸运轮盘★：增加一次骰子机会  |");
            Console.WriteLine();
            #endregion
            #region 画地图第一行
            for (int i = 0; i <= 29; i++)
            {
                Console.Write(DrawIcon(i));
            }
            Console.WriteLine();
            #endregion
            #region 地图第一列
            for (int i = 30; i <= 34; i++)
            {
                for (int j = 0; j < 29; j++)
                    Console.Write("  ");
                Console.Write(DrawIcon(i));
                Console.WriteLine();
            }
            #endregion
            #region 地图第二行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawIcon(i));
            }
            Console.WriteLine();
            #endregion
            #region 地图第二列
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawIcon(i));
            }
            #endregion
            #region 地图第三行
            for (int i = 70; i <= 99; i++)
                Console.Write(DrawIcon(i));
            Console.WriteLine();
            #endregion
        }
        /// <summary>
        /// 画各关卡图标
        /// </summary>
        /// <param name="i">地图位置</param>
        /// <returns></returns>
        public static string DrawIcon(int i)
        {
            string str = " ";
            if (A.Pos == B.Pos && i == A.Pos)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                str = "<>";
            }
            else if (i == A.Pos)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                str = "A.";
            }
            else if (i == B.Pos)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                str = "B.";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "->";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "ㄨ";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "▲";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "卐";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "★";
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "■";
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "※";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        /// <summary>
        /// 角色初始化
        /// </summary>
        public static void PlayerInit()
        {
            string str = " ";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************************************************");
            Console.WriteLine("*                        飞行棋游戏                        *");
            Console.WriteLine("************************************************************");
            Console.WriteLine("请输入玩家A名字：");
            str = Console.ReadLine();
            while (str == "")
            {
                Console.WriteLine("玩家名不能为空，请重新输入！");
                Console.WriteLine("请输入玩家A名字：");
                str = Console.ReadLine();
            }
            A = new Player(str, 0, false);
            Console.WriteLine("请输入玩家B名字：");
            str = Console.ReadLine();
            while (str == "" || str == A.Name)
            {
                if (str == "")
                {
                    Console.WriteLine("玩家名不能为空，请重新输入！");
                    Console.WriteLine("请输入玩家B名字：");
                    str = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的名字不能与玩家A相同，请重新输入！");
                    Console.WriteLine("请输入玩家B名字：");
                    str = Console.ReadLine();
                }
            }
            B = new Player(str, 0, false);
            Console.WriteLine("按任意键开始游戏");
            Console.ReadKey();
        }
    }

    public class Player
    {
        Random r1 = new Random();
        int temp;
        private bool winner, flag;
        private string name;
        private int pos;
        private int touzi;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int Pos
        {
            get { return this.pos; }
            set { this.pos = value; }
        }
        public int Touzi
        {
            get { return this.touzi; }
            //set { touzi = value; }
        }
        public bool Winner
        {
            get { return this.winner; }
        }
        public bool Flag
        {
            get { return this.flag; }
            set { this.flag = value; }
        }

        public Player(string n, int p, bool w)
        {
            this.Name = n;
            this.Pos = p;
            this.winner = w;
        }

        public void Checkpos()
        {
            if (this.pos < 0)
            {
                this.pos = 0;
            }
            if (this.pos >= 99)
            {
                this.pos = 99;
            }
        }

        public void Playgame(int[] Maps)
        {
            Console.WriteLine("玩家{0}按任意键开始掷骰子", this.name);
            Console.ReadKey();
            this.touzi = r1.Next(1, 7);
            Console.WriteLine("玩家{0}掷出了{1}", this.name, this.touzi);
            this.pos += this.touzi;
            Checkpos();
            Console.ReadKey();
            Console.WriteLine("玩家{0}按任意键开始行动", this.name);
            Console.ReadKey();
            Console.Clear();
            Program.DrawMaps();
            Console.WriteLine("玩家{0}行动完成", this.name);
            Console.ReadKey();
            do
            {
                switch (Maps[pos])
                {
                    case 1:
                        temp = r1.Next(1, 7);
                        Console.WriteLine("玩家{0}进入陷阱，随机后退{1}格", this.name, temp);
                        this.pos -= temp;
                        temp = 1;
                        Console.ReadKey();
                        break;
                    case 2:
                        this.flag = true;
                        //Program.flag[1] = false;
                        Console.WriteLine("玩家{0}踩中地雷，暂停一个回合", this.name);
                        temp = 0;
                        Console.ReadKey();
                        break;
                    case 3:
                        temp = r1.Next(1, 7);
                        Console.WriteLine("玩家{0}进入传送地，随机前进{1}", this.name, temp);
                        this.pos += temp;
                        temp = 1;
                        Console.ReadKey();
                        break;
                    case 4:
                        this.flag = false;
                        Console.WriteLine("玩家{0}踩中幸运轮盘，可再掷一个骰子", this.name);
                        temp = 0;
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine("玩家{0}踩中方块，安全", this.name);
                        temp = 0;
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                Checkpos();
            } while ((Maps[pos] == 1 || Maps[pos] == 2 || Maps[pos] == 3 || Maps[pos] == 4) && temp == 1);
            Console.Clear();
            Program.DrawMaps();
            if (pos == 99)
            {
                Console.WriteLine("玩家{0}赢了", this.name);
                winner = true;
                Console.ReadKey();
            }
        }
    }
}
