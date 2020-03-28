using GTA3TOOLS.Utils;
using RageCore.Common.GameFiles;
using RageCore.GTA1.GameFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTA3TOOLS
{
    public partial class Gta3ExplorerForm : GTA3TOOLS.ExplorerForm
    {
        public Gta3ExplorerForm(GTAPATH gp) : base(gp)
        {
            InitializeComponent();
        }

        public override ArchiveFile LoadArchive(string filepath)
        {
            ImgFile1 img = new ImgFile1(filepath);
            img.Load();

            return img;
        }

        public override void DisplayArchive(ArchiveFile arch)
        {
            MainListView.Items.Clear();

            var img = arch as ImgFile1;
            foreach(var entry in img.DirectoryEntries)
            {
               // MessageBox.Show(entry.Offset.ToString() + " " + entry.Size.ToString());

                var lvi = new ListViewItem(entry.Name, 1);
                var type = entry.Name.Split('.')[1].ToUpper() + " FILE";
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, type));
                lvi.Tag = entry;
                MainListView.Items.Add(lvi);
            }
        }

        public override void ViewFile(ArchiveFileEntry afe)
        {
            var entry = afe as Img1DirectoryEntry;

            var filepath = entry.Name;
            var data = entry.Data;

            switch(entry.Extension)
            {
                default:
                    ViewTextFile(filepath, data);
                    break;
            }

        }
    }
}
