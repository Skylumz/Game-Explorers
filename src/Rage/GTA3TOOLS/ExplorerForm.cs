using GTA3TOOLS.Utils;
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
    public partial class ExplorerForm : Form
    {
        private GTAPATH GtaPath;

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
            var root = new TreeNode(di.Name);
            root.Tag = di;
            GetAllFolders(ref root, di, "*");
            root.Expand();
            MainTreeView.Nodes.Add(root);
        }
        
        private void GetAllFolders(ref TreeNode root, DirectoryInfo dir, string searchPattern)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                var folder = new TreeNode(d.Name);
                folder.Tag = d;
                root.Nodes.Add(folder);
                GetAllFolders(ref folder, d, searchPattern);
            }
        }
        
        private void UpdateMainListView()
        {
            MainListView.Items.Clear();

            var di = MainTreeView.SelectedNode.Tag as DirectoryInfo;

            foreach(var d in di.GetDirectories())
            {
                var lvi = new ListViewItem(d.Name);
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "File Folder"));
                lvi.Tag = d;
                MainListView.Items.Add(lvi);
            }

            foreach(var f in di.GetFiles())
            {
                var lvi = new ListViewItem(f.Name);
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, f.Extension.TrimStart('.').ToUpper() + " FILE"));
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "TODO"));
                lvi.Tag = f;
                MainListView.Items.Add(lvi);
            }

            UpdateMainStatusStrip();
        }
        
        private void UpdateMainStatusStrip()
        {
            var di = MainTreeView.SelectedNode.Tag as DirectoryInfo;
            AmountOfItemsInDirectoryLabel.Text = MainListView.Items.Count.ToString() + " Items";
            AmountOfItemsSelectedInListViewLabel.Text = MainListView.SelectedItems.Count.ToString() + " Items Selected";
        }

        private void SelectTreeNodeFromDirectory(DirectoryInfo di)
        {
            foreach(TreeNode n in MainTreeView.SelectedNode.Nodes)
            {
                var ndi = n.Tag as DirectoryInfo;
                if(ndi.FullName == di.FullName)
                {
                    MainTreeView.SelectedNode = n;
                    n.Expand();
                }
            }
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

            if (si.Tag is DirectoryInfo)
            {
                SelectTreeNodeFromDirectory(si.Tag as DirectoryInfo);
            }

        }
        private void MainListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateMainStatusStrip();
        }
    }
}
