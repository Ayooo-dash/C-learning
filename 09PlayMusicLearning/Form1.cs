using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _09PlayMusicLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> musicList = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] path = Directory.GetFiles(@"E:\WAV", "*.wav");
            for (int i = 0; i < path.Length; i++)
            {
                listBoxMusic.Items.Add(Path.GetFileName(path[i]));
                musicList.Add(path[i]);
            }
        }

        private void listBoxMusic_DoubleClick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = musicList[listBoxMusic.SelectedIndex];
        }
    }
}
