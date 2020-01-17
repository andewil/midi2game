using Melanchall.DryWetMidi.Core;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

    [XmlInclude(typeof(HandleRuleAction))]
    [XmlInclude(typeof(RuleActionKey))]
    [XmlInclude(typeof(RuleActionKey))]
    [XmlInclude(typeof(RuleActionMouseOffset))]
    [XmlInclude(typeof(RuleActionMouseAbs))]    
    public class HandleRule : ICloneable
    {
        public string RuleName { get; set; }
        public HandleRuleAction Action {get; set; }
        public Boolean StopProcessing { get; set; } = true;
        public HandleRule()
        {
            _sValues = new Collection<HandleRuleProperty>();
        }

        public virtual Boolean CheckMatch(object sender, MidiEvent midiEvent)
        {
            return false;
        }

        public HandleRuleProperty FindProperty(string name)
        {
            foreach (HandleRuleProperty item in _sValues)
                if (string.Equals(name, item.Name))
                    return item;
            return null;
        }

        public void SetValue(string name, int value)
        {
            HandleRuleProperty p = FindProperty(name);
            if (p == null)
            {
                p = new HandleRuleProperty();               
                p.Name = name;
                _sValues.Add(p);
            }
            p.Value = value;
        }

        public Boolean CheckValue(int value)
        {
            return ((value >= 0) && (value <= 127));
        }

        public void SetValueChecked(string name, int value)
        {
            if (!CheckValue(value))
                throw new Exception($"Value {value} is not correct value. Value must be in 0 - 127");
            SetValue(name, value);
        }

        public int GetValue(string name)
        {
            HandleRuleProperty p = FindProperty(name);
            if (p == null)
            {
                p = new HandleRuleProperty();
                p.Name = name;
                _sValues.Add(p);
            }
            return p.Value;
        }

        public object Clone()
        {
            var dstObj = (HandleRule) MemberwiseClone();
            if (Action != null) {
                var dstAction = (HandleRuleAction)Action.Clone();
                dstObj.Action = dstAction;
            }
            return dstObj;
        }

        public virtual string GetHumanName()
        {
            return "<abstract rule>";
        }

        public virtual void CopyHeadInformationFrom(HandleRule source)
        {
            StopProcessing = source.StopProcessing;
            if (source.Action != null) {
                var a = (HandleRuleAction)source.Action.Clone();
                Action = a;
            }
            SValues = source.SValues;
            this.RuleName = source.RuleName;
        }

        private Collection<HandleRuleProperty> _sValues;

        [XmlArray("SValues"), XmlArrayItem(typeof(HandleRuleProperty), ElementName = "Item")]
        public Collection<HandleRuleProperty> SValues
        {
            get { return _sValues; }
            set
            {
                _sValues.Clear();
                if (value != null)
                    foreach (HandleRuleProperty item in value)
                    {
                        var v = new HandleRuleProperty(item.Name, item.Value);
                        _sValues.Add(v);
                    }
            }
        }

        //[XmlArray("DValues"), XmlArrayItem(typeof(KeyValuePair<string, int>), ElementName = "Item")]
        //public StringIntDictionary DValues
        //{
        //    get { return _values; }
        //    set
        //    {
        //        _values.Clear();
        //        if (value != null)
        //            foreach (KeyValuePair<string, int> item in value)
        //            {
        //                _values.Add(item.Key, item.Value);
        //            }
        //    }
        //}
    }

    public class NoteOnRule : HandleRule
    {
        [XmlIgnore]
        public int Note {
            get => GetValue("Note");
            set => SetValue("Note", value);
        }
        public NoteOnRule() { }
        public NoteOnRule(int noteValue)
        {
            Note = noteValue;
            SetValue("Note", noteValue);
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
        [XmlIgnore]
        public int Note
        {
            get => GetValue("Note");
            set => SetValue("Note", value);
        }
        public NoteOffRule() { }
        public NoteOffRule(int noteValue)
        {
            Note = noteValue;
            SetValue("Note", noteValue);
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
        [XmlIgnore]
        public int ControlNumber
        {
            get => GetValue("ControlNumber");
            set => SetValue("ControlNumber", value);
        }

        [XmlIgnore]
        public int ControlValue
        {
            get => GetValue("ControlValue");
            set => SetValue("ControlValue", value);
        }

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

        [XmlIgnore]
        public int ControlNumber
        {
            get => GetValue("ControlNumber");
            set => SetValue("ControlNumber", value);
        }
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

        [XmlIgnore]
        public int ControlNumber
        {
            get => GetValue("ControlNumber");
            set => SetValue("ControlNumber", value);
        }

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

        [XmlIgnore]
        public int ProgramNumber {
            get { return GetValue("ProgramNumber"); }
            set { SetValue("ProgramNumber", value); }
        }
    }


    public class ClassRecord
    {
        public Type classRecord;
        public string visibleName;
        public Type classForm;

        public ClassRecord(Type ruleClassArg, string visibleNameArg, Type formClassArg)
        {
            classRecord = ruleClassArg;
            visibleName = visibleNameArg;
            classForm = formClassArg;
        }

        public override string ToString()
        {
            return visibleName;
        }
    }

    public class HandleRuleProperty
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public HandleRuleProperty() { }
        public HandleRuleProperty(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
