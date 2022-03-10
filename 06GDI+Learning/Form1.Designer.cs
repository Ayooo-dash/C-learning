namespace _06GDI_Learning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.picBoxVerifyCode = new System.Windows.Forms.PictureBox();
            this.btnNextCode = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVerifyCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawLine.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawLine.Location = new System.Drawing.Point(12, 618);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(1189, 30);
            this.btnDrawLine.TabIndex = 0;
            this.btnDrawLine.Text = "绘制直线";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // picBoxVerifyCode
            // 
            this.picBoxVerifyCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxVerifyCode.Location = new System.Drawing.Point(497, 478);
            this.picBoxVerifyCode.Name = "picBoxVerifyCode";
            this.picBoxVerifyCode.Size = new System.Drawing.Size(207, 86);
            this.picBoxVerifyCode.TabIndex = 1;
            this.picBoxVerifyCode.TabStop = false;
            this.picBoxVerifyCode.Click += new System.EventHandler(this.picBoxVerifyCode_Click);
            // 
            // btnNextCode
            // 
            this.btnNextCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextCode.Location = new System.Drawing.Point(12, 654);
            this.btnNextCode.Name = "btnNextCode";
            this.btnNextCode.Size = new System.Drawing.Size(1189, 30);
            this.btnNextCode.TabIndex = 2;
            this.btnNextCode.Text = "换一个验证码";
            this.btnNextCode.UseVisualStyleBackColor = true;
            this.btnNextCode.Click += new System.EventHandler(this.btnNextCode_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(486, 304);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 696);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnNextCode);
            this.Controls.Add(this.picBoxVerifyCode);
            this.Controls.Add(this.btnDrawLine);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVerifyCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.PictureBox picBoxVerifyCode;
        private System.Windows.Forms.Button btnNextCode;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

