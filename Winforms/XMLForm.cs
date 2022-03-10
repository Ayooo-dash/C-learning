using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Winforms
{
    public partial class XMLForm : Form
    {
        public XMLForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xDoc = new XDocument();
            xDoc.Declaration = new XDeclaration("1.0", "UTF-8", "no");
            xDoc.Add(new XElement("List"));
            xDoc.Save("1.xml");
        }
    }
}
