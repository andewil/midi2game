﻿using Melanchall.DryWetMidi.Devices;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace midi2games
{
    public class PresetFile
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PresetFile()
        {
            
        }

        public string Author { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }

        [XmlArray("Rules"), XmlArrayItem(typeof(HandleRule), ElementName = "Rule")]
        public ObservableCollection<HandleRule> rulesStorage = new ObservableCollection<HandleRule>();

        public event RuleEventHandler BeforeDeleteRule;
        public event RuleEventHandler AfterAddRule;
        public event RuleEventHandler BeforeReplace;
        public event EventHandler BeforeClearAllRules;

        public void AddRule(HandleRule rule)
        {
            rulesStorage.Add(rule);
            AfterAddRule?.Invoke(this, rule);
        }

        public void RemoveRule(HandleRule rule)
        {
            BeforeDeleteRule?.Invoke(this, rule);
            rulesStorage.Remove(rule);
        }

        public void Clear()
        {
            BeforeClearAllRules?.Invoke(this, null);
            rulesStorage.Clear();
        }

        public int GetIndexByRule(HandleRule rule)
        {
            return rulesStorage.IndexOf(rule);
        }

        public void ReplaceRule(int index, HandleRule replacement)
        {
            BeforeReplace?.Invoke(this, replacement);
            var removingItem = rulesStorage[index];
            rulesStorage.Insert(index, replacement);
            rulesStorage.Remove(item: removingItem);
        }

        public void ReplaceRule(HandleRule oldRule, HandleRule newRule)
        {
            BeforeReplace?.Invoke(this, newRule);
            int index = rulesStorage.IndexOf(oldRule);
            if (index < 0)
                index = rulesStorage.Count;
            rulesStorage.Insert(index, newRule);
            rulesStorage.Remove(oldRule);
        }

        public string SerializeXml()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PresetFile));
            var xml = "";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, this);
                    xml += sww.ToString(); // Your XML
                }
            }
            logger.Debug(xml);
            return xml;
        }

        public static void SerializeToFile(string fileName, PresetFile rulesData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PresetFile));
            using (var sww = new FileStream(fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(sww, rulesData);
            }
        }

        public static PresetFile DeserializeFromFile(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PresetFile));
            PresetFile res;
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                res = (PresetFile)xmlSerializer.Deserialize(reader);
            }
            return res;
        }

    }
}
