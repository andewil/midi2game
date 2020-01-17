using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace midi2games
{
    public class AppSettings
    {
        public string LastDeviceName { get; set; }

        public static void SerializeToFile(string fileName, AppSettings appSettings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
            using (var sww = new FileStream(fileName, FileMode.Create))
            {                
                xmlSerializer.Serialize(sww, appSettings);
            }
        }

        public static AppSettings DeserializeFromFile(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
            AppSettings res;
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                res = (AppSettings)xmlSerializer.Deserialize(reader);
            }
            return res;
        }
    }
}
