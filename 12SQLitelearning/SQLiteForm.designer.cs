namespace _12SQLitelearning
{
    partial class SQLiteForm
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
            this.txt_DBfileName = new System.Windows.Forms.TextBox();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.lbFileName = new System.Windows.Forms.Label();
            this.btnQueryData = new System.Windows.Forms.Button();
            this.panel_DBData = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txt_DBfileName
            // 
            this.txt_DBfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DBfileName.Location = new System.Drawing.Point(67, 16);
            this.txt_DBfileName.Name = "txt_DBfileName";
            this.txt_DBfileName.Size = new System.Drawing.Size(658, 25);
            this.txt_DBfileName.TabIndex = 1;
            this.txt_DBfileName.TextChanged += new System.EventHandler(this.txt_DBfileName_TextChanged);
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SelectFile.Location = new System.Drawing.Point(731, 16);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(87, 25);
            this.btn_SelectFile.TabIndex = 2;
            this.btn_SelectFile.Text = "选择文件";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Location = new System.Drawing.Point(16, 21);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(45, 15);
            this.lbFileName.TabIndex = 5;
            this.lbFileName.Text = "文件:";
            // 
            // btnQueryData
            // 
            this.btnQueryData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryData.Location = new System.Drawing.Point(824, 16);
            this.btnQueryData.Name = "btnQueryData";
            this.btnQueryData.Size = new System.Drawing.Size(102, 25);
            this.btnQueryData.TabIndex = 11;
            this.btnQueryData.Text = "查询数据";
            this.btnQueryData.UseVisualStyleBackColor = true;
            this.btnQueryData.Click += new System.EventHandler(this.btnQueryData_Click);
            // 
            // panel_DBData
            // 
            this.panel_DBData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_DBData.Location = new System.Drawing.Point(12, 65);
            this.panel_DBData.Name = "panel_DBData";
            this.panel_DBData.Size = new System.Drawing.Size(925, 510);
            this.panel_DBData.TabIndex = 21;
            // 
            // SQLiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 587);
            this.Controls.Add(this.panel_DBData);
            this.Controls.Add(this.btnQueryData);
            this.Controls.Add(this.lbFileName);
            this.Controls.Add(this.btn_SelectFile);
            this.Controls.Add(this.txt_DBfileName);
            this.Name = "SQLiteForm";
            this.Text = "SQLiteForm";
            this.Load += new System.EventHandler(this.SQLiteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_DBfileName;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.Button btnQueryData;
        private System.Windows.Forms.Panel panel_DBData;
    }
}