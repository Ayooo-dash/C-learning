using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Management;
using System.Text.RegularExpressions;

namespace Winforms
{
    public partial class XMLForm : Form
    {
        public XMLForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public int Formstate = (int)ShowWindowEnum.ShowNormal;
        private string SplitStr = "";
        public string[] key = new string[]{
            "Win32_Processor", // CPU 处理器 
            "Win32_PhysicalMemory", // 物理内存条 
            "Win32_Keyboard", // 键盘 
            "Win32_PointingDevice", // 点输入设备，包括鼠标。 
            "Win32_FloppyDrive", // 软盘驱动器 
            "Win32_DiskDrive", // 硬盘驱动器 
            "Win32_CDROMDrive", // 光盘驱动器 
            "Win32_BaseBoard", // 主板 
            "Win32_BIOS", // BIOS 芯片 
            "Win32_ParallelPort", // 并口 
            "Win32_SerialPort", // 串口 
            "Win32_SerialPortConfiguration", // 串口配置 
            "Win32_SoundDevice", // 多媒体设置，一般指声卡。 
            "Win32_SystemSlot", // 主板插槽 (ISA & PCI & AGP) 
            "Win32_USBController", // USB 控制器 
            "Win32_NetworkAdapter", // 网络适配器 
            "Win32_NetworkAdapterConfiguration", // 网络适配器设置 
            "Win32_Printer", // 打印机 
            "Win32_PrinterConfiguration", // 打印机设置 
            "Win32_PrintJob", // 打印机任务 
            "Win32_TCPIPPrinterPort", // 打印机端口 
            "Win32_POTSModem", // MODEM 
            "Win32_POTSModemToSerialPort", // MODEM 端口 
            "Win32_DesktopMonitor", // 显示器 
            "Win32_DisplayConfiguration", // 显卡 
            "Win32_DisplayControllerConfiguration", // 显卡设置 
            "Win32_VideoController", // 显卡细节。 
            "Win32_VideoSettings", // 显卡支持的显示模式。 
 
            // 操作系统 
            "Win32_TimeZone", // 时区 
            "Win32_SystemDriver", // 驱动程序 
            "Win32_DiskPartition", // 磁盘分区 
            "Win32_LogicalDisk", // 逻辑磁盘 
            "Win32_LogicalDiskToPartition", // 逻辑磁盘所在分区及始末位置。 
            "Win32_LogicalMemoryConfiguration", // 逻辑内存配置 
            "Win32_PageFile", // 系统页文件信息 
            "Win32_PageFileSetting", // 页文件设置 
            "Win32_BootConfiguration", // 系统启动配置 
            "Win32_ComputerSystem", // 计算机信息简要 
            "Win32_OperatingSystem", // 操作系统信息 
            "Win32_StartupCommand", // 系统自动启动程序 
            "Win32_Service", // 系统安装的服务 
            "Win32_Group", // 系统管理组 
            "Win32_GroupUser", // 系统组帐号 
            "Win32_UserAccount", // 用户帐号 
            "Win32_Process", // 系统进程 
            "Win32_Thread", // 系统线程 
            "Win32_Share", // 共享 
            "Win32_NetworkClient", // 已安装的网络客户端 
            "Win32_NetworkProtocol", // 已安装的网络协议
        };

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xDoc = new XDocument();
            xDoc.Declaration = new XDeclaration("1.0", "UTF-8", "no");
            xDoc.Add(new XElement("List"));
            xDoc.Save("1.xml");



            Thread NewTh = new Thread(CaptureImage1);
            NewTh.SetApartmentState(ApartmentState.STA);//必须启动单元线程
            NewTh.Start();
        }

        public void CaptureImage1()
        {
            string strWinTitle = textBox1.Text;
            try
            {
                using (MemoryStream MS = new MemoryStream())  //抓取窗口图片
                {
                    Image image = CaptureWindow2(strWinTitle);
                    if (null == image)
                    {
                        return;
                    }
                    //Image img = (Image)image.Clone();
                    string FileName = @"D:\1\Screenshot.png";
                    image.Save(MS, ImageFormat.Png);
                    image.Dispose();
                    Image.FromStream(MS).Save(FileName);
                    MS.Dispose();
                    MS.Close();
                    //img.Save(@"D:\Screenshot1.png");
                    //img.Dispose();
                }
                MessageBox.Show("截图成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr handle = FindWindow(null, textBox1.Text);
            if (0 == (int)handle)
            {
                label1.Text = ShowWindowEnum.Closed.ToString();
                return;
            }
            Windowplacement placement = new Windowplacement();
            GetWindowPlacement(handle, ref placement);
            label1.Text = ((ShowWindowEnum)placement.showCmd).ToString();
            if (this.ParentForm.WindowState != FormWindowState.Minimized)
            {
                if ((ShowWindowEnum)placement.showCmd == ShowWindowEnum.ShowMinimized)
                {
                    ShowWindow(handle, Formstate);
                    this.Activate();
                }
                else
                {
                    Formstate = placement.showCmd;
                    return;
                }
            }

            //set user's focus to the window
            //SetForegroundWindow(handle);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "SetParent")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "PrintWindow")]
        private static extern int PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11, Closed = 12
        };

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }


        /// <summary>
        /// 根据句柄名截图
        /// </summary>
        /// <param name="handle">句柄名</param>
        /// <returns></returns>
        public Image CaptureWindow1(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// 根据窗口名称截图
        /// </summary>
        /// <param name="windowName">窗口名称</param>
        /// <returns></returns>
        public Image CaptureWindow2(string windowName)
        {
            IntPtr handle = FindWindow(null, windowName);
            if (0 == (int)handle)
            {
                MessageBox.Show(string.Format("查找不到 '{0}' 窗口！", windowName));
                return null;
            }
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            ShowWindow(hdcSrc, 1);
            //SetForegroundWindow(handle);
            Thread.Sleep(500);
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            GDI32.SelectObject(hdcDest, hOld);
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            Image img = Image.FromHbitmap(hBitmap);
            GDI32.DeleteObject(hBitmap);

            return img;
        }

        private void XMLForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (ZipFile(@"D://1", @"D://2//1.zip"))
            //    MessageBox.Show("压缩文件成功！");
            //else
            //    MessageBox.Show("压缩文件失败！");
            //DeleteLogs(@"D:\1\log", 7);
        }
        private static void ZipCompress(string strFile, ZipOutputStream outStream, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile += Path.DirectorySeparatorChar;
            }

            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            if (0 == filenames.Length)
            {
                return;
            }

            foreach (string file in filenames)
            {
                if (Directory.Exists(file))  //递归找子文件夹里的源文件
                {
                    ZipCompress(file, outStream, staticFile);
                }
                else
                {

                    FileStream fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];

                    fs.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(staticFile.LastIndexOf("\\") + 1);

                    ZipEntry entry = new ZipEntry(tempFile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;

                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;

                    outStream.PutNextEntry(entry);
                    outStream.Write(buffer, 0, buffer.Length);

                }
            }
        }
        /// <summary>
        /// 将指定的文件输出为流
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="strZip"></param>
        /// <returns></returns>
        private bool ZipFile(string strFile, string strZip)
        {
            try
            {
                if (strFile == "")
                {
                    MessageBox.Show("指定压缩文件夹不能为空");
                    return false;
                }

                if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                {
                    strFile += Path.DirectorySeparatorChar;
                }

                int nIndex = strZip.LastIndexOf("/");
                string strDir = strZip.Substring(0, nIndex);
                if (!Directory.Exists(strDir))
                {
                    Directory.CreateDirectory(strDir);
                }
                ZipOutputStream outStream = new ZipOutputStream(File.Create(strZip));
                outStream.SetLevel(6);
                ZipCompress(strFile, outStream, strFile);
                outStream.Finish();
                outStream.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("压缩文件失败：" + e.ToString());

                return false;
            }
        }

        private void XMLForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.WriteLog(e.CloseReason.ToString());
        }


        /// <summary>
        /// 自动删除文件，删除该目录下文件修改时间大于定义天数的所有文件（包括子目录下的文件）
        /// </summary>
        /// <param name="dir">路径</param>
        /// <param name="days">时间</param>
        public static void DeleteLogs(string dir, int days)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    return;
                }
                var now = DateTime.Now;
                foreach (var f in Directory.GetFileSystemEntries(dir)/*.Where(f => File.Exists(f))*/)
                {
                    if (File.Exists(f))
                    {
                        //var t = DateTime.Parse(Path.GetFileNameWithoutExtension(f));
                        //var t = File.GetCreationTime(f);
                        var t = File.GetLastWriteTime(f);
                        var elapsedTicks = now.Ticks - t.Ticks;
                        var elaspsedSpan = new TimeSpan(elapsedTicks);
                        if (elaspsedSpan.TotalDays > days)
                        {
                            File.Delete(f);
                        }
                    }
                    else if (Directory.Exists(f))
                    {
                        DeleteLogs(f, days);
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //DeleteLogs(@"D:\1", 100);
            //label1.Text = string.Format(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff±{0}"), TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString("hhmm"));
            string CPUName = "Name";
            string CPUNumber = "NumberOfCores";
            string CPUId = "ProcessorId";
            var list = GetComputerInfo("Win32_NetworkAdapter");
            foreach (var l in list)
            {
                foreach (var dic in l)
                {
                    textBox2.Text += (dic.Key + ":" + dic.Value + "\r\n");
                }
                if (list.IndexOf(l) < list.Count -1)
                    WriteSplitLine(textBox2, "*");
            }
        }

        public List<Dictionary<string, string>> GetComputerInfo(string key)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            List<string> list1 = new List<string>();
            List<string[]> list2 = new List<string[]>();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + key);
                int i = 0;
                foreach (ManagementObject share in searcher.Get())
                {
                    try
                    {
                        list1.Add(share.GetText(TextFormat.Mof));
                    }
                    catch (Exception ex)
                    {

                    }
                    i++;
                }
                foreach (var l in list1)
                {
                    list2.Add(l.Split('{', '}')[1].Split(new char[] { '\n', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries));
                }
                foreach (var l2 in list2)
                {
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    foreach (var s in l2)
                    {
                        string[] ss = s.Split('=');
                        temp.Add(ss[0], ss[1]);
                    }
                    list.Add(temp);
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                list.Clear();
                return list;
            }
        }

        public void WriteSplitLine(TextBox tb, string str)
        {
            SplitStr = "";
            for (int i = 0; i < tb.Width / tb.Font.Size; i++)
            {
                SplitStr += str;
            }
            SplitStr += "\r\n";
            tb.Text += SplitStr;
        }
    }

    //class CaptureWindow
    //{

    //    /// <summary>
    //    /// Helper class containing User32 API functions
    //    /// </summary>
    //    private class User32
    //    {
    //        [StructLayout(LayoutKind.Sequential)]
    //        public struct RECT
    //        {
    //            public int left;
    //            public int top;
    //            public int right;
    //            public int bottom;
    //        }
    //        [DllImport("user32.dll")]
    //        public static extern IntPtr GetDesktopWindow();
    //        [DllImport("user32.dll")]
    //        public static extern IntPtr GetWindowDC(IntPtr hWnd);
    //        [DllImport("user32.dll")]
    //        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
    //        [DllImport("user32.dll")]
    //        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
    //    }
    //    private class GDI32
    //    {

    //        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
    //        [DllImport("gdi32.dll")]
    //        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
    //            int nWidth, int nHeight, IntPtr hObjectSource,
    //            int nXSrc, int nYSrc, int dwRop);
    //        [DllImport("gdi32.dll")]
    //        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
    //            int nHeight);
    //        [DllImport("gdi32.dll")]
    //        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
    //        [DllImport("gdi32.dll")]
    //        public static extern bool DeleteDC(IntPtr hDC);
    //        [DllImport("gdi32.dll")]
    //        public static extern bool DeleteObject(IntPtr hObject);
    //        [DllImport("gdi32.dll")]
    //        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    //    }
    //    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    //    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    //    [DllImport("User32.dll", EntryPoint = "SetParent")]
    //    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    //    [DllImport("user32.dll", EntryPoint = "ShowWindow")]
    //    private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
    //    [DllImport("user32.dll", EntryPoint = "PrintWindow")]
    //    private static extern int PrintWindow(IntPtr hwnd,IntPtr hdcBlt, uint nFlags);
    //    [DllImport("user32.dll", SetLastError = true)]
    //    private static extern int SetForegroundWindow(IntPtr hwnd);


    //    /// <summary>
    //    /// 根据句柄名截图
    //    /// </summary>
    //    /// <param name="handle">句柄名</param>
    //    /// <returns></returns>
    //    public Image CaptureWindow1(IntPtr handle)
    //    {
    //        // get te hDC of the target window
    //        IntPtr hdcSrc = User32.GetWindowDC(handle);
    //        // get the size
    //        User32.RECT windowRect = new User32.RECT();
    //        User32.GetWindowRect(handle, ref windowRect);
    //        int width = windowRect.right - windowRect.left;
    //        int height = windowRect.bottom - windowRect.top;
    //        // create a device context we can copy to
    //        IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
    //        // create a bitmap we can copy it to,
    //        // using GetDeviceCaps to get the width/height
    //        IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
    //        // select the bitmap object
    //        IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
    //        // bitblt over
    //        GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
    //        // restore selection
    //        GDI32.SelectObject(hdcDest, hOld);
    //        // clean up
    //        GDI32.DeleteDC(hdcDest);
    //        User32.ReleaseDC(handle, hdcSrc);
    //        // get a .NET image object for it
    //        Image img = Image.FromHbitmap(hBitmap);
    //        // free up the Bitmap object
    //        GDI32.DeleteObject(hBitmap);
    //        return img;
    //    }
    //    /// <summary>
    //    /// 根据窗口名称截图
    //    /// </summary>
    //    /// <param name="windowName">窗口名称</param>
    //    /// <returns></returns>
    //    public Image CaptureWindow2(string windowName)
    //    {
    //        IntPtr handle = FindWindow(null, windowName);
    //        if (0 == (int)handle)
    //        {
    //            MessageBox.Show(string.Format("查找不到 '{0}' 窗口！", windowName));
    //            return null;
    //        }
    //        IntPtr hdcSrc = User32.GetWindowDC(handle);
    //        ShowWindow(handle, 9);
    //        //ShowWindow(hdcSrc, 1);
    //        SetForegroundWindow(handle);
    //        Thread.Sleep(500);
    //        User32.RECT windowRect = new User32.RECT();
    //        User32.GetWindowRect(handle, ref windowRect);
    //        int width = windowRect.right - windowRect.left;
    //        int height = windowRect.bottom - windowRect.top;
    //        IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
    //        IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
    //        IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
    //        GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
    //        GDI32.SelectObject(hdcDest, hOld);
    //        GDI32.DeleteDC(hdcDest);
    //        User32.ReleaseDC(handle, hdcSrc);
    //        Image img = Image.FromHbitmap(hBitmap);
    //        GDI32.DeleteObject(hBitmap);

    //        return img;
    //    }
    //}
}
