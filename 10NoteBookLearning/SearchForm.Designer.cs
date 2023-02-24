namespace _10NoteBookLearning
{
    partial class SearchForm
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
            this.lbSearch = new System.Windows.Forms.Label();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.btnCannel = new System.Windows.Forms.Button();
            this.txtSearchContent = new System.Windows.Forms.TextBox();
            this.groupBoxDirection = new System.Windows.Forms.GroupBox();
            this.rbDownward = new System.Windows.Forms.RadioButton();
            this.rbUpward = new System.Windows.Forms.RadioButton();
            this.cbDiffUpperLower = new System.Windows.Forms.CheckBox();
            this.cbLoop = new System.Windows.Forms.CheckBox();
            this.groupBoxDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Location = new System.Drawing.Point(12, 26);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(75, 15);
            this.lbSearch.TabIndex = 0;
            this.lbSearch.Text = "查找内容:";
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(496, 12);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(120, 35);
            this.btnSearchNext.TabIndex = 1;
            this.btnSearchNext.Text = "查找下一个";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // btnCannel
            // 
            this.btnCannel.Location = new System.Drawing.Point(496, 53);
            this.btnCannel.Name = "btnCannel";
            this.btnCannel.Size = new System.Drawing.Size(120, 35);
            this.btnCannel.TabIndex = 2;
            this.btnCannel.Text = "取消";
            this.btnCannel.UseVisualStyleBackColor = true;
            this.btnCannel.Click += new System.EventHandler(this.btnCannel_Click);
            // 
            // txtSearchContent
            // 
            this.txtSearchContent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearchContent.Location = new System.Drawing.Point(93, 19);
            this.txtSearchContent.Name = "txtSearchContent";
            this.txtSearchContent.Size = new System.Drawing.Size(377, 27);
            this.txtSearchContent.TabIndex = 3;
            this.txtSearchContent.WordWrap = false;
            this.txtSearchContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchContent_KeyDown);
            // 
            // groupBoxDirection
            // 
            this.groupBoxDirection.Controls.Add(this.rbDownward);
            this.groupBoxDirection.Controls.Add(this.rbUpward);
            this.groupBoxDirection.Location = new System.Drawing.Point(327, 68);
            this.groupBoxDirection.Name = "groupBoxDirection";
            this.groupBoxDirection.Size = new System.Drawing.Size(154, 67);
            this.groupBoxDirection.TabIndex = 4;
            this.groupBoxDirection.TabStop = false;
            this.groupBoxDirection.Text = "方向";
            // 
            // rbDownward
            // 
            this.rbDownward.AutoSize = true;
            this.rbDownward.Location = new System.Drawing.Point(82, 31);
            this.rbDownward.Name = "rbDownward";
            this.rbDownward.Size = new System.Drawing.Size(58, 19);
            this.rbDownward.TabIndex = 1;
            this.rbDownward.TabStop = true;
            this.rbDownward.Text = "向下";
            this.rbDownward.UseVisualStyleBackColor = true;
            // 
            // rbUpward
            // 
            this.rbUpward.AutoSize = true;
            this.rbUpward.Location = new System.Drawing.Point(6, 31);
            this.rbUpward.Name = "rbUpward";
            this.rbUpward.Size = new System.Drawing.Size(58, 19);
            this.rbUpward.TabIndex = 0;
            this.rbUpward.TabStop = true;
            this.rbUpward.Text = "向上";
            this.rbUpward.UseVisualStyleBackColor = true;
            // 
            // cbDiffUpperLower
            // 
            this.cbDiffUpperLower.AutoSize = true;
            this.cbDiffUpperLower.Location = new System.Drawing.Point(15, 116);
            this.cbDiffUpperLower.Name = "cbDiffUpperLower";
            this.cbDiffUpperLower.Size = new System.Drawing.Size(104, 19);
            this.cbDiffUpperLower.TabIndex = 5;
            this.cbDiffUpperLower.Text = "区分大小写";
            this.cbDiffUpperLower.UseVisualStyleBackColor = true;
            // 
            // cbLoop
            // 
            this.cbLoop.AutoSize = true;
            this.cbLoop.Location = new System.Drawing.Point(15, 154);
            this.cbLoop.Name = "cbLoop";
            this.cbLoop.Size = new System.Drawing.Size(59, 19);
            this.cbLoop.TabIndex = 6;
            this.cbLoop.Text = "循环";
            this.cbLoop.UseVisualStyleBackColor = true;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 185);
            this.Controls.Add(this.cbLoop);
            this.Controls.Add(this.cbDiffUpperLower);
            this.Controls.Add(this.groupBoxDirection);
            this.Controls.Add(this.txtSearchContent);
            this.Controls.Add(this.btnCannel);
            this.Controls.Add(this.btnSearchNext);
            this.Controls.Add(this.lbSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SearchForm";
            this.Text = "查找";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.groupBoxDirection.ResumeLayout(false);
            this.groupBoxDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.Button btnCannel;
        private System.Windows.Forms.TextBox txtSearchContent;
        private System.Windows.Forms.GroupBox groupBoxDirection;
        private System.Windows.Forms.RadioButton rbDownward;
        private System.Windows.Forms.RadioButton rbUpward;
        private System.Windows.Forms.CheckBox cbDiffUpperLower;
        private System.Windows.Forms.CheckBox cbLoop;
        public System.Windows.Forms.Button btnSearchNext;
    }
}