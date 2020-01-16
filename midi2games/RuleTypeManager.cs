using System;
using System.Collections.Generic;

namespace midi2games
{
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
