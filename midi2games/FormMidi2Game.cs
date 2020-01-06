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
        private RulesManager rulesManager;
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
        }

        void InitRulesManager()
        {
            rulesManager = new RulesManager();

            var r1 = new ControlValueRule(43, 127);
            r1.RuleName = "Rule 1";
            var a1 = new RuleActionKey();
            a1.KeysToSend = "{w}";
            r1.Action = a1;
            rulesManager.AddRule(r1);

            var r2 = new ControlValueRule(93, 127);
            r2.RuleName = "Rule 2";
            var a2 = new RuleActionKey();
            a2.KeysToSend = "{s}";
            r2.Action = a2;
            r2.StopProcessing = true;
            rulesManager.AddRule(r2);

            var r3 = new ControlValueIncreaceRule();
            r3.ControlNumber = 43;
            r3.RuleName = "Rule 3";
            r3.Action = new RuleActionKey("{z}");
            rulesManager.AddRule(r3);

            var r4 = new NoteOnRule(77);
            r2.RuleName = "Rule 4 - note";
            var a4 = new RuleActionKey("{d}");
            r4.Action = a4;
            rulesManager.AddRule(r4);

            var r5 = new ControlValueDecreaceRule();
            r5.ControlNumber = 43;
            r5.RuleName = "Rule 5 - decrease";
            r5.Action = new RuleActionKey("{e}");
            rulesManager.AddRule(r5);

            var r6 = new ControlValueDecreaceRule();
            r6.ControlNumber = 93;
            r6.RuleName = "Rule 6 - decrease";
            r6.Action = new RuleActionKey("{t}");
            rulesManager.AddRule(r6);


            var r7 = new ControlValueIncreaceRule();
            r7.ControlNumber = 93;
            r7.RuleName = "Rule 7 - increace";
            r7.Action = new RuleActionKey("{y}");
            rulesManager.AddRule(r7);

            RefreshRules();
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

        void OpenLogForm()
        {
            if (formLog == null)
                formLog = new FormLog();
            formLog.formMain = this;
            formLog.Show();
            formLog.BringToFront();            
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
                rulesManager.MidiHandler = null;
                midiHandler.Close();
                midiHandler = null;
            }
            var device = (InputDevice)tsComboBoxDevice.SelectedItem;
            logger.Info($"Start handler {device.Name} id={device.Id}");
            logger.Info($"   Product identifier  : {device.ProductIdentifier}");
            logger.Info($"   Driver manufacturer : {device.DriverManufacturer} version {device.DriverVersion} ");
            if (midiHandler == null)
            {
                logger.Debug("Creating handler...");
                midiHandler = new MidiHandler(device.Id);
                rulesManager.MidiHandler = midiHandler;

                // attach to Monitor form
                if (formMonitor != null)
                {
                    logger.Debug("Attaching handler to FormMonitor");
                    formMonitor.SetHandler(midiHandler);
                }
                logger.Debug("Handler created");
            }            
        }

        private void tsbCloseDevice_Click(object sender, EventArgs e)
        {
            if (midiHandler == null)
            {
                logger.Info("Nothing to close :)");
                return;
            }
            midiHandler.Close();
            rulesManager.MidiHandler = null;
            midiHandler = null;
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
            item.SubItems.Clear();
            item.SubItems.Add(rule.RuleName);
            item.SubItems.Add(rule.GetHumanName());
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
            listView1.BeginUpdate();
            try
            {
                int counter = 0;
                foreach (HandleRule rule in rulesManager.rulesStorage)
                {
                    ListViewItem itm = new ListViewItem();
                    FillRule(itm, rule);
                    itm.Text = counter.ToString();
                    listView1.Items.Add(itm);
                    counter++;
                }
            } finally
            {
                listView1.EndUpdate();
            }
        }
    }
}
