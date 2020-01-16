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
    public partial class FormControlValueIncDec : Form, IFormRule
    {
        public FormControlValueIncDec()
        {
            InitializeComponent();
        }

        ControlValueDecreaceRule handleRuleDec;
        ControlValueIncreaceRule handleRuleInc;

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void SaveRule()
        {
            if (handleRuleDec != null)
            {
                handleRuleDec.ControlNumber = int.Parse(editControlNumber.Text);
            }
            else if (handleRuleInc != null)
            {
                handleRuleInc.ControlNumber = int.Parse(editControlNumber.Text);
            }
        }

        public void SetRule(HandleRule rule)
        {
            handleRuleDec = rule as ControlValueDecreaceRule;
            handleRuleInc = rule as ControlValueIncreaceRule;
            if (handleRuleDec != null)
            {
                editControlNumber.Text = handleRuleDec.ControlNumber.ToString();
            } else if (handleRuleInc != null)
            {
                editControlNumber.Text = handleRuleInc.ControlNumber.ToString();
            }
        }
    }
}
