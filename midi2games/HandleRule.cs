using Melanchall.DryWetMidi.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace midi2games
{
    [XmlInclude(typeof(NoteOnRule))]
    [XmlInclude(typeof(NoteOffRule))]
    [XmlInclude(typeof(ControlValueRule))]
    [XmlInclude(typeof(ControlValueIncreaceRule))]
    [XmlInclude(typeof(ControlValueDecreaceRule))]
    [XmlInclude(typeof(AnyMidiEventRule))]
    [XmlInclude(typeof(ProgramChangeRule))]

    [XmlInclude(typeof(RuleAction))]
    [XmlInclude(typeof(RuleActionKey))]
    [XmlInclude(typeof(RuleActionKey))]
    [XmlInclude(typeof(RuleActionMouseOffset))]
    [XmlInclude(typeof(RuleActionMouseAbs))]
    public class HandleRule
    {
        public string RuleName { get; set; }

        public RuleAction Action {get; set; }
        public Boolean StopProcessing { get; set; } = true;
        public virtual Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            return false;
        }

        public virtual string GetHumanName()
        {
            return "<abstract rule>";
        }

    }

    public class NoteOnRule : HandleRule
    {
        public int Note { get; set; }
        public NoteOnRule() { }
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

    public class NoteOffRule : HandleRule
    {
        public int Note { get; set; }
        public NoteOffRule() { }
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

    public class ControlValueRule : HandleRule
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

    public class ControlValueIncreaceRule : HandleRule
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

    public class ControlValueDecreaceRule : HandleRule
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

    public class AnyMidiEventRule : HandleRule
    {
        public override bool CheckMatch(object sender, MidiEvent midiEvent)
        {
            return true;
        }

        public override string GetHumanName()
        {
            return $"Any MIDI event";
        }
    }

    public class ProgramChangeRule : HandleRule
    {
        public override bool CheckMatch(object sender, MidiEvent midiEvent)
        {
            if (midiEvent.EventType != MidiEventType.ProgramChange)
                return false;
            var e = (ProgramChangeEvent)midiEvent;
            return (e.ProgramNumber == ProgramNumber);
        }

        public override string GetHumanName()
        {
            return $"Any MIDI event";
        }

        public int ProgramNumber { get; set; }
    }

    public abstract class RuleAction
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

    public class RuleActionKey : RuleAction
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

    public class RuleActionMouseOffset : RuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Orientation Direction { get; set; }
        public int Offset { get; set; }
        public override void Execute()
        {
            Cursor cursor = new Cursor(Cursor.Current.Handle);
            if (Direction == Orientation.Horizontal)
            {
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + Offset, Cursor.Position.Y);
            } else
            {
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y + Offset);
            }
        }
        public RuleActionMouseOffset() { }

        public override string GetHumanName()
        {
            return $"Mouse offset ({Direction.ToString()}, {Offset})";
        }
    }

    public class RuleActionMouseAbs : RuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public int X { get; set; }
        public int Y { get; set; }
        public override void Execute()
        {
            Cursor cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new System.Drawing.Point(X, Y);           
        }
        public RuleActionMouseAbs() { }

        public override string GetHumanName()
        {
            return $"Mouse absolute position ({X}, {Y})";
        }
    }

    public class RuleClassRecord
    {
        public Type ruleClass;
        public string visibleName;
        public Type formClass;

        public RuleClassRecord(Type ruleClassArg, string visibleNameArg, Type formClassArg)
        {
            ruleClass = ruleClassArg;
            visibleName = visibleNameArg;
            formClass = formClassArg;
        }

        public override string ToString()
        {
            return visibleName;
        }
    }

    public class RuleTypeManager
    {
        public List<RuleClassRecord> ruleTypes = new List<RuleClassRecord>();
        public List<Type> actionTypes = new List<Type>();
        public RuleTypeManager()
        {
            InitBasicRuleClasses();
            InitBasicActionClasses();
        }

        public void RegisterRuleType(RuleClassRecord record)
        {
            ruleTypes.Add(record);
        }

        public void InitBasicRuleClasses()
        {            
            RegisterRuleType(new RuleClassRecord(typeof(NoteOnRule), "NoteOn", typeof(FormRuleNote)));
            RegisterRuleType(new RuleClassRecord(typeof(NoteOffRule), "NoteOff", typeof(FormRuleNote)));
            RegisterRuleType(new RuleClassRecord(typeof(ControlValueRule), "Control value match", typeof(FormControlValeMatch)));
            RegisterRuleType(new RuleClassRecord(typeof(ControlValueDecreaceRule), "Control value decrease", typeof(FormControlValueIncDec)));
            RegisterRuleType(new RuleClassRecord(typeof(ControlValueIncreaceRule), "Control value increase", typeof(FormControlValueIncDec)));
            RegisterRuleType(new RuleClassRecord(typeof(AnyMidiEventRule), "Any MIDI event", null));
            RegisterRuleType(new RuleClassRecord(typeof(ProgramChangeRule), "Program change", null));
        }

        public void InitBasicActionClasses()
        {

        }
    }

}
