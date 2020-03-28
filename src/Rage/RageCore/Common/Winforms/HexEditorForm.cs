using RageCore.Common.GameFiles;
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

namespace RageCore.Common.Winforms
{
    public partial class HexEditorForm : Form
    {
        private string FilePath { get; set; }
        private string FileName { get { return Path.GetFileName(FilePath); } }
        private byte[] Data { get; set; }
        
        public HexEditorForm(Icon icon, string filePath, byte[] data)
        {
            InitializeComponent();

            FilePath = filePath;
            Data = data;

            UpdateHexTextBox();
            UpdateForm(icon);
        }

        private void UpdateForm(Icon icon)
        {
            Icon = icon;

            FilePathStatusStripLabel.Text = "Loaded " + FilePath;
            this.Text = "Hex Editor - Skylumz - " + FileName;
        }

        private void UpdateHexTextBox()
        {
            if(Data == null) { return; }

            var text = ByteArrayToString(Data);

            HexTextBox.Text = text;
        }

        //move to Utils/HexUtilties.cs?
        public static string ByteArrayToString(byte[] data, bool withOffsets = true)
        {
            return data.ToString();
        }
    }
}
