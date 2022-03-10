using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06GDI_Learning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread th;
        public void DrawVerifyCode()
        {
            Random r = new Random();
            Bitmap bmp = new Bitmap(120, 35);
            Graphics g = Graphics.FromImage(bmp);
            string str = null;
            for (int i = 0; i < 5; i++)
                str += r.Next(0, 10).ToString();
            for (int i = 0; i < 5; i++)
            {
                string[] fonts = { "微软雅黑", "宋体", "隶书", "楷书", "仿宋" };
                Color[] colors = { Color.Black, Color.Blue, Color.Green, Color.DarkRed, Color.YellowGreen };
                g.DrawString(str[i].ToString(), new Font(fonts[r.Next(0, 5)], r.Next(14, 24), FontStyle.Bold), new SolidBrush(colors[r.Next(0, 5)]), i * 20, 0);

            }
            for (int i = 0; i < 25; i++)
                g.DrawLine(new Pen(Brushes.Green), new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height)), new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height)));
            for (int i = 0; i < 500; i++)
                bmp.SetPixel(r.Next(0, bmp.Width), r.Next(0, bmp.Height), Color.Black);

            picBoxVerifyCode.Image = bmp;
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            th = new Thread(draw);
            th.IsBackground = true;
            th.Start();

        }
        public int x = 0, y = 10, i = 0;
        int h = 0, w = 0;
        Bitmap bmp = Properties.Resources.bg;
        public void draw()
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Brushes.Red);
            while (true)
            {
                for (int i = 0; i < Form1.ActiveForm.Size.Height; i++)
                {
                    if (i > h)
                        break;
                    for (int j = 0; j < Form1.ActiveForm.Size.Width; j++)
                    {
                        if (j < w)
                        {
                            pen.Color = bmp.GetPixel(j, i);
                            //btnDrawLine.Text = (~bmp.GetPixel(j, i).ToArgb()).ToString();
                            g.DrawLine(pen, j, i, j + 1, i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (th != null)
                th.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DrawVerifyCode();
            CheckForIllegalCrossThreadCalls = false;
            pictureBox1.Hide();
            h = bmp.Height;
            w = bmp.Width;
        }

        private void picBoxVerifyCode_Click(object sender, EventArgs e)
        {
            //DrawVerifyCode();
        }

        private void btnNextCode_Click(object sender, EventArgs e)
        {
            //DrawVerifyCode();
        }
    }
}
