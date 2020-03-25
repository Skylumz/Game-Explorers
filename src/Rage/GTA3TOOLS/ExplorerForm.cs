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
        public ExplorerForm(GTAPATH gp)
        {
            if (gp == null) { return; }

            GtaPath = gp;
            if(GtaPath.HaveFolder())
            {
                InitializeComponent();
                InitExplorer();
            }
        }
        private void InitExplorer()
        {
            var di = new DirectoryInfo(GtaPath.FolderPath);
            var root = new TreeNode(di.Name);
            root.Tag = di;
            GetAllFolders(ref root, di, "*");
            treeView1.Nodes.Add(root);
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
            listView1.Items.Clear();

            var di = treeView1.SelectedNode.Tag as DirectoryInfo;

            foreach(var d in di.GetDirectories())
            {
                var lvi = new ListViewItem(d.Name);
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "File Folder"));
                lvi.Tag = d;
                listView1.Items.Add(lvi);
            }

            foreach(var f in di.GetFiles())
            {
                var lvi = new ListViewItem(f.Name);
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, f.Extension.TrimStart('.').ToUpper() + " FILE"));
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, "TODO"));
                lvi.Tag = f;
                listView1.Items.Add(lvi);
            }
        }
        private void SelectTreeNodeFromDirectory(DirectoryInfo di)
        {
            foreach(TreeNode n in treeView1.Nodes)
            {
                if(n.Tag == di)
                {
                    treeView1.SelectedNode = n;
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           UpdateMainListView();
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 1) { return; }

            var tag = listView1.SelectedItems[0].Tag;
            if (tag is DirectoryInfo)
            {
                SelectTreeNodeFromDirectory(tag as DirectoryInfo);
            }
        }
    }
}
