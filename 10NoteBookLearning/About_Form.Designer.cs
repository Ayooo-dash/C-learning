namespace _10NoteBookLearning
{
    partial class About_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbAbout = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbAbout
            // 
            this.lbAbout.Location = new System.Drawing.Point(48, 135);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(198, 39);
            this.lbAbout.TabIndex = 0;
            this.lbAbout.Text = "这是一个写着玩的简单的记事本应用！";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(93, 33);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(96, 28);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "记事本";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "作者：Nobody";
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(98, 218);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(91, 25);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // About_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 253);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About_Form";
            this.Text = "关于\"记事本\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAbout;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnYes;
    }
}