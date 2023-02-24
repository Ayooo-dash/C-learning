namespace Winforms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPicForm = new System.Windows.Forms.Button();
            this.btnForm2 = new System.Windows.Forms.Button();
            this.btnForm3 = new System.Windows.Forms.Button();
            this.btnPaintForm = new System.Windows.Forms.Button();
            this.btnNoteBookForm = new System.Windows.Forms.Button();
            this.btnSocketClientForm = new System.Windows.Forms.Button();
            this.btnGameStartForm = new System.Windows.Forms.Button();
            this.btnGameForm = new System.Windows.Forms.Button();
            this.btnLoginForm = new System.Windows.Forms.Button();
            this.btnSocketServerForm = new System.Windows.Forms.Button();
            this.lbName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 452);
            this.panel1.TabIndex = 0;
            // 
            // btnPicForm
            // 
            this.btnPicForm.Location = new System.Drawing.Point(0, 21);
            this.btnPicForm.Name = "btnPicForm";
            this.btnPicForm.Size = new System.Drawing.Size(60, 60);
            this.btnPicForm.TabIndex = 1;
            this.btnPicForm.UseVisualStyleBackColor = true;
            this.btnPicForm.Click += new System.EventHandler(this.BtnPicForm_Click);
            // 
            // btnForm2
            // 
            this.btnForm2.Location = new System.Drawing.Point(66, 21);
            this.btnForm2.Name = "btnForm2";
            this.btnForm2.Size = new System.Drawing.Size(60, 60);
            this.btnForm2.TabIndex = 2;
            this.btnForm2.UseVisualStyleBackColor = true;
            this.btnForm2.Click += new System.EventHandler(this.BtnForm2_Click);
            // 
            // btnForm3
            // 
            this.btnForm3.Location = new System.Drawing.Point(132, 21);
            this.btnForm3.Name = "btnForm3";
            this.btnForm3.Size = new System.Drawing.Size(60, 60);
            this.btnForm3.TabIndex = 3;
            this.btnForm3.UseVisualStyleBackColor = true;
            this.btnForm3.Click += new System.EventHandler(this.btnForm3_Click);
            // 
            // btnPaintForm
            // 
            this.btnPaintForm.Location = new System.Drawing.Point(198, 21);
            this.btnPaintForm.Name = "btnPaintForm";
            this.btnPaintForm.Size = new System.Drawing.Size(60, 60);
            this.btnPaintForm.TabIndex = 4;
            this.btnPaintForm.UseVisualStyleBackColor = true;
            this.btnPaintForm.Click += new System.EventHandler(this.btnPaintForm_Click);
            // 
            // btnNoteBookForm
            // 
            this.btnNoteBookForm.Location = new System.Drawing.Point(264, 21);
            this.btnNoteBookForm.Name = "btnNoteBookForm";
            this.btnNoteBookForm.Size = new System.Drawing.Size(60, 60);
            this.btnNoteBookForm.TabIndex = 5;
            this.btnNoteBookForm.UseVisualStyleBackColor = true;
            this.btnNoteBookForm.Click += new System.EventHandler(this.btnNoteBookForm_Click);
            // 
            // btnSocketClientForm
            // 
            this.btnSocketClientForm.Location = new System.Drawing.Point(653, 21);
            this.btnSocketClientForm.Name = "btnSocketClientForm";
            this.btnSocketClientForm.Size = new System.Drawing.Size(60, 60);
            this.btnSocketClientForm.TabIndex = 7;
            this.btnSocketClientForm.UseVisualStyleBackColor = true;
            this.btnSocketClientForm.Click += new System.EventHandler(this.btnSocketClientForm_Click);
            // 
            // btnGameStartForm
            // 
            this.btnGameStartForm.Location = new System.Drawing.Point(719, 21);
            this.btnGameStartForm.Name = "btnGameStartForm";
            this.btnGameStartForm.Size = new System.Drawing.Size(60, 60);
            this.btnGameStartForm.TabIndex = 8;
            this.btnGameStartForm.UseVisualStyleBackColor = true;
            this.btnGameStartForm.Click += new System.EventHandler(this.btnGameForm_Click);
            // 
            // btnGameForm
            // 
            this.btnGameForm.Location = new System.Drawing.Point(785, 21);
            this.btnGameForm.Name = "btnGameForm";
            this.btnGameForm.Size = new System.Drawing.Size(60, 60);
            this.btnGameForm.TabIndex = 9;
            this.btnGameForm.UseVisualStyleBackColor = true;
            this.btnGameForm.Click += new System.EventHandler(this.btnGameForm_Click_1);
            // 
            // btnLoginForm
            // 
            this.btnLoginForm.Location = new System.Drawing.Point(851, 21);
            this.btnLoginForm.Name = "btnLoginForm";
            this.btnLoginForm.Size = new System.Drawing.Size(60, 60);
            this.btnLoginForm.TabIndex = 10;
            this.btnLoginForm.UseVisualStyleBackColor = true;
            this.btnLoginForm.Click += new System.EventHandler(this.BtnLoginForm_Click);
            // 
            // btnSocketServerForm
            // 
            this.btnSocketServerForm.Location = new System.Drawing.Point(587, 21);
            this.btnSocketServerForm.Name = "btnSocketServerForm";
            this.btnSocketServerForm.Size = new System.Drawing.Size(60, 60);
            this.btnSocketServerForm.TabIndex = 6;
            this.btnSocketServerForm.UseVisualStyleBackColor = true;
            this.btnSocketServerForm.Click += new System.EventHandler(this.btnSocketServerForm_Click);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 22F);
            this.lbName.Location = new System.Drawing.Point(346, 34);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(226, 37);
            this.lbName.TabIndex = 11;
            this.lbName.Text = "WinFormsApp";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnLoginForm);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.btnPicForm);
            this.panel2.Controls.Add(this.btnForm2);
            this.panel2.Controls.Add(this.btnGameForm);
            this.panel2.Controls.Add(this.btnForm3);
            this.panel2.Controls.Add(this.btnGameStartForm);
            this.panel2.Controls.Add(this.btnPaintForm);
            this.panel2.Controls.Add(this.btnSocketClientForm);
            this.panel2.Controls.Add(this.btnNoteBookForm);
            this.panel2.Controls.Add(this.btnSocketServerForm);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(911, 100);
            this.panel2.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(426, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 582);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPicForm;
        private System.Windows.Forms.Button btnForm2;
        private System.Windows.Forms.Button btnForm3;
        private System.Windows.Forms.Button btnPaintForm;
        private System.Windows.Forms.Button btnNoteBookForm;
        private System.Windows.Forms.Button btnSocketClientForm;
        private System.Windows.Forms.Button btnGameStartForm;
        private System.Windows.Forms.Button btnGameForm;
        private System.Windows.Forms.Button btnLoginForm;
        private System.Windows.Forms.Button btnSocketServerForm;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer2;
    }
}

