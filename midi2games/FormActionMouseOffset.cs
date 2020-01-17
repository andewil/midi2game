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
    public partial class FormActionMouseOffset : Form, IFormAction
    {
        public FormActionMouseOffset()
        {
            InitializeComponent();
        }
       
        RuleActionMouseOffset action;

        private void editOffset_TextChanged(object sender, EventArgs e)
        {
            action.Offset = AppUtils.StrToIntDef(editOffset.Text, 0);
        }

        private void rbVertical_CheckedChanged(object sender, EventArgs e)
        {
            action.Direction = Orientation.Vertical;
        }

        private void rbHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            action.Direction = Orientation.Horizontal;
        }

        public void SetAction(HandleRuleAction value)
        {
            action = (RuleActionMouseOffset)value;
            if (action.Direction == Orientation.Vertical)
            {
                rbVertical.Checked = true;
            }
            else
            {
                rbHorizontal.Checked = true;
            }
            editOffset.Text = action.Offset.ToString();
        }
    }
}
