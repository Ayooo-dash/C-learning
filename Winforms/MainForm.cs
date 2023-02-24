using _06GDI_Learning;
using AutoFrame;
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

namespace Winforms
{
    public partial class MainForm : Form
    {
        public static Dictionary<Button, Form> dicForm = new Dictionary<Button, Form>();
        public static PicForm picForm = new PicForm();
        public static FileManagerForm fileManagerForm = new FileManagerForm();
        public static LoginForm loginForm = new LoginForm();
        public static XMLForm xmlFrm = new XMLForm();
        public static SQLiteForm sqliteFrm = new SQLiteForm();
        public static PaintForm paintForm = new PaintForm();
        public static _10NoteBookLearning.Main_Form noteookForm = new _10NoteBookLearning.Main_Form();
        public static _03SocketLearning.Form1 socketServerForm = new _03SocketLearning.Form1();
        public static _04SocketClientLearning.Form1 socketClientForm = new _04SocketClientLearning.Form1();
        public static _07窗体飞行棋.StartForm gameStartForm = new _07窗体飞行棋.StartForm();
        public static _07窗体飞行棋.GameForm gameForm = new _07窗体飞行棋.GameForm();

        public static string strPath = @"D:\log";

        public MainForm()
        {
            InitializeComponent();
            timer2.Enabled = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            dicForm.Add(btnPicForm, sqliteFrm);
            dicForm.Add(btnForm2, fileManagerForm);
            dicForm.Add(btnLoginForm, loginForm);
            dicForm.Add(btnForm3, xmlFrm);
            dicForm.Add(btnPaintForm, paintForm);
            dicForm.Add(btnNoteBookForm, noteookForm);
            dicForm.Add(btnSocketServerForm, socketServerForm);
            dicForm.Add(btnSocketClientForm, socketClientForm);
            dicForm.Add(btnGameStartForm, gameStartForm);
            dicForm.Add(btnGameForm, gameForm);
            foreach (KeyValuePair<Button, Form> kp in dicForm)
            {
                kp.Value.TopLevel = false;
                kp.Value.Parent = this.panel1;
                kp.Value.Dock = DockStyle.Fill;
                kp.Value.FormBorderStyle = FormBorderStyle.None;
            }
            gameForm.TopLevel = false;
            gameForm.Parent = this.panel1;
            gameForm.Dock = DockStyle.Fill;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            Control.CheckForIllegalCrossThreadCalls = false;
            sqliteFrm.Show();
            //loginForm.Show();
            //panel2.Enabled = false;
            //Thread th = new Thread(() =>
            // {
            //     while (true)
            //     {
            //         if (loginForm.flag == DialogResult.OK)
            //         {
            //             //picForm.Show();
            //             panel2.Enabled = true;
            //             break;
            //         }
            //     }
            // });
            //th.IsBackground = true;
            //th.Start();
        }

        public void SwitchBtn(Button btn)
        {
            foreach (KeyValuePair<Button, Form> kp in dicForm)
            {
                if (kp.Key == btn)
                {
                    if (btn == dicForm.ElementAt(9).Key)
                    {
                        if (dicForm.ElementAt(8).Value.DialogResult == DialogResult.OK)
                        {
                            kp.Value.Show();
                            this.Text = kp.Value.Text;
                        }
                        else
                            return;
                    }
                    else
                    {
                        kp.Value.Show();
                        this.Text = kp.Value.Text;
                    }
                    
                }
                else
                    kp.Value.Hide();
            }
        }
        private void BtnLoginForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnLoginForm);
        }

        private void BtnPicForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnPicForm);
        }
        private void BtnForm2_Click(object sender, EventArgs e)
        {
            //while (frm2.th.ThreadState != ThreadState.Stopped)
            //    ;
            SwitchBtn(btnForm2);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            fileManagerForm.tvFileManager.Refresh();
            //FileManagerForm.m mm = new FileManagerForm.m(frm2.LoadAllDirsFiles);
            //if (frm2.treeView1.InvokeRequired)
            //    frm2.treeView1.BeginInvoke(mm);
            //else
            //    frm2.LoadAllDirsFiles();
        }

        private void btnForm3_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnForm3);
        }

        private void btnPaintForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnPaintForm);
        }

        private void btnNoteBookForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnNoteBookForm);
        }

        private void btnSocketServerForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnSocketServerForm);
        }

        private void btnSocketClientForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnSocketClientForm);
        }

        private void btnGameForm_Click(object sender, EventArgs e)
        {
            SwitchBtn(btnGameStartForm);
        }

        private void btnGameForm_Click_1(object sender, EventArgs e)
        {
            SwitchBtn(btnGameForm);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                //应用程序要求关闭窗口
                case CloseReason.ApplicationExitCall:
                    WriteLog(e.CloseReason.ToString());
                    //e.Cancel = false; //不拦截，响应操作
                    break;
                //自身窗口上的关闭按钮
                case CloseReason.FormOwnerClosing:
                    WriteLog(e.CloseReason.ToString());
                    break;
                //MDI窗体关闭事件
                case CloseReason.MdiFormClosing:
                    WriteLog(e.CloseReason.ToString());
                    break;
                //用户通过UI关闭窗口或者通过Alt+F4关闭窗口
                case CloseReason.UserClosing:

                    if (MessageBox.Show("The application will be closed,do you want to continue?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;//拦截，不响应操作
                    }
                    WriteLog(e.CloseReason.ToString());
                    break;
                //不明原因的关闭
                case CloseReason.None:
                    WriteLog(e.CloseReason.ToString());
                    e.Cancel = true;
                    break;
                //任务管理器关闭进程
                case CloseReason.TaskManagerClosing:
                    //e.Cancel = false;//不拦截，响应操作
                    WriteLog(e.CloseReason.ToString());
                    break;

                //操作系统准备关机
                case CloseReason.WindowsShutDown:
                    //e.Cancel = false;//不拦截，响应操作
                    WriteLog(e.CloseReason.ToString());
                    break;
                default:
                    break;
            }
        }

        public static void WriteLog(string strLog)
        {
            string strDir = strPath;
            if (strDir == "")
            {
                return;
            }

            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }

            DateTime now = DateTime.Now;

            string FileName = strDir + "\\" + now.ToString("yyyy_MM_dd") + ".csv";

            CsvOperationEx csv = new CsvOperationEx();
            csv.BQuota = false;//保存上没有引号


            string[] Heads;
            
            Heads = new string[] { "Time", "User", "Info" };

            int col = 0;
            int row = 0;
            if (!File.Exists(FileName))
            {
                foreach (string str in Heads)
                {
                    csv[0, col++] = str;
                }
                col = 0;
                row++;
            }

            csv[row, 0] = now.ToString("HH:mm:ss");
            csv[row, 1] = System.Environment.UserName;
            csv[row, 2] = strLog;

            csv.Save(FileName);

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
