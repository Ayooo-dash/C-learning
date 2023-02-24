using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace _10NoteBookLearning
{
    public enum EncodeFormat
    {
        ANSI = 0,
        UTF16_LE,
        UTF16_BE,
        UTF8,
        UTF8withBOM,
    };
    public enum Wrapper
    {
        Windows = 0,
        UnixLinux,
        MacOS,
    }
    public struct m_FileInfo
    {
        public EncodeFormat encodeFormat;
        public Wrapper Wrapper;
        public string fileName;
        public string fileText;
        public string filePath;
        public m_FileInfo(EncodeFormat e, Wrapper w, string fName, string fText, string fPath)
        {
            this.encodeFormat = e;
            this.Wrapper = w;
            this.fileName = fName;
            this.fileText = fText;
            this.filePath = fPath;
        }
    };
    public partial class Main_Form : Form
    {
        #region 变量
        //public static bool Flag = false;
        Thread th;
        public static string path = null;
        public m_FileInfo fileInfo = new m_FileInfo(EncodeFormat.UTF8, Wrapper.Windows, null, null, null);
        public static string context = null;
        public static bool saveFlag = true;
        public static bool newFlag = true;
        public static Dictionary<Wrapper, string> dicWrapper = new Dictionary<Wrapper, string>()
        {
            { Wrapper.Windows,"Windows(CRLF)" },
            { Wrapper.UnixLinux ,"Unix(LF)" },
            { Wrapper.MacOS,"MacOS(CR)" }
        };
        public static Dictionary<EncodeFormat, Encoding> dicEncode = new Dictionary<EncodeFormat, Encoding>()
        {
            { EncodeFormat.ANSI,Encoding.Default},
            { EncodeFormat.UTF16_LE,Encoding.Unicode},
            { EncodeFormat.UTF16_BE,Encoding.BigEndianUnicode},
            { EncodeFormat.UTF8,new UTF8Encoding(false)},
            { EncodeFormat.UTF8withBOM,Encoding.UTF8}
        };
        public ToolStripItemCollection tsc;
        public static int num = 0;

        #endregion
        public Main_Form(m_FileInfo af)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon;
            fileInfo = af;
        }

        public Main_Form()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon;
        }

        #region 菜单选项事件
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"E:\GZY";
                ofd.Filter = "文本文档|*.txt|所有文件|*.*";
                ofd.Title = "选择要打开的文件";
                ofd.ShowDialog();
                fileInfo.filePath = ofd.FileName;
                fileInfo = ReadFile(fileInfo.filePath, ref saveFlag, ref newFlag);
                ToolStripMenuItem tsi = (ToolStripMenuItem)tsc[(int)fileInfo.encodeFormat];
                statusBarEncodingFormat.Text = tsi.Text;
                num = SwitchCodingFormat(tsi);
                statusBarCharNum.Text = string.Format("{0} 字符", txtFile.Text.Length);
                txtFile.Text = fileInfo.fileText;
                this.Text = fileInfo.fileName;
                statusBarWrapper.Text = dicWrapper[fileInfo.Wrapper];
            }
            catch/* (Exception ex)*/
            {
                //MessageBox.Show(ex.Message + ex.Source + ex.TargetSite.Name);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = @"E:\GZY";
                sfd.Filter = "文本文档|*.txt|所有文件|*.*";
                sfd.Title = "另存为";
                sfd.ShowDialog(this);
                if (WriteFile(sfd.FileName, txtFile.Text, dicEncode.ElementAt(num).Value, fileInfo.Wrapper))
                {
                    this.Text = Path.GetFileName(sfd.FileName) + " - 记事本";
                    statusBarEncodingFormat.Text = tsc[num].Text;
                    //MessageBox.Show("保存成功！");
                    saveFlag = true;
                    newFlag = false;
                    fileInfo.filePath = sfd.FileName;
                }
            }
            catch
            {
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (newFlag == false)
            {
                if (WriteFile(fileInfo.filePath, txtFile.Text, dicEncode.ElementAt(num).Value, fileInfo.Wrapper))
                {
                    if (this.Text.First() == '*')
                        this.Text = this.Text.Remove(0, 1);
                    statusBarEncodingFormat.Text = tsc[num].Text;
                    //MessageBox.Show("保存成功！");
                    saveFlag = true;
                }
            }
            else
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"E:\GZY";
                    sfd.Filter = "文本文档|*.txt|所有文件|*.*";
                    sfd.Title = "保存文件";
                    sfd.OverwritePrompt = true;
                    sfd.ShowDialog(this);
                    if (WriteFile(sfd.FileName, txtFile.Text, dicEncode.ElementAt(num).Value, fileInfo.Wrapper))
                    {
                        fileInfo.filePath = sfd.FileName;
                        this.Text = Path.GetFileName(sfd.FileName) + " - 记事本";
                        statusBarEncodingFormat.Text = tsc[num].Text;
                        //MessageBox.Show("保存成功！");
                        saveFlag = true;

                    }
                }
                catch
                {
                }
                newFlag = false;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag == true)
            {
                txtFile.Text = String.Empty;
                this.Text = "无标题 - 记事本";
                // path = @"E:\GZY";
                newFlag = true;
            }
            else
            {
                MessageBox.Show("未保存更改！");
            }
        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (自动换行ToolStripMenuItem.Checked == true)
            {
                txtFile.WordWrap = false;
                自动换行ToolStripMenuItem.Checked = false;
            }
            else
            {
                txtFile.WordWrap = true;
                自动换行ToolStripMenuItem.Checked = true;
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtFile.Font = fd.Font;
            }
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cl = new ColorDialog();
            if (cl.ShowDialog() == DialogResult.OK)
            {
                txtFile.ForeColor = cl.Color;
            }
        }

        private void 关于记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Form aboutForm = new About_Form();
            aboutForm.ShowDialog();
        }
        #endregion

        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text.First() == '*')
            {
                if (txtFile.Text == String.Empty && newFlag)
                    return;
                DialogResult dr = MessageBox.Show("文件未保存，是否将文件保存？", "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    保存ToolStripMenuItem.PerformClick();
                    if (th != null)
                        th.Abort();
                }
                else if (dr == DialogResult.No)
                {
                    if (th != null)
                        th.Abort();
                    return;
                }
                else if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (th != null)
                    th.Abort();
                return;
            }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            txtFile.MouseWheel += txtFile_MouseWheel;
            tsc = 编码格式ToolStripMenuItem.DropDownItems;
            num = SwitchCodingFormat((ToolStripMenuItem)tsc[3]);
            statusBarEncodingFormat.Alignment = ToolStripItemAlignment.Right;
            statusBarEncodingFormat.Text = "UTF-8";
            statusBarWrapper.Alignment = ToolStripItemAlignment.Right;
            statusBarWrapper.Text = "Windows(CRLF)";
            statusBarZoomProp.Alignment = ToolStripItemAlignment.Right;
            statusBarZoomProp.Text = "100%";
            statusBarCursorPos.Alignment = ToolStripItemAlignment.Right;
            statusBarCursorPos.Text = "第1行，第1列";
            statusBarCharNum.Alignment = ToolStripItemAlignment.Right;
            statusBarCharNum.Text = "0 字符";
            if (fileInfo.filePath != null)
            {
                fileInfo = ReadFile(fileInfo.filePath, ref saveFlag, ref newFlag);
                txtFile.Text = fileInfo.fileText;
                this.Text = fileInfo.fileName;
                ToolStripMenuItem tsi = (ToolStripMenuItem)tsc[(int)fileInfo.encodeFormat];
                statusBarEncodingFormat.Text = tsi.Text;
                num = SwitchCodingFormat(tsi);
                statusBarWrapper.Text = dicWrapper[fileInfo.Wrapper];
                txtFile.SelectionStart = 0;
            }
            th = new Thread(CheckCursorPos);
            th.IsBackground = true;
            th.Start();
            if (txtFile.Text == String.Empty)
            {
                查找ToolStripMenuItem.Enabled = false;
                查找下一个ToolStripMenuItem.Enabled = false;
                查找上一个ToolStripMenuItem.Enabled = false;
            }
            else
            {
                查找ToolStripMenuItem.Enabled = true;
                查找下一个ToolStripMenuItem.Enabled = true;
                查找上一个ToolStripMenuItem.Enabled = true;
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            if (this.Text.First() != '*')
                this.Text = "*" + this.Text;
            if (txtFile.Text == String.Empty)
            {
                查找ToolStripMenuItem.Enabled = false;
                查找下一个ToolStripMenuItem.Enabled = false;
                查找上一个ToolStripMenuItem.Enabled = false;
            }
            else
            {
                查找ToolStripMenuItem.Enabled = true;
                查找下一个ToolStripMenuItem.Enabled = true;
                查找上一个ToolStripMenuItem.Enabled = true;
            }
            statusBarCharNum.Text = string.Format("{0} 字符", txtFile.Text.Length);
        }

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            string[] dragData = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            fileInfo = ReadFile(dragData[0], ref saveFlag, ref newFlag);
            txtFile.Text = fileInfo.fileText;
            this.Text = fileInfo.fileName;
        }

        public string StatusBarShowCursorPos()
        {
            try
            {
                char[] txts = txtFile.Text.ToCharArray();
                int n = txtFile.SelectionStart;
                int i = 1, j = 1;
                if (n == 0)
                    j = 1;
                else
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (txts[k] == '\n')
                        {
                            i++;
                            j = 1;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                return (statusBarCursorPos.Text = string.Format("第{0}行，第{1}列", i, j));
            }
            catch
            {
                return statusBarCursorPos.Text;
            }
        }

        public static m_FileInfo ReadFile(string path, ref bool saveFlag, ref bool newFlag)
        {
            m_FileInfo fInfo;
            fInfo.encodeFormat = EncodeFormat.ANSI;
            fInfo.fileName = null;
            fInfo.fileText = null;
            fInfo.filePath = null;
            fInfo.Wrapper = Wrapper.Windows;
            using (FileStream fsread = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                List<byte> data = new List<byte>();
                int r = 0;
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];//定义文件缓存区
                    r = fsread.Read(buffer, 0, buffer.Length);
                    if (r == 0)
                        break;
                    data.AddRange(buffer.Take(r));
                }
                #region 抽离封装
                //if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                //{
                //    encodeFormat = EncodeFormat.UTF16_LE;
                //    text = Encoding.Unicode.GetString(buffer, 0, r);
                //}
                //else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                //{
                //    encodeFormat = EncodeFormat.UTF16_BE;
                //    text = Encoding.BigEndianUnicode.GetString(buffer, 0, r);
                //}
                //else if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                //{
                //    encodeFormat = EncodeFormat.UTF8withBOM;
                //    text = Encoding.UTF8.GetString(buffer, 0, r);
                //}
                //else if (CheckUTF8withoutBOM(buffer))
                //{
                //    encodeFormat = EncodeFormat.UTF8;
                //    text = new UTF8Encoding(false).GetString(buffer, 0, r);
                //}
                //else
                //{
                //    encodeFormat = EncodeFormat.ANSI;
                //    text = Encoding.Default.GetString(buffer, 0, r);
                //}
                #endregion
                fInfo.encodeFormat = CheckEncodeFormat(data.ToArray(), data.Count, ref fInfo.fileText);
                fInfo.Wrapper = CheckWrapper(fInfo.fileText);
                fInfo.filePath = path;
            }
            fInfo.fileName = Path.GetFileName(path) + " - 记事本";
            saveFlag = true;
            newFlag = false;
            if (fInfo.Wrapper == Wrapper.UnixLinux)
            {
                fInfo.fileText = Regex.Replace(fInfo.fileText, "(\n)", "\r\n");
            }
            else if (fInfo.Wrapper == Wrapper.MacOS)
            {
                fInfo.fileText = Regex.Replace(fInfo.fileText, "(\r)", "\r\n");
            }
            return fInfo;
        }

        public static EncodeFormat CheckEncodeFormat(byte[] buffer, int r, ref string text)
        {
            EncodeFormat encodeFormat/* = EncodeFormat.ANSI*/;
            if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                encodeFormat = EncodeFormat.UTF16_LE;
                text = dicEncode[encodeFormat].GetString(buffer, 0, r);
                return encodeFormat;
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                encodeFormat = EncodeFormat.UTF16_BE;
                text = dicEncode[encodeFormat].GetString(buffer, 0, r);
                return encodeFormat;
            }
            else if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
            {
                encodeFormat = EncodeFormat.UTF8withBOM;
                text = dicEncode[encodeFormat].GetString(buffer, 0, r);
                return encodeFormat;
            }
            else if (CheckUTF8withoutBOM(buffer, r))
            {
                encodeFormat = EncodeFormat.UTF8;
                text = dicEncode[encodeFormat].GetString(buffer, 0, r);
                return encodeFormat;
            }
            else
            {
                encodeFormat = EncodeFormat.ANSI;
                text = dicEncode[encodeFormat].GetString(buffer, 0, r);
                return encodeFormat;
            }
        }

        /// <summary>
        /// UTF-8是一种变长字节编码方式。对于某一个字符的UTF-8编码，如果只有一个字节则其最高二进制位为0；如果是多字节，其第一个字节从最高位开始，连续的二进制位值为1的个数决定了其编码的位数，其余各字节均以10开头。UTF-8最多可用到6个字节。
        ///如表：
        ///1字节 0xxxxxxx
        ///2字节 110xxxxx 10xxxxxx
        ///3字节 1110xxxx 10xxxxxx 10xxxxxx
        ///4字节 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
        ///5字节 111110xx 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx
        ///6字节 1111110x 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx
        ///因此UTF-8中可以用来表示字符编码的实际位数最多有31位，即上表中x所表示的位。除去那些控制位（每字节开头的10等），这些x表示的位与UNICODE编码是一一对应的，位高低顺序也相同。
        ///实际将UNICODE转换为UTF-8编码时应先去除高位0，然后根据所剩编码的位数决定所需最小的UTF-8编码位数。
        ///因此那些基本ASCII字符集中的字符（UNICODE兼容ASCII）只需要一个字节的UTF-8编码（7个二进制位）便可以表示。
        /// </summary>
        /// <param name="data">读取到的文件数据</param>
        /// <returns>UTF-8不带BOM时，返回true</returns>
        public static bool CheckUTF8withoutBOM(byte[] data, int length)
        {
            int i = 0;
            UInt32 nBytes = 0;
            byte buf;
            bool b = true;
            for (i = 0; i < length; i++)
            {
                buf = data[i];
                if ((buf & 0x80) != 0)
                    b = false;
                if (nBytes == 0)
                {
                    if (buf >= 0x80)
                    {
                        if (buf >= 0xfc && buf <= 0xfd)
                            nBytes = 6;
                        else if (buf >= 0xf8)
                            nBytes = 5;
                        else if (buf >= 0xf0)
                            nBytes = 4;
                        else if (buf >= 0xe0)
                            nBytes = 3;
                        else if (buf >= 0xc0)
                            nBytes = 2;
                        else
                            return false;
                        nBytes--;
                    }
                }
                else
                {
                    if ((buf & 0xc0) != 0x80)
                        return false;
                    nBytes--;
                }
            }
            if (nBytes > 0)
                return false;
            if (b)
                return false;
            return true;
        }

        public static Wrapper CheckWrapper(string text)
        {
            if (Regex.IsMatch(text, "\r\n"))
                return Wrapper.Windows;
            else if (Regex.IsMatch(text, "[^\r]\n"))
                return Wrapper.UnixLinux;
            else if (Regex.IsMatch(text, "\r[^\n]"))
                return Wrapper.MacOS;
            else
                return Wrapper.Windows;
            // Match(text, @"\(([a-zA-Z]:)\)(.*)");
        }

        public static bool WriteFile(string path, string text, Encoding e, Wrapper wrapper)
        {
            List<byte> buffer = new List<byte>();
            try
            {
                if (wrapper == Wrapper.UnixLinux)
                {
                    text = Regex.Replace(text, "(\r\n)", "\n");
                }
                else if (wrapper == Wrapper.MacOS)
                {
                    text = Regex.Replace(text, "(\r\n)", "\r");
                }
                buffer = e.GetBytes(text).ToList();
                //InsertHead(ref buffer);
                using (FileStream fswrite = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fswrite.Write(buffer.ToArray(), 0, buffer.Count);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void InsertHead(ref List<byte> data)
        {
            switch (dicEncode.ElementAt(num).Key)
            {
                case EncodeFormat.ANSI:
                    break;
                case EncodeFormat.UTF16_BE:
                    data.Insert(0, 0xff);
                    data.Insert(0, 0xfe);
                    break;
                case EncodeFormat.UTF16_LE:
                    data.Insert(0, 0xfe);
                    data.Insert(0, 0xff);
                    break;
                case EncodeFormat.UTF8:
                    break;
                case EncodeFormat.UTF8withBOM:
                    data.Insert(0, 0xbf);
                    data.Insert(0, 0xbb);
                    data.Insert(0, 0xef);
                    break;
            }
        }

        private void txtFile_MouseDown(object sender, MouseEventArgs e)
        {
        }

        public void CheckCursorPos()
        {
            while (true)
            {
                StatusBarShowCursorPos();
            }
        }

        private void 新窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main_Form frm = new Main_Form();
            frm.Show();
        }

        public Size sb = new Size();
        //public Size sa = new Size();

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sb = txtFile.Size;
            if (状态栏ToolStripMenuItem.Checked == true)
            {
                状态栏ToolStripMenuItem.Checked = false;
                txtFile.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                txtFile.Dock = DockStyle.Fill;
                statusBar.Hide();
                //sa = txtFile.Size;
            }
            else
            {
                状态栏ToolStripMenuItem.Checked = true;
                txtFile.Dock = DockStyle.None;
                txtFile.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                txtFile.Size = new Size(sb.Width, sb.Height - statusBar.Height);
                statusBar.Show();
            }
        }

        private void Main_Form_SizeChanged(object sender, EventArgs e)
        {
            sb = txtFile.Size;
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontFamily ff = txtFile.Font.FontFamily;
            float fz = txtFile.Font.Size + 1;
            if (fz > 42)
                return;
            txtFile.Font = new Font(ff, fz);
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontFamily ff = txtFile.Font.FontFamily;
            float fz = txtFile.Font.Size - 1;
            if (fz <= 0)
                return;
            txtFile.Font = new Font(ff, fz);
        }

        private void 恢复默认缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtFile.Font = new Font("微软雅黑 Light", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
        }
        public Keys k = Keys.None;

        private void txtFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                k = Keys.ControlKey;
            else
                k = Keys.None;

        }
        private void txtFile_KeyUp(object sender, KeyEventArgs e)
        {
            k = Keys.None;
        }
        public int mDelta;
        private void txtFile_MouseWheel(object sender, MouseEventArgs e)
        {
            if (k == Keys.ControlKey)
            {
                float fz = 0;
                FontFamily ff = txtFile.Font.FontFamily;
                if (e.Delta > 0)
                    fz = txtFile.Font.Size + 1;
                else
                    fz = txtFile.Font.Size - 1;
                if (fz <= 0)
                    return;
                if (fz > 58)
                    return;
                txtFile.Font = new Font(ff, fz);
                statusBarZoomProp.Text = string.Format("{0}%", (int)(fz / 12f * 100));
            }
        }

        private void txtFile_MouseClick(object sender, MouseEventArgs e)
        {
            SearchForm.startIndex = txtFile.SelectionStart;
            ReplaceForm.startIndex = txtFile.SelectionStart;
        }

        public SearchForm searchForm = new SearchForm();
        public ReplaceForm replaceForm = new ReplaceForm();
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SearchForm.searchShowFlag)
            {
                return;
            }
            else
            {
                searchForm.ShowInTaskbar = false;
                searchForm.MinimizeBox = false;
                searchForm.MaximizeBox = false;
                searchForm.TopMost = true;
                searchForm.Show();
            }
        }

        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchForm.btnSearchNext.PerformClick();
        }

        private void 查找上一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplaceForm.searchShowFlag)
            {
                return;
            }
            else
            {
                replaceForm.ShowInTaskbar = false;
                replaceForm.MinimizeBox = false;
                replaceForm.MaximizeBox = false;
                replaceForm.TopMost = true;
                replaceForm.Show();
            }
        }

        public int SwitchCodingFormat(ToolStripMenuItem tsmi)
        {
            foreach (ToolStripMenuItem t in tsc)
            {
                if (t == tsmi)
                    t.Checked = true;
                else
                    t.Checked = false;
            }
            return tsc.IndexOf(tsmi);
        }

        private void aNSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            num = SwitchCodingFormat(aNSIToolStripMenuItem);
        }

        private void uTF16LEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            num = SwitchCodingFormat(uTF16LEToolStripMenuItem);
        }

        private void uTF16BEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            num = SwitchCodingFormat(uTF16BEToolStripMenuItem);
        }

        private void uTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            num = SwitchCodingFormat(uTF8ToolStripMenuItem);
        }

        private void uTF8WithBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            num = SwitchCodingFormat(uTF8WithBOMToolStripMenuItem);
        }
    }

    public class MyEvent
    {
        public delegate void MyEventHander(object sender, EventArgs e);
        public event MyEventHander myEvent;
        public void OnEvent()
        {
            if (this.myEvent != null)
            {
                this.myEvent(this, new EventArgs());
            }
        }
    }
}
