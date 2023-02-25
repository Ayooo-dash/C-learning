using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace Winforms
{
    public partial class SQLiteForm : Form
    {
        private SQLiteConnection Conn;
        private Dictionary<object, string> m_dicValue = new Dictionary<object, string>();
        private Dictionary<string, List<string>> m_dicTable = new Dictionary<string, List<string>>();
        private Dictionary<string, List<Dictionary<string, string>>> m_dicRowData = new Dictionary<string, List<Dictionary<string, string>>>();
        private List<DataTable> m_listDataTable = new List<DataTable>();
        private static string FilePath;

        /// <summary>
        /// 互斥锁
        /// </summary>
        private static readonly object m_lock = new object();


        public SQLiteForm()
        {
            InitializeComponent();
        }

        private void btn_ConnectDB_Click(object sender, EventArgs e)
        {
            if (txt_DBfileName.Text != String.Empty)
            {
                if (!File.Exists(FilePath))
                {
                    FilePath = Application.StartupPath + "\\" + txt_DBfileName.Text + ".db";
                    txt_DBfileName.Text = FilePath;
                    SQLiteConnection.CreateFile(FilePath);
                }
                try
                {
                    Conn = new SQLiteConnection("Data Source=" + FilePath + ";Version=3;");
                    Conn.Open();
                    MessageBox.Show("数据库：\"" + FilePath + "\" 打开成功！");
                }
                catch (Exception ex)
                {
                    throw new Exception("打开数据库：" + FilePath + "的连接失败：" + ex.Message);
                }
            }
            else
            {
                btn_SelectFile.PerformClick();
                btn_ConnectDB.PerformClick();
            }
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
                btn_ConnectDB.PerformClick();
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

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                for (int i = 0; i < cbbRowName.Items.Count; i++)
                {
                    str += cbbRowName.Items[i].ToString();
                    m_dicValue.Add(cbbRowName.Items[i], "");
                    if (i == cbbRowName.Items.Count - 1)
                        break;
                    str += ",";
                }
                string sql = "create table " + txtTableName.Text + string.Format("(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,{0})", str);
                SQLiteCommand command = new SQLiteCommand(sql, Conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表" + txtTableName.Text + "失败：" + ex.Message);
            }
        }

        private void SQLiteForm_Load(object sender, EventArgs e)
        {
            cbbDataType.Items.Add("string");
            cbbDataType.Items.Add("int");
            cbbDataType.Items.Add("double");
            cbbDataType.Items.Add("datetime");
            cbbDataType.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = cbbRowName.Text.ToString();
            if (name != String.Empty)
            {
                cbbRowName.Items.Add(name + " " + cbbDataType.SelectedItem.ToString());
            }

        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                try
                {
                    string strName = "";
                    string strValue = "";
                    for (int i = 0; i < cbbRowName.Items.Count; i++)
                    {
                        strName += cbbRowName.Items[i].ToString().Split(' ')[0];
                        strValue += ("@" + cbbRowName.Items[i].ToString().Split(' ')[0]);
                        if (i == cbbRowName.Items.Count - 1)
                            break;
                        strName += ",";
                        strValue += ",";
                    }
                    string sql = "insert into " + txtTableName.Text + string.Format(" ({0})", strName) + string.Format(" values ({0})", strValue);
                    SQLiteCommand command = new SQLiteCommand(sql, Conn);
                    foreach (var d in m_dicValue)
                    {
                        SQLiteParameter parameter = new SQLiteParameter("@" + d.Key.ToString().Split(' ')[0], d.Value);
                        command.Parameters.Add(parameter);
                    }
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("插入数据：" + txtTableName.Text + ":" + txtInsertData.Text + "失败：" + ex.Message);
                }

            }
        }


        private void btnQueryData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSQLiteTable();
            }
            catch (Exception ex)
            {
                throw new Exception("查询数据失败：" + ex.Message);
            }
        }

        private void cbbRowName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbRowName.SelectedIndex >= 0 && m_dicValue.ContainsKey(cbbRowName.SelectedItem.ToString()))
                txtInsertData.Text = m_dicValue[cbbRowName.SelectedItem.ToString()];
            else
                txtInsertData.Text = "";
        }

        private void btnEditValue_Click(object sender, EventArgs e)
        {
            if (cbbRowName.SelectedIndex >= 0 && m_dicValue.ContainsKey(cbbRowName.SelectedItem.ToString()))
                m_dicValue[cbbRowName.SelectedItem.ToString()] = txtInsertData.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            m_dicValue.Remove(cbbRowName.SelectedItem);
            cbbRowName.Items.Remove(cbbRowName.SelectedItem);
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            lock (m_lock)
            {
                try
                {
                    string sql = "delete from " + txtTableName.Text + " where " + "ID = " + txtID.Text;
                    SQLiteCommand command = new SQLiteCommand(sql, Conn);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("删除数据：" + txtTableName.Text + ": ID = " + txtID.Text + "失败：" + ex.Message);
                }
            }
        }

        private void LoadSQLiteTable()
        {
            try
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    //string sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
                    //SQLiteCommand command = new SQLiteCommand(sql, Conn);
                    //m_dicTable.Clear();
                    //m_dicRowData.Clear();
                    //using (SQLiteDataReader reader = command.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        string ret = "";
                    //        ret = reader.GetValue(0).ToString();
                    //        m_dicTable.Add(ret, new List<string>());
                    //        m_dicRowData.Add(ret, new List<Dictionary<string, string>>());
                    //    }

                    //}
                    string sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
                    SQLiteCommand command = new SQLiteCommand(sql, Conn);
                    m_listDataTable.Clear();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ret = "";
                            ret = reader.GetValue(0).ToString();
                            DataTable dt = new DataTable(ret);
                            m_listDataTable.Add(dt);
                        }
                    }
                    tabControl1.TabPages.Clear();
                    for (int i =0;i<m_listDataTable.Count;i++)
                    {
                        DataGridView dgv = new DataGridView();
                        dgv.Dock = DockStyle.Fill;
                        dgv.AllowUserToAddRows = false;
                        dgv.RowHeadersVisible = false;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        TabPage tp = new TabPage(m_listDataTable[i].TableName);
                        tp.Controls.Add(dgv);
                        tabControl1.TabPages.Add(tp);
                        DataTable temp = new DataTable(m_listDataTable[i].TableName);
                        LoadTableData(ref temp);
                        m_listDataTable[i] = temp;
                        foreach (DataColumn d in m_listDataTable[i].Columns)
                        {
                            DataGridViewColumn dgvc = new DataGridViewColumn();
                            DataGridViewCell dgvct = new DataGridViewTextBoxCell();
                            dgvc.HeaderText = d.ColumnName;
                            dgvc.Name = d.ColumnName;
                            dgvc.CellTemplate = dgvct;
                            int index = dgv.Columns.Add(dgvc);
                        }
                        foreach (DataRow row in m_listDataTable[i].Rows)
                        {
                            int index = dgv.Rows.Add();
                            dgv.Rows[index].SetValues(row.ItemArray);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void LoadTableData(ref DataTable dt)
        {
            string sql = "";
            SQLiteCommand command;
            try
            {
                //string sql = $"pragma table_info({table})";
                //SQLiteCommand command = new SQLiteCommand(sql, Conn);
                //m_dicTable[table].Clear();
                //using (SQLiteDataReader reader = command.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        m_dicTable[table].Add(reader.GetValue(1).ToString());
                //    }

                //}

                //string sql1 = $"select * from {table}";
                //SQLiteCommand command1 = new SQLiteCommand(sql1, Conn);
                //using (SQLiteDataReader reader = command1.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        Dictionary<string, string> dic = new Dictionary<string, string>();
                //        foreach (var d in m_dicTable[table])
                //        {
                //            string ret = "";
                //            dic.Add(d, reader[d].ToString());
                //            ret = reader[d].ToString();
                //        }
                //        m_dicRowData[table].Add(dic);
                //    }
                //}
                //foreach (var dt in m_listDataTable)
                //{
                sql = $"pragma table_info({dt.TableName})";
                command = new SQLiteCommand(sql, Conn);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Columns.Add(reader.GetValue(1).ToString());
                    }
                }
                sql = $"select * from {dt.TableName}";
                command = new SQLiteCommand(sql, Conn);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<object> dr = new List<object>();
                        foreach (DataColumn d in dt.Columns)
                        {
                            object ret = reader[d.ColumnName];
                            dr.Add(ret);
                        }
                        dt.Rows.Add(dr.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void btnEditIDData_Click(object sender, EventArgs e)
        {
            try
            {
                //UPDATE table SET name = 'Texas' WHERE ID = 6;
                string sql = string.Format("update {0} set {1} = {2} where id = {3}", txtTableName.Text, "", "", "");
                SQLiteCommand command = new SQLiteCommand(sql, Conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            SQLiteCommand command;
            if (Conn != null && Conn.State == ConnectionState.Open)
            {
                sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
                command = new SQLiteCommand(sql, Conn);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ret = "";
                        ret = reader.GetValue(0).ToString();
                        DataTable dt = new DataTable(ret);
                        m_listDataTable.Add(dt);
                    }
                }

                foreach (var dt in m_listDataTable)
                {
                    sql = $"pragma table_info({dt.TableName})";
                    command = new SQLiteCommand(sql, Conn);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Columns.Add(reader.GetValue(1).ToString());
                        }
                    }
                    sql = $"select * from {dt.TableName}";
                    command = new SQLiteCommand(sql, Conn);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<object> dr = new List<object>();
                            foreach (DataColumn d in dt.Columns)
                            {
                                object ret = reader[d.ColumnName];
                                dr.Add(ret);
                            }
                            dt.Rows.Add(dr.ToArray());
                        }
                    }
                }
            }
        }
    }
}
