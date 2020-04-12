using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RageCore.Common.Utils;
using RageCore.Common.GameFiles;
using RageCore.GTA3.GameFiles;
using RageCore.Common.Winforms;
using System.IO;

namespace GTA3TOOLS
{
    public partial class Gta3ExplorerForm : ExplorerForm
    {
        public Gta3ExplorerForm(GTAPATH gp) : base(gp)
        {
            InitializeComponent();
            ArchiveFileExtension = ".img";
            AddFileExtensionImageIndexs();
        }

        private void AddFileExtensionImageIndexs()
        {
            FileTypeImageIndexDict.Add("dff", 121);
            FileTypeImageIndexDict.Add("col", 123);
            FileTypeImageIndexDict.Add("txd", 714);
            FileTypeImageIndexDict.Add("ide", 674);
            FileTypeImageIndexDict.Add("set", 674);
            FileTypeImageIndexDict.Add("gxt", 674);
            FileTypeImageIndexDict.Add("ipl", 674);
            FileTypeImageIndexDict.Add("scm", 635);
            FileTypeImageIndexDict.Add("raw", 278);
            FileTypeImageIndexDict.Add("sdt", 278);
            FileTypeImageIndexDict.Add("anm", 405);
            FileTypeImageIndexDict.Add("ifp", 405);
            FileTypeImageIndexDict.Add("img", FileTypeImageIndexDict["archive"]);
        }

        //make virtual in base ?
        private ListViewItem ListViewItemFromArchiveFileEntry(Img1DirectoryEntry entry)
        {
            var lvi = new ListViewItem(entry.Name, GetImageIndex(entry.Extension));
            var type = entry.Name.Split('.')[1].ToUpper() + " FILE";
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, type));
            lvi.Tag = entry;
            return lvi;
        }

        public override ArchiveFile LoadArchive(string filepath)
        {
            ImgFile img = new ImgFile(filepath);
            img.Load();

            return img;
        }

        public override void DisplayArchive(ArchiveFile arch)
        {
            MainListView.Items.Clear();

            var img = arch as ImgFile;
            foreach(var entry in img.DirectoryEntries)
            {
                MainListView.Items.Add(ListViewItemFromArchiveFileEntry(entry));
            }

            //really dont want path text box public
            PathTextBox.Text = arch.FilePath;
        }

        public void ViewDffFile(string filepath, byte[] data)
        {
            var dff = new DffFile(filepath);
            dff.Load(data);

            //Form f = new Form();
            //PropertyGrid p = new PropertyGrid();
            //p.Dock = DockStyle.Fill;
            //p.SelectedObject = dff;
            //f.Controls.Add(p);
            //f.Show();
        }
        public override void ViewTxdFile(string filepath, byte[] data)
        {
            TxdEditorForm tf = new TxdEditorForm(this, filepath, data);
            tf.Show();
        }
        public override void ViewFile(ArchiveFileEntry afe, bool hex = false)
        {
            var entry = afe as Img1DirectoryEntry;

            var filepath = entry.Name;
            var data = entry.Data;

            if (hex)
            {
                ViewHexFile(filepath, data);
                return;
            }

            switch(entry.Extension.Replace(".", string.Empty).ToLower())
            {
                case "dff":
                    ViewDffFile(filepath, data);
                    break;
                case "txd":
                    ViewTxdFile(filepath, data);
                    break;
                case "dat":
                    ViewTextFile(filepath, data);
                    break;
                case "ini":
                    ViewTextFile(filepath, data);
                    break;
                case "txt":
                    ViewTextFile(filepath, data);
                    break;
                case "cfg":
                    ViewTextFile(filepath, data);
                    break;
                case "xml":
                    ViewXmlFile(filepath, data);
                    break;
                default:
                    ViewHexFile(filepath, data);
                    break;
            }

        }

        public override void SearchArchive(ArchiveFile af, string searchString, ref List<ListViewItem> searchItems)
        {
            var img = af as ImgFile;

            foreach (var afe in img.DirectoryEntries)
            {
                if (afe.Name.ToLower().Contains(searchString))
                {
                    searchItems.Add(ListViewItemFromArchiveFileEntry(afe));
                }
            }
        }

        public override void ExtractArchiveFile(ArchiveFileEntry afe)
        {
            var entry = afe as Img1DirectoryEntry;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(fbd.SelectedPath + "\\" + entry.Name, entry.Data);
            }
        }
    }
}
