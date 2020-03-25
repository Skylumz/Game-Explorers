using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA3TOOLS.FormResources;

namespace GTA3TOOLS.Utils
{
    public class GTAPATH
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

        public GTAPATH(string k, string p = null)
        {
            key = k;
            if (ExePath != null) { ExePath = p; }
            else { GetGtaFolder(); }
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

            ExePath = FormBoxes.ShowFolderSelector("Please select a folder that contains gta3.exe!");
        }
    }

    
}
