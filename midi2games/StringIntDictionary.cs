using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace midi2games
{
    [XmlRoot("StringIntDictionary")]
    public class StringIntDictionary : Dictionary<string, int>, IXmlSerializable
    {
        public XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement) { return; }
            reader.Read();
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var key = reader.GetAttribute("Name");
                var value = reader.GetAttribute("Value");
                this.Add(key, int.Parse(value));
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var key in this.Keys)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Name", key.ToString());
                writer.WriteAttributeString("Value", this[key].ToString());
                writer.WriteEndElement();
            }

        }
    }
}
