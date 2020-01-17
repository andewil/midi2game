using System;
using System.Collections.Generic;

namespace midi2games
{
    public class RuleTypeManager
    {
        public List<ClassRecord> ruleTypes = new List<ClassRecord>();
        public List<ClassRecord> actionTypes = new List<ClassRecord>();
        public RuleTypeManager()
        {
            InitBasicRuleClasses();
            InitBasicActionClasses();
        }

        public void RegisterRuleType(ClassRecord record)
        {
            ruleTypes.Add(record);
        }

        public void RegisterActionType(ClassRecord record)
        {
            actionTypes.Add(record);
        }

        public void InitBasicRuleClasses()
        {            
            RegisterRuleType(new ClassRecord(typeof(NoteOnRule), "NoteOn", typeof(FormRuleNote)));
            RegisterRuleType(new ClassRecord(typeof(NoteOffRule), "NoteOff", typeof(FormRuleNote)));
            RegisterRuleType(new ClassRecord(typeof(ControlValueRule), "Control value match", typeof(FormRuleControlValeMatch)));
            RegisterRuleType(new ClassRecord(typeof(ControlValueDecreaceRule), "Control value decrease", typeof(FormRuleControlValueIncDec)));
            RegisterRuleType(new ClassRecord(typeof(ControlValueIncreaceRule), "Control value increase", typeof(FormRuleControlValueIncDec)));
            RegisterRuleType(new ClassRecord(typeof(AnyMidiEventRule), "Any MIDI event", null));
            RegisterRuleType(new ClassRecord(typeof(ProgramChangeRule), "Program change", null));           
        }

        public void InitBasicActionClasses()
        {
            RegisterActionType(new ClassRecord(typeof(RuleActionKey), "Send key", typeof(FormActionSendKey)));
            RegisterActionType(new ClassRecord(typeof(RuleActionMouseAbs), "Move mouse pointer to position", typeof(FormActionMouseAbs)));
            RegisterActionType(new ClassRecord(typeof(RuleActionMouseOffset), "Move mouse (offset)", typeof(FormActionMouseOffset)));
        }
    }
}
