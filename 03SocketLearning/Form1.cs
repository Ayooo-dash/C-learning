using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03SocketLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread th, th1;//等待连接线程、接收数据线程
        Socket socketwatch;
        Socket socket;
        IPAddress ip;
        IPEndPoint point;
        string str = " ";
        Dictionary<string, Socket> SocketDic = new Dictionary<string, Socket>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = IPAddress.Parse(cbIPAddress.SelectedItem.ToString());//Any;
            point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
            if (lbState.Text.Contains("未监听"))
            {
                try
                {
                    socketwatch.Bind(point);
                    socketwatch.Listen(10);
                    lbState.Text = "监听状态：已监听";
                    th = new Thread(WaitListen);
                    th.IsBackground = true;
                    th.Start(socketwatch);
                }
                catch
                {
                    MessageBox.Show("无效ip地址");
                }
            }
            else
            {
                MessageBox.Show("已开启监听！");
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

        /// <summary>
        /// 等待连接
        /// </summary>
        public void WaitListen(object o)
        {
            Socket socketwatch = o as Socket;
            while (true)
            {
                socket = socketwatch.Accept();
                SocketDic.Add(socket.RemoteEndPoint.ToString(), socket);
                cbbConnected.Items.Add(socket.RemoteEndPoint.ToString()); // + "：连接成功......\r\n";
                th1 = new Thread(Receive);
                th1.IsBackground = true;
                th1.Start(socket);
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (cbbConnected.SelectedItem != null)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txtSend.Text);
                List<byte> bufferList = new List<byte>();
                bufferList.Add(0);
                bufferList.AddRange(buffer);
                SocketDic[cbbConnected.SelectedItem.ToString()].Send(bufferList.ToArray());
            }
            else
            {
                MessageBox.Show("未选择可连接主机的IP");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lbConnected.Text.Contains("已监听"))
            {
                socket.Close();
                socketwatch.Close();
            }
            //if (th1 != null)
            //{
            //    th1.Abort();
            //}
            //if (th != null)
            //{
            //    th.Abort();
            //}
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            //string path = txtPath.Text;
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
                SocketDic[cbbConnected.SelectedItem.ToString()].Send(bufferList.ToArray(), 0, bufferList.Count, SocketFlags.None);
            }
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

        /// <summary>
        /// 接收数据
        /// </summary>
        public void Receive(object o)
        {
            Socket socket = o as Socket;
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
                        str = Encoding.UTF8.GetString(BufferList.ToArray(), 1, BufferList.Count - 1);
                        txtReceive.Text = txtReceive.Text + socket.RemoteEndPoint.ToString() + "：" + str + "\r\n";
                        if (str.Contains("Disconnect"))
                            cbbConnected.Items.Remove(socket.RemoteEndPoint.ToString());
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
                        str = Encoding.UTF8.GetString(BufferList.ToArray(), 1, BufferList.Count - 1);
                        txtReceive.Text = txtReceive.Text + socket.RemoteEndPoint.ToString() + "：" + str + "\r\n";
                        Shaking();
                    }
                }
                catch(Exception ex)
                {
                    //cbbConnected.Items.Remove(socket.RemoteEndPoint.ToString());
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnShake_Click(object sender, EventArgs e)
        {
            if (cbbConnected.SelectedItem != null)
            {
                SocketDic[cbbConnected.SelectedItem.ToString()].Send(RebuildBuf(2, "震死你个龟孙"));
            }
            else
            {
                MessageBox.Show("未选择可连接主机的IP");
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

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.Text.Length;
            txtReceive.ScrollToCaret();
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
    }
}
