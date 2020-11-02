using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RageCore.Common.Winforms
{
    public class FormUtils
    {
        private static OpenFileDialog ofd;

        public static string ShowFolderSelector(string title, string message = "")
        {
            ofd = new OpenFileDialog();
            ofd.Title = title + " - " + message;
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
