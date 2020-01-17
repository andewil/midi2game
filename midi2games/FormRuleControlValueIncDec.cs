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
    public partial class FormRuleControlValueIncDec : Form, IFormRule
    {
        public FormRuleControlValueIncDec()
        {
            InitializeComponent();
        }

        HandleRule handleRule;

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void SaveRule()
        {
            
        }

        public void SetRule(HandleRule rule)
        {
            handleRule = rule;
            editControlNumber.Text = handleRule.GetValue("ControlNumber").ToString();
        }

        private void editControlNumber_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != null)
            {
                int val = AppUtils.GetIntFromTextBox((TextBox)sender, 0);
                handleRule.SetValue("ControlNumber", val);
            }
        }
    }
}
