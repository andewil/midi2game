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

        /// <summary>
        /// Information about class and additional form class for type rule
        /// </summary>
        private RuleClassRecord activeRuleClassRecord;
        /// <summary>
        /// Instance of form (if exists) for selected type rule
        /// </summary>
        private Form ruleAdditionalForm;
        /// <summary>
        /// Instance of form (if exists) for selected type action
        /// </summary>
        private Form actionAdditionalForm;
        /// <summary>
        /// For editing: original rule
        /// </summary>
        private HandleRule rule;
        /// <summary>
        /// Instance of rule. If "edit rule" operation called than workingRule will be copy of original rule
        /// </summary>
        private HandleRule workingRule;

        public event IndexedRuleEventHandler OnSaveRule;

        /// <summary>
        /// For editing: original rule
        /// </summary>
        public HandleRule Rule
        {
            get { return rule; }
            set
            {
                rule = value;
                EditRule = rule;
            }
        }

        /// <summary>
        /// Flag is true, when setting new value of rule. It needs to avoid UI events of changing indexes in ListBoxes, ComboBoxes
        /// </summary>
        private Boolean blockUIChange = false;

        /// <summary>
        /// Instance of rule. If "edit rule" operation called than workingRule will be copy of original rule
        /// </summary>
        public HandleRule EditRule
        {
            get { return workingRule; }
            set
            {
                if (value == null)
                {
                    workingRule = null;
                    return;
                }
                workingRule = (HandleRule)value.Clone();
                // block UI event handlers
                blockUIChange = true;
                if (workingRule != null)
                {
                    // set common rule data
                    editName.Text = workingRule.RuleName;
                    editStopProcessing.Checked = workingRule.StopProcessing;
                    // find class of rule in ListBox, and select it
                    for (int i = 0; i < listBoxRuleType.Items.Count - 1; i++)
                    {
                        RuleClassRecord rec = (RuleClassRecord)listBoxRuleType.Items[i];
                        if (rec.ruleClass == workingRule.GetType())
                        {
                            listBoxRuleType.SelectedIndex = i;
                            break;
                        }
                    }
                    // set rule to additional rule form
                    IFormRule ifrm = ruleAdditionalForm as IFormRule;
                    if (ifrm != null)
                    {
                        ifrm.SetRule(workingRule);
                    }
                }
                // unblock UI event handlers
                blockUIChange = false;
            }
        }

        /// <summary>
        /// Instance of rule information (additional forms, visible name)
        /// </summary>
        public RuleClassRecord ActiveRuleClassRecord
        {
            get { return activeRuleClassRecord; }
            set
            {
                if (ruleAdditionalForm != null)
                {
                    ruleAdditionalForm.Hide();
                    ruleAdditionalForm.Dispose();
                    ruleAdditionalForm = null;
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
                        ruleAdditionalForm = inst;
                        IFormRule ifrm = ruleAdditionalForm as IFormRule;
                        if (ifrm != null)
                        {
                            ifrm.SetRule(EditRule);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Current preset file, which contains information of presets and rules
        /// </summary>
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
            if (EditRule == null)
                return;
            EditRule.RuleName = editName.Text;
            EditRule.StopProcessing = editStopProcessing.Checked;
            IFormRule ifrm = ruleAdditionalForm as IFormRule;
            if (ifrm != null)
            {
                ifrm.SaveRule();
            }
            
            // Change object in storage, if 
            if ((Rule != null) && (PresetFile != null))
            {
                PresetFile.ReplaceRule(Rule, EditRule);
                Rule = EditRule;
                int index = PresetFile.GetIndexByRule(Rule);
                OnSaveRule?.Invoke(this, Rule, index);
            }
            
        }

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
            //if (blockUIChange)
            //    return;

            if (listBoxRuleType.SelectedIndex < 0) {
                ActiveRuleClassRecord = null;
                return;
                // TODO: hide all elements, clear active records
            }
            // set selected rule information
            ActiveRuleClassRecord = (RuleClassRecord)listBoxRuleType.Items[listBoxRuleType.SelectedIndex];
            // change EditRule to new type
            var tmpRule = (HandleRule) Activator.CreateInstance(ActiveRuleClassRecord.ruleClass);
            tmpRule.CopyHeadInformationFrom(EditRule);
            EditRule = tmpRule;
        }
    }
}
