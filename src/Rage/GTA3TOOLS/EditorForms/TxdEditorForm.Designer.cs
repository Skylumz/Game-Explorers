namespace GTA3TOOLS
{
    partial class TxdEditorForm
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
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.AmountOfTexturesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MainListView = new System.Windows.Forms.ListView();
            this.MainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SelectionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.MainStatusStrip.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.MainContextMenuStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AmountOfTexturesLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.MainStatusStrip.TabIndex = 0;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // AmountOfTexturesLabel
            // 
            this.AmountOfTexturesLabel.Name = "AmountOfTexturesLabel";
            this.AmountOfTexturesLabel.Size = new System.Drawing.Size(135, 17);
            this.AmountOfTexturesLabel.Text = "AmountOfTexturesLabel";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(800, 25);
            this.MainToolStrip.TabIndex = 2;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.MainTabControl);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.AutoScroll = true;
            this.MainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.MainSplitContainer.Panel2.Controls.Add(this.MainPictureBox);
            this.MainSplitContainer.Size = new System.Drawing.Size(800, 379);
            this.MainSplitContainer.SplitterDistance = 175;
            this.MainSplitContainer.TabIndex = 3;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.tabPage1);
            this.MainTabControl.Controls.Add(this.tabPage2);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(175, 379);
            this.MainTabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MainListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(167, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Textures";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainListView
            // 
            this.MainListView.ContextMenuStrip = this.MainContextMenuStrip;
            this.MainListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainListView.HideSelection = false;
            this.MainListView.Location = new System.Drawing.Point(3, 3);
            this.MainListView.MultiSelect = false;
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(161, 347);
            this.MainListView.TabIndex = 0;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.List;
            this.MainListView.SelectedIndexChanged += new System.EventHandler(this.MainListView_SelectedIndexChanged);
            // 
            // MainContextMenuStrip
            // 
            this.MainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractTextureToolStripMenuItem});
            this.MainContextMenuStrip.Name = "contextMenuStrip1";
            this.MainContextMenuStrip.Size = new System.Drawing.Size(152, 26);
            // 
            // extractTextureToolStripMenuItem
            // 
            this.extractTextureToolStripMenuItem.Name = "extractTextureToolStripMenuItem";
            this.extractTextureToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.extractTextureToolStripMenuItem.Text = "Extract Texture";
            this.extractTextureToolStripMenuItem.Click += new System.EventHandler(this.extractTextureToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SelectionPropertyGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(167, 353);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Properties";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SelectionPropertyGrid
            // 
            this.SelectionPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectionPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.SelectionPropertyGrid.Name = "SelectionPropertyGrid";
            this.SelectionPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.SelectionPropertyGrid.Size = new System.Drawing.Size(161, 347);
            this.SelectionPropertyGrid.TabIndex = 0;
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.Location = new System.Drawing.Point(127, 62);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(336, 253);
            this.MainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MainPictureBox.TabIndex = 0;
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseDown);
            this.MainPictureBox.MouseEnter += new System.EventHandler(this.MainPictureBox_MouseEnter);
            this.MainPictureBox.MouseLeave += new System.EventHandler(this.MainPictureBox_MouseLeave);
            this.MainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseMove);
            this.MainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseUp);
            // 
            // TxdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "TxdEditorForm";
            this.Text = "TxdEditorForm";
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.MainContextMenuStrip.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.PropertyGrid SelectionPropertyGrid;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel AmountOfTexturesLabel;
        private System.Windows.Forms.ContextMenuStrip MainContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem extractTextureToolStripMenuItem;
    }
}