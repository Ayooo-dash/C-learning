using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07窗体飞行棋
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPlayerA.Text != "" && txtPlayerB.Text != "" && txtPlayerA.Text != " " && txtPlayerB.Text != " ")
            {
                if (txtPlayerA.Text == txtPlayerB.Text)
                {
                    MessageBox.Show("玩家A与玩家B名字不能重复，请重新输入！");
                }
                else if(txtPlayerA.Text.Length > 8 || txtPlayerB.Text.Length > 8)
                {
                    MessageBox.Show("玩家名不能超过8个字符，请重新输入！");
                }
                else
                {
                    GameForm.A = new Player(txtPlayerA.Text, 0, false, false);
                    GameForm.B = new Player(txtPlayerB.Text , 0, false, false);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("玩家名不能为空，请重新输入！");
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
