using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCore
{
    public class GAMEPATH
    {
        public string key { get; set; }

        public string ExePath
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public string FolderPath
        {
            get
            {
                return ExePath.Replace(key, "");
            }
        }

        private string path;
        private Dictionary<string, string> settings;

        public GAMEPATH(string k)
        {
            key = k;

            settings = ApplicationSettings.GetSettings();
            var p = settings["gtapath"];

            GetGtaFolder();

            //if(!p.Contains(key)) { GetGtaFolder(); }
            //else { ExePath = p; }
        }

        public bool HaveFolder()
        {
            if (ExePath != null)
            {
                if (ExePath.Contains(key)) { return true; }
                else { return false; }
            }
            else { return false; }
        }

        public void GetGtaFolder()
        {
            if (HaveFolder()) { return; }

            ExePath = FormUtils.ShowFolderSelector("Please select a folder that contains gta3.exe!");
            settings["gtapath"] = ExePath;
            ApplicationSettings.SetSettings(settings);
        }
    }

    
}
