namespace GameCore
{
    partial class HexEditorForm
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
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.AmountOfBytesSelectedStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.BytePerLineComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.HexTextBox = new System.Windows.Forms.RichTextBox();
            this.SelectionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MainStatusStrip.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AmountOfBytesSelectedStatusStripLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.MainStatusStrip.TabIndex = 0;
            this.MainStatusStrip.Text = "MainStatusStrip";
            // 
            // AmountOfBytesSelectedStatusStripLabel
            // 
            this.AmountOfBytesSelectedStatusStripLabel.Name = "AmountOfBytesSelectedStatusStripLabel";
            this.AmountOfBytesSelectedStatusStripLabel.Size = new System.Drawing.Size(136, 17);
            this.AmountOfBytesSelectedStatusStripLabel.Text = "AmountOfBytesSelected";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MainMenuStrip.TabIndex = 3;
            this.MainMenuStrip.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BytePerLineComboBox});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(800, 25);
            this.MainToolStrip.TabIndex = 4;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // BytePerLineComboBox
            // 
            this.BytePerLineComboBox.Items.AddRange(new object[] {
            "16",
            "32",
            "64"});
            this.BytePerLineComboBox.Name = "BytePerLineComboBox";
            this.BytePerLineComboBox.Size = new System.Drawing.Size(121, 25);
            this.BytePerLineComboBox.SelectedIndexChanged += new System.EventHandler(this.BytePerLineComboBox_SelectedIndexChanged);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.HexTextBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.SelectionPropertyGrid);
            this.MainSplitContainer.Size = new System.Drawing.Size(800, 379);
            this.MainSplitContainer.SplitterDistance = 555;
            this.MainSplitContainer.TabIndex = 5;
            // 
            // HexTextBox
            // 
            this.HexTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HexTextBox.Location = new System.Drawing.Point(0, 0);
            this.HexTextBox.Name = "HexTextBox";
            this.HexTextBox.Size = new System.Drawing.Size(555, 379);
            this.HexTextBox.TabIndex = 0;
            this.HexTextBox.Text = "";
            this.HexTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HexTextBox_MouseUp);
            // 
            // SelectionPropertyGrid
            // 
            this.SelectionPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectionPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.SelectionPropertyGrid.Name = "SelectionPropertyGrid";
            this.SelectionPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.SelectionPropertyGrid.Size = new System.Drawing.Size(241, 379);
            this.SelectionPropertyGrid.TabIndex = 0;
            // 
            // HexEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "HexEditorForm";
            this.Text = "HexEditorForm";
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel AmountOfBytesSelectedStatusStripLabel;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripComboBox BytePerLineComboBox;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.RichTextBox HexTextBox;
        private System.Windows.Forms.PropertyGrid SelectionPropertyGrid;
    }
}