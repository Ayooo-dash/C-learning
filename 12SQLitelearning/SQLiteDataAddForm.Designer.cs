namespace _12SQLitelearning
{
    partial class SQLiteDataAddForm
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
            this.dataGridView_AddRows = new System.Windows.Forms.DataGridView();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddRows)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_AddRows
            // 
            this.dataGridView_AddRows.AllowUserToAddRows = false;
            this.dataGridView_AddRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_AddRows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_AddRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AddRows.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_AddRows.Name = "dataGridView_AddRows";
            this.dataGridView_AddRows.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView_AddRows.RowTemplate.Height = 27;
            this.dataGridView_AddRows.Size = new System.Drawing.Size(463, 201);
            this.dataGridView_AddRows.TabIndex = 0;
            this.dataGridView_AddRows.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AddRows_CellValueChanged);
            this.dataGridView_AddRows.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView_AddRows_ColumnWidthChanged);
            this.dataGridView_AddRows.CurrentCellChanged += new System.EventHandler(this.dataGridView_AddRows_CurrentCellChanged);
            this.dataGridView_AddRows.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_AddRows_Scroll);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnConfirm.Location = new System.Drawing.Point(248, 219);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(102, 28);
            this.btnConfirm.TabIndex = 23;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(361, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 28);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddRow.Location = new System.Drawing.Point(22, 219);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(102, 28);
            this.btnAddRow.TabIndex = 25;
            this.btnAddRow.Text = "添加行";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteRow.Location = new System.Drawing.Point(135, 219);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(102, 28);
            this.btnDeleteRow.TabIndex = 26;
            this.btnDeleteRow.Text = "删除选择行";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // SQLiteDataAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 253);
            this.Controls.Add(this.btnDeleteRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.dataGridView_AddRows);
            this.Name = "SQLiteDataAddForm";
            this.Text = "SQLiteDataAddForm";
            this.Load += new System.EventHandler(this.SQLiteDataAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AddRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_AddRows;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteRow;
    }
}