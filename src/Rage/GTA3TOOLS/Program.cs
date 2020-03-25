using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA3TOOLS.Utils;

namespace GTA3TOOLS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gtaPath = new GTAPATH("gta3.exe");

            Application.Run(new ExplorerForm(gtaPath));
        }
    }
}
