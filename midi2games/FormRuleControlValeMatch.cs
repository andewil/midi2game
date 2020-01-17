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
    public partial class FormRuleControlValeMatch : Form, IFormRule
    {
        public FormRuleControlValeMatch()
        {
            InitializeComponent();
        }

        public void CancelEdit()
        {

        }

        public void SaveRule()
        {
            
        }

        HandleRule handleRule;
        public void SetRule(HandleRule rule)
        {
            handleRule = rule;
        }

        private void editCN_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != null)
            {
                int val = AppUtils.GetIntFromTextBox((TextBox)sender, 0);
                handleRule.SetValue("ControlNumber", val);
            }
        }

        private void editCV_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != null)
            {
                int val = AppUtils.GetIntFromTextBox((TextBox)sender, 0);
                handleRule.SetValue("ControlValue", val);
            }
        }
    }
}
