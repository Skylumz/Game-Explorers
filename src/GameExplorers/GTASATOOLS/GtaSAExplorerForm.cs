using GameCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTASATOOLS
{
    public partial class GtaSAExplorerForm : ExplorerForm
    {
        private GAMEPATH gtap;

        public GtaSAExplorerForm(GAMEPATH gp) : base(gp)
        {
            InitializeComponent();
            gtap = gp;
        }
    }
}
