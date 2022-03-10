using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07窗体飞行棋
{
    /// <summary>
    /// GameForm窗体
    /// </summary>
    public partial class GameForm : Form
    {
        #region 字段
        /// <summary>
        /// 100个地图关卡按钮
        /// </summary>
        public static Button[] btns = new Button[100];
        /// <summary>
        /// 重置地图按钮
        /// </summary>
        public static Button btnRst = new Button();
        /// <summary>
        /// lbMsg行动过程信息label、lbshow顶部关卡图标说明label、lbplayer玩家名称显示label
        /// </summary>
        public static Label lbMsg = new Label(), lbshow = new Label(), lbplayer = new Label();
        /// <summary>
        /// 骰子图片PictureBox
        /// </summary>
        public static PictureBox PicBoxTouzi = new PictureBox();
        /// <summary>
        /// 自定义玩家类  A、B
        /// </summary>
        public static Player A, B;
        /// <summary>
        /// 地图关卡参数
        /// </summary>
        public static int[] Maps = new int[100];
        /// <summary>
        /// 随机数生成器r
        /// </summary>
        public static Random r = new Random();
        /// <summary>
        /// 定时器事件计数tim
        /// </summary>
        public static int tim = 0;
        /// <summary>
        /// 玩家位置动态移动线程
        /// </summary>
        public static Thread th;
        #endregion

        /// <summary>
        /// GameForm构造函数
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// GameForm窗体加载时运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;    //取消跨线程访问
            timer1.Enabled = false;     //定时器关闭
            Maps_Init();    //地图参数初始化
            ButtonDraw();   //关卡按钮控件绘制
            LabelDraw();    //各label控件绘制
            PicBoxDraw();   //骰子图片控件绘制
            DrawMaps();     //地图绘制
        }

        /// <summary>
        /// 按钮控件设置
        /// </summary>
        public void ButtonDraw()
        {
            #region 地图关卡按钮初始化
            //第一行按钮初始化
            for (int i = 0; i <= 29; i++)
            {
                btns[i] = new Button();
                btns[i].Text = "";
                btns[i].Font = new Font("微软雅黑", 15.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btns[i].AutoSize = false;
                btns[i].Location = new Point(12 + 45 * i + 6 * i, 102);
                btns[i].Size = new Size(45, 45);
                btns[i].TextAlign = ContentAlignment.TopCenter;
                btns[i].UseVisualStyleBackColor = true;
                btns[i].BackColor = Color.White;
                btns[i].ForeColor = Color.Black;
                btns[i].Parent = this;
                // btns[i].Click += new System.EventHandler(this.btns_Click);
                this.Controls.Add(btns[i]);
            }

            //第一列按钮初始化
            for (int i = 30; i <= 34; i++)
            {
                btns[i] = new Button();
                btns[i].Text = "";
                btns[i].Font = new Font("微软雅黑", 15.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btns[i].Location = new Point(1491, 153 + 45 * (i - 30) + 6 * (i - 30));
                btns[i].Size = new Size(45, 45);
                btns[i].TextAlign = ContentAlignment.TopCenter;
                btns[i].UseVisualStyleBackColor = true;
                btns[i].AutoSize = false;
                btns[i].BackColor = Color.White;
                btns[i].ForeColor = Color.Black;
                btns[i].Parent = this;
                // btns[i].Click += new System.EventHandler(this.btns_Click);
                this.Controls.Add(btns[i]);
            }

            //第二行按钮初始化
            for (int i = 35; i <= 64; i++)
            {
                btns[i] = new Button();
                btns[i].Text = "";
                btns[i].Size = new Size(45, 45);
                btns[i].Location = new Point(1491 - 6 * (i - 35) - 45 * (i - 35), 357 + 45 + 6);
                btns[i].Font = new Font("微软雅黑", 15.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btns[i].TextAlign = ContentAlignment.TopCenter;
                btns[i].UseVisualStyleBackColor = true;
                btns[i].AutoSize = false;
                btns[i].BackColor = Color.White;
                btns[i].ForeColor = Color.Black;
                btns[i].Parent = this;
                // btns[i].Click += new System.EventHandler(this.btns_Click);
                this.Controls.Add(btns[i]);
            }

            //第二列按钮初始化
            for (int i = 65; i <= 69; i++)
            {
                btns[i] = new Button();
                btns[i].Text = "";
                btns[i].Size = new Size(45, 45);
                btns[i].Location = new Point(12, 459 + 45 * (i - 65) + 6 * (i - 65));
                btns[i].Font = new Font("微软雅黑", 15.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btns[i].TextAlign = ContentAlignment.TopCenter;
                btns[i].UseVisualStyleBackColor = true;
                btns[i].AutoSize = false;
                btns[i].BackColor = Color.White;
                btns[i].ForeColor = Color.Black;
                btns[i].Parent = this;
                // btns[i].Click += new System.EventHandler(this.btns_Click);
                this.Controls.Add(btns[i]);
            }

            //第三行按钮初始化
            for (int i = 70; i <= 99; i++)
            {
                btns[i] = new Button();
                btns[i].Text = "";
                btns[i].Size = new Size(45, 45);
                btns[i].Location = new Point(12 + 45 * (i - 70) + 6 * (i - 70), 663+45+6);
                btns[i].Font = new Font("微软雅黑", 15.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                btns[i].TextAlign = ContentAlignment.TopCenter;
                btns[i].UseVisualStyleBackColor = true;
                btns[i].AutoSize = false;
                btns[i].BackColor = Color.White;
                btns[i].ForeColor = Color.Black;
                btns[i].Parent = this;
                // btns[i].Click += new System.EventHandler(this.btns_Click);
                this.Controls.Add(btns[i]);
            }
            #endregion
            #region 玩家骰子按钮初始化
            A.btnTouzi.Text = "A";
            A.btnTouzi.Font = new Font("微软雅黑", 12.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            A.btnTouzi.AutoSize = false;
            A.btnTouzi.Location = new Point(500, 215);
            A.btnTouzi.Size = new Size(50, 50);
            A.btnTouzi.TextAlign = ContentAlignment.MiddleCenter;
            A.btnTouzi.UseVisualStyleBackColor = true;
            //A.btnTouzi.BackColor = Color.White;
            //A.btnTouzi.ForeColor = Color.Black;
            A.btnTouzi.Parent = this;
            A.btnTouzi.Click += new System.EventHandler(this.btnTouziA_Click);
            this.Controls.Add(A.btnTouzi);
            B.btnTouzi.Text = "B";
            B.btnTouzi.Font = new Font("微软雅黑", 12.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            B.btnTouzi.AutoSize = false;
            B.btnTouzi.Location = new Point(750, 215);
            B.btnTouzi.Size = new Size(50, 50);
            B.btnTouzi.TextAlign = ContentAlignment.MiddleCenter;
            B.btnTouzi.UseVisualStyleBackColor = true;
            //B.btnTouzi.BackColor = Color.White;
            //B.btnTouzi.ForeColor = Color.Black;
            B.btnTouzi.Parent = this;
            B.btnTouzi.Click += new System.EventHandler(this.btnTouziB_Click);
            this.Controls.Add(B.btnTouzi);
            A.btnTouzi.Enabled = true;
            B.btnTouzi.Enabled = false;
            #endregion
            #region 重新开局/更换地图按钮
            //btnRst.Text = "重置地图";
            //btnRst.Font = new Font("宋体", 12.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            //btnRst.AutoSize = false;
            //btnRst.Location = new Point(1111, 30);
            //btnRst.Size = new Size(100, 40);
            //btnRst.TextAlign = ContentAlignment.MiddleCenter;
            //btnRst.UseVisualStyleBackColor = true;
            ////btnRst.BackColor = Color.White;
            ////btnRst.ForeColor = Color.Black;
            //btnRst.Parent = this;
            //btnRst.Click += new System.EventHandler(this.btnRst_Click);
            //this.Controls.Add(btnRst);
            #endregion
        }

        /// <summary>
        /// Label控件设置
        /// </summary>
        public void LabelDraw()
        {
            //动作过程信息显示label
            lbMsg.Text = "玩家" + A.Name + "先手。";
            lbMsg.Font = new Font("宋体", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            lbMsg.AutoSize = true;
            lbMsg.Location = new Point(835, 215);
            lbMsg.Size = new Size(55, 15);
            lbMsg.TextAlign = ContentAlignment.TopLeft;
            lbMsg.Parent = this;
            this.Controls.Add(lbMsg);

            //顶部关卡图标及说明label
            lbshow.Text = "※：起点；🏁：终点；⚔：A与B处于同一位置；☠：陷阱，后退随机格\r\n❂：幸运轮盘，可再掷一次骰子；🛫：坐飞机，前进随机格；☢：地雷，暂停一个回合";
            lbshow.Font = new Font("宋体", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            lbshow.AutoSize = true;
            lbshow.Location = new Point(20, 20);
            lbshow.Size = new Size(55, 15);
            lbshow.TextAlign = ContentAlignment.TopLeft;
            lbshow.Parent = this;
            this.Controls.Add(lbshow);

            //玩家名称显示label
            lbplayer.Text = "玩家A：" + A.Name + "\r\n玩家B：" + B.Name;
            lbplayer.Font = new Font("宋体", 17F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            lbplayer.AutoSize = true;
            lbplayer.Location = new Point(185, 215);
            lbplayer.Size = new Size(55, 15);
            lbplayer.TextAlign = ContentAlignment.TopLeft;
            lbplayer.Parent = this;
            this.Controls.Add(lbplayer);
        }

        /// <summary>
        /// 骰子图片控件设置
        /// </summary>
        public void PicBoxDraw()
        {
            //骰子图片显示控件
            //PicBoxTouzi.Anchor = AnchorStyles.None;
            PicBoxTouzi.Image = Properties.Resources.touzi0;
            PicBoxTouzi.Location = new Point(600, 190);
            PicBoxTouzi.Name = "PicBoxTouzi";
            PicBoxTouzi.Size = new Size(100, 100);
            PicBoxTouzi.SizeMode = PictureBoxSizeMode.AutoSize;
            PicBoxTouzi.TabIndex = 1;
            PicBoxTouzi.TabStop = false;
            this.Controls.Add(PicBoxTouzi);
        }

        /// <summary>
        /// 地图参数初始化
        /// </summary>
        public static void Maps_Init()
        {
            //随机初始化地图关卡参数
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
        /// 地图参数对应关卡图标字符
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawIcon(int i)
        {
            string str = " ";
            if (A.Pos == B.Pos && i == A.Pos)
            {
                str = "⚔";
            }
            else if (i == A.Pos)
            {
                str = "A";
            }
            else if (i == B.Pos)
            {
                str = "B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        str = "※";
                        break;
                    case 1:
                        str = "☠";
                        break;
                    case 2:
                        str = "☢";
                        break;
                    case 3:
                        str = "🛫";
                        break;
                    case 4:
                        str = "❂";
                        break;
                    case 5:
                        str = " ";
                        break;
                    case 6:
                        str = "🏁";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 绘制按钮地图
        /// </summary>
        public static void DrawMaps()
        {
            for (int i = 0; i < 100; i++)
            {
                GameForm.btns[i].Text = GameForm.DrawIcon(i);
                if (GameForm.btns[i].Text.Contains("A"))
                {
                    GameForm.btns[i].BackColor = Color.MidnightBlue;
                    GameForm.btns[i].ForeColor = Color.White;
                }
                else if (GameForm.btns[i].Text.Contains("B"))
                {
                    GameForm.btns[i].BackColor = Color.DarkRed;
                    GameForm.btns[i].ForeColor = Color.White;
                }
                else if (GameForm.btns[i].Text.Contains("⚔"))
                {
                    GameForm.btns[i].BackColor = Color.LightSeaGreen;
                    GameForm.btns[i].ForeColor = Color.White;
                }
                else
                {
                    GameForm.btns[i].BackColor = Color.White;
                    GameForm.btns[i].ForeColor = Color.Black;
                }
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        /// <summary>
        /// 玩家A掷骰子按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTouziA_Click(object sender, EventArgs e)
        {
            if (A.Pos < 99 && B.Pos < 99)
            {
                A.btnTouzi.Enabled = false;
                B.btnTouzi.Enabled = false;
                A.Touzi = r.Next(1, 7);
                switch (A.Touzi)
                {
                    case 1:
                        PicBoxTouzi.Image = Properties.Resources.touzi1;
                        break;
                    case 2:
                        PicBoxTouzi.Image = Properties.Resources.touzi2;
                        break;
                    case 3:
                        PicBoxTouzi.Image = Properties.Resources.touzi3;
                        break;
                    case 4:
                        PicBoxTouzi.Image = Properties.Resources.touzi4;
                        break;
                    case 5:
                        PicBoxTouzi.Image = Properties.Resources.touzi5;
                        break;
                    case 6:
                        PicBoxTouzi.Image = Properties.Resources.touzi6;
                        break;
                }
                th = new Thread(A.PlayerWalk);
                th.IsBackground = true;
                th.Start(B);
                timer1.Enabled = true;
            }
        }

        /// <summary>
        /// 玩家B掷骰子按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTouziB_Click(object sender, EventArgs e)
        {
            if (A.Pos < 99 && B.Pos < 99)
            {
                A.btnTouzi.Enabled = false;
                B.btnTouzi.Enabled = false;
                B.Touzi = r.Next(1, 7);
                switch (B.Touzi)
                {
                    case 1:
                        PicBoxTouzi.Image = Properties.Resources.touzi1;
                        break;
                    case 2:
                        PicBoxTouzi.Image = Properties.Resources.touzi2;
                        break;
                    case 3:
                        PicBoxTouzi.Image = Properties.Resources.touzi3;
                        break;
                    case 4:
                        PicBoxTouzi.Image = Properties.Resources.touzi4;
                        break;
                    case 5:
                        PicBoxTouzi.Image = Properties.Resources.touzi5;
                        break;
                    case 6:
                        PicBoxTouzi.Image = Properties.Resources.touzi6;
                        break;
                }
                th = new Thread(B.PlayerWalk);
                th.IsBackground = true;
                th.Start(A);
                timer1.Enabled = true;
            }
        }

        //private void btnRst_Click(object sender, EventArgs e)
        //{
        //    timer1.Enabled = false;
        //    Maps_Init();
        //    A = new Player(A.Name, 0, false, false);
        //    B = new Player(B.Name, 0, false, false);
        //    A.btnTouzi.Enabled = true;
        //    B.btnTouzi.Enabled = false;
        //    DrawMaps();
        //}

        /// <summary>
        /// 定时器触发事件(100ms)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            tim++;
            if (tim == 17)
                timer1.Enabled = false;
        }
    }

    /// <summary>
    /// 自定义玩家类
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 标记玩家赢家
        /// </summary>
        private bool winner;
        /// <summary>
        /// 标记暂停及再次回合
        /// </summary>
        private bool flag;
        /// <summary>
        /// 玩家名
        /// </summary>
        private string name;
        /// <summary>
        /// 玩家位置及骰子数
        /// </summary>
        private int pos, touzi;
        /// <summary>
        /// 玩家掷骰子按钮
        /// </summary>
        public Button btnTouzi = new Button();
        /// <summary>
        /// Name属性
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 位置属性
        /// </summary>
        public int Pos
        {
            get { return this.pos; }
            set { this.pos = value; }
        }
        /// <summary>
        /// 骰子数属性
        /// </summary>
        public int Touzi
        {
            get { return this.touzi; }
            set { this.touzi = value; }
        }
        /// <summary>
        /// 赢家属性
        /// </summary>
        public bool Winner
        {
            get { return this.winner; }
            set { this.winner = value; }
        }
        /// <summary>
        /// 暂停及再掷标记属性
        /// </summary>
        public bool Flag
        {
            get { return this.flag; }
            set { this.flag = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="n">玩家名字</param>
        /// <param name="p">位置</param>
        /// <param name="w">赢家标记</param>
        /// <param name="f">暂停标记</param>
        public Player(string n, int p, bool w, bool f)
        {
            this.name = n;
            this.Pos = p;
            this.winner = w;
            this.flag = f;

            ////btns[i].Anchor = AnchorStyles.None;
            //btnTouzi.Text = "A";
            //btnTouzi.Font = new Font("微软雅黑", 12.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            //btnTouzi.AutoSize = false;
            //btnTouzi.Location = new Point(x, y);
            //btnTouzi.Size = new Size(35, 35);
            //btnTouzi.TextAlign = ContentAlignment.MiddleCenter;
            //btnTouzi.UseVisualStyleBackColor = true;
            //btnTouzi.BackColor = Color.White;
            //btnTouzi.ForeColor = Color.Black;
            //btnTouzi.Parent = z as GameForm1;
        }
        /// <summary>
        /// 检查位置并修正
        /// </summary>
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
        /// <summary>
        /// 玩家移动
        /// </summary>
        /// <param name="o">另一名玩家的类</param>
        public void PlayerWalk(object o)
        {
            Player player = o as Player;
            while (GameForm.tim < 17)
                ;
            GameForm.lbMsg.Text = "玩家" + this.name + "掷出了" + this.touzi.ToString() + "前进" + this.touzi.ToString() + "格\r\n";
            int temp = this.pos + this.touzi;
            do
            {
                while (true)
                {
                    this.Checkpos();
                    if (this.pos == 99)
                    {
                        this.winner = true;
                        GameForm.lbMsg.Text += "玩家" + this.name + "获得了胜利！\r\n";
                        this.name += "🏆";
                        MessageBox.Show("恭喜玩家" + this.name + "获得了胜利！");
                        break;
                    }
                    if (temp == this.pos)
                    {
                        break;
                    }
                    if (this.pos < temp)
                        this.pos++;
                    if (this.pos > temp)
                        this.pos--;
                    this.Checkpos();
                    GameForm.DrawMaps();
                    Thread.Sleep(600);
                    if(this.pos == 0)
                    {
                        break;
                    }
                }
                GameForm.tim = 0;
                switch (GameForm.Maps[this.pos])
                {
                    case 1:
                        temp = GameForm.r.Next(1, 7);
                        GameForm.lbMsg.Text += "玩家" + this.name + "踩中陷阱，随机倒退" + temp.ToString() + "格\r\n";
                        temp = this.pos - temp;
                        break;
                    case 2:
                        this.flag = true;
                        //player.Flag = false;
                        GameForm.lbMsg.Text += "玩家" + this.name + "踩中地雷，暂停一个回合";
                        if(player.Flag == true)
                        {
                            player.Flag = false;
                            this.flag = false;
                        }
                        this.btnTouzi.Enabled = false;
                        player.btnTouzi.Enabled = true;
                        break;
                    case 3:
                        temp = GameForm.r.Next(1, 7);
                        GameForm.lbMsg.Text = "玩家" + this.name + "乘坐飞机，随机前进" + temp.ToString() + "格\r\n";
                        temp = this.pos + temp;
                        break;
                    case 4:
                        this.flag = false;
                        GameForm.lbMsg.Text += "玩家" + this.name + "踩中幸运轮盘，可再掷一个骰子\r\n";
                        this.btnTouzi.Enabled = true;
                        player.btnTouzi.Enabled = false;
                        break;
                    case 5:
                        GameForm.lbMsg.Text += "玩家" + this.name + "进入安全区，安全\r\n";
                        if (player.Flag == true)
                        {
                            this.btnTouzi.Enabled = true;
                            player.btnTouzi.Enabled = false;
                            player.Flag = false;
                        }
                        else
                        {
                            this.btnTouzi.Enabled = false;
                            player.btnTouzi.Enabled = true;
                        }
                        break;
                    case 6:
                        this.btnTouzi.Enabled = false;
                        player.btnTouzi.Enabled = false;
                        break;
                    default:
                        this.btnTouzi.Enabled = false;
                        player.btnTouzi.Enabled = true;
                        break;
                }
                this.Checkpos();
            } while (GameForm.Maps[this.Pos] == 1 || GameForm.Maps[this.Pos] == 3);
        }
    }
}
