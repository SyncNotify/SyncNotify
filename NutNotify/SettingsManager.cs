using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SyncNotify
{
    internal class SettingsManager
    {

        public static void SaveSettingsToFile(Settings settings)
        {
            if (!Directory.Exists(App.RootPath))
            {
                Directory.CreateDirectory(App.RootPath);
            }
            string text = JsonConvert.SerializeObject(settings, Formatting.Indented);
            try
            {
                File.WriteAllText(App.RootPath + Settings.settingsFileName, text);
            }
            catch { }
        }
    }
}
