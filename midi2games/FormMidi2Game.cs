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
    }
}
