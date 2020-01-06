using Melanchall.DryWetMidi.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    class HandleRule
    {
        public string RuleName { get; set; }
        public RuleAction Action {get; set; }
        public Boolean StopProcessing { get; set; }
        public virtual Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            return false;
        }

        public virtual string GetHumanName()
        {
            return "<abstract rule>";
        }
    }

    class NoteOnRule : HandleRule
    {
        public int Note { get; set; }
        public NoteOnRule(int noteValue)
        {
            Note = noteValue;
        }

        public override bool CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.NoteOn)
                return false;
            var noteEvent = (NoteEvent)midiEvent;
            var noteValue = noteEvent.NoteNumber;
            return ((noteValue == Note) && (noteEvent.Velocity > 0));
        }

        public override string GetHumanName()
        {
            return $"Note On {Note}";
        }
    }

    class NoteOffRule : HandleRule
    {
        public int Note { get; set; }
        public NoteOffRule(int noteValue)
        {
            Note = noteValue;
        }

        public override bool CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.NoteOn)
                return false;
            var noteEvent = (NoteEvent)midiEvent;
            var noteValue = noteEvent.NoteNumber;
            return ((noteValue == Note) && (noteEvent.Velocity == 0));
        }

        public override string GetHumanName()
        {
            return $"Note Off {Note}";
        }
    }

    class ControlValueRule : HandleRule
    {
        public int ControlNumber { get; set; }
        public int ControlValue { get; set; }
        public override Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.ControlChange)
                return false;
            var e = (ControlChangeEvent)midiEvent;
            var cn = e.ControlNumber;
            var cv = e.ControlValue;
            var c1 = cn == ControlNumber;
            var c2 = cv == ControlValue;
            return c1 && c2;
        }

        public ControlValueRule() {  }
        public ControlValueRule(int ctrlNumber, int ctrlValue)
        {
            ControlNumber = ctrlNumber;
            ControlValue = ctrlValue;
        }

        public override string GetHumanName()
        {
            return $"Control value ({ControlNumber}, {ControlValue})";
        }
    }

    class ControlValueIncreaceRule : HandleRule
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private int previousControlValue = 0;
        public int ControlNumber { get; set; }
        public override Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.ControlChange)
                return false;
            var e = (ControlChangeEvent)midiEvent;
            var cn = e.ControlNumber;
            if (cn != ControlNumber)
                return false;
            var cv = e.ControlValue;
            var res = (cv > previousControlValue);
            logger.Trace($"--- Compare current control value ({cv} and previous value ({previousControlValue}). Result = {res}");
            previousControlValue = cv;
            return res;
        }

        public override string GetHumanName()
        {
            return $"Control value increace ({ControlNumber})";
        }
    }

    class ControlValueDecreaceRule : HandleRule
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private int previousControlValue = 0;
        public int ControlNumber { get; set; }
        public override Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.ControlChange)
                return false;
            var e = (ControlChangeEvent)midiEvent;
            var cn = e.ControlNumber;
            if (cn != ControlNumber)
                return false;
            var cv = e.ControlValue;
            var res = (cv < previousControlValue);
            logger.Trace($"--- Compare current control value ({cv} and previous value ({previousControlValue}). Result = {res}");
            previousControlValue = cv;
            return res;
        }

        public override string GetHumanName()
        {
            return $"Control value decreace ({ControlNumber})";
        }
    }

    abstract class RuleAction
    {
        /// <summary>
        /// Execute action
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Get humanize description of action
        /// </summary>
        /// <returns>String</returns>
        public abstract string GetHumanName();
    }

    class RuleActionKey : RuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string KeysToSend { get; set; }
        public override void Execute()
        {
            EmulateKeyPress(KeysToSend);
            //throw new NotImplementedException();
        }
        public RuleActionKey() { }
        public RuleActionKey(string keys)
        {
            KeysToSend = keys;
        }

        private void EmulateKeyPress(string s)
        {
            SendKeys.SendWait(s);
            logger.Debug("Key press : " + s);
        }

        public override string GetHumanName()
        {
            return "Key press: " + KeysToSend;
        }
    }
}
