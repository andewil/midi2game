using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
