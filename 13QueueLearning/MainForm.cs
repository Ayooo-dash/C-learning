using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _13QueueLearning
{
    public partial class MainForm : Form
    {
        public Queue<object> m_Queue = new Queue<object>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_Queue.Enqueue("hello");
            m_Queue.Enqueue("lllll");
        }

        /// <summary>
        /// 深度拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_Queue.Count > 0)
                label1.Text = (string)m_Queue.Dequeue();
            else
                label1.Text = "NULL";
        }
    }
}
