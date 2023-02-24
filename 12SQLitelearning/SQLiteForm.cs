using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace _12SQLitelearning
{
    public partial class SQLiteForm : Form
    {
        private static string FilePath;
        private MySQLiteDBData m_mySQLiteDBData;

        public SQLiteForm()
        {
            InitializeComponent();
        }

        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Data Base File|*.db|All File|*.*";
            ofd.Title = "选择要打开的文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = txt_DBfileName.Text = ofd.FileName;

                m_mySQLiteDBData = new MySQLiteDBData(FilePath);
                m_mySQLiteDBData.ConnectDB();
                btnQueryData.PerformClick();
            }
            else
            {
                FilePath = txt_DBfileName.Text = "";
            }
        }

        private void txt_DBfileName_TextChanged(object sender, EventArgs e)
        {
            txt_DBfileName.SelectionStart = txt_DBfileName.Text.Length;
            txt_DBfileName.ScrollToCaret();
        }

        private void SQLiteForm_Load(object sender, EventArgs e)
        {
        }

        private void btnQueryData_Click(object sender, EventArgs e)
        {
            if (m_mySQLiteDBData == null)
                return;
            m_mySQLiteDBData.AddDBDataToTabControl(tabControl1);
        }
    }

    public class MySQLiteDBData
    {
        //互斥锁
        private static readonly object m_lock = new object();
        public string FilePath;
        private int m_TabControlSelectIndex = 0;
        private SQLiteConnection m_sqlConnection;
        private TabControl m_tabControl;
        private Dictionary<string, List<int[]>> m_dicEditIndex;
        private Dictionary<string, MyTableData> m_dicTableData = new Dictionary<string, MyTableData>();
        private Dictionary<string, MyTableData> m_dicTableTempData = new Dictionary<string, MyTableData>();
        public enum SqliteCmd
        {
            getTableName = 0,
            getColumnNameOfTable,
            getRowDataOfTable,

            createTable,
            deleteTable,
            renameTable,
            insertRowData,
            deleteRowData,
            AddColumn,
            DeleteColumn,
            updateTableData
        }
        private Dictionary<string[], EventHandler> m_dicBtn = new Dictionary<string[], EventHandler>();

        public MySQLiteDBData(SQLiteConnection sqlConn)
        {
            m_sqlConnection = sqlConn;
        }

        public MySQLiteDBData(string filePath)
        {
            FilePath = filePath;
        }

        public bool ConnectDB()
        {
            m_dicBtn.Clear();
            m_dicBtn.Add(new string[] { "btnRenameTable", "重命名表" }, BtnRenameTable_Click);
            m_dicBtn.Add(new string[] { "btnCreateTable", "创建表" }, BtnCreateTable_Click);
            m_dicBtn.Add(new string[] { "btnDeleteTable", "删除表" }, BtnDeleteTable_Click);
            m_dicBtn.Add(new string[] { "btnInsertData", "插入数据" }, btnInsertData_Click);
            m_dicBtn.Add(new string[] { "btnDeleteRow", "删除行" }, BtnDeleteRow_Click);
            m_dicBtn.Add(new string[] { "btnAddColumn", "添加列" }, BtnAddColumn_Click);
            m_dicBtn.Add(new string[] { "btnDeleteColumn", "删除列" }, BtnDeleteColumn_Click);
            m_dicBtn.Add(new string[] { "btnUpdate", "更新数据" }, BtnUpdate_Click);

            m_tabControl = new TabControl();
            m_tabControl.Dock = DockStyle.Fill;
            m_tabControl.Name = "m_tabControl";
            m_tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            if (!File.Exists(FilePath))
            {
                SQLiteConnection.CreateFile(FilePath);
            }
            try
            {
                m_sqlConnection = new SQLiteConnection("Data Source=" + FilePath + ";Version=3;");
                m_sqlConnection.Open();
                MessageBox.Show("数据库：\"" + FilePath + "\" 打开成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开数据库：" + FilePath + "的连接失败：" + ex.Message);
                return false;
            }
            return true;
        }

        public Dictionary<string, MyTableData> QueryDBData()
        {
            Dictionary<string, MyTableData> dicTableTempData = new Dictionary<string, MyTableData>();
            if (m_sqlConnection == null)
                return null;
            try
            {
                #region 抽离封装
#if false
                m_dicTableData.Clear();
                dicTableTempData.Clear();
                m_dicEditIndex = null;
                //查找DB文件中存在的表
                string sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
                SQLiteCommand command = new SQLiteCommand(sql, m_sqlConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ret = "";
                        ret = reader.GetValue(0).ToString();
                        m_dicTableData.Add(ret, new MyTableData(ret));
                        dicTableTempData.Add(ret, new MyTableData(ret));
                    }
                }
                foreach (var t in dicTableTempData.Values)
                {
                    //查找该表的列名
                    sql = $"pragma table_info({t.m_tableName})";
                    command = new SQLiteCommand(sql, m_sqlConnection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            t.m_listColumnName.Add(reader.GetValue(1).ToString());
                            m_dicTableData[t.m_tableName].m_listColumnName.Add(reader.GetValue(1).ToString());
                        }

                    }

                    //查找该表的所有数据
                    sql = $"select * from {t.m_tableName}";
                    command = new SQLiteCommand(sql, m_sqlConnection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<SQLiteRowData> list = new List<SQLiteRowData>();
                            List<SQLiteRowData> list1 = new List<SQLiteRowData>();
                            foreach (var d in t.m_listColumnName)
                            {
                                object ret = reader[d];
                                list.Add(new SQLiteRowData(d, ret, ret.GetType()));
                                list1.Add(new SQLiteRowData(d, ret, ret.GetType()));
                            }
                            t.Rows.Add(list);
                            m_dicTableData[t.m_tableName].Rows.Add(list1);
                        }
                    }
                }
#endif
                #endregion
                SendSqliteCmd(SqliteCmd.getTableName, ref dicTableTempData);
                SendSqliteCmd(SqliteCmd.getColumnNameOfTable, ref dicTableTempData);
                SendSqliteCmd(SqliteCmd.getRowDataOfTable, ref dicTableTempData);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dicTableTempData;
        }

        private void SendSqliteCmd(SqliteCmd cmd, ref Dictionary<string, MyTableData> dicTableTempData)
        {
            string sql = "";
            string tName = "";
            int index;
            SQLiteCommand command;
            SQLiteDataAddForm addForm;
            DataGridView dgv;
            switch (cmd)
            {
                case SqliteCmd.getTableName:    //查找该数据库的所有表
                    dicTableTempData = new Dictionary<string, MyTableData>();
                    m_dicTableData.Clear();
                    dicTableTempData.Clear();
                    m_dicEditIndex = null;
                    sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
                    command = new SQLiteCommand(sql, m_sqlConnection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ret = "";
                            ret = reader.GetValue(0).ToString();
                            m_dicTableData.Add(ret, new MyTableData(ret));
                            dicTableTempData.Add(ret, new MyTableData(ret));
                        }
                    }
                    break;

                case SqliteCmd.getColumnNameOfTable:    //查找该表的列名
                    foreach (var t in dicTableTempData.Values)
                    {
                        sql = $"pragma table_info({t.m_tableName})";
                        command = new SQLiteCommand(sql, m_sqlConnection);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                t.m_listColumnName.Add(reader.GetValue(1).ToString());
                                m_dicTableData[t.m_tableName].m_listColumnName.Add(reader.GetValue(1).ToString());
                            }
                        }
                    }
                    break;

                case SqliteCmd.getRowDataOfTable:   //查找该表的所有数据
                    foreach (var t in dicTableTempData.Values)
                    {
                        sql = $"select * from {t.m_tableName}";
                        command = new SQLiteCommand(sql, m_sqlConnection);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<SQLiteRowData> list = new List<SQLiteRowData>();
                                List<SQLiteRowData> list1 = new List<SQLiteRowData>();
                                foreach (var d in t.m_listColumnName)
                                {
                                    object ret = reader[d];
                                    list.Add(new SQLiteRowData(d, ret, ret.GetType()));
                                    list1.Add(new SQLiteRowData(d, ret, ret.GetType()));
                                }
                                t.Rows.Add(list);
                                m_dicTableData[t.m_tableName].Rows.Add(list1);
                            }
                        }
                    }
                    break;

                case SqliteCmd.createTable: //创建表
                    string tableName = "";
                    addForm = new SQLiteDataAddForm(AddStep.CreateTable);
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            sql = addForm.m_sqlCreateTable.Sql;
                            tableName = addForm.m_sqlCreateTable.tableName;
                            command = new SQLiteCommand(sql, m_sqlConnection);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("创建数据表" + tableName + "失败：" + ex.Message);
                        }
                        MyTableData md = new MyTableData(tableName);
                        MyTableData md1 = new MyTableData(tableName);
                        md.m_listColumnName = (from s in addForm.m_sqlCreateTable.RowTemplate select s.Name).ToList();
                        md1.m_listColumnName = (from s in addForm.m_sqlCreateTable.RowTemplate select s.Name).ToList();
                        md.Rows.RowTemplate = new List<SQLiteRowData>(addForm.m_sqlCreateTable.RowTemplate);
                        md1.Rows.RowTemplate = new List<SQLiteRowData>(addForm.m_sqlCreateTable.RowTemplate);
                        m_dicTableTempData.Add(tableName, md);
                        m_dicTableData.Add(tableName, md1);
                        AddTabPageOfTableData(md);
                        MessageBox.Show($"创建数据表{tableName}成功！");
                    }
                    break;

                case SqliteCmd.renameTable: //重命名表
                    TabPage tpp1;
                    string tNewName = "";
                    addForm = new SQLiteDataAddForm(AddStep.RenameTable);
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            tNewName = addForm.m_NewTableName;
                            tpp1 = m_tabControl.SelectedTab;
                            tName = tpp1.Text;
                            sql = $"alter table {tName} rename to {tNewName}";
                            command = new SQLiteCommand(sql, m_sqlConnection);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("数据表" + tName + $"重命名为 {tNewName}失败：" + ex.Message);
                        }
                        tpp1.Text = tNewName;
                        m_dicTableData[tName].m_tableName = tNewName;
                        m_dicTableData = m_dicTableData.ToDictionary(k => k.Key == tName ? tNewName : k.Key, k => k.Value);
                        m_dicTableTempData[tName].m_tableName = tNewName;
                        m_dicTableTempData = m_dicTableTempData.ToDictionary(k => k.Key == tName ? tNewName : k.Key, k => k.Value);
                        MessageBox.Show($"数据表{tName} " + $"重命名为 {tNewName}成功！");
                    }
                    break;

                case SqliteCmd.deleteTable: //删除表
                    command = new SQLiteCommand(sql, m_sqlConnection);
                    TabPage tpp;
                    try
                    {
                        tpp = m_tabControl.SelectedTab;
                        tName = tpp.Text;
                        sql = $"drop table {tName}";
                        command = new SQLiteCommand(sql, m_sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("删除数据表" + tName + "失败：" + ex.Message);
                    }
                    m_dicTableData.Remove(tName);
                    m_dicTableTempData.Remove(tName);
                    m_tabControl.TabPages.Remove(tpp);
                    MessageBox.Show($"删除数据表{tName}成功！");
                    break;

                case SqliteCmd.insertRowData:   //插入数据
                    tName = m_tabControl.SelectedTab.Text;
                    MyTableDataRowCollection AddRowCollection = new MyTableDataRowCollection();
                    var rowTemplate = m_dicTableTempData[tName].Rows.RowTemplate;
                    addForm = new SQLiteDataAddForm(AddStep.InsertRow, rowTemplate);
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            AddRowCollection = addForm.m_AddRowCollection;
                            foreach (var item in AddRowCollection)
                            {
                                string strName = "";
                                string strValue = "";
                                for (int i = 0; i < item.Count; i++)
                                {
                                    strName += item[i].Name;
                                    strValue += ("@" + item[i].Name);
                                    if (i == item.Count - 1)
                                        break;
                                    strName += ",";
                                    strValue += ",";
                                }
                                sql = "insert into " + tName + string.Format(" ({0})", strName) + string.Format(" values ({0})", strValue);
                                command = new SQLiteCommand(sql, m_sqlConnection);
                                foreach (var dat in item)
                                {
                                    SQLiteParameter parameter = new SQLiteParameter("@" + dat.Name, dat.Value);
                                    command.Parameters.Add(parameter);
                                }
                                command.ExecuteNonQuery();
                                m_dicTableData[tName].Rows.Add(item);
                                m_dicTableTempData[tName].Rows.Add(item);
                                dgv = (from Control d in m_tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                                dgv.Rows.Add((from s in item select s.Value).ToArray());
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"表{tName} 插入数据失败：" + ex.Message);
                        }
                        MessageBox.Show($"表{tName} 插入数据成功！");
                    }
                    break;

                case SqliteCmd.deleteRowData:   //删除行数据
                    tName = m_tabControl.SelectedTab.Text;
                    dgv = (from Control d in m_tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                    index = dgv.CurrentRow.Index;
                    var id = m_dicTableTempData[tName].Rows[index][0];
                    try
                    {
                        if (id.Name != "id")
                        {
                            throw new Exception($"表{tName}" + "不存在唯一ID");
                        }
                        sql = "delete from " + tName + " where " + "ID = " + id.Value;
                        command = new SQLiteCommand(sql, m_sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("删除数据：" + tName + ": ID = null" + " 失败：" + ex.Message);
                    }
                    m_dicTableData[tName].Rows.RemoveAt(index);
                    m_dicTableTempData[tName].Rows.RemoveAt(index);
                    dgv.Rows.RemoveAt(index);
                    MessageBox.Show("删除数据：" + tName + ": ID = " + id.Value + " 成功！");
                    break;

                case SqliteCmd.updateTableData: //更新该表更改的数据
                    try
                    {
                        tName = m_tabControl.SelectedTab.Text;
                        foreach (var m_index in m_dicEditIndex[tName])
                        {
                            var data = GetInsertDataFormat(m_dicTableTempData[tName].Rows[m_index[0]][m_index[1]]);
                            var data0 = GetInsertDataFormat(m_dicTableData[tName].Rows[m_index[0]][m_index[1]]);
                            var id1 = m_dicTableTempData[tName].Rows[m_index[0]][0];
                            sql = "";
                            if (id1.Name != "id")
                            {
                                throw new Exception($"表{tName}" + "不存在唯一ID");
                                sql = string.Format("update {0} set {1} = {2} where {3} = {4}", tName, data.Name, data.Value, data.Name, data0.Value);
                            }
                            else
                                sql = string.Format("update {0} set {1} = {2} where id = {3}", tName, data.Name, data.Value, id1.Value);
                            command = new SQLiteCommand(sql, m_sqlConnection);
                            command.ExecuteNonQuery();
                            Thread.Sleep(20);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"表{tName} " + "更新数据失败：" + ex.Message);
                    }
                    m_dicEditIndex[tName].Clear();
                    MessageBox.Show($"表{tName} " + "更新数据成功！");
                    break;

                case SqliteCmd.AddColumn:   //向该表添加列
                    addForm = new SQLiteDataAddForm(AddStep.AddColumn, null);
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        TabPage tpp2;
                        try
                        {
                            tpp2 = m_tabControl.SelectedTab;
                            dgv = (from Control d in tpp2.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                            tName = tpp2.Text;
                            var dicType = addForm.m_Type;
                            foreach (var l in addForm.m_AddColumns)
                            {
                                sql = $"alter table {tName} add column {l[0]} {l[1]}";
                                command = new SQLiteCommand(sql, m_sqlConnection);
                                command.ExecuteNonQuery();
                                index = dgv.Columns.Add(l[0], l[0]);
                                m_dicTableData[tName].m_listColumnName.Add(l[0]);
                                m_dicTableData[tName].Rows.AddColumn(l[0], dicType[l[1]]);
                                m_dicTableTempData[tName].m_listColumnName.Add(l[0]);
                                m_dicTableTempData[tName].Rows.AddColumn(l[0], dicType[l[1]]);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("数据表" + tName + $"添加列失败：" + ex.Message);
                        }
                        MessageBox.Show($"数据表{tName} " + $"添加列成功！");
                    }
                    break;

                case SqliteCmd.DeleteColumn:    //删除该表的选择列
                    TabPage tpp3;
                    string DeleteColName = "";
                    try
                    {
                        tpp3 = m_tabControl.SelectedTab;
                        dgv = (from Control d in tpp3.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                        DeleteColName = dgv.CurrentCell.OwningColumn.HeaderCell.Value.ToString();
                        tName = tpp3.Text;
                        sql = $"alter table {tName} drop column {DeleteColName}";
                        command = new SQLiteCommand(sql, m_sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("数据表" + tName + $"删除列 {DeleteColName}失败：" + ex.Message);
                    }
                    dgv.Columns.Remove(DeleteColName);
                    m_dicTableData[tName].m_listColumnName.Remove(DeleteColName);
                    m_dicTableData[tName].Rows.RemoveColumn(DeleteColName);
                    m_dicTableTempData[tName].m_listColumnName.Remove(DeleteColName);
                    m_dicTableTempData[tName].Rows.RemoveColumn(DeleteColName);
                    MessageBox.Show($"数据表{tName} " + $"删除列 {DeleteColName}成功！");
                    break;

                default:
                    break;
            }
        }

        public bool AddDBDataToTabControl(TabControl tabControl = null)
        {
            if (m_dicTableData == null)
                return false;
            try
            {
                if (tabControl != null)
                {
                    tabControl.TabPages.Clear();
                    tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
                    m_tabControl = tabControl;
                }
                else
                {
                    if (m_tabControl.TabPages != null)
                        m_tabControl.TabPages.Clear();
                }
                m_dicTableTempData.Clear();
                m_dicTableTempData = QueryDBData();
                foreach (var l in m_dicTableTempData.Values)
                {
                    AddTabPageOfTableData(l);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据失败：" + ex.Message);
                return false;
            }
            m_dicEditIndex = new Dictionary<string, List<int[]>>();
            return true;
        }

        private void AddTabPageOfTableData(MyTableData tableData)
        {
            TabPage tp = new TabPage(tableData.m_tableName);
            DataGridView dgv = new DataGridView();
            m_tabControl.TabPages.Add(tp);

            LoadButtonAndDataGridView(tp, ref dgv);

            //int tabWidth = tp.Size.Width;
            //int tabHeigth = tp.Size.Height;

            //Button btnCreateTable = new Button();
            //btnCreateTable.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //btnCreateTable.Location = new Point(tabWidth - 102 - 6, 6);
            //btnCreateTable.Name = string.Format("btnCreatetable{0}", tableData.m_tableName);
            //btnCreateTable.Size = new Size(102, 25);
            //btnCreateTable.Text = "创建数据表";
            //btnCreateTable.UseVisualStyleBackColor = true;
            //btnCreateTable.Click += BtnCreateTable_Click;

            //Button btnDeleteTable = new Button();
            //btnDeleteTable.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //btnDeleteTable.Location = new Point(tabWidth - 102 - 6, 6 + 25 + 6);
            //btnDeleteTable.Name = string.Format("btnDeleteTable{0}", tableData.m_tableName);
            //btnDeleteTable.Size = new Size(102, 25);
            //btnDeleteTable.Text = "删除数据表";
            //btnDeleteTable.UseVisualStyleBackColor = true;
            //btnDeleteTable.Click += BtnDeleteTable_Click;

            //Button btnDeleteRow = new Button();
            //btnDeleteRow.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //btnDeleteRow.Location = new Point(tabWidth - 102 - 6, 6 + (25 + 6) * 2);
            //btnDeleteRow.Name = string.Format("btnDeleteRow{0}", tableData.m_tableName);
            //btnDeleteRow.Size = new Size(102, 25);
            //btnDeleteRow.Text = "删除行";
            //btnDeleteRow.UseVisualStyleBackColor = true;
            //btnDeleteRow.Click += BtnDeleteRow_Click;

            //Button btnInsertData = new Button();
            //btnInsertData.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //btnInsertData.Location = new Point(tabWidth - 102 - 6, 6 + (25 + 6) * 3);
            //btnInsertData.Name = string.Format("btnInsertData{0}", tableData.m_tableName);
            //btnInsertData.Size = new Size(102, 25);
            //btnInsertData.Text = "插入数据";
            //btnInsertData.UseVisualStyleBackColor = true;
            //btnInsertData.Click += btnInsertData_Click;

            //Button btnUpdate = new Button();
            //btnUpdate.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //btnUpdate.Location = new Point(tabWidth - 102 - 6, 6 + (25 + 6) * 4);
            //btnUpdate.Name = string.Format("btnUpdate{0}", tableData.m_tableName);
            //btnUpdate.Size = new Size(102, 25);
            //btnUpdate.Text = "更新数据";
            //btnUpdate.UseVisualStyleBackColor = true;
            //btnUpdate.Click += BtnUpdate_Click;

            //DataGridView dgv = new DataGridView();
            //dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dgv.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            //dgv.Location = new Point(3, 3);
            //dgv.Name = tableData.m_tableName;
            //dgv.RowTemplate.Height = 27;
            //dgv.Size = new Size(tabWidth - 3 - 102 - 6 * 2, tabHeigth - 3 * 2);
            //dgv.AllowUserToAddRows = false;
            //dgv.RowHeadersVisible = false;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgv.CellValueChanged += Dgv_CellValueChanged;

            //tp.Controls.Add(dgv);
            //tp.Controls.Add(btnCreateTable);
            //tp.Controls.Add(btnDeleteTable);
            //tp.Controls.Add(btnDeleteRow);
            //tp.Controls.Add(btnInsertData);
            //tp.Controls.Add(btnUpdate);

            foreach (var d in tableData.m_listColumnName)
            {
                DataGridViewColumn dgvc = new DataGridViewColumn();
                DataGridViewCell dgvct = new DataGridViewTextBoxCell();
                dgvc.HeaderText = d;
                dgvc.Name = d;
                dgvc.CellTemplate = dgvct;
                int index = dgv.Columns.Add(dgvc);
            }
            foreach (List<SQLiteRowData> row in tableData.Rows)
            {
                int index = dgv.Rows.Add();
                dgv.Rows[index].SetValues((from s in row select s.Value).ToArray());
            }
        }

        public TabControl GetDBDataTabControl()
        {
            if (m_tabControl == null)
                return null;
            return m_tabControl;
        }

        private void LoadButtonAndDataGridView(TabPage tp, ref DataGridView dgv)
        {
            //dgv = new DataGridView();
            int tabWidth = tp.Size.Width;
            int tabHeigth = tp.Size.Height;
            for (int i = 0; i < m_dicBtn.Count; i++)
            {
                Button btn = new Button();
                btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                btn.Location = new Point(tabWidth - 102 - 6, 6 + (25 + 6) * i);
                btn.Name = string.Format("{0}{1}", m_dicBtn.ElementAt(i).Key[0], tp.Text);
                btn.Size = new Size(102, 25);
                btn.Text = m_dicBtn.ElementAt(i).Key[1];
                btn.UseVisualStyleBackColor = true;
                btn.Click += m_dicBtn.ElementAt(i).Value;
                tp.Controls.Add(btn);
            }

            dgv = new DataGridView();
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            dgv.Location = new Point(3, 3);
            dgv.Name = tp.Text;
            dgv.RowTemplate.Height = 27;
            dgv.Size = new Size(tabWidth - 3 - 102 - 6 * 2, tabHeigth - 3 * 2);
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.CellValueChanged += Dgv_CellValueChanged;
            tp.Controls.Add(dgv);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_TabControlSelectIndex = m_tabControl.SelectedIndex;
        }

        private void BtnCreateTable_Click(object sender, EventArgs e)
        {
            //string sql = "", tableName = "";
            lock (m_lock)
            {
                //SQLiteDataAddForm addForm = new SQLiteDataAddForm(AddStep.CreateTable);
                //if (addForm.ShowDialog() == DialogResult.OK)
                //{
                //    try
                //    {
                //        sql = addForm.m_sqlCreateTable.Sql;
                //        tableName = addForm.m_sqlCreateTable.tableName;
                //        SQLiteCommand command = new SQLiteCommand(sql, m_sqlConnection);
                //        command.ExecuteNonQuery();
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new Exception("创建数据表" + tableName + "失败：" + ex.Message);
                //    }
                //    MyTableData md = new MyTableData(tableName);
                //    MyTableData md1 = new MyTableData(tableName);
                //    md.m_listColumnName = (from s in addForm.m_sqlCreateTable.RowTemplate select s.Name).ToList();
                //    md1.m_listColumnName = (from s in addForm.m_sqlCreateTable.RowTemplate select s.Name).ToList();
                //    md.Rows.RowTemplate = new List<SQLiteRowData>(addForm.m_sqlCreateTable.RowTemplate);
                //    md1.Rows.RowTemplate = new List<SQLiteRowData>(addForm.m_sqlCreateTable.RowTemplate);
                //    m_dicTableTempData.Add(tableName, md);
                //    m_dicTableData.Add(tableName, md1);
                //    MessageBox.Show($"创建数据表{tableName}成功！");
                //    AddTabPageOfTableData(md);
                //    //QueryDBData(m_tabControl);
                //    //m_tabControl.SelectedIndex = m_TabControlSelectIndex;
                //}

                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.createTable, ref d);
            }
        }

        private void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.deleteTable, ref d);
            }
        }

        private void BtnRenameTable_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.renameTable, ref d);
            }
        }

        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            //string tName = m_tabControl.SelectedTab.Text;
            lock (m_lock)
            {
                //    DataGridView dgv = (from Control d in m_tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                //    int index = dgv.CurrentRow.Index;
                //    var id = m_dicTableTempData[tName].Rows[index][0];
                //    try
                //    {
                //        if (id.Name != "id")
                //        {
                //            throw new Exception($"表{tName}" + "不存在唯一ID");
                //        }
                //        string sql = "delete from " + tName + " where " + "ID = " + id.Value;
                //        SQLiteCommand command = new SQLiteCommand(sql, m_sqlConnection);
                //        command.ExecuteNonQuery();
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new Exception("删除数据：" + tName + ": ID = null" + " 失败：" + ex.Message);
                //    }
                //    m_dicTableData[tName].Rows.RemoveAt(index);
                //    m_dicTableTempData[tName].Rows.RemoveAt(index);
                //    dgv.Rows.RemoveAt(index);
                //    MessageBox.Show("删除数据：" + tName + ": ID = " + id.Value + " 成功！");
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.deleteRowData, ref d);
            }
        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            //string tName = m_tabControl.SelectedTab.Text;
            //MyTableDataRowCollection AddRowCollection = new MyTableDataRowCollection();
            lock (m_lock)
            {
                //var rowTemplate = m_dicTableTempData[tName].Rows.RowTemplate;
                //SQLiteDataAddForm addForm = new SQLiteDataAddForm(AddStep.InsertRow, rowTemplate);
                //if (addForm.ShowDialog() == DialogResult.OK)
                //{
                //try
                //{
                //    AddRowCollection = addForm.m_AddRowCollection;
                //    foreach (var item in AddRowCollection)
                //    {
                //        string strName = "";
                //        string strValue = "";
                //        for (int i = 0; i < item.Count; i++)
                //        {
                //            //if (item[i].Name == "id")
                //            //    continue;
                //            strName += item[i].Name;
                //            strValue += ("@" + item[i].Name);
                //            if (i == item.Count - 1)
                //                break;
                //            strName += ",";
                //            strValue += ",";
                //        }
                //        string sql = "insert into " + tName + string.Format(" ({0})", strName) + string.Format(" values ({0})", strValue);
                //        SQLiteCommand command = new SQLiteCommand(sql, m_sqlConnection);
                //        foreach (var dat in item)
                //        {
                //            //if (dat.Name == "id")
                //            //    continue;
                //            SQLiteParameter parameter = new SQLiteParameter("@" + dat.Name, dat.Value);
                //            command.Parameters.Add(parameter);
                //        }
                //        command.ExecuteNonQuery();
                //        m_dicTableData[tName].Rows.Add(item);
                //        m_dicTableTempData[tName].Rows.Add(item);
                //        DataGridView dgv = (from Control d in m_tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                //        dgv.Rows.Add((from s in item select s.Value).ToArray());
                //    }

                //}
                //catch (Exception ex)
                //{
                //    throw new Exception($"表{tName} 插入数据失败：" + ex.Message);
                //}
                //MessageBox.Show($"表{tName} 插入数据成功！");
                //}
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.insertRowData, ref d);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //string tName = m_tabControl.SelectedTab.Text;
            lock (m_lock)
            {
                //try
                //{
                //    //UPDATE table SET name = 'Texas' WHERE ID = 6;
                //    foreach (var index in m_dicEditIndex[tName])
                //    {
                //        var data = GetInsertDataFormat(m_dicTableTempData[tName].Rows[index[0]][index[1]]);
                //        var data0 = GetInsertDataFormat(m_dicTableData[tName].Rows[index[0]][index[1]]);
                //        var id = m_dicTableTempData[tName].Rows[index[0]][0];
                //        string sql = "";
                //        if (id.Name != "id")
                //        {
                //            throw new Exception($"表{tName}" + "不存在唯一ID");
                //            sql = string.Format("update {0} set {1} = {2} where {3} = {4}", tName, data.Name, data.Value, data.Name, data0.Value);
                //        }
                //        else
                //            sql = string.Format("update {0} set {1} = {2} where id = {3}", tName, data.Name, data.Value, id.Value);
                //        SQLiteCommand command = new SQLiteCommand(sql, m_sqlConnection);
                //        command.ExecuteNonQuery();
                //        Thread.Sleep(20);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception($"表{tName} " + "更新数据失败：" + ex.Message);
                //}
                //m_dicEditIndex[tName].Clear();
                //MessageBox.Show("Updated Successfully!");
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.updateTableData, ref d);
            }
        }

        private void BtnAddColumn_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.AddColumn, ref d);
            }
        }

        private void BtnDeleteColumn_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.DeleteColumn, ref d);
            }
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_dicEditIndex == null)
                return;
            string tName = m_tabControl.SelectedTab.Text;
            lock (m_lock)
            {
                int[] index = new int[] { e.RowIndex, e.ColumnIndex };
                try
                {
                    DataGridView dgv = (from Control d in m_tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                    m_dicTableTempData[tName].Rows[e.RowIndex][e.ColumnIndex].Value = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    //var x = m_dicTableData[tName].Rows[e.RowIndex][e.ColumnIndex].Value;
                    if (!m_dicEditIndex.Keys.Contains(tName))
                        m_dicEditIndex.Add(tName, new List<int[]>());
                    if (!m_dicEditIndex[tName].Contains(index))
                        m_dicEditIndex[tName].Add(index);
                }
                catch (Exception ex)
                {
                    ;
                }
            }
        }

        private SQLiteRowData GetInsertDataFormat(SQLiteRowData sqldata)
        {
            SQLiteRowData temp = new SQLiteRowData();
            temp = sqldata;
            if (sqldata.Type == typeof(string))
                temp.Value = "'" + sqldata.Value.ToString() + "'";
            else if (sqldata.Type == typeof(DateTime))
                temp.Value = "'" + ((DateTime)sqldata.Value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            return temp;
        }
    }

    public class MyTableData
    {
        public string m_tableName = "";
        public List<string> m_listColumnName = new List<string>();
        private MyTableDataRowCollection myTableDataRows;

        public MyTableData(string tablename)
        {
            m_tableName = tablename;
        }

        public MyTableDataRowCollection Rows
        {
            get
            {
                if (myTableDataRows == null)
                    return myTableDataRows = new MyTableDataRowCollection();
                else
                    return myTableDataRows;
            }
        }
    }

    public class SQLiteRowData
    {
        public string Name;
        public object Value;
        public Type Type;

        public SQLiteRowData()
        {
            Name = "";
            Value = new object();
            Type = Value.GetType();
        }

        public SQLiteRowData(string name, object value, Type type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }

    public class MyTableDataRowCollection
    {
        private List<List<SQLiteRowData>> rows = new List<List<SQLiteRowData>>();
        private List<SQLiteRowData> rowTemplate;
        private int Index = 0;

        public List<SQLiteRowData> this[int index]
        {
            get
            {
                if (index < 0 || index >= rows.Count)
                    throw new Exception("索引不能小于0或大于等于行数");
                return rows[index];
            }
            set
            {
                if (index < 0 || index >= rows.Count)
                    throw new Exception("索引不能小于0或大于等于行数");
                rows[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return rows.Count;
            }
        }

        public List<SQLiteRowData> RowTemplate
        {
            get
            {
                if (Count > 0)
                {
                    List<SQLiteRowData> temp = new List<SQLiteRowData>();
                    foreach (var t in rows[Count - 1])
                    {
                        object o = t.Name == "id" ? (object)(Convert.ToInt64(t.Value) + 1) : null;
                        temp.Add(new SQLiteRowData(t.Name, o, t.Type));
                    }
                    return temp;
                }
                return rowTemplate;
            }
            set
            {
                List<SQLiteRowData> temp = new List<SQLiteRowData>();
                foreach (var t in value)
                    temp.Add(new SQLiteRowData(t.Name, t.Value, t.Type));
                rowTemplate = temp;
            }
        }

        public int Add(List<SQLiteRowData> dat)
        {
            rows.Add(dat);
            return Index++;
        }

        public void AddRange(IEnumerable<List<SQLiteRowData>> collection)
        {
            rows.AddRange(collection);
        }

        public void AddColumn(string ColumnName, Type type)
        {
            foreach (var item in rows)
            {
                item.Add(new SQLiteRowData(ColumnName, null, type));
            }
        }

        public bool Remove(List<SQLiteRowData> dat)
        {
            return rows.Remove(dat);
        }

        public void RemoveAt(int index)
        {
            rows.RemoveAt(index);
        }

        public void RemoveColumn(string ColumnName)
        {
            foreach (var item in rows)
            {
                SQLiteRowData d = (from i in item where i.Name == ColumnName select i).FirstOrDefault();
                item.Remove(d);
            }
        }

        public List<List<SQLiteRowData>>.Enumerator GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        public bool Contains(object value)
        {
            return rows.Contains(value);
        }

        public void Clear()
        {
            rows.Clear();
        }

        public int IndexOf(List<SQLiteRowData> value)
        {
            return rows.IndexOf(value);
        }

        public void Insert(int index, List<SQLiteRowData> value)
        {
            rows.Insert(index, value);
        }

        public void CopyTo(List<SQLiteRowData>[] array, int index)
        {
            rows.CopyTo(array, index);
        }

        public List<SQLiteRowData>[] ToArray()
        {
            return rows.ToArray();
        }
    }
}
