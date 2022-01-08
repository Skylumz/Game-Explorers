using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace GameCore
{
    public static class ApplicationSettings
    {
        private static string SettingsFileName = "settings.xml";

        public static Dictionary<string, string> GetSettings()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + SettingsFileName;
            var xmlString = File.ReadAllText(path);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var settings = new Dictionary<string, string>();

            settings["gtapath"] = xmlDoc["gtapath"].InnerText;

            return settings;
        }

        public static bool SetSettings(Dictionary<string, string> settings)
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + SettingsFileName;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
                
                foreach(var setting in settings)
                {
                    sb.AppendLine("<" + setting.Key + ">" + setting.Value + "</" + setting.Key + ">");
                }

                File.WriteAllText(path, sb.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
