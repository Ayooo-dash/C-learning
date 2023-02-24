using _10NoteBookLearning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms
{
    public partial class FileManagerForm : Form
    {
        #region 变量
        public Thread th;
        string mv = null;
        public static ContextMenuStrip cms = new ContextMenuStrip();
        public static Dictionary<TreeNode, m_type> IsFileDir = new Dictionary<TreeNode, m_type>();
        public static bool IsCopy = false;
        public enum m_type
        {
            dir = 0,
            file,
            driveDir,
        };
        #endregion

        public FileManagerForm()
        {
            InitializeComponent();
        }
        private void FileManagerForm_Load(object sender, EventArgs e)
        {
            #region 右键菜单配置
            cms.Items.Add("新建文件", null, new EventHandler(cms_新建文件Clicked));
            cms.Items.Add("新建文件夹", null, new EventHandler(cms_新建文件夹Clicked));
            cms.Items.Add("打开", null, new EventHandler(cms_打开Clicked));
            cms.Items.Add("剪切", null, new EventHandler(cms_剪切Clicked));
            cms.Items.Add("复制", null, new EventHandler(cms_复制Clicked));
            cms.Items.Add("粘贴", null, new EventHandler(cms_粘贴Clicked));
            cms.Items.Add("删除", null, new EventHandler(cms_删除Clicked));
            cms.Items.Add("刷新", null, new EventHandler(cms_刷新Clicked));
            cms.Items.Add("重命名", null, new EventHandler(cms_重命名Clicked));
            //cms.Items[0].Click += new EventHandler(cms_打开Clicked);
            //cms.Items[1].Click += new EventHandler(cms_剪切Clicked);
            //cms.Items[2].Click += new EventHandler(cms_复制Clicked);
            //cms.Items[3].Click += new EventHandler(cms_粘贴Clicked);
            //cms.Items[4].Click += new EventHandler(cms_删除Clicked);
            //cms.Items[5].Click += new EventHandler(cms_刷新Clicked);
            //cms.Items[6].Click += new EventHandler(cms_重命名Clicked);
            tvFileManager.ContextMenuStrip = cms;
            #endregion
            #region 右键菜单图标
            ImageList images = new ImageList();
            images.Images.Add("sysdisk", Properties.Resources.SysDiskIcon);
            images.Images.Add("disk", Properties.Resources.DiskIcon);
            images.Images.Add("dir", Properties.Resources.DirIcon);
            images.Images.Add("file", Properties.Resources.FileIcon);
            images.Images.Add("app", Properties.Resources.AppIcon);
            images.Images.Add("txt", Properties.Resources.TextIcon2);
            images.Images.Add("pic", Properties.Resources.PicIcon);
            images.Images.Add("zip", Properties.Resources.ZipIcon);
            images.Images.Add("music", Properties.Resources.MusicIcon);
            images.Images.Add("video", Properties.Resources.VideoIcon);
            images.Images.Add("img", Properties.Resources.ImgIcon);
            images.ImageSize = new Size(20, 20);
            images.ColorDepth = ColorDepth.Depth32Bit;
            tvFileManager.ImageList = images;
            #endregion
            LoadDrives(tvFileManager.Nodes);
        }

        #region 加载目录、文件及其图标
        public void LoadDirsFiles(string path, TreeNodeCollection tnCollection)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode treenode = tnCollection.Add(Path.GetFileName(dir));
                    treenode.ImageKey = "dir";
                    IsFileDir.Add(treenode, m_type.dir);
                }
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    TreeNode treenode = tnCollection.Add(Path.GetFileName(file));
                    treenode.ImageKey = LoadIcon(Path.GetExtension(file));
                    IsFileDir.Add(treenode, m_type.file);
                }
            }
            catch (Exception e)
            {
                textBox1.Text += e.Message.Replace("\r\n", "") + "\r\n";
            }
        }
        public void LoadDrives(TreeNodeCollection tnCollection)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                TreeNode tn = tnCollection.Add(string.Format("{0}({1})", drive.VolumeLabel, drive.Name.Remove(drive.Name.Length - 1)));
                if (drive.Name.Contains("C:"))
                    tn.ImageKey = "sysdisk";
                else
                    tn.ImageKey = "disk";
                IsFileDir.Add(tn, m_type.driveDir);
                LoadDirsFiles(drive.Name, tn.Nodes);
            }
        }
        public string LoadIcon(string ext)
        {
            if (ext.Equals(".txt", StringComparison.CurrentCultureIgnoreCase))
                return "txt";
            else if (ext.Equals(".exe", StringComparison.CurrentCultureIgnoreCase))
                return "app";
            else if (ext.Equals(".zip", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".rar", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".tar", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".bz2", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".tgz", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".7z", StringComparison.CurrentCultureIgnoreCase))
                return "zip";
            else if (ext.Equals(".jpg", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".png", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".jpeg", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".bmp", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".ico", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".gif", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".tif", StringComparison.CurrentCultureIgnoreCase))
                return "pic";
            else if (ext.Equals(".mp3", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".wav", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".flac", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".acc", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".ogg", StringComparison.CurrentCultureIgnoreCase)/* | ext.Equals("gif")*/)
                return "music";
            else if (ext.Equals(".iso", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".img", StringComparison.CurrentCultureIgnoreCase) /*| ext.Equals("jpeg") | ext.Equals("bmp") | ext.Equals("ico") | ext.Equals("gif")*/)
                return "img";
            else if (ext.Equals(".avi", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".mp4", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".mkv", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".rmvb", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".flv", StringComparison.CurrentCultureIgnoreCase) | ext.Equals(".m4v", StringComparison.CurrentCultureIgnoreCase)
                | ext.Equals(".wmv", StringComparison.CurrentCultureIgnoreCase))
                return "video";
            else
                return "file";
        }
        public string RemoveDriveVolumeLabel(string str)
        {
            Match match = Regex.Match(str, @"\(([a-zA-Z]:)\)(.*)");
            return match.Groups[1].Value + match.Groups[2].Value;
        }
        #endregion

        #region 右键菜单事件
        private void cms_新建文件Clicked(object sender, EventArgs e)
        {
            TreeNode tn = tvFileManager.SelectedNode;
            if (IsFileDir[tn] == m_type.driveDir)
                return;
            else
            {
                string destName = RemoveDriveVolumeLabel(tn.Parent.FullPath) + "\\" + "新建文本";
                int x = 1;
                string temp = destName;
                while (File.Exists(destName))
                {
                    destName = temp + String.Format("({0})", x);
                    x++;
                }
                File.Create(destName, 1, FileOptions.WriteThrough).Close();
                TreeNode tn1 = tn.Parent.Nodes.Add(Path.GetFileName(destName));
                tn1.ImageKey = "file";
                IsFileDir.Add(tn1, m_type.file);
                tvFileManager.SelectedNode = tn1;
                tn1.BeginEdit();
            }
        }
        private void cms_新建文件夹Clicked(object sender, EventArgs e)
        {
            TreeNode tn = tvFileManager.SelectedNode;
            if (IsFileDir[tn] == m_type.driveDir)
                return;
            else
            {
                string destName = RemoveDriveVolumeLabel(tn.Parent.FullPath) + "\\" + "新建文件夹";
                int x = 1;
                string temp = destName;
                while (Directory.Exists(destName))
                {
                    destName = temp + String.Format("({0})", x);
                    x++;
                }
                Directory.CreateDirectory(destName);
                TreeNode tn1 = tn.Parent.Nodes.Add(Path.GetFileName(destName));
                tn1.ImageKey = "dir";
                IsFileDir.Add(tn1, m_type.dir);
                tvFileManager.SelectedNode = tn1;
                tn1.BeginEdit();
            }
        }
        private void cms_打开Clicked(object sender, EventArgs e)
        {
            if (IsFileDir[tvFileManager.SelectedNode] != m_type.file)
            {
                if (!tvFileManager.SelectedNode.IsExpanded)
                    tvFileManager.SelectedNode.Expand();
            }
            else
            {
                MessageBox.Show("这是个文件，还不支持打开操作！");
            }
        }
        private void cms_剪切Clicked(object sender, EventArgs e)
        {
            mv = RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath);
            if (IsFileDir[tvFileManager.SelectedNode] == m_type.driveDir)
            {
                mv = null;
                MessageBox.Show("不支持剪切磁盘！");
            }
            IsCopy = false;
        }
        private void cms_复制Clicked(object sender, EventArgs e)
        {
            mv = RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath);
            if (IsFileDir[tvFileManager.SelectedNode] == m_type.driveDir)
            {
                mv = null;
                MessageBox.Show("不支持复制磁盘！");
                return;
            }
            else if (IsFileDir[tvFileManager.SelectedNode] == m_type.dir)
            {
                mv = null;
                MessageBox.Show("暂不支持复制文件夹！");
                return;
            }
            IsCopy = true;
        }
        private void cms_粘贴Clicked(object sender, EventArgs e)
        {
            if (mv != null)
            {
                if (IsCopy)
                    File.Copy(mv, RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath) + "\\" + Path.GetFileName(mv), true);
                else
                    Directory.Move(mv, RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath) + "\\" + Path.GetFileName(mv));
                mv = null;
            }
            cms.Items[7].PerformClick();
        }
        private void cms_删除Clicked(object sender, EventArgs e)
        {
            if (IsFileDir[tvFileManager.SelectedNode] == m_type.dir)
                Directory.Delete(RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath), true);
            else if (IsFileDir[tvFileManager.SelectedNode] == m_type.file)
                File.Delete(RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath));
            else
                MessageBox.Show("不支持删除磁盘！");
            cms.Items[7].PerformClick();
        }
        private void cms_刷新Clicked(object sender, EventArgs e)
        {
            TreeNode tt = new TreeNode();
            if (RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath) == "C:"
                || RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath) == "D:"
                || RemoveDriveVolumeLabel(tvFileManager.SelectedNode.FullPath) == "E:")
            {
                tt = tvFileManager.SelectedNode;
            }
            else
                tt = tvFileManager.SelectedNode.Parent;
            tt.Nodes.Clear();
            LoadDirsFiles(RemoveDriveVolumeLabel(tt.FullPath) + "\\", tt.Nodes);
            foreach (TreeNode tn in tt.Nodes)
            {
                if (IsFileDir[tn] != m_type.file)
                    LoadDirsFiles(RemoveDriveVolumeLabel(tn.FullPath), tn.Nodes);
            }
        }
        private void cms_重命名Clicked(object sender, EventArgs e)
        {
            if (tvFileManager.SelectedNode.Text.Contains("C:") | tvFileManager.SelectedNode.Text.Contains("D:") | tvFileManager.SelectedNode.Text.Contains("E:"))
            {
                MessageBox.Show("不支持更改磁盘盘符!");
                return;
            }
            else
                tvFileManager.SelectedNode.BeginEdit();
        }
        #endregion

        #region 行为触发事件
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                tvFileManager.SelectedNode = e.Node;
        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            LoadDirsFiles(RemoveDriveVolumeLabel(e.Node.FullPath) + "\\", e.Node.Nodes);
            foreach (TreeNode tn in e.Node.Nodes)
            {
                if (IsFileDir[tn] != m_type.file)
                    LoadDirsFiles(RemoveDriveVolumeLabel(tn.FullPath), tn.Nodes);
            }
            //for(int i = 0;i<e.Node.Nodes.Count;i++)
            //    LoadDirsFiles(e.Node.Nodes[i].FullPath, e.Node.Nodes[i].Nodes);
        }
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.SelectedImageKey = e.Node.ImageKey;
        }
        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            //foreach (TreeNode tn in e.Node.Nodes)
            //    tn.Nodes.Clear();
        }
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Text.Contains("C:") | e.Node.Text.Contains("D:") | e.Node.Text.Contains("E:"))
            {
                return;
            }
            string name = e.Label;
            try
            {
                if (IsFileDir[e.Node] == m_type.dir)
                {
                    Directory.Move(RemoveDriveVolumeLabel(e.Node.FullPath), Path.GetDirectoryName(RemoveDriveVolumeLabel(e.Node.FullPath)) + "\\" + name);
                }
                else if (IsFileDir[e.Node] == m_type.file)
                    File.Move(RemoveDriveVolumeLabel(e.Node.FullPath), Path.GetDirectoryName(RemoveDriveVolumeLabel(e.Node.FullPath)) + "\\" + name);
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                textBox1.Text += ex.Message.Replace("\r\n", "") + "\r\n";
            }
        }
        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Text.Contains("C:") | e.Node.Text.Contains("D:") | e.Node.Text.Contains("E:"))
            {
                MessageBox.Show("不支持更改磁盘盘符！");
                e.Node.EndEdit(true);
                return;
            }
        }
        private void tvFileManager_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = RemoveDriveVolumeLabel(e.Node.FullPath);
            if (LoadIcon(Path.GetExtension(path)) == "txt" | Path.GetExtension(path) == ".cs")
            {
                //using (FileStream fsread = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                //{
                //    byte[] buffer = new byte[1024 * 1024 * 5];//定义文件缓存区
                //    int r = fsread.Read(buffer, 0, buffer.Length);
                //    _10NoteBookLearning.Main_Form.context = Encoding.UTF8.GetString(buffer, 0, r);
                //}
                //_10NoteBookLearning.Main_Form.path = path;
                //_10NoteBookLearning.Main_Form.saveFlag = true;
                //_10NoteBookLearning.Main_Form.newFlag = false;
                _10NoteBookLearning.m_FileInfo af = _10NoteBookLearning.Main_Form.ReadFile(path, ref _10NoteBookLearning.Main_Form.saveFlag, ref _10NoteBookLearning.Main_Form.newFlag);
                _10NoteBookLearning.Main_Form.context = af.fileText;
                _10NoteBookLearning.Main_Form.path = path;
                MainForm.dicForm.Keys.ElementAt(5).PerformClick();
            }
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
    }
}
