using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10NoteBookLearning
{
    static class Program
    {
        public static Main_Form d;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string str = null;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args != null)
                {
                    str = args[0];
                    m_FileInfo af = Main_Form.ReadFile(str, ref Main_Form.saveFlag, ref Main_Form.newFlag);
                    Application.Run(d = new Main_Form(af));
                }
            }
            catch
            {
                Application.Run(d = new Main_Form());
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
