namespace Winforms
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
            this.btn_ConnectDB = new System.Windows.Forms.Button();
            this.txt_DBfileName = new System.Windows.Forms.TextBox();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.lbFileName = new System.Windows.Forms.Label();
            this.lbTableName = new System.Windows.Forms.Label();
            this.cbbRowName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnQueryData = new System.Windows.Forms.Button();
            this.cbbDataType = new System.Windows.Forms.ComboBox();
            this.btnInsertData = new System.Windows.Forms.Button();
            this.txtInsertData = new System.Windows.Forms.TextBox();
            this.btnEditValue = new System.Windows.Forms.Button();
            this.btnDeleteData = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnEditIDData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ConnectDB
            // 
            this.btn_ConnectDB.Location = new System.Drawing.Point(335, 15);
            this.btn_ConnectDB.Name = "btn_ConnectDB";
            this.btn_ConnectDB.Size = new System.Drawing.Size(102, 23);
            this.btn_ConnectDB.TabIndex = 0;
            this.btn_ConnectDB.Text = "连接数据库";
            this.btn_ConnectDB.UseVisualStyleBackColor = true;
            this.btn_ConnectDB.Click += new System.EventHandler(this.btn_ConnectDB_Click);
            // 
            // txt_DBfileName
            // 
            this.txt_DBfileName.Location = new System.Drawing.Point(78, 14);
            this.txt_DBfileName.Name = "txt_DBfileName";
            this.txt_DBfileName.Size = new System.Drawing.Size(158, 25);
            this.txt_DBfileName.TabIndex = 1;
            this.txt_DBfileName.TextChanged += new System.EventHandler(this.txt_DBfileName_TextChanged);
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(242, 15);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(87, 23);
            this.btn_SelectFile.TabIndex = 2;
            this.btn_SelectFile.Text = "选择文件";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(377, 51);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(102, 23);
            this.btnCreateTable.TabIndex = 3;
            this.btnCreateTable.Text = "创建数据表";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(78, 50);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(102, 25);
            this.txtTableName.TabIndex = 4;
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Location = new System.Drawing.Point(27, 19);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(45, 15);
            this.lbFileName.TabIndex = 5;
            this.lbFileName.Text = "文件:";
            // 
            // lbTableName
            // 
            this.lbTableName.AutoSize = true;
            this.lbTableName.Location = new System.Drawing.Point(12, 55);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.Size = new System.Drawing.Size(60, 15);
            this.lbTableName.TabIndex = 6;
            this.lbTableName.Text = "数据表:";
            // 
            // cbbRowName
            // 
            this.cbbRowName.FormattingEnabled = true;
            this.cbbRowName.Location = new System.Drawing.Point(186, 51);
            this.cbbRowName.Name = "cbbRowName";
            this.cbbRowName.Size = new System.Drawing.Size(121, 23);
            this.cbbRowName.TabIndex = 7;
            this.cbbRowName.SelectedIndexChanged += new System.EventHandler(this.cbbRowName_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(321, 51);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(321, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "去除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQueryData
            // 
            this.btnQueryData.Location = new System.Drawing.Point(485, 111);
            this.btnQueryData.Name = "btnQueryData";
            this.btnQueryData.Size = new System.Drawing.Size(102, 23);
            this.btnQueryData.TabIndex = 11;
            this.btnQueryData.Text = "查询数据";
            this.btnQueryData.UseVisualStyleBackColor = true;
            this.btnQueryData.Click += new System.EventHandler(this.btnQueryData_Click);
            // 
            // cbbDataType
            // 
            this.cbbDataType.FormattingEnabled = true;
            this.cbbDataType.Location = new System.Drawing.Point(186, 80);
            this.cbbDataType.Name = "cbbDataType";
            this.cbbDataType.Size = new System.Drawing.Size(121, 23);
            this.cbbDataType.TabIndex = 12;
            // 
            // btnInsertData
            // 
            this.btnInsertData.Location = new System.Drawing.Point(377, 111);
            this.btnInsertData.Name = "btnInsertData";
            this.btnInsertData.Size = new System.Drawing.Size(102, 23);
            this.btnInsertData.TabIndex = 13;
            this.btnInsertData.Text = "插入数据";
            this.btnInsertData.UseVisualStyleBackColor = true;
            this.btnInsertData.Click += new System.EventHandler(this.btnInsertData_Click);
            // 
            // txtInsertData
            // 
            this.txtInsertData.Location = new System.Drawing.Point(186, 110);
            this.txtInsertData.Name = "txtInsertData";
            this.txtInsertData.Size = new System.Drawing.Size(121, 25);
            this.txtInsertData.TabIndex = 14;
            // 
            // btnEditValue
            // 
            this.btnEditValue.Location = new System.Drawing.Point(321, 111);
            this.btnEditValue.Name = "btnEditValue";
            this.btnEditValue.Size = new System.Drawing.Size(50, 23);
            this.btnEditValue.TabIndex = 17;
            this.btnEditValue.Text = "修改";
            this.btnEditValue.UseVisualStyleBackColor = true;
            this.btnEditValue.Click += new System.EventHandler(this.btnEditValue_Click);
            // 
            // btnDeleteData
            // 
            this.btnDeleteData.Location = new System.Drawing.Point(485, 80);
            this.btnDeleteData.Name = "btnDeleteData";
            this.btnDeleteData.Size = new System.Drawing.Size(102, 23);
            this.btnDeleteData.TabIndex = 18;
            this.btnDeleteData.Text = "删除ID数据";
            this.btnDeleteData.UseVisualStyleBackColor = true;
            this.btnDeleteData.Click += new System.EventHandler(this.btnDeleteData_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(377, 79);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(102, 25);
            this.txtID.TabIndex = 19;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(12, 141);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(925, 434);
            this.tabControl1.TabIndex = 20;
            // 
            // btnEditIDData
            // 
            this.btnEditIDData.Location = new System.Drawing.Point(593, 80);
            this.btnEditIDData.Name = "btnEditIDData";
            this.btnEditIDData.Size = new System.Drawing.Size(102, 23);
            this.btnEditIDData.TabIndex = 21;
            this.btnEditIDData.Text = "修改ID数据";
            this.btnEditIDData.UseVisualStyleBackColor = true;
            this.btnEditIDData.Click += new System.EventHandler(this.btnEditIDData_Click);
            // 
            // SQLiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 587);
            this.Controls.Add(this.btnEditIDData);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnDeleteData);
            this.Controls.Add(this.btnEditValue);
            this.Controls.Add(this.txtInsertData);
            this.Controls.Add(this.btnInsertData);
            this.Controls.Add(this.cbbDataType);
            this.Controls.Add(this.btnQueryData);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbbRowName);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.lbFileName);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.btn_SelectFile);
            this.Controls.Add(this.txt_DBfileName);
            this.Controls.Add(this.btn_ConnectDB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SQLiteForm";
            this.Text = "SQLiteForm";
            this.Load += new System.EventHandler(this.SQLiteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ConnectDB;
        private System.Windows.Forms.TextBox txt_DBfileName;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.Label lbTableName;
        private System.Windows.Forms.ComboBox cbbRowName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnQueryData;
        private System.Windows.Forms.ComboBox cbbDataType;
        private System.Windows.Forms.Button btnInsertData;
        private System.Windows.Forms.TextBox txtInsertData;
        private System.Windows.Forms.Button btnEditValue;
        private System.Windows.Forms.Button btnDeleteData;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnEditIDData;
    }
}