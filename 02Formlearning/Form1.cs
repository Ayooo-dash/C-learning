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

namespace _02Formlearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread th;
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            th = new Thread(GetNowTime);
            th.IsBackground = true;
            th.Start();
        }

        public void GetNowTime()
        {
            while (true)
            {
                DateTime t = DateTime.Now;
                lbTime.Text = "Time："+t.ToString();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if(rbStudent.Checked | rbTeacher.Checked)
            {
                if(textBox1.Text == "student" && textBox2.Text == "student" && rbStudent.Checked)
                {
                    MessageBox.Show("登录成功！");
                }
                else if(textBox1.Text == "teacher" && textBox2.Text == "teacher" && rbTeacher.Checked)
                {
                    MessageBox.Show("登录成功！");
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                    MessageBox.Show("登录失败，用户名、密码或登录身份错误，请重新尝试！");

                }
            }
            else
            {
                MessageBox.Show("请选择登录身份！");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(th != null)
            {
                th.Abort();
            }
        }
    }
}
