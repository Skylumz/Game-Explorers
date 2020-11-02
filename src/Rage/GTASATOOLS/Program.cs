using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RageCore.Common.Utils;

namespace GTASATOOLS
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

            GTAPATH gp = new GTAPATH("gta_sa.exe");

            Application.Run(new GtaSAExplorerForm(gp));
        }
    }
}
