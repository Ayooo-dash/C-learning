using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Winforms
{
        /// <summary>实现单件的模板类</summary>
        /// <typeparam name="T"></typeparam>
        public class SingletonTemplate<T> where T : class, new()
        {
            /// <summary>线程互斥对像</summary>
            private static readonly object syslock = new object();
            /// <summary>用来显示站位运行日志的列表框</summary>
            private Control m_LogListBox = (Control)null;
            /// <summary>实例对像</summary>
            private Thread m_thread = (Thread)null;
            /// <summary>实例引用</summary>
            private static T m_instance;
            /// <summary>线程是否运行中</summary>
            protected bool m_bRunThread;

            /// <summary>日志信息显示事件</summary>
            //public event LogHandler LogEvent;

            /// <summary>触发日志显示事件</summary>
            /// <param name="strLog"></param>
            /// <param name="level">显示的等级,便于颜色提示</param>
            //public void ShowLog(string strLog, LogLevel level = LogLevel.Info)
            //{
            //    if (this.LogEvent == null || this.m_LogListBox == null)
            //        return;
            //    this.LogEvent(this.m_LogListBox, strLog, level);
            //}

            /// <summary>设置日志要显示在哪个列表框上</summary>
            /// <param name="logListBox"></param>
            public void SetLogListBox(Control logListBox)
            {
                this.m_LogListBox = logListBox;
            }

            /// <summary>获取实例</summary>
            /// <returns></returns>
            public static T GetInstance()
            {
                if ((object)SingletonTemplate<T>.m_instance == null)
                {
                    lock (SingletonTemplate<T>.syslock)
                    {
                        if ((object)SingletonTemplate<T>.m_instance == null)
                            SingletonTemplate<T>.m_instance = new T();
                    }
                }
                return SingletonTemplate<T>.m_instance;
            }

            /// <summary>线程函数</summary>
            public virtual void ThreadMonitor()
            {
                if (!this.m_bRunThread)
                    return;
                Thread.Sleep(100);
            }

            /// <summary>开始监视线程</summary>
            public void StartMonitor()
            {
                if (this.m_thread == null)
                    this.m_thread = new Thread(new ThreadStart(this.ThreadMonitor));
                if ((uint)this.m_thread.ThreadState <= 0U)
                    return;
                this.m_bRunThread = true;
                this.m_thread.Start();
            }

            /// <summary>结束监视线程</summary>
            public void StopMonitor()
            {
                if (this.m_thread == null)
                    return;
                this.m_bRunThread = false;
                if (!this.m_thread.Join(5000))
                    this.m_thread.Abort();
                this.m_thread = (Thread)null;
            }

        }
}
