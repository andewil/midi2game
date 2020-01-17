using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    class AppUtils
    {
        public const string AppDirectoryName = "midi2games";
        public const string PresetsDirectoryName = "presets";
        public static string GetBaseDataDirectory()
        {
            var s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = string.Format(@"{0}\{1}", s, AppDirectoryName);
            return fullPath;
        }

        public static string GetPresetsDirectory()
        {
            return string.Format(@"{0}\{1}", GetBaseDataDirectory(), PresetsDirectoryName);
        }

        public static int StrToIntDef(string text, int defaultValue = -1)
        {
            if (text != null)
            {
                int val = 0;
                if (int.TryParse(text, out val))
                    return val;
            }
            return defaultValue;
        }

        public static int GetIntFromTextBox(TextBox edit, int defaultValue = -1)
        {
            var s = edit.Text.Trim();
            return StrToIntDef(s, defaultValue);
        }
    }
}
