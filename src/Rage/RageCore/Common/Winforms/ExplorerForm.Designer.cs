namespace RageCore.Common.Winforms
{
    partial class ExplorerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.AmountOfItemsInDirectoryLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AmountOfItemsSelectedInListViewLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MainTreeView = new System.Windows.Forms.TreeView();
            this.FileImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainListView = new System.Windows.Forms.ListView();
            this.FileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SearchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.GoBackButton = new System.Windows.Forms.ToolStripButton();
            this.PathTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.MainMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AmountOfItemsInDirectoryLabel,
            this.AmountOfItemsSelectedInListViewLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.MainStatusStrip.TabIndex = 1;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // AmountOfItemsInDirectoryLabel
            // 
            this.AmountOfItemsInDirectoryLabel.Name = "AmountOfItemsInDirectoryLabel";
            this.AmountOfItemsInDirectoryLabel.Size = new System.Drawing.Size(93, 17);
            this.AmountOfItemsInDirectoryLabel.Text = "AmountOfItems";
            // 
            // AmountOfItemsSelectedInListViewLabel
            // 
            this.AmountOfItemsSelectedInListViewLabel.Name = "AmountOfItemsSelectedInListViewLabel";
            this.AmountOfItemsSelectedInListViewLabel.Size = new System.Drawing.Size(137, 17);
            this.AmountOfItemsSelectedInListViewLabel.Text = "AmountOfItemsSelected";
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.MainTreeView);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.MainListView);
            this.MainSplitContainer.Size = new System.Drawing.Size(800, 379);
            this.MainSplitContainer.SplitterDistance = 266;
            this.MainSplitContainer.TabIndex = 2;
            // 
            // MainTreeView
            // 
            this.MainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTreeView.ImageIndex = 0;
            this.MainTreeView.ImageList = this.FileImageList;
            this.MainTreeView.Location = new System.Drawing.Point(0, 0);
            this.MainTreeView.Name = "MainTreeView";
            this.MainTreeView.SelectedImageIndex = 0;
            this.MainTreeView.Size = new System.Drawing.Size(266, 379);
            this.MainTreeView.TabIndex = 0;
            this.MainTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.MainTreeView_BeforeSelect);
            this.MainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MainTreeView_AfterSelect);
            // 
            // FileImageList
            // 
            this.FileImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileImageList.ImageStream")));
            this.FileImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.FileImageList.Images.SetKeyName(0, "folder.png");
            this.FileImageList.Images.SetKeyName(1, "file-10.png");
            this.FileImageList.Images.SetKeyName(2, "folder-2.png");
            // 
            // MainListView
            // 
            this.MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameColumnHeader,
            this.TypeColumnHeader,
            this.SizeColumnHeader});
            this.MainListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainListView.HideSelection = false;
            this.MainListView.Location = new System.Drawing.Point(0, 0);
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(530, 379);
            this.MainListView.SmallImageList = this.FileImageList;
            this.MainListView.TabIndex = 0;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.Details;
            this.MainListView.ItemActivate += new System.EventHandler(this.MainListView_ItemActivate);
            this.MainListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.MainListView_ItemSelectionChanged);
            // 
            // FileNameColumnHeader
            // 
            this.FileNameColumnHeader.Text = "Name";
            this.FileNameColumnHeader.Width = 103;
            // 
            // TypeColumnHeader
            // 
            this.TypeColumnHeader.Text = "Type";
            this.TypeColumnHeader.Width = 103;
            // 
            // SizeColumnHeader
            // 
            this.SizeColumnHeader.Text = "Size";
            this.SizeColumnHeader.Width = 106;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GoBackButton,
            this.toolStripSeparator1,
            this.PathTextBox,
            this.RefreshButton,
            this.toolStripSeparator2,
            this.SearchTextBox,
            this.SearchButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 25);
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // SearchButton
            // 
            this.SearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchButton.Image")));
            this.SearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(23, 22);
            this.SearchButton.Text = "toolStripButton1";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // GoBackButton
            // 
            this.GoBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GoBackButton.Image = ((System.Drawing.Image)(resources.GetObject("GoBackButton.Image")));
            this.GoBackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(23, 22);
            this.GoBackButton.Text = "toolStripButton1";
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(575, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "toolStripButton1";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "ExplorerForm";
            this.Text = "Explorer  - Skylumz";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TreeView MainTreeView;
        private System.Windows.Forms.ColumnHeader FileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader TypeColumnHeader;
        private System.Windows.Forms.ColumnHeader SizeColumnHeader;
        private System.Windows.Forms.ToolStripStatusLabel AmountOfItemsInDirectoryLabel;
        private System.Windows.Forms.ToolStripStatusLabel AmountOfItemsSelectedInListViewLabel;
        private System.Windows.Forms.ImageList FileImageList;
        public System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox SearchTextBox;
        private System.Windows.Forms.ToolStripButton SearchButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton GoBackButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripTextBox PathTextBox;
    }
}