using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RageCore.Common.Utils;
using RageCore.Common.Winforms;

namespace GTASATOOLS
{
    public partial class GtaSAExplorerForm : ExplorerForm
    {
        private GTAPATH gtap;

        public GtaSAExplorerForm(GTAPATH gp) : base(gp)
        {
            InitializeComponent();
            gtap = gp;
        }
    }
}
