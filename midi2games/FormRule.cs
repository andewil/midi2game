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
    public partial class FormRule : Form
    {
        public FormRule()
        {
            InitializeComponent();
            InitRuleTypes();
            InitActionTypes();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //
        }

        private HandleRule rule;
        public HandleRule Rule
        {
            get { return rule; }
            set
            {
                rule = value;
                if (rule != null) {
                    editName.Text = rule.RuleName;
                    editStopProcessing.Checked = rule.StopProcessing;
                    // find class of rule
                    for (int i = 0; i < listBoxRuleType.Items.Count - 1; i++)
                    {
                        RuleClassRecord rec = (RuleClassRecord)listBoxRuleType.Items[i];
                        if (rec.ruleClass == rule.GetType())
                        {
                            listBoxRuleType.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private RuleClassRecord activeRuleClassRecord;
        private Form ruleForm;
        private Form actionForm;
        public RuleClassRecord ActiveRuleClassRecord
        {
            get { return activeRuleClassRecord; }
            set
            {
                if (ruleForm != null)
                {
                    ruleForm.Hide();
                    ruleForm.Dispose();
                    ruleForm = null;
                }
                activeRuleClassRecord = value;
                if (activeRuleClassRecord != null)
                {
                    if (activeRuleClassRecord.formClass != null)
                    {
                        Form inst = (Form)Activator.CreateInstance(activeRuleClassRecord.formClass);
                        inst.TopLevel = false;
                        //inst.Parent = tabPageAdditionalPropertiesAction;
                        tabPageAdditionalPropertiesRule.Controls.Add(inst);
                        inst.FormBorderStyle = FormBorderStyle.None;
                        inst.Dock = DockStyle.Fill;
                        inst.Show();
                        ruleForm = inst;
                    }
                }
            }
        }

        public PresetFile PresetFile { get; set; }
        public FormMidi2Game MainForm { get; set; }
        private RuleTypeManager ruleTypeManager = new RuleTypeManager();
        private void InitRuleTypes()
        {
            for (int i = 0; i < ruleTypeManager.ruleTypes.Count - 1; i++)
            {
                RuleClassRecord rec = ruleTypeManager.ruleTypes[i];
                listBoxRuleType.Items.Add(rec);
            }
        }

        private void InitActionTypes()
        {

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (rule == null)
                return;
            rule.RuleName = editName.Text;
            rule.StopProcessing = editStopProcessing.Checked;
            OnSaveRule?.Invoke(this, rule);
        }

        public event RuleEventHandler OnSaveRule;

        private void tsbPrevRule_Click(object sender, EventArgs e)
        {
            if ((PresetFile == null) || (rule == null))
                return;
            int index = PresetFile.GetIndexByRule(rule);
            if (index <= 0)
                return;
            Rule = PresetFile.rulesStorage[index - 1];
        }

        private void tsbNextRule_Click(object sender, EventArgs e)
        {
            if ((PresetFile == null) || (rule == null))
                return;
            int index = PresetFile.GetIndexByRule(rule);
            if (index >= PresetFile.rulesStorage.Count - 1)
                return;
            Rule = PresetFile.rulesStorage[index + 1];
        }

        private void listBoxRuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxRuleType.SelectedIndex < 0)
                return;
            ActiveRuleClassRecord = (RuleClassRecord)listBoxRuleType.Items[listBoxRuleType.SelectedIndex];
        }
    }
}
