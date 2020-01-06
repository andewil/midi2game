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
            inputDevice.StopEventsListening();
            inputDevice.EventReceived -= OnEventReceived;
            inputDevice.Dispose();
        }

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice) sender;
            logger.Debug($"MIDI event received from '{midiDevice.Name}: ' {e.Event}");
            switch (e.Event.EventType)
            {
                case MidiEventType.ProgramChange:
                    HandleProgramChange((ProgramChangeEvent) e.Event);
                    break;
                case MidiEventType.ControlChange:
                    HandleControlChange((ControlChangeEvent)e.Event);
                    break;
                default:
                    break;
            }

            OnRecieveEvent?.Invoke(this, e);
        }

        private void HandleProgramChange(ProgramChangeEvent e)
        {
            switch (e.ProgramNumber)
            {
                case 15:
                    EmulateKeyPress("{w}");
                    break;
                case 16:
                    EmulateKeyPress("{a}");
                    break;
                case 17:
                    EmulateKeyPress("{s}");
                    break;
                case 18:
                    EmulateKeyPress("{d}");
                    break;
            }
        }

        private void HandleControlChange(ControlChangeEvent e)
        {

        }

        private void EmulateKeyPress(string s)
        {
            SendKeys.SendWait(s);
            logger.Debug("emulate keys: " + s);
        }

        public delegate void RecieveEventHandler(object sender, MidiEventReceivedEventArgs args);
        public event RecieveEventHandler OnRecieveEvent;
    }

    public class LogMidiEvent
    {
        public string EventTypeName;
        public int Channel;
        public int Program;
        public int ProgramValue;
        public int Control;
    }
}
