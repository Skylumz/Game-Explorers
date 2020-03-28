using RageCore.Common.GameFiles;
using RageCore.Common.Utils;
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
    public partial class ExplorerForm : Form
    {
        public GTAPATH GtaPath;

        private TreeNode PreviousTreeNode;

        public ExplorerForm(GTAPATH gp)
        {
            if (gp == null) { return; }

            GtaPath = gp;
            if(GtaPath.HaveFolder())
            {
                InitializeComponent();
                this.Icon = Icon.ExtractAssociatedIcon(GtaPath.ExePath);
                InitExplorer();
            }
        }

        private void InitExplorer()
        {
            var di = new DirectoryInfo(GtaPath.FolderPath);
            var root = new TreeNode(di.Name, 0, 0);
            root.Tag = di;
            GetAllFolders(ref root, di, "*");
            root.Expand();
            MainTreeView.Nodes.Add(root);
        }
        
        private void GetAllFolders(ref TreeNode root, DirectoryInfo dir, string searchPattern)
        {
            foreach(FileInfo f in dir.GetFiles())
            {
                if(f.Extension == ".img" || f.Extension == ".rpf")
                {
                    var afolder = new TreeNode(f.Name, 2, 2);
                    var archiveFile = LoadArchive(f.FullName);
                    afolder.Tag = archiveFile;
                    root.Nodes.Add(afolder);
                }
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                var folder = new TreeNode(d.Name, 0, 0);
                folder.Tag = d;
                root.Nodes.Add(folder);
                GetAllFolders(ref folder, d, searchPattern);
            }
        }
        
        private void UpdateMainListView()
        {
            MainListView.Items.Clear();

            var t = MainTreeView.SelectedNode.Tag;

            if(t is DirectoryInfo)
            {
                var di = t as DirectoryInfo;

                foreach (var d in di.GetDirectories())
                {
                    var lvi = new ListViewItem(d.Name, 0);
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "File Folder"));
                    lvi.Tag = d;
                    MainListView.Items.Add(lvi);
                }

                foreach (var f in di.GetFiles())
                {
                    var lvi = new ListViewItem(f.Name, 1);
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, f.Extension.TrimStart('.').ToUpper() + " FILE"));
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "TODO"));
                    object tag = f;
                    if(f.Extension == ".img" || f.Extension == ".rpf")
                    {
                        tag = LoadArchive(f.FullName);
                    }
                    lvi.Tag = tag;
                    MainListView.Items.Add(lvi);
                }
            }
            else if(t is ArchiveFile)
            {
                DisplayArchive(t as ArchiveFile);
            }

            UpdateMainStatusStrip();
        }
        
        private void UpdateMainStatusStrip()
        {
            var di = MainTreeView.SelectedNode.Tag as DirectoryInfo;
            AmountOfItemsInDirectoryLabel.Text = MainListView.Items.Count.ToString() + " Items";
            AmountOfItemsSelectedInListViewLabel.Text = MainListView.SelectedItems.Count.ToString() + " Items Selected";
        }
        
        public virtual ArchiveFile LoadArchive(string filepath) { return null; }
        public virtual void DisplayArchive(ArchiveFile arch) { }

        public void ViewItem(object tag)
        {
            if (tag is DirectoryInfo)
            {
                var di = tag as DirectoryInfo;
                foreach (TreeNode n in MainTreeView.SelectedNode.Nodes)
                {
                    var ndi = n.Tag as DirectoryInfo;
                    if (di.FullName == ndi.FullName)
                    {
                        MainTreeView.SelectedNode = n;
                        return;
                    }
                }
            }
            else if (tag is ArchiveFile)
            {
                DisplayArchive(tag as ArchiveFile);
            }
            else if (tag is FileInfo)
            {
                ViewFile(tag as FileInfo);
            }
            else if (tag is ArchiveFileEntry)
            {
                ViewFile(tag as ArchiveFileEntry);
            }
        }

        public virtual void ViewFile(ArchiveFileEntry afe) { }
        public void ViewFile(FileInfo file)
        {
            var filepath = file.FullName;
            var data = File.ReadAllBytes(filepath);
            switch (file.Extension)
            {
                default:
                    ViewHexFile(filepath, data);
                    break;
            }
        }

        public void ViewTextFile(string filepath, byte[] data)
        {
            var name = Path.GetFileName(filepath);
            var text = Encoding.UTF8.GetString(data);

            MessageBox.Show(text);
        }
        public void ViewHexFile(string filepath, byte[] data)
        {
            var hf = new HexEditorForm(this.Icon, filepath, data);
            hf.Show();
        }

        private void GoBack()
        {
            MainTreeView.SelectedNode = PreviousTreeNode;
        }

        //control functions
        private void MainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateMainListView();
        }
        private void MainTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            PreviousTreeNode = MainTreeView.SelectedNode;
        }
        private void TempBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void MainListView_ItemActivate(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count != 1) { return; }

            var si = MainListView.SelectedItems[0];

            ViewItem(si.Tag);
        }
        private void MainListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateMainStatusStrip();
        }
    }
}
