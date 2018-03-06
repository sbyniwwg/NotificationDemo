using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationDemo
{
    public partial class Form1 : Form
    {
        
        /// <summary>
        /// 提供给其他线程修改文本的委托
        /// </summary>
        /// <param name="str"></param>
        delegate void ChangeTextValue(string str);

        public Form1()
        {
            InitializeComponent();

            NotificationUtil.Instance.regEvent(this, EventConfig.event_name_test, (object obj) => {
                this.BeginInvoke(new ChangeTextValue(updateTxt), "Form2通知修改");
            });
        }

        private void updateTxt(string txt)
        {
            label1.Text = txt;
        }


       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            NotificationUtil.Instance.removeEvent(EventConfig.event_name_test);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }
    }
}
