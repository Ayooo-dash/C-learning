using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06GDI_Learning
{
    public partial class PaintForm : Form
    {
        Pen pen = new Pen(Brushes.Red);
        Graphics g, g1;
        Bitmap bmp;
        Point mousePoint = new Point()/*List<Point>()*/;
        Point mousePoint1 = new Point()/*List<Point>()*/;
        List<Point> mp = new List<Point>();
        int i = 0;
        MouseButtons mouseBtn;
        Thread th;
        public PaintForm()
        {
            InitializeComponent();
        }

        public void s()
        {
            while (mouseBtn == MouseButtons.Left)
            {
                DrawLine(g, pen, mousePoint1, mousePoint);
                DrawLine(g1, pen, mousePoint1, mousePoint);
            }
        }

        public void DrawLine(Graphics g, Pen pen, Point start, Point end)
        {
            try
            {
                g.DrawLine(pen, start.X - 1, start.Y, end.X - 1, end.Y);
                g.DrawLine(pen, start.X + 1, start.Y, end.X + 1, end.Y);
                g.DrawLine(pen, start.X, start.Y - 1, end.X, end.Y - 1);
                g.DrawLine(pen, start.X, start.Y + 1, end.X, end.Y + 1);
                g.DrawLine(pen, start.X, start.Y, end.X, end.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PaintPanel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }

        private void PaintPanel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void PaintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseBtn = e.Button;
            mousePoint1 = e.Location;
            mousePoint = e.Location;
            mp.Add(e.Location);
            th = new Thread(s);
            th.IsBackground = true;
            th.Start();
        }

        private void PaintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseBtn = 0;
        }

        private void PaintPanel_SizeChanged(object sender, EventArgs e)
        {
            g.DrawImage(bmp, 0, 0);
            g1.DrawImage(bmp, 0, 0);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有文件|*.*|BMP图像|*.bmp";
            ofd.InitialDirectory = @"C:\Users\admin\Pictures";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                Bitmap bmp1 = new Bitmap(ofd.FileName);
                g.DrawImage(bmp1, 0, 0);
                g1.DrawImage(bmp1, 0, 0);
                bmp1.Dispose();
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            g1.Clear(Color.White);
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "所有文件|*.*|BMP图像 | *.bmp";
            sfd.InitialDirectory = @"C:\Users\admin\Pictures";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                bmp.Save(sfd.FileName, ImageFormat.Bmp);
            }
        }

        private void PaintForm_Load(object sender, EventArgs e)
        {
            g = PaintPanel.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            bmp = new Bitmap(PaintPanel.Width, PaintPanel.Height, g);
            g1 = Graphics.FromImage(bmp);
            g1.SmoothingMode = SmoothingMode.HighQuality;
            g1.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), bmp.Size));
            PaintPanel.BackColor = Color.White;
            PaintPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PaintPanel_Paint(object sender, PaintEventArgs e)
        {
            g.DrawImage(bmp, 0, 0);
            g1.DrawImage(bmp, 0, 0);
        }
        

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Image img = Properties.Resources.bg;
            //img.Save(@"C:\Users\admin\Pictures\bg.png", ImageFormat.Png);
            g1.FillRegion(Brushes.White, new Region(new Rectangle(new Point(0, 0), bmp.Size)));
            //g1.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), bmp.Size));
            g.DrawImage(bmp, 0, 0);
            g1.DrawImage(bmp, 0, 0);
        }

        private void PaintPanel_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            string[] data =(string[])e.Data.GetData(DataFormats.FileDrop, true);
            //bmp = new Bitmap(data[0]);
            g.DrawImage(new Bitmap(data[0]), 0, 0);
            g1.DrawImage(new Bitmap(data[0]), 0, 0);
        }

        private void PaintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseBtn == MouseButtons.Left)
            {
                mp.Add(e.Location);
                mousePoint1 = mousePoint;
                mousePoint = e.Location;
            }
            else
            {
                mp.Add(new Point(0, 0));
            }
        }
    }
}
