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
    public partial class FormActionMouseAbs : Form, IFormAction
    {
        public FormActionMouseAbs()
        {
            InitializeComponent();
        }

        RuleActionMouseAbs action;

        public void SetAction(HandleRuleAction value)
        {
            action = (RuleActionMouseAbs)value;
            editX.Text = action.X.ToString();
            editY.Text = action.Y.ToString();
        }

        private void editX_TextChanged(object sender, EventArgs e)
        {
            action.X = AppUtils.StrToIntDef(editX.Text, 0);
        }

        private void editY_TextChanged(object sender, EventArgs e)
        {
            action.Y = AppUtils.StrToIntDef(editY.Text, 0);
        }
    }
}
