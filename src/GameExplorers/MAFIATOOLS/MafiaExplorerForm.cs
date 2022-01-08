using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameCore;

namespace MAFIATOOLS
{
    public partial class MafiaExplorerForm : ExplorerForm
    {
        public MafiaExplorerForm(GAMEPATH gp) : base(gp)
        {
            InitializeComponent();
        }
    }
}
