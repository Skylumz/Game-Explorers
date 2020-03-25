using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA3TOOLS.FormResources
{
    public class FormBoxes
    {
        private static OpenFileDialog ofd;

        public static string ShowFolderSelector(string title, string message = "")
        {
            ofd = new OpenFileDialog();
            ofd.Title = message;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return "NULL";
            }
        }       
    }
}
