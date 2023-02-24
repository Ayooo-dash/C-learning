using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms
{
    public partial class LoginForm : Form
    {
        public enum loginID
        {
            None = 0,
            Visitor,
            User,
            Admin,
        }
        public loginID id = loginID.Visitor;
        public DialogResult flag;
        //public delegate void 
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (rdBtnVisitor.Checked | rdBtnUser.Checked | rdBtnAdmin.Checked)
            {
                if (txtBoxUserName.Text == "1" && txtBoxPassword.Text == "1" && rdBtnUser.Checked)
                {
                    id = loginID.User;
                    if ((flag = MessageBox.Show("登录成功！")) == DialogResult.OK)
                    {
                        this.Hide();
                        MainForm.picForm.Show();
                    }
                }
                else if (txtBoxUserName.Text == "1" && txtBoxPassword.Text == "1" && rdBtnAdmin.Checked)
                {
                    id = loginID.Admin;
                    if ((flag = MessageBox.Show("登录成功！")) == DialogResult.OK)
                    {
                        this.Hide();
                        MainForm.picForm.Show();
                    }
                }
                else if (txtBoxUserName.Text == "" && txtBoxPassword.Text == "" && rdBtnVisitor.Checked)
                {
                    id = loginID.Visitor;
                    if ((flag = MessageBox.Show("登录成功！")) == DialogResult.OK)
                    {
                        this.Hide();
                        MainForm.picForm.Show();
                    }
                }
                else
                {
                    //txtBoxUserName.Clear();
                    //txtBoxPassword.Clear();
                    //txtBoxUserName.Focus();
                    MessageBox.Show("登录失败，用户名、密码或登录身份错误，请重新尝试！");

                }
            }
            else
            {
                MessageBox.Show("请选择登录身份！");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            rdBtnVisitor.Select();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            txtBoxUserName.Clear();
            txtBoxPassword.Clear();
            rdBtnVisitor.Select();
            id = loginID.None;
        }
    }
}
