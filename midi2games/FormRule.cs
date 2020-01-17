using NLog;
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

        #region fields definition  
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Information about class and additional form class for type rule
        /// </summary>
        private ClassRecord activeRuleClassRecord;
        /// <summary>
        /// Information about action
        /// </summary>
        private ClassRecord activeActionRecord;
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
        private HandleRule editRule;
        public PresetFile PresetFile { get; set; }
        public FormMidi2Game MainForm { get; set; }
        private RuleTypeManager ruleTypeManager = new RuleTypeManager();
        /// <summary>
        /// Flag is true, when setting new value of rule. It needs to avoid UI events of changing indexes in ListBoxes, ComboBoxes
        /// </summary>
        private Boolean blockUIChange = false;
        public event IndexedRuleEventHandler OnSaveRule;
        #endregion

        #region UI methods
        private void SetListIndexRuleType(Type t)
        {
            for (int i = 0; i < listBoxRuleType.Items.Count; i++)
            {
                ClassRecord rec = (ClassRecord)listBoxRuleType.Items[i];
                if (rec.classRecord == t)
                {
                    listBoxRuleType.SelectedIndex = i;
                    return;
                }
            }
            listBoxRuleType.SelectedIndex = -1;
        }
        private void SetListIndexActionType(Type t)
        {
            for (int i = 0; i < listBoxActionType.Items.Count; i++)
            {
                ClassRecord rec = (ClassRecord)listBoxActionType.Items[i];
                if (rec.classRecord == t)
                {
                    // if called Previous or Next rule action, SelectedIndex may be same and ChangeSelected wouldn't be called
                    // flagReinit used for set value in these cases
                    Boolean flagReinit = listBoxActionType.SelectedIndex == i;
                    listBoxActionType.SelectedIndex = i;
                    if (flagReinit)
                    {
                        IFormAction ifrm = actionAdditionalForm as IFormAction;
                        if (ifrm != null)
                        {
                            ifrm.SetAction(editRule.Action);
                        }
                    }
                    return;
                }
            }
            listBoxActionType.SelectedIndex = -1;
        }
        private void ShowAdditionalRuleForm(Type classForm)
        {
            Form inst = (Form)Activator.CreateInstance(classForm);
            inst.TopLevel = false;
            panelAdditionRuleForm.Controls.Add(inst);
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
        private void ShowAdditionalActionForm(Type classForm)
        {
            Form inst = (Form)Activator.CreateInstance(classForm);
            inst.TopLevel = false;
            panelAdditionalActionForm.Controls.Add(inst);
            inst.FormBorderStyle = FormBorderStyle.None;
            inst.Dock = DockStyle.Fill;
            inst.Show();
            actionAdditionalForm = inst;
            IFormAction ifrm = actionAdditionalForm as IFormAction;
            if (ifrm != null)
            {
                ifrm.SetAction(EditRule.Action);
            }
        }
        /// <summary>
        /// Hide additional form of action
        /// </summary>
        private void HideAdditionalActionForm()
        {
            panelAdditionalActionForm.Controls.Remove(actionAdditionalForm);
            actionAdditionalForm.Hide();
            actionAdditionalForm.Dispose();
            actionAdditionalForm = null;
        }
        /// <summary>
        /// Hide additional form of rule
        /// </summary>
        private void HideAdditionalRuleForm()
        {
            panelAdditionalActionForm.Controls.Remove(ruleAdditionalForm);
            ruleAdditionalForm.Dispose();
            ruleAdditionalForm = null;
        }
        /// <summary>
        /// Current preset file, which contains information of presets and rules
        /// </summary>
        private void InitRuleTypes()
        {
            for (int i = 0; i < ruleTypeManager.ruleTypes.Count; i++)
            {
                ClassRecord rec = ruleTypeManager.ruleTypes[i];
                listBoxRuleType.Items.Add(rec);
            }
        }
        private void InitActionTypes()
        {
            for (int i = 0; i < ruleTypeManager.actionTypes.Count; i++)
            {
                ClassRecord rec = ruleTypeManager.actionTypes[i];
                listBoxActionType.Items.Add(rec);
                logger.Debug(rec);
            }
        }
        #endregion

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
        /// Instance of rule. If "edit rule" operation called than workingRule will be copy of original rule
        /// </summary>
        public HandleRule EditRule
        {
            get { return editRule; }
            set
            {
                if (blockUIChange)
                    return;

                if (value == null)
                {
                    editRule = null;
                    return;
                }
                editRule = (HandleRule)value.Clone();
                // block UI event handlers
                blockUIChange = true;
                if (editRule != null)
                {
                    // set common rule data
                    editName.Text = editRule.RuleName;
                    editStopProcessing.Checked = editRule.StopProcessing;
                    // find class of rule in ListBox, and select it
                    SetListIndexRuleType(editRule.GetType());
                    if (editRule.Action != null)
                    {
                        SetListIndexActionType(editRule.Action.GetType());
                    } else
                    {
                        SetListIndexActionType(null);
                    }
                }
                // unblock UI event handlers
                blockUIChange = false;
            }
        }
        /// <summary>
        /// Instance of rule information (additional forms, visible name)
        /// </summary>
        public ClassRecord ActiveRuleType
        {
            get { return activeRuleClassRecord; }
            set
            {
                if (ruleAdditionalForm != null)
                {
                    HideAdditionalRuleForm();
                }
                activeRuleClassRecord = value;
                if ((activeRuleClassRecord != null) && (activeRuleClassRecord.classForm != null))
                {
                    ShowAdditionalRuleForm(activeRuleClassRecord.classForm);
                }
            }
        }
        /// <summary>
        /// Instance of rule action (additional forms, visible name)
        /// </summary>
        public ClassRecord ActiveActionType
        {
            get { return activeActionRecord; }
            set
            {
                if (actionAdditionalForm != null)
                {
                    HideAdditionalActionForm();
                }
                activeActionRecord = value;
                if ((activeActionRecord != null) && (activeActionRecord.classForm != null))
                {
                    ShowAdditionalActionForm(activeActionRecord.classForm);
                }
            }
        }        
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (EditRule == null)
                return;
            EditRule.RuleName = editName.Text;
            EditRule.StopProcessing = editStopProcessing.Checked;           
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
                ActiveRuleType = null;
                return;
            }
            // set selected rule information
            ActiveRuleType = (ClassRecord)listBoxRuleType.Items[listBoxRuleType.SelectedIndex];
            // change EditRule to new type
            var tmpRule = (HandleRule) Activator.CreateInstance(ActiveRuleType.classRecord);
            tmpRule.CopyHeadInformationFrom(EditRule);
            EditRule = tmpRule;
        }
        private void listBoxActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxActionType.SelectedIndex < 0)
            {
                ActiveActionType = null;
                return;
            }
            // get selected rule information
            var tmpActionClass = (ClassRecord)listBoxActionType.Items[listBoxActionType.SelectedIndex];
            // change EditRule.Action to new type
            if (!blockUIChange)
            {
                HandleRuleAction tmpAction = (HandleRuleAction)Activator.CreateInstance(tmpActionClass.classRecord);
                EditRule.Action = tmpAction;
            }
            ActiveActionType = tmpActionClass;
        }
    }
}
