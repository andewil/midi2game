using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    public class MidiHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();    
        private InputDevice inputDevice;
        PresetFile presetFile;

        public PresetFile PresetFile {
            get { return presetFile; }
            set { presetFile = value; }
        }

        public MidiHandler(string deviceName)
        {
            inputDevice = InputDevice.GetByName(deviceName);
            OpenDevice();
        }

        public MidiHandler(int deviceId)
        {
            inputDevice = InputDevice.GetById(deviceId);
            OpenDevice();
        }

        private void OpenDevice()
        {
            inputDevice.EventReceived += OnEventReceived;
            try
            {
                inputDevice.StartEventsListening();
            } catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        public InputDevice Device => inputDevice;

        public void Close()
        {
            try {
                inputDevice.StopEventsListening();
                inputDevice.EventReceived -= OnEventReceived;
                inputDevice.Dispose();
            } catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice) sender;
            logger.Debug($"MIDI event received from '{midiDevice.Name}: ' {e.Event}");
            ProcessRules(e);
            OnRecieveEvent?.Invoke(this, e);
        }

        private void ProcessRules(MidiEventReceivedEventArgs e)
        {
            if (presetFile == null)
                return;

            var ruleIndex = 0;
            foreach (HandleRule rule in presetFile.rulesStorage)
            {
                logger.Trace($"handle rule index={ruleIndex} : {rule.GetType().Name}");
                var matchResult = rule.CheckMatch(this, e.Event);
                logger.Trace($" -- check match = {matchResult}");
                if (matchResult)
                {
                    // call event
                    OnMatchRuleEvent?.Invoke(this, rule);

                    // execute action
                    if (rule.Action != null)
                    {
                        logger.Trace($"-- executed linked action: {rule.Action.GetType().Name}");                        
                        rule.Action.Execute();
                    }
                    else
                    {
                        logger.Trace("-- there is no linked action");
                    }


                    if (rule.StopProcessing)
                    {
                        logger.Trace("-- flag StopProcessing is set. Break.");
                        break;
                    }
                }
                ruleIndex++;
            }
        }


        public delegate void RecieveEventHandler(object sender, MidiEventReceivedEventArgs args);        
        public event RecieveEventHandler OnRecieveEvent;
        public event RuleEventHandler OnMatchRuleEvent;
    }

}
