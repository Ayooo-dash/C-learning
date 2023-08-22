namespace _03SocketLearning
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
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbbConnected = new System.Windows.Forms.ComboBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.lbIPServer = new System.Windows.Forms.Label();
            this.lbPort = new System.Windows.Forms.Label();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.lbConnected = new System.Windows.Forms.Label();
            this.lbReceive = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.lbSend = new System.Windows.Forms.Label();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnShake = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cbIPAddress = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(111, 57);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(85, 25);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "10000";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(304, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 25);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始监听";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbbConnected
            // 
            this.cbbConnected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbConnected.FormattingEnabled = true;
            this.cbbConnected.Location = new System.Drawing.Point(111, 98);
            this.cbbConnected.Name = "cbbConnected";
            this.cbbConnected.Size = new System.Drawing.Size(205, 23);
            this.cbbConnected.TabIndex = 4;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(13, 356);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(586, 136);
            this.txtSend.TabIndex = 5;
            // 
            // lbIPServer
            // 
            this.lbIPServer.AutoSize = true;
            this.lbIPServer.Location = new System.Drawing.Point(13, 22);
            this.lbIPServer.Name = "lbIPServer";
            this.lbIPServer.Size = new System.Drawing.Size(68, 15);
            this.lbIPServer.TabIndex = 6;
            this.lbIPServer.Text = "IP地址：";
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(13, 60);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(52, 15);
            this.lbPort.TabIndex = 7;
            this.lbPort.Text = "端口：";
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(13, 167);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(586, 147);
            this.txtReceive.TabIndex = 9;
            this.txtReceive.TextChanged += new System.EventHandler(this.txtReceive_TextChanged);
            // 
            // lbConnected
            // 
            this.lbConnected.AutoSize = true;
            this.lbConnected.Location = new System.Drawing.Point(13, 101);
            this.lbConnected.Name = "lbConnected";
            this.lbConnected.Size = new System.Drawing.Size(83, 15);
            this.lbConnected.TabIndex = 10;
            this.lbConnected.Text = "已连接IP：";
            // 
            // lbReceive
            // 
            this.lbReceive.AutoSize = true;
            this.lbReceive.Location = new System.Drawing.Point(13, 149);
            this.lbReceive.Name = "lbReceive";
            this.lbReceive.Size = new System.Drawing.Size(82, 15);
            this.lbReceive.TabIndex = 11;
            this.lbReceive.Text = "接收数据：";
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(423, 24);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(127, 15);
            this.lbState.TabIndex = 12;
            this.lbState.Text = "监听状态：未监听";
            // 
            // lbSend
            // 
            this.lbSend.AutoSize = true;
            this.lbSend.Location = new System.Drawing.Point(13, 338);
            this.lbSend.Name = "lbSend";
            this.lbSend.Size = new System.Drawing.Size(82, 15);
            this.lbSend.TabIndex = 13;
            this.lbSend.Text = "发送数据：";
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(13, 507);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(586, 25);
            this.btnSendMsg.TabIndex = 14;
            this.btnSendMsg.Text = "发送消息";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(505, 538);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(94, 25);
            this.btnSendFile.TabIndex = 15;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnShake
            // 
            this.btnShake.Location = new System.Drawing.Point(13, 567);
            this.btnShake.Name = "btnShake";
            this.btnShake.Size = new System.Drawing.Size(586, 25);
            this.btnShake.TabIndex = 16;
            this.btnShake.Text = "震动窗口";
            this.btnShake.UseVisualStyleBackColor = true;
            this.btnShake.Click += new System.EventHandler(this.btnShake_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(408, 538);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(91, 25);
            this.btnSelectFile.TabIndex = 17;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(13, 536);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(389, 25);
            this.txtPath.TabIndex = 18;
            // 
            // cbIPAddress
            // 
            this.cbIPAddress.FormattingEnabled = true;
            this.cbIPAddress.Location = new System.Drawing.Point(111, 19);
            this.cbIPAddress.Name = "cbIPAddress";
            this.cbIPAddress.Size = new System.Drawing.Size(168, 23);
            this.cbIPAddress.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 600);
            this.Controls.Add(this.cbIPAddress);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnShake);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.lbSend);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.lbReceive);
            this.Controls.Add(this.lbConnected);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.lbIPServer);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.cbbConnected);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtPort);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cbbConnected;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label lbIPServer;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.Label lbConnected;
        private System.Windows.Forms.Label lbReceive;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label lbSend;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnShake;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ComboBox cbIPAddress;
    }
}

