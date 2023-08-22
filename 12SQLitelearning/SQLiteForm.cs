using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            //string table = "";
            //SQLiteDataAdapter mAdapter = new SQLiteDataAdapter($"select * from {table}", m_mySQLiteDBData.SqlConnection);
            //DataTable dt = new DataTable();
            //mAdapter.Fill(dt);
            if (m_mySQLiteDBData == null)
                return;
            m_mySQLiteDBData.AddDBDataToTabControl(panel_DBData);
        }
    }

    public class MySQLiteDBData
    {
        //互斥锁
        private static readonly object m_lock = new object();
        public string FilePath;
        private int m_TabControlSelectIndex = 0;
        private SQLiteConnection m_sqlConnection;
        private Panel m_Panel;
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

        public SQLiteConnection SqlConnection
        {
            get
            {
                return m_sqlConnection;
            }
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

            if (!File.Exists(FilePath))
            {
                SQLiteConnection.CreateFile(FilePath);
            }
            try
            {
                m_sqlConnection = new SQLiteConnection();
                SQLiteConnectionStringBuilder connsb = new SQLiteConnectionStringBuilder();
                connsb.DataSource = FilePath;
                connsb.Version = 3;
                //connsb.Password = "Wcf2019";
                m_sqlConnection.ConnectionString = connsb.ToString();
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
            TabControl tabControl = (from Control d in m_Panel.Controls where d is TabControl select d).FirstOrDefault() as TabControl;
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
                    tabControl = (from Control d in m_Panel.Controls where d is TabControl select d).FirstOrDefault() as TabControl;
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            tNewName = addForm.m_NewTableName;
                            tpp1 = tabControl.SelectedTab;
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
                        tpp = tabControl.SelectedTab;
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
                    tabControl.TabPages.Remove(tpp);
                    MessageBox.Show($"删除数据表{tName}成功！");
                    break;

                case SqliteCmd.insertRowData:   //插入数据
                    tName = tabControl.SelectedTab.Text;
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
                                dgv = (from Control d in tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
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
                    tName = tabControl.SelectedTab.Text;
                    dgv = (from Control d in tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
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
                        tName = tabControl.SelectedTab.Text;
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
                            tpp2 = tabControl.SelectedTab;
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
                        tpp3 = tabControl.SelectedTab;
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

        public bool AddDBDataToTabControl(Panel panel)
        {
            if (m_dicTableData == null)
                return false;
            try
            {
                if (panel != null)
                {
                    panel.Controls.Clear();
                    m_Panel = panel;
                }
                else
                    throw new Exception("传参Panel为Null！");

                LoadTabControlAndButton();

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

        private void LoadTabControlAndButton()
        {
            if (m_Panel == null)
                return;
            TabControl tabControl = new TabControl();
            tabControl.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            tabControl.Location = new Point(3, 3);
            tabControl.Name = "tabControl";
            tabControl.Size = new Size(811, m_Panel.Height - 6);
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            for (int i = 0; i < m_dicBtn.Count; i++)
            {
                Button btn = new Button();
                btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                btn.Location = new Point(m_Panel.Width - 102 - 6, 28 + (25 + 6) * i);
                btn.Name = string.Format("{0}", m_dicBtn.ElementAt(i).Key[0]);
                btn.Size = new Size(102, 25);
                btn.Text = m_dicBtn.ElementAt(i).Key[1];
                btn.UseVisualStyleBackColor = true;
                btn.Click += m_dicBtn.ElementAt(i).Value;
                m_Panel.Controls.Add(btn);
            }
            m_Panel.Controls.Add(tabControl);
        }

        private void AddTabPageOfTableData(MyTableData tableData)
        {
            TabPage tp = new TabPage(tableData.m_tableName);
            TabControl tabControl = (from Control d in m_Panel.Controls where d is TabControl select d).FirstOrDefault() as TabControl;
            tabControl.TabPages.Add(tp);
            LoadDataGridView(tp, tableData);
        }

        private void LoadDataGridView(TabPage tp, MyTableData tableData)
        {
            DataGridView dgv = new DataGridView(); ;
            int tabWidth = tp.Size.Width;
            int tabHeigth = tp.Size.Height;

            dgv = new DataGridView();
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(3, 3);
            dgv.Name = tp.Text;
            dgv.RowTemplate.Height = 27;
            dgv.Size = new Size(tabWidth - 6, tabHeigth - 3 * 2);
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.CellValueChanged += Dgv_CellValueChanged;
            tp.Controls.Add(dgv);

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

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = (from Control d in m_Panel.Controls where d is TabControl select d).FirstOrDefault() as TabControl;
            m_TabControlSelectIndex = tabControl.SelectedIndex;
        }

        private void BtnCreateTable_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
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
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.deleteRowData, ref d);
            }
        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                var d = new Dictionary<string, MyTableData>();
                SendSqliteCmd(SqliteCmd.insertRowData, ref d);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
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
            TabControl tabControl = (from Control d in m_Panel.Controls where d is TabControl select d).FirstOrDefault() as TabControl;
            string tName = tabControl.SelectedTab.Text;
            lock (m_lock)
            {
                int[] index = new int[] { e.RowIndex, e.ColumnIndex };
                try
                {
                    DataGridView dgv = (from Control d in tabControl.SelectedTab.Controls where d is DataGridView select d).FirstOrDefault() as DataGridView;
                    m_dicTableTempData[tName].Rows[e.RowIndex][e.ColumnIndex].Value = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
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

        public SQLiteRowData this[int index, string ColumnName]
        {
            get
            {
                if (index < 0 || index >= rows.Count)
                    throw new Exception("索引不能小于0或大于等于行数");
                if (ColumnName == null || ColumnName == string.Empty)
                    throw new Exception("索引名称不能为空");
                return (from r in rows[index] where r.Name == ColumnName select r).FirstOrDefault();
            }
            set
            {
                if (index < 0 || index >= rows.Count)
                    throw new Exception("索引不能小于0或大于等于行数");
                if (ColumnName == null || ColumnName == string.Empty)
                    throw new Exception("索引名称不能为空");
                foreach (var r in rows[index])
                {
                    if (r.Name == ColumnName)
                    {
                        r.Name = value.Name;
                        r.Value = value.Value;
                        r.Type = value.Type;
                    }
                }
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
