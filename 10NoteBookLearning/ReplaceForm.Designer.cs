namespace _10NoteBookLearning
{
    partial class ReplaceForm
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
            this.txtSearchContent = new System.Windows.Forms.TextBox();
            this.lbSearch = new System.Windows.Forms.Label();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.lbReplace = new System.Windows.Forms.Label();
            this.txtReplaceContent = new System.Windows.Forms.TextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnAllReplace = new System.Windows.Forms.Button();
            this.btnCannel = new System.Windows.Forms.Button();
            this.cbLoop = new System.Windows.Forms.CheckBox();
            this.cbDiffUpperLower = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtSearchContent
            // 
            this.txtSearchContent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearchContent.HideSelection = false;
            this.txtSearchContent.Location = new System.Drawing.Point(91, 30);
            this.txtSearchContent.Name = "txtSearchContent";
            this.txtSearchContent.Size = new System.Drawing.Size(311, 27);
            this.txtSearchContent.TabIndex = 5;
            this.txtSearchContent.WordWrap = false;
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Location = new System.Drawing.Point(8, 36);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(75, 15);
            this.lbSearch.TabIndex = 4;
            this.lbSearch.Text = "查找内容:";
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(421, 12);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(120, 32);
            this.btnSearchNext.TabIndex = 6;
            this.btnSearchNext.Text = "查找下一个";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // lbReplace
            // 
            this.lbReplace.AutoSize = true;
            this.lbReplace.Location = new System.Drawing.Point(8, 69);
            this.lbReplace.Name = "lbReplace";
            this.lbReplace.Size = new System.Drawing.Size(60, 15);
            this.lbReplace.TabIndex = 7;
            this.lbReplace.Text = "替换为:";
            // 
            // txtReplaceContent
            // 
            this.txtReplaceContent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReplaceContent.Location = new System.Drawing.Point(91, 63);
            this.txtReplaceContent.Name = "txtReplaceContent";
            this.txtReplaceContent.Size = new System.Drawing.Size(311, 27);
            this.txtReplaceContent.TabIndex = 8;
            this.txtReplaceContent.WordWrap = false;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(421, 50);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(120, 32);
            this.btnReplace.TabIndex = 9;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnAllReplace
            // 
            this.btnAllReplace.Location = new System.Drawing.Point(421, 88);
            this.btnAllReplace.Name = "btnAllReplace";
            this.btnAllReplace.Size = new System.Drawing.Size(120, 32);
            this.btnAllReplace.TabIndex = 10;
            this.btnAllReplace.Text = "全部替换";
            this.btnAllReplace.UseVisualStyleBackColor = true;
            this.btnAllReplace.Click += new System.EventHandler(this.btnAllReplace_Click);
            // 
            // btnCannel
            // 
            this.btnCannel.Location = new System.Drawing.Point(421, 126);
            this.btnCannel.Name = "btnCannel";
            this.btnCannel.Size = new System.Drawing.Size(120, 32);
            this.btnCannel.TabIndex = 11;
            this.btnCannel.Text = "取消";
            this.btnCannel.UseVisualStyleBackColor = true;
            this.btnCannel.Click += new System.EventHandler(this.btnCannel_Click);
            // 
            // cbLoop
            // 
            this.cbLoop.AutoSize = true;
            this.cbLoop.Location = new System.Drawing.Point(11, 182);
            this.cbLoop.Name = "cbLoop";
            this.cbLoop.Size = new System.Drawing.Size(59, 19);
            this.cbLoop.TabIndex = 13;
            this.cbLoop.Text = "循环";
            this.cbLoop.UseVisualStyleBackColor = true;
            // 
            // cbDiffUpperLower
            // 
            this.cbDiffUpperLower.AutoSize = true;
            this.cbDiffUpperLower.Location = new System.Drawing.Point(11, 150);
            this.cbDiffUpperLower.Name = "cbDiffUpperLower";
            this.cbDiffUpperLower.Size = new System.Drawing.Size(104, 19);
            this.cbDiffUpperLower.TabIndex = 12;
            this.cbDiffUpperLower.Text = "区分大小写";
            this.cbDiffUpperLower.UseVisualStyleBackColor = true;
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 211);
            this.Controls.Add(this.cbLoop);
            this.Controls.Add(this.cbDiffUpperLower);
            this.Controls.Add(this.btnCannel);
            this.Controls.Add(this.btnAllReplace);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.txtReplaceContent);
            this.Controls.Add(this.lbReplace);
            this.Controls.Add(this.btnSearchNext);
            this.Controls.Add(this.txtSearchContent);
            this.Controls.Add(this.lbSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReplaceForm";
            this.Text = "替换";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplaceForm_FormClosing);
            this.Load += new System.EventHandler(this.ReplaceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchContent;
        private System.Windows.Forms.Label lbSearch;
        public System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.Label lbReplace;
        private System.Windows.Forms.TextBox txtReplaceContent;
        public System.Windows.Forms.Button btnReplace;
        public System.Windows.Forms.Button btnAllReplace;
        public System.Windows.Forms.Button btnCannel;
        private System.Windows.Forms.CheckBox cbLoop;
        private System.Windows.Forms.CheckBox cbDiffUpperLower;
    }
}