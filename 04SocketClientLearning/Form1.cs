using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04SocketClientLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread th;
        Socket socket;
        IPAddress ip;
        IPEndPoint point;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (lbConnectState.Text.Contains("未连接") | lbConnectState.Text.Contains("断开连接"))
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ip = IPAddress.Parse(cbIPAddress.SelectedItem.ToString());
                point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
                try
                {
                    socket.Connect(point);
                    lbConnectState.Text = "连接状态：已连接";
                    socket.Send(RebuildBuf(0, "Connected!"));
                    th = new Thread(Receive);
                    th.IsBackground = true;
                    th.Start(socket);
                }
                catch
                {
                    MessageBox.Show("服务端未监听，无法连接！");
                }
            }
            else
            {
                MessageBox.Show("已连接");
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (lbConnectState.Text.Contains("已连接"))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txtSend.Text);
                List<byte> bufferList = new List<byte>();
                bufferList.Add(0);
                bufferList.AddRange(buffer);
                byte[] buffer1 = bufferList.ToArray();
                socket.Send(buffer1);
            }
            else
            {
                MessageBox.Show("未连接！");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

            if (lbConnectState.Text.Contains("已连接"))
            {
                socket.Send(RebuildBuf(0, "Disconnected!"));
                socket.Close();
                //socket.Disconnect(true);
                lbConnectState.Text = "连接状态：断开连接";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDisconnect.PerformClick();
            if (th != null)
            {
                th.Abort();
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="o"></param>
        public void Receive(object o)
        {
            Socket socket = o as Socket;
            List<ArraySegment<byte>> List = new List<ArraySegment<byte>>();
            while (true)
            {
                int rec = 0;
                List<byte> BufferList = new List<byte>();
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1024 * 1024 * 5];
                        rec = socket.Receive(buffer);
                        BufferList.AddRange(buffer.Take(rec));
                        if (rec < buffer.Length)
                            break;
                    }
                    if (BufferList[0] == 0)
                    {
                        string str = Encoding.UTF8.GetString(BufferList.ToArray(), 1, BufferList.Count - 1);
                        txtReceive.Text = txtReceive.Text + socket.RemoteEndPoint.ToString() + "：" + str + "\r\n";
                    }
                    else if (BufferList[0] == 1)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = @"E:\GZY";
                        sfd.Filter = "所有文件 | *.*";
                        sfd.Title = "保存文件";
                        sfd.ShowDialog(this);
                        using (FileStream fswrite = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fswrite.Write(BufferList.ToArray(), 1, BufferList.Count - 1);
                        }
                        MessageBox.Show("保存文件成功！");
                    }
                    else if (BufferList[0] == 2)
                    {
                        Shaking();
                        string str = Encoding.UTF8.GetString(BufferList.ToArray(), 1, BufferList.Count - 1);
                        txtReceive.Text = txtReceive.Text + socket.RemoteEndPoint.ToString() + "：" + str + "\r\n";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipp in ips)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    cbIPAddress.Items.Add(ipp.ToString());
                }
            }
            cbIPAddress.SelectedIndex = 0;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"E:\GZY";
            ofd.Filter = "所有文件 | *.*";
            ofd.Title = "选择要发送的文件";
            ofd.ShowDialog();
            txtPath.Text = ofd.FileName;
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {

            if (lbConnectState.Text.Contains("已连接"))
            {
                using (FileStream fsread = new FileStream(txtPath.Text, FileMode.Open, FileAccess.Read))
                {
                    List<byte> bufferList = new List<byte>();
                    bufferList.Add(1);
                    while (true)
                    {
                        byte[] buffer = new byte[1024 * 1024 * 5];//定义文件缓存区
                        int r = fsread.Read(buffer, 0, buffer.Length);
                        bufferList.AddRange(buffer.Take(r));
                        if (r < buffer.Length)
                            break;
                    }
                    socket.Send(bufferList.ToArray(), 0, bufferList.Count, SocketFlags.None);
                }
            }
            else
            {
                MessageBox.Show("未连接！");
            }
        }
        public byte[] RebuildBuf(byte flag, string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            List<byte> bufferList = new List<byte>();
            bufferList.Add(flag);
            bufferList.AddRange(buffer);
            return bufferList.ToArray();
        }

        public void Shaking()
        {
            int x = this.Location.X, y = this.Location.Y;
            for (int i = 0; i < 500; i++)
            {
                this.Location = new Point(x - 5, y + 5);
                this.Location = new Point(x + 10, y - 10);
            }
            this.Location = new Point(x, y);
        }

        private void btnShake_Click(object sender, EventArgs e)
        {
            socket.Send(RebuildBuf(2, "震死你个龟孙"));
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.Text.Length;
            txtReceive.ScrollToCaret();
        }
    }
}
