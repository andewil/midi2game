using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    public partial class FormMidi2Game : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private PresetFile rulesManager;
        public FormMidi2Game()
        {
            InitializeComponent();
        }

        private void FormMidi2Game_Load(object sender, EventArgs e)
        {
            SetupLogOutput();
            OpenLogForm();
            FillInputDevices();
            tsbMonitor.PerformClick();
            InitRulesManager();
            RefreshStatusDevice();
        }

        void InitRulesManager()
        {
            var f = new PresetFile();
            f.Author = "Andewil";
            f.Version = "1.0";
            f.Comment = "For tests only";

            var r1 = new ControlValueIncreaceRule();
            r1.ControlNumber = 50;
            r1.RuleName = "Rule 1";
            var a1 = new RuleActionMouseAbs();
            a1.X = 400;
            a1.Y = 720;
            r1.Action = a1;
            f.AddRule(r1);

            var r2 = new ControlValueDecreaceRule();
            r2.RuleName = "Rule 2";
            r2.ControlNumber = 50;
            var a2 = new RuleActionMouseAbs();
            a2.X = 1500;
            a2.Y = 720;
            r2.Action = a2;
            f.AddRule(r2);

            var r3 = new ControlValueIncreaceRule();
            r3.ControlNumber = 43;
            r3.RuleName = "Rule 3";
            r3.Action = new RuleActionKey("{z}");
            f.AddRule(r3);

            var r4 = new NoteOnRule(77);
            r2.RuleName = "Rule 4 - note";
            var a4 = new RuleActionKey("{d}");
            r4.Action = a4;
            f.AddRule(r4);

            var r5 = new ControlValueDecreaceRule();
            r5.ControlNumber = 93;
            r5.RuleName = "Rule 5 - decrease";
            var a5 = new RuleActionMouseOffset();
            a5.Offset = 50;
            a5.Direction = Orientation.Horizontal;
            r5.Action = a5;
            f.AddRule(r5);

            var r6 = new ControlValueIncreaceRule();
            r6.ControlNumber = 93;
            r6.RuleName = "Rule 6 - increase";
            var a6 = new RuleActionMouseOffset();
            a6.Offset = -50;
            a6.Direction = Orientation.Horizontal;
            r6.Action = a6;
            f.AddRule(r6);

            var r7 = new ControlValueIncreaceRule();
            r7.ControlNumber = 43;
            r7.RuleName = "Rule 7 - increace";
            r7.Action = new RuleActionKey("{y}");
            f.AddRule(r7);

            var r8 = new ControlValueDecreaceRule();
            r8.ControlNumber = 51;
            r8.RuleName = "Rule 8 - decrease";
            var a8 = new RuleActionMouseOffset();
            a8.Offset = 50;
            a8.Direction = Orientation.Vertical;
            r8.Action = a8;
            f.AddRule(r8);

            var r9 = new ControlValueIncreaceRule();
            r9.ControlNumber = 51;
            r9.RuleName = "Rule 9 - increase";
            var a9 = new RuleActionMouseOffset();
            a9.Offset = -50;
            a9.Direction = Orientation.Vertical;
            r9.Action = a9;
            f.AddRule(r9);

            SetPresetFile(f);
        }

        void FillInputDevices()
        {            
            tsComboBoxDevice.Items.Clear();
            var devices = InputDevice.GetAll();
            foreach (InputDevice device in devices)
            {
                tsComboBoxDevice.Items.Add(device);
            }

            // select first one
            if (devices.Count() > 0)
            {
                tsComboBoxDevice.SelectedIndex = 0;
            }
        }

        public FormLog formLog;
        public FormMonitor formMonitor;
        public FormRule formRule;

        void OpenLogForm()
        {
            if (formLog == null)
                formLog = new FormLog();
            formLog.formMain = this;
            formLog.Show();
            formLog.BringToFront();            
        }

        private void SetPresetFile(PresetFile file)
        {
            rulesManager = file;
            if (midiHandler != null)
            {
                midiHandler.PresetFile = file;
            }
            RefreshRules();
        }

        private void SetHandler(MidiHandler h)
        {
            if (midiHandler != null)
            {
                midiHandler.OnRecieveEvent -= HandleRecieveEvent;
                midiHandler.OnMatchRuleEvent -= OnMatchRule;
            }
            midiHandler = h;
            if (midiHandler != null)
            {
                midiHandler.OnMatchRuleEvent += OnMatchRule;
                midiHandler.OnRecieveEvent += HandleRecieveEvent;
                midiHandler.PresetFile = rulesManager;
            }
            // attach to Monitor form
            if (formMonitor != null)
            {
                logger.Debug("Attaching handler to FormMonitor");
                formMonitor.SetHandler(midiHandler);
            }            
        }

        void SetupLogOutput()
        {
            var t = new NLog.Targets.MethodCallTarget("textBoxLogTarget", (logEvent, parms) => AddLogString(logEvent, parms));
            if (LogManager.Configuration == null)
            {
                var config = new NLog.Config.LoggingConfiguration();
                config.AddTarget(t);
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, t);
                LogManager.Configuration = config;
            }
            else
            {
                LogManager.Configuration.AddTarget(t);
                LogManager.Configuration.AddRule(LogLevel.Debug, LogLevel.Fatal, t);
                LogManager.ReconfigExistingLoggers();
            }
        }

        void AddLogString(LogEventInfo eventInfo, object[] parms)
        {
            if (formLog != null)
                formLog.AddLogString(DateTime.Now.ToString("HH:mm:ss:fff") + "  :  " + eventInfo.Level.ToString() + " : " + eventInfo.FormattedMessage + Environment.NewLine);
        }

        private MidiHandler midiHandler;
        private void tsbOpenDevice_Click(object sender, EventArgs e)
        {
            logger.Info("Try to open device...");
            if (midiHandler != null)
            {
                midiHandler.Close();
                SetHandler(null);
            }
            var device = (InputDevice)tsComboBoxDevice.SelectedItem;
            logger.Info($"Start handler {device.Name} id={device.Id}");
            logger.Info($"   Product identifier  : {device.ProductIdentifier}");
            logger.Info($"   Driver manufacturer : {device.DriverManufacturer} version {device.DriverVersion} ");
            if (midiHandler == null)
            {
                logger.Debug("Creating handler...");
                var h = new MidiHandler(device.Id);
                SetHandler(h);
                logger.Debug("Handler created");
            }
            RefreshStatusDevice();
        }

        private void tsbCloseDevice_Click(object sender, EventArgs e)
        {
            if (midiHandler == null)
            {
                logger.Info("Nothing to close :)");
                return;
            }
            midiHandler.Close();
            SetHandler(null);
            logger.Info("Device closed");
        }

        private void tsbOpenDataDirectory_Click(object sender, EventArgs e)
        {
            string s = AppUtils.GetBaseDataDirectory();
            Process.Start("explorer.exe", s);
        }
        
        private void tsbMonitor_Click(object sender, EventArgs e)
        {
            logger.Debug("Open FormMonitor");
            if (formMonitor == null)
            {
                logger.Debug("Creating FormMonitor");
                formMonitor = new FormMonitor();
                formMonitor.SetHandler(midiHandler);
                formMonitor.formMain = this;
            }
            formMonitor.Show();
            formMonitor.BringToFront();
        }

        private void toolStripContainer1_DragEnter(object sender, DragEventArgs e)
        {
            logger.Debug(e.ToString());
        }

        private void toolStripContainer1_DragOver(object sender, DragEventArgs e)
        {
            logger.Debug(e.ToString());
        }

        private void FillRule(ListViewItem item, HandleRule rule)
        {
            item.Tag = rule;
            item.SubItems.Clear();
            item.SubItems.Add(rule.RuleName);
            item.SubItems.Add(rule.GetHumanName());
            item.Text = item.Index.ToString();
            item.StateImageIndex = 0;
            if (rule.Action != null)
            {
                item.SubItems.Add(rule.Action.GetHumanName());
            }        
            else
            {
                item.SubItems.Add("no action");
            }
        }

        private void RefreshRules()
        {
            listViewRules.BeginUpdate();
            try                
            {                
                listViewRules.Items.Clear();
                if (rulesManager != null)
                {
                    int counter = 0;
                    foreach (HandleRule rule in rulesManager.rulesStorage)
                    {
                        ListViewItem itm = new ListViewItem();
                        FillRule(itm, rule);
                        // Set text = counter value, because item is not added yet and has no index
                        itm.Text = counter.ToString();
                        listViewRules.Items.Add(itm);
                        counter++;
                    }
                }
            } finally
            {
                listViewRules.EndUpdate();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rulesManager.SerializeXml());
            PresetFile.SerializeToFile(AppUtils.GetPresetsDirectory() + "/test.xml", rulesManager);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rulesManager.Clear();
            RefreshRules();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rm = PresetFile.DeserializeFromFile(AppUtils.GetPresetsDirectory() + "/test.xml");
            MessageBox.Show(rm.SerializeXml());
            SetPresetFile(rm);
        }

        private void tsbRuleUp_Click(object sender, EventArgs e)
        {
            if (listViewRules.SelectedItems.Count != 1) return;
            var oldIndex = listViewRules.SelectedItems[0].Index;
            if (oldIndex == 0) return;
            var newIndex = oldIndex - 1;
            rulesManager.rulesStorage.Move(oldIndex, newIndex);
            RefreshRules();
            listViewRules.Items[newIndex].Selected = true;
        }

        private void tsbRuleDown_Click(object sender, EventArgs e)
        {
            if (listViewRules.SelectedItems.Count != 1) return;
            var oldIndex = listViewRules.SelectedItems[0].Index;
            if (oldIndex == listViewRules.Items.Count - 1) return;
            var newIndex = oldIndex + 1;
            rulesManager.rulesStorage.Move(oldIndex, newIndex);
            RefreshRules();
            listViewRules.Items[newIndex].Selected = true;
        }

        private void tsbRuleDelete_Click(object sender, EventArgs e)
        {
            if (listViewRules.SelectedItems.Count == 0) return;
            foreach (ListViewItem itm in listViewRules.SelectedItems)
            {
                rulesManager.RemoveRule((HandleRule)itm.Tag);
            }
            RefreshRules();
        }

        private void RefreshStatusDevice()
        {
            string status = "";
            if (midiHandler == null)
            {
                status = "<None>";
            }
            else
            {
                status = midiHandler.Device.Name;
            }
            statusDevice.Text = status;
        }

        ListViewItem FindListItemByRule(HandleRule rule)
        {
            foreach (ListViewItem item in listViewRules.Items)
            {
                if (item.Tag == rule)
                    return item;
            }
            return null;
        }

        private Queue<HandleRule> matchedRules = new Queue<HandleRule>();
        private Queue<MidiEvent> queueEvents = new Queue<MidiEvent>();
        private void OnMatchRule(object sender, HandleRule rule)
        {
            matchedRules.Enqueue(rule);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {          
            // Clear rule state
            foreach (ListViewItem item in listViewRules.Items)
            {
                item.StateImageIndex = 0;
            }

            // Set Matched state to rule items
            while (matchedRules.Count > 0)
            {
                var rule = matchedRules.Dequeue();
                ListViewItem item = FindListItemByRule(rule);
                if (item != null)
                {
                    item.StateImageIndex = 1;
                }               
            }

            // Clear status event state
            statusSignal.BackColor = SystemColors.ButtonFace;
            statusSignal.ForeColor = SystemColors.ControlText;

            // Set signal state
            if (queueEvents.Count > 0)
            {
                queueEvents.Clear();
                statusSignal.BackColor = SystemColors.Highlight;
                statusSignal.ForeColor = SystemColors.HighlightText;
            }
        }

        private void HandleRecieveEvent(object sender, MidiEventReceivedEventArgs e)
        {
            queueEvents.Enqueue(e.Event);
        }

        private ListViewItem GetFirstSelectedRuleItem()
        {
            if (listViewRules.SelectedItems.Count == 0) return null;
            return listViewRules.SelectedItems[0];
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ListViewItem item = GetFirstSelectedRuleItem();
            if (item == null)
                return;
            HandleRule rule = (HandleRule)item.Tag;
            if (formRule == null)
            {
                formRule = new FormRule();
                formRule.OnSaveRule += AfterSaveRule;
                formRule.FormClosed += AfterCloseForm;
            }
            formRule.Rule = rule;
            formRule.PresetFile = rulesManager;
            formRule.Show();
            formRule.BringToFront();
        }

        private void AfterSaveRule(object sender, HandleRule rule, int index)
        {
            // set saved rule to ListItem, because new rule object was created
            ListViewItem item = listViewRules.Items[index];
            item.Tag = rule;
            FillRule(item, rule);
            item.Text = index.ToString();
        }

        private void AfterCloseForm(object sender, FormClosedEventArgs e)
        {
            if (sender == formRule)
            {
                formRule.OnSaveRule -= AfterSaveRule;
                formRule.FormClosed -= AfterCloseForm;
                formRule = null;
            }
        }

        private void tsbNex_Click(object sender, EventArgs e)
        {

        }
    }

}
