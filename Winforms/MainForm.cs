using _06GDI_Learning;
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

namespace Winforms
{
    public partial class MainForm : Form
    {
        public static Dictionary<Button, Form> dicForm = new Dictionary<Button, Form>();
        public static PicForm picForm = new PicForm();
        public static FileManagerForm frm2 = new FileManagerForm();
        public static LoginForm loginForm = new LoginForm();
        public static XMLForm xmlFrm = new XMLForm();
        public static PaintForm paintForm = new PaintForm();
        public static _10NoteBookLearning.Main_Form noteookForm = new _10NoteBookLearning.Main_Form();
        public static _03SocketLearning.Form1 socketServerForm = new _03SocketLearning.Form1();
        public static _04SocketClientLearning.Form1 socketClientForm = new _04SocketClientLearning.Form1();
        public static _07窗体飞行棋.StartForm gameStartForm = new _07窗体飞行棋.StartForm();
        public static _07窗体飞行棋.GameForm gameForm = new _07窗体飞行棋.GameForm();
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            dicForm.Add(btnPicForm, picForm);
            dicForm.Add(btnForm2, frm2);
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
            frm2.Show();
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
            frm2.tvFileManager.Refresh();
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
    }
}
