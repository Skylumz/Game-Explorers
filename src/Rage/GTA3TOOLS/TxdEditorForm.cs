using RageCore.Common.Winforms;
using RageCore.GTA3.GameFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA3TOOLS
{
    public partial class TxdEditorForm : Form
    {
        private string FilePath { get; set; }
        private string FileName { get { return Path.GetFileName(FilePath); } }
        private byte[] Data { get; set; }

        public TxdEditorForm(ExplorerForm ef, string fp, byte[] data)
        {
            InitializeComponent();

            Owner = ef;
            FilePath = fp;
            Data = data;

            InitForm();
            UpdatePropertyGrid();
        }

        private void InitForm()
        {
            Icon = Owner.Icon;
            Text = "Txd Editor - Skylumz -" + FileName;
        }

        private void UpdatePropertyGrid()
        {
            TxdFile txd = new TxdFile(FilePath);
            txd.Load(Data);
            propertyGrid1.SelectedObject = txd;
        }
    }
}
