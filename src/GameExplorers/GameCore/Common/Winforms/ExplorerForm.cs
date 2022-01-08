using GameCore.RenderWare.Sections;
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

namespace GameCore
{
    public interface IExplorerForm
    {
        void InitFileTypeImageDict();
        ArchiveFile LoadArchive(string filepath);
        void DisplayArchive(ArchiveFile arch);
        void ViewFile(ArchiveFileEntry afe, bool hex = false);
        void SearchArchive(ArchiveFile af, string searchString, ref List<ListViewItem> searchItems);
        void ExtractArchiveFile(ArchiveFileEntry afe);
    }

    public partial class ExplorerForm : Form, IExplorerForm
    {
        public GAMEPATH GtaPath;
        private List<TreeNode> PreviousTreeNodes;

        public string ArchiveFileExtension;
        public Dictionary<string, int> FileTypeImageIndexDict;

        public ExplorerForm(GAMEPATH gp)
        {
            if (gp == null) { return; }

            GtaPath = gp;
            if (GtaPath.HaveFolder())
            {
                InitializeComponent();
                
                InitExplorer();
            }
        }

        private void InitExplorer()
        {
            MainTreeView.Nodes.Clear();
            MainListView.Items.Clear();

            InitFileTypeImageDict();
            PreviousTreeNodes = new List<TreeNode>();
            this.Icon = Icon.ExtractAssociatedIcon(GtaPath.ExePath);
            FileImageList.Images.Add(this.Icon);

            var di = new DirectoryInfo(GtaPath.FolderPath);
            var root = new TreeNode(di.Name);
            root.SelectedImageIndex = FileTypeImageIndexDict["exeicon"];
            root.Tag = di;
            GetAllFolders(ref root, di, "*");
            root.Expand();
            MainTreeView.Nodes.Add(root);
            MainTreeView.SelectedNode = root;
        }

        public virtual void InitFileTypeImageDict()
        {
            FileTypeImageIndexDict = new Dictionary<string, int>()
            {
                { "default", 607 },
                { "folder", 422 },
                { "archive", 428 },
                { "xml", 610 },
                { "dll", 617 },
                { "exe", 617 },
                { "asi", 617 },
                { "html", 933 },
                { "url", 933 },
                { "txt", 674 },
                { "dat", 674 },
                { "cfg", 674 },
                { "ini", 674 },
                { "bmp", 704 },
                { "ico", 704 },
                { "png", 704 },
                { "jpg", 704 },
                { "jpeg", 674 },
                { "exeicon", FileImageList.Images.Count - 1 }
            };
        }

        private void GetAllFolders(ref TreeNode root, DirectoryInfo dir, string searchPattern)
        {
            foreach (FileInfo f in dir.GetFiles())
            {
                if (f.Extension == ArchiveFileExtension)
                {
                    var afolder = new TreeNode(f.Name, FileTypeImageIndexDict["archive"], FileTypeImageIndexDict["archive"]);
                    var archiveFile = LoadArchive(f.FullName);
                    afolder.Tag = archiveFile;
                    root.Nodes.Add(afolder);
                }
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                var folder = new TreeNode(d.Name, FileTypeImageIndexDict["folder"], FileTypeImageIndexDict["folder"]);
                folder.Tag = d;
                root.Nodes.Add(folder);
                GetAllFolders(ref folder, d, searchPattern);
            }
        }

        public int GetImageIndex(string extension)
        {
            var ext = extension.Replace(".", string.Empty).ToLower();
            foreach(var key in FileTypeImageIndexDict.Keys)
            {
                if(key == ext)
                {
                    return FileTypeImageIndexDict[key];
                }
            }
            return FileTypeImageIndexDict["default"];
        }

        private ListViewItem ListViewItemFromFile(FileInfo f)
        {
            var lvi = new ListViewItem(f.Name, GetImageIndex(f.Extension));
            if(f.Name == GtaPath.key) { lvi.ImageIndex = FileTypeImageIndexDict["exeicon"]; }
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, f.Extension.TrimStart('.').ToUpper() + " FILE"));
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "TODO"));
            object tag = f;
            if (f.Extension == ArchiveFileExtension)
            {
                tag = LoadArchive(f.FullName);
            }
            lvi.Tag = tag;
            return lvi;
        }

        private ListViewItem ListViewItemFromDirectory(DirectoryInfo d)
        {
            var lvi = new ListViewItem(d.Name, FileTypeImageIndexDict["folder"]);
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "File Folder"));
            lvi.Tag = d;
            return lvi;
        }

        private void UpdateMainListView()
        {
            MainListView.Items.Clear();

            var t = MainTreeView.SelectedNode.Tag;

            if (t is DirectoryInfo)
            {
                var di = t as DirectoryInfo;

                foreach (var d in di.GetDirectories())
                {
                    MainListView.Items.Add(ListViewItemFromDirectory(d));
                }

                foreach (var f in di.GetFiles())
                {
                    MainListView.Items.Add(ListViewItemFromFile(f));
                }
            }
            else if (t is ArchiveFile)
            {
                DisplayArchive(t as ArchiveFile);
            }

            PathTextBox.Text = GetCurrentPath();
            SearchTextBox.Text = string.Empty;
            UpdateMainStatusStrip();
        }

        private string GetCurrentPath()
        {
            var tag = MainTreeView.SelectedNode.Tag;
            if (tag is DirectoryInfo)
            {
                var di = tag as DirectoryInfo;
                return di.FullName;
            }
            else
            {
                var arch = tag as ArchiveFile;
                return arch.FilePath;
            }
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

        public virtual void ViewFile(ArchiveFileEntry afe, bool hex = false) { }
        public void ViewFile(FileInfo file)
        {
            var filepath = file.FullName;
            var data = File.ReadAllBytes(filepath);

            switch (file.Extension.Replace(".", string.Empty).ToLower())
            {
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
                case "txd":
                    ViewTxdFile(filepath, data);
                    break;
                case "ide":
                    ViewTextFile(filepath, data);
                    break;
                case "ipl":
                    ViewTextFile(filepath, data);
                    break;
                case "html":
                    System.Diagnostics.Process.Start(filepath);
                    break;
                case "url":
                    System.Diagnostics.Process.Start(filepath);
                    break; 
                default:
                    ViewHexFile(filepath, data);
                    break;
            }
        }

        public virtual void ViewTxdFile(string filepath, byte[] data) { }
        public void ViewXmlFile(string filepath, byte[] data)
        {
            var tf = new TextEditorForm(this, filepath, data, true);
            tf.Show();
        }
        public void ViewTextFile(string filepath, byte[] data)
        {
            var tf = new TextEditorForm(this, filepath, data);
            tf.Show();
        }
        public void ViewHexFile(string filepath, byte[] data)
        {
            var hf = new HexEditorForm(this, filepath, data);
            hf.Show();
        }

        private void Search()
        {
            var selectedTag = MainTreeView.SelectedNode.Tag;
            var searchItems = new List<ListViewItem>();
            var searchString = SearchTextBox.Text.ToLower();
            if(selectedTag is DirectoryInfo)
            {
                var dir = selectedTag as DirectoryInfo;
                SearchDirectoryInfo(dir, searchString, ref searchItems);
            }
            else
            {
                var arch = selectedTag as ArchiveFile;
                SearchArchive(arch, searchString, ref searchItems);
            }

            if(searchItems.Count >= 1)
            {
                MainListView.Items.Clear();
                foreach(var lvi in searchItems)
                {
                    MainListView.Items.Add(lvi);
                }
                PathTextBox.Text = "Search Items";
            }
            else
            {
                MessageBox.Show("Nothing found!");
            }
        }
        public virtual void SearchArchive(ArchiveFile af, string searchString, ref List<ListViewItem> searchItems) { }
        private void SearchDirectoryInfo(DirectoryInfo SearchDI, string searchString, ref List<ListViewItem> searchItems)
        {
            foreach(var file in SearchDI.GetFiles())
            {
                if (file.Name.ToLower().Contains(searchString))
                {
                    searchItems.Add(ListViewItemFromFile(file));
                }
                if(file.Name.Contains(ArchiveFileExtension))
                {
                    SearchArchive(LoadArchive(file.FullName), searchString, ref searchItems);
                }
            }

            foreach (var dir in SearchDI.GetDirectories())
            {
                SearchDirectoryInfo(dir, searchString, ref searchItems);
            }
        }

        private void GoBack()
        {
            if(PreviousTreeNodes.Count == 0) { return; }

            // DO NOT UNDERSTAND THIS (remove twice)
            try
            {
                MainTreeView.SelectedNode = PreviousTreeNodes[PreviousTreeNodes.Count - 1];
                PreviousTreeNodes.RemoveAt(PreviousTreeNodes.Count - 1);
                PreviousTreeNodes.RemoveAt(PreviousTreeNodes.Count - 1);
            }
            catch { }
        }

        public virtual void ExtractArchiveFile(ArchiveFileEntry afe) { }
        private void ExtractFile(FileInfo fi)
        {
            var data = File.ReadAllBytes(fi.FullName);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(fbd.SelectedPath + "\\" + fi.Name, data);
            }
        }
        private void Extract()
        {
            var items = MainListView.SelectedItems;
            if (items.Count == 0) { return; }
            else if (items.Count > 1) { throw new NotImplementedException("Multiple file extractions not possible yet"); }
            else
            {
                var item = items[0].Tag;
                if(item is DirectoryInfo) { throw new NotImplementedException("Cannot extract directorys yet!"); }
                if(item is FileInfo) { ExtractFile(item as FileInfo); }
                if(item is ArchiveFile) { ArchiveFile af = item as ArchiveFile; FileInfo fi = new FileInfo(af.FilePath); ExtractFile(fi); }
                if(item is ArchiveFileEntry) { ExtractArchiveFile(item as ArchiveFileEntry); }
            }
        }

        private void MainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateMainListView();
        }
        private void MainTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            PreviousTreeNodes.Add(MainTreeView.SelectedNode);
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
        
        private void GoBackButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            InitExplorer();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Extract();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var items = MainListView.SelectedItems;
            if(items.Count == 0) { return; }
            if(items.Count > 1) { return; }
            var item = items[0].Tag;
            if(item is DirectoryInfo) { return; }
            if(item is FileInfo) { var fi = item as FileInfo; ViewHexFile(fi.FullName, File.ReadAllBytes(fi.FullName)); }
            if(item is ArchiveFileEntry) { var afe = item as ArchiveFileEntry; ViewFile(afe, true); }
        }
    }
}
