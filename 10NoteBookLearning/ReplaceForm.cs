using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10NoteBookLearning
{
    public partial class ReplaceForm : Form
    {
        public Dictionary<CheckState, StringComparison> dicUpperLower = new Dictionary<CheckState, StringComparison>()
        {
            { CheckState.Unchecked,StringComparison.CurrentCultureIgnoreCase },
            {CheckState.Checked,StringComparison.CurrentCulture }
        };
        public ReplaceForm()
        {
            InitializeComponent();
        }

        public static bool searchShowFlag = false;
        private void ReplaceForm_Load(object sender, EventArgs e)
        {
            searchShowFlag = true;
        }

        private void ReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            searchShowFlag = false;
            this.Hide();
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int startIndex = 0;
        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            int r = Program.d.txtFile.Text.IndexOf(txtSearchContent.Text, startIndex, dicUpperLower[cbDiffUpperLower.CheckState]);
            if (r == -1 | r == 0)
            {
                MessageBox.Show("找不到" + "\"" + txtSearchContent.Text + "\"");
                return;
            }
            Program.d.txtFile.Select(r, txtSearchContent.Text.Length);
            startIndex = r + txtSearchContent.Text.Length;
            Program.d.txtFile.ScrollToCaret();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (Program.d.txtFile.Text == String.Empty)
                return;
            if (Program.d.txtFile.SelectedText == txtSearchContent.Text)
            {
                startIndex = Program.d.txtFile.SelectionStart + txtReplaceContent.Text.Length - 1;
                Program.d.txtFile.SelectedText = txtReplaceContent.Text;
                btnSearchNext.PerformClick();
            }
            else
                btnSearchNext.PerformClick();
        }

        private void btnAllReplace_Click(object sender, EventArgs e)
        {
            if (Program.d.txtFile.Text == String.Empty)
                return;
            string txt = Program.d.txtFile.Text.Replace(txtSearchContent.Text, txtReplaceContent.Text);
            if (txt.Equals(Program.d.txtFile.Text))
            {
                MessageBox.Show("找不到" + "\"" + txtSearchContent.Text + "\"");
            }
            else
                Program.d.txtFile.Text = txt;
        }
    }
}
