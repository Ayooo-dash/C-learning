namespace _07窗体飞行棋
{
    partial class StartForm
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
            this.lbGameName = new System.Windows.Forms.Label();
            this.lbPlayerA = new System.Windows.Forms.Label();
            this.lbPlayerB = new System.Windows.Forms.Label();
            this.txtPlayerA = new System.Windows.Forms.TextBox();
            this.txtPlayerB = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbGameName
            // 
            this.lbGameName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbGameName.AutoSize = true;
            this.lbGameName.Font = new System.Drawing.Font("新宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbGameName.Location = new System.Drawing.Point(103, 29);
            this.lbGameName.Name = "lbGameName";
            this.lbGameName.Size = new System.Drawing.Size(217, 40);
            this.lbGameName.TabIndex = 0;
            this.lbGameName.Text = "飞行棋游戏";
            // 
            // lbPlayerA
            // 
            this.lbPlayerA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPlayerA.AutoSize = true;
            this.lbPlayerA.Location = new System.Drawing.Point(54, 117);
            this.lbPlayerA.Name = "lbPlayerA";
            this.lbPlayerA.Size = new System.Drawing.Size(60, 15);
            this.lbPlayerA.TabIndex = 1;
            this.lbPlayerA.Text = "玩家A：";
            // 
            // lbPlayerB
            // 
            this.lbPlayerB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPlayerB.AutoSize = true;
            this.lbPlayerB.Location = new System.Drawing.Point(54, 177);
            this.lbPlayerB.Name = "lbPlayerB";
            this.lbPlayerB.Size = new System.Drawing.Size(60, 15);
            this.lbPlayerB.TabIndex = 2;
            this.lbPlayerB.Text = "玩家B：";
            // 
            // txtPlayerA
            // 
            this.txtPlayerA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPlayerA.Location = new System.Drawing.Point(120, 114);
            this.txtPlayerA.Name = "txtPlayerA";
            this.txtPlayerA.Size = new System.Drawing.Size(200, 25);
            this.txtPlayerA.TabIndex = 3;
            // 
            // txtPlayerB
            // 
            this.txtPlayerB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPlayerB.Location = new System.Drawing.Point(120, 174);
            this.txtPlayerB.Name = "txtPlayerB";
            this.txtPlayerB.Size = new System.Drawing.Size(200, 25);
            this.txtPlayerB.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStart.AutoSize = true;
            this.btnStart.Location = new System.Drawing.Point(57, 260);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "开始游戏";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.Location = new System.Drawing.Point(263, 260);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 30);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "退出游戏";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(418, 326);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtPlayerB);
            this.Controls.Add(this.txtPlayerA);
            this.Controls.Add(this.lbPlayerB);
            this.Controls.Add(this.lbPlayerA);
            this.Controls.Add(this.lbGameName);
            this.Name = "StartForm";
            this.Text = "飞行棋";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbGameName;
        private System.Windows.Forms.Label lbPlayerA;
        private System.Windows.Forms.Label lbPlayerB;
        private System.Windows.Forms.TextBox txtPlayerA;
        private System.Windows.Forms.TextBox txtPlayerB;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
    }
}

