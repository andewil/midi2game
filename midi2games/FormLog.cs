using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    public partial class FormLog : Form
    {
        public FormLog()
        {
            InitializeComponent();
        }

        public FormMidi2Game formMain;

        private delegate void SafeAddLogString(string text);
        public void AddLogString(string text)
        {
            if (richTextBoxLog.InvokeRequired)
            {
                var d = new SafeAddLogString(AddLogString);
                richTextBoxLog.Invoke(d, new object[] { text });
            }
            else
            {
                richTextBoxLog.AppendText(text);
            }
        }

        public void AddLogStringWithTime(string text)
        {
            AddLogString($"{DateTime.Now}: {text}{Environment.NewLine}");
        }

        private void FormLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (formMain != null)
            {
                formMain.formLog = null;
            }
        }
    }
}
