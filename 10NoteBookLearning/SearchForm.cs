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
    public partial class SearchForm : Form
    {
        public Dictionary<CheckState, StringComparison> dicUpperLower = new Dictionary<CheckState, StringComparison>()
        {
            { CheckState.Unchecked,StringComparison.CurrentCultureIgnoreCase },
            {CheckState.Checked,StringComparison.CurrentCulture }
        };
        public SearchForm()
        {
            InitializeComponent();
        }

        public static int startIndex = 0;
        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            int r = Program.d.txtFile.Text.IndexOf(txtSearchContent.Text, startIndex, dicUpperLower[cbDiffUpperLower.CheckState]);
            if (r == -1)
            {
                MessageBox.Show("找不到" + "\"" + txtSearchContent.Text + "\"");
                return;
            }
            Program.d.txtFile.Select(r, txtSearchContent.Text.Length);
            startIndex = r + txtSearchContent.Text.Length;
            Program.d.txtFile.ScrollToCaret();
        }

        public static bool searchShowFlag = false;
        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            searchShowFlag = false;
            this.Hide();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            searchShowFlag = true;
        }

        private void txtSearchContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchNext.PerformClick();
        }
    }
}
