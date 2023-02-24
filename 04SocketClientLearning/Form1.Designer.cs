namespace _04SocketClientLearning
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.lbReceive = new System.Windows.Forms.Label();
            this.lbSend = new System.Windows.Forms.Label();
            this.lbConnectState = new System.Windows.Forms.Label();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnShake = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cbIPAddress = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(313, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(87, 25);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "ip地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(106, 53);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 25);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "8080";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(313, 56);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(87, 25);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(32, 114);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(497, 122);
            this.txtReceive.TabIndex = 6;
            this.txtReceive.TextChanged += new System.EventHandler(this.txtReceive_TextChanged);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(32, 269);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(497, 112);
            this.txtSend.TabIndex = 7;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(32, 397);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(497, 25);
            this.btnSendMsg.TabIndex = 8;
            this.btnSendMsg.Text = "发送消息";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // lbReceive
            // 
            this.lbReceive.AutoSize = true;
            this.lbReceive.Location = new System.Drawing.Point(29, 96);
            this.lbReceive.Name = "lbReceive";
            this.lbReceive.Size = new System.Drawing.Size(82, 15);
            this.lbReceive.TabIndex = 9;
            this.lbReceive.Text = "接收数据：";
            // 
            // lbSend
            // 
            this.lbSend.AutoSize = true;
            this.lbSend.Location = new System.Drawing.Point(29, 251);
            this.lbSend.Name = "lbSend";
            this.lbSend.Size = new System.Drawing.Size(82, 15);
            this.lbSend.TabIndex = 10;
            this.lbSend.Text = "发送数据：";
            // 
            // lbConnectState
            // 
            this.lbConnectState.AutoSize = true;
            this.lbConnectState.Location = new System.Drawing.Point(417, 24);
            this.lbConnectState.Name = "lbConnectState";
            this.lbConnectState.Size = new System.Drawing.Size(127, 15);
            this.lbConnectState.TabIndex = 11;
            this.lbConnectState.Text = "连接状态：未连接";
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(438, 428);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(91, 25);
            this.btnSendFile.TabIndex = 12;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnShake
            // 
            this.btnShake.Location = new System.Drawing.Point(32, 459);
            this.btnShake.Name = "btnShake";
            this.btnShake.Size = new System.Drawing.Size(497, 25);
            this.btnShake.TabIndex = 13;
            this.btnShake.Text = "震动窗口";
            this.btnShake.UseVisualStyleBackColor = true;
            this.btnShake.Click += new System.EventHandler(this.btnShake_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(341, 428);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(91, 25);
            this.btnSelectFile.TabIndex = 14;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(32, 428);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(294, 25);
            this.txtPath.TabIndex = 15;
            // 
            // cbIPAddress
            // 
            this.cbIPAddress.FormattingEnabled = true;
            this.cbIPAddress.Location = new System.Drawing.Point(106, 19);
            this.cbIPAddress.Name = "cbIPAddress";
            this.cbIPAddress.Size = new System.Drawing.Size(171, 23);
            this.cbIPAddress.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 494);
            this.Controls.Add(this.cbIPAddress);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnShake);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.lbConnectState);
            this.Controls.Add(this.lbSend);
            this.Controls.Add(this.lbReceive);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.Label lbReceive;
        private System.Windows.Forms.Label lbSend;
        private System.Windows.Forms.Label lbConnectState;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnShake;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ComboBox cbIPAddress;
    }
}

