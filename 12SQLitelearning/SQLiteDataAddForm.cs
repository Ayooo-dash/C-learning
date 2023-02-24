using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12SQLitelearning
{
    public enum AddStep
    {
        /// <summary>
        /// 创建数据表
        /// </summary>
        CreateTable = 0,

        /// <summary>
        /// 重命名表
        /// </summary>
        RenameTable,

        /// <summary>
        /// 插入行数据
        /// </summary>
        InsertRow,

        /// <summary>
        /// 添加列
        /// </summary>
        AddColumn,
    }

    public partial class SQLiteDataAddForm : Form
    {
        public AddStep m_Step;
        private ComboBox m_comboBoxType = new ComboBox();
        public sqlCreateTable m_sqlCreateTable = new sqlCreateTable("", "", null);
        public MyTableDataRowCollection m_AddRowCollection = new MyTableDataRowCollection();
        public List<SQLiteRowData> m_RowTemplate;
        public List<string[]> m_AddColumns;
        public string m_NewTableName = "";
        public Dictionary<string, Type> m_Type = new Dictionary<string, Type>()
        {
            { "int",typeof(int) },
            { "int64",typeof(long) },
            { "float",typeof(float) },
            { "double",typeof(double) },
            { "string", typeof(string)},
            { "DateTime", typeof(DateTime)}
        };

        public SQLiteDataAddForm(AddStep step, List<SQLiteRowData> RowTemplate = null)
        {
            InitializeComponent();
            m_Step = step;
            m_RowTemplate = RowTemplate == null ? null : new List<SQLiteRowData>(RowTemplate);
        }

        private void InitComboBox()
        {
            m_comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            m_comboBoxType.Items.Clear();
            m_comboBoxType.Items.Add("int");
            m_comboBoxType.Items.Add("float");
            m_comboBoxType.Items.Add("double");
            m_comboBoxType.Items.Add("string");
            m_comboBoxType.Items.Add("DateTime");
            m_comboBoxType.Visible = false;
            m_comboBoxType.SelectedIndexChanged += M_comboBoxType_SelectedIndexChanged; ;
            m_comboBoxType.LostFocus += M_comboBoxType_SelectedIndexChanged;

            dataGridView_AddRows.Controls.Add(m_comboBoxType);
        }

        private void M_comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_AddRows.CurrentCell.Value = m_comboBoxType.SelectedItem;
        }

        private void SQLiteDataAddForm_Load(object sender, EventArgs e)
        {
            switch (m_Step)
            {
                case AddStep.CreateTable:
                    InitComboBox();
                    LoadCreateTableForm();
                    break;
                case AddStep.RenameTable:
                    LoadRenameTableForm();
                    break;
                case AddStep.InsertRow:
                    LoadInsertRowForm();
                    break;
                case AddStep.AddColumn:
                    InitComboBox();
                    LoadAddColumnForm();
                    break;
                default:
                    break;
            }
        }

        private void LoadCreateTableForm()
        {
            DataGridViewColumn dgvc0 = new DataGridViewColumn();
            DataGridViewCell dgvct0 = new DataGridViewTextBoxCell();
            dgvc0.HeaderText = "名称";
            dgvc0.Name = "名称";
            dgvc0.CellTemplate = dgvct0;
            dataGridView_AddRows.Columns.Add(dgvc0);
            DataGridViewColumn dgvc1 = new DataGridViewColumn();
            DataGridViewCell dgvct2 = new DataGridViewTextBoxCell();
            dgvc1.HeaderText = "值类型";
            dgvc1.Name = "值类型";
            dgvc1.CellTemplate = dgvct2;
            dataGridView_AddRows.Columns.Add(dgvc1);

            int index = dataGridView_AddRows.Rows.Add();
            dataGridView_AddRows.Rows[index].HeaderCell.Value = "表名";
            dataGridView_AddRows.Rows[index].SetValues(new object[] { "", "" });
            index = dataGridView_AddRows.Rows.Add();
            dataGridView_AddRows.Rows[index].HeaderCell.Value = "列名";
            dataGridView_AddRows.Rows[index].SetValues(new object[] { "", "int" });
        }

        private void LoadRenameTableForm()
        {
            dataGridView_AddRows.Columns.Add("表名","表名");
            dataGridView_AddRows.ColumnHeadersVisible = false;
            int index = dataGridView_AddRows.Rows.Add();
            dataGridView_AddRows.Rows[index].HeaderCell.Value = "表名";
            dataGridView_AddRows.Rows[index].SetValues(new object[] { "" });
            btnAddRow.Visible = false;
            btnDeleteRow.Visible = false;
        }

        private void LoadInsertRowForm()
        {
            if (m_RowTemplate == null)
                return;
            List<object> value = new List<object>();
            foreach (var item in m_RowTemplate)
            {
                DataGridViewColumn dgvc = new DataGridViewColumn();
                DataGridViewCell dgvct = new DataGridViewTextBoxCell();
                dgvc.HeaderText = item.Name;
                dgvc.Name = item.Name;
                dgvc.ValueType = item.Type;
                dgvc.CellTemplate = dgvct;
                dataGridView_AddRows.Columns.Add(dgvc);
                value.Add(item.Value);
            }
            int index = dataGridView_AddRows.Rows.Add(value.ToArray());
        }

        private void LoadAddColumnForm()
        {
            DataGridViewColumn dgvc1 = new DataGridViewColumn();
            DataGridViewCell dgvct1 = new DataGridViewTextBoxCell();
            dgvc1.HeaderText = "列名";
            dgvc1.Name = "列名";
            dgvc1.CellTemplate = dgvct1;
            dataGridView_AddRows.Columns.Add(dgvc1);
            DataGridViewColumn dgvc2 = new DataGridViewColumn();
            DataGridViewCell dgvct2 = new DataGridViewTextBoxCell();
            dgvc2.HeaderText = "值类型";
            dgvc2.Name = "值类型";
            dgvc2.CellTemplate = dgvct2;
            dataGridView_AddRows.Columns.Add(dgvc2);

            int index = dataGridView_AddRows.Rows.Add();
            dataGridView_AddRows.Rows[index].SetValues(new object[] { "", "int" });
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            int index;
            List<object> value;
            switch (m_Step)
            {
                case AddStep.CreateTable:
                    index = dataGridView_AddRows.Rows.Add();
                    dataGridView_AddRows.Rows[index].HeaderCell.Value = "列名";
                    dataGridView_AddRows.Rows[index].SetValues(new object[] { "", "int" });
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                    value = new List<object>();
                    foreach (var s in m_RowTemplate)
                    {
                        if (s.Name == "id")
                            s.Value = Convert.ToInt64(s.Value) + 1;
                        value.Add(s.Value);
                    }
                    index = dataGridView_AddRows.Rows.Add(value.ToArray());
                    break;
                case AddStep.AddColumn:
                    index = dataGridView_AddRows.Rows.Add();
                    dataGridView_AddRows.Rows[index].SetValues(new object[] { "", "int" });
                    break;
                default:
                    break;
            }
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            switch (m_Step)
            {
                case AddStep.CreateTable:
                    if (dataGridView_AddRows.Rows.Count > 2 && dataGridView_AddRows.CurrentRow.Index > 0)
                        dataGridView_AddRows.Rows.RemoveAt(dataGridView_AddRows.CurrentRow.Index);
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                case AddStep.AddColumn:
                    if (dataGridView_AddRows.CurrentRow != null && dataGridView_AddRows.CurrentRow.Index >= 0)
                        dataGridView_AddRows.Rows.RemoveAt(dataGridView_AddRows.CurrentRow.Index);
                    break;
                default:
                    break;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                switch (m_Step)
                {
                    case AddStep.CreateTable:
                        string tableName = dataGridView_AddRows.Rows[0].Cells[0].Value.ToString().Trim();
                        string str = "";
                        List<SQLiteRowData> CreateTableTemplate = new List<SQLiteRowData>();
                        if (tableName == null || tableName == "")
                        {
                            MessageBox.Show("请输入表名！");
                            return;
                        }
                        for (int i = 0; i < dataGridView_AddRows.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                SQLiteRowData sqlData1 = new SQLiteRowData("id", 1, typeof(long));
                                CreateTableTemplate.Add(sqlData1);
                                continue;
                            }
                            string name = dataGridView_AddRows.Rows[i].Cells[0].Value.ToString().Trim();
                            string type = dataGridView_AddRows.Rows[i].Cells[1].Value.ToString().Trim();
                            SQLiteRowData sqlData = new SQLiteRowData(name, null, m_Type[type]);
                            str += (name + " " + type);
                            CreateTableTemplate.Add(sqlData);
                            if (i == dataGridView_AddRows.Rows.Count - 1)
                                break;
                            str += ",";
                        }
                        m_sqlCreateTable.Sql = "create table " + tableName + string.Format("(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,{0})", str);
                        m_sqlCreateTable.tableName = tableName;
                        m_sqlCreateTable.RowTemplate = new List<SQLiteRowData>(CreateTableTemplate);
                        break;
                    case AddStep.RenameTable:
                        string strTemp = dataGridView_AddRows.Rows[0].Cells[0].Value.ToString().Trim();
                        if (strTemp == null || strTemp == string.Empty)
                            return;
                        m_NewTableName = strTemp;
                        break;
                    case AddStep.InsertRow:
                        if (dataGridView_AddRows.Rows.Count <= 0)
                            return;
                        foreach (DataGridViewRow item in dataGridView_AddRows.Rows)
                        {
                            List<SQLiteRowData> temp = new List<SQLiteRowData>();
                            for (int i = 0; i < item.Cells.Count; i++)
                            {
                                object value = item.Cells[i].Value;
                                SQLiteRowData sqlData = new SQLiteRowData(m_RowTemplate[i].Name, value, m_RowTemplate[i].Type);
                                temp.Add(sqlData);
                            }
                            m_AddRowCollection.Add(new List<SQLiteRowData>(temp));
                        }
                        break;
                    case AddStep.AddColumn:
                        if (dataGridView_AddRows.Rows.Count <= 0)
                            return;
                        m_AddColumns = new List<string[]>();
                        m_AddColumns.Clear();
                        foreach (DataGridViewRow item in dataGridView_AddRows.Rows)
                        {
                            m_AddColumns.Add(new string[] { item.Cells[0].Value.ToString().Trim(), item.Cells[1].Value.ToString().Trim() });
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dataGridView_AddRows_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            switch (m_Step)
            {
                case AddStep.CreateTable:
                case AddStep.AddColumn:
                    {
                        m_comboBoxType.Visible = false;

                        if (grid == null || grid.CurrentCell == null)
                        {
                            return;
                        }

                        int rowIndex = grid.CurrentCell.RowIndex;
                        int colIndex = grid.CurrentCell.ColumnIndex;

                        if (grid == dataGridView_AddRows)
                        {
                            if (colIndex == 1)
                            {
                                if (rowIndex == 0 && m_Step == AddStep.CreateTable)
                                {
                                    grid.CurrentCell.ReadOnly = true;
                                    return;
                                }
                                Rectangle rect = grid.GetCellDisplayRectangle(colIndex, rowIndex, true);

                                m_comboBoxType.Top = rect.Top;
                                m_comboBoxType.Left = rect.Left;
                                m_comboBoxType.Width = rect.Width;
                                m_comboBoxType.Height = rect.Height;

                                if (grid.CurrentCell.Value != null)
                                {
                                    m_comboBoxType.SelectedIndex = m_comboBoxType.Items.IndexOf(grid.CurrentCell.Value);
                                }
                                else
                                {
                                    m_comboBoxType.SelectedIndex = -1;
                                }

                                m_comboBoxType.Visible = true;
                            }
                        }
                    }
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                    if (grid != null && grid.CurrentCell != null && grid.CurrentCell.ColumnIndex == 0)
                        grid.CurrentCell.ReadOnly = true;
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_AddRows_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            switch (m_Step)
            {
                case AddStep.CreateTable:
                case AddStep.AddColumn:
                    m_comboBoxType.Visible = false;
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_AddRows_Scroll(object sender, ScrollEventArgs e)
        {
            switch (m_Step)
            {
                case AddStep.CreateTable:
                case AddStep.AddColumn:
                    m_comboBoxType.Visible = false;
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_AddRows_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (m_Step)
            {
                case AddStep.CreateTable:
                    break;
                case AddStep.RenameTable:
                    break;
                case AddStep.InsertRow:
                    break;
                case AddStep.AddColumn:
                    break;
                default:
                    break;
            }
        }
    }

    public class sqlCreateTable
    {
        public string tableName;
        public string Sql;
        public List<SQLiteRowData> RowTemplate;

        public sqlCreateTable(string name, string sql, List<SQLiteRowData> rowTemplate)
        {
            tableName = name;
            Sql = sql;
            RowTemplate = rowTemplate == null ? null : new List<SQLiteRowData>(rowTemplate);
        }
    }
}
