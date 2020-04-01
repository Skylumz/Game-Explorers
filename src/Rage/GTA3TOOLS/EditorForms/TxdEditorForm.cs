using RageCore.Common.Winforms;
using RageCore.GTA3.GameFiles;
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
using RageCore.RenderWare.Sections;
//TODO
//UI WORK - toolstrip and shit
//Import texture
//texture mip maps

namespace GTA3TOOLS
{
    public partial class TxdEditorForm : Form
    {
        private string FilePath { get; set; }
        private string FileName { get { return Path.GetFileName(FilePath); } }
        private byte[] Data { get; set; } //unnecessary? keeping to match other edit forms for now

        private TxdFile Txd { get; set; }
        private string CurrentTextureName { get { return MainListView.SelectedItems[0].Text; } }

        //zoom, pan
        private new Point MouseDown;
        private bool MouseIsHovering = false;
        private bool CanZoom = true;

        public TxdEditorForm(ExplorerForm ef, string fp, byte[] data)
        {
            InitializeComponent();

            Owner = ef;
            FilePath = fp;
            Data = data;

            Txd = new TxdFile(fp);
            Txd.Load(data);

            InitForm();
            UpdateSelectionPropertyGrid();
            UpdatePictureBox();
        }

        private void InitForm()
        {
            Icon = Owner.Icon;
            Text = "Txd Editor - Skylumz -" + FileName;
            foreach(var texture in Txd.TextureDictionary.Textures)
            {
                MainListView.Items.Add(ListViewItemFromGtaTexture(texture.Key, texture.Value));
            }
            MainListView.Items[0].Selected = true;

            SelectionPropertyGrid.SelectedObject = Txd;
            AmountOfTexturesLabel.Text = Txd.TextureDictionary.TextureCount == 1 ? Txd.TextureDictionary.TextureCount.ToString() + " Texture" : Txd.TextureDictionary.TextureCount.ToString() + " Textures";
        }

        private ListViewItem ListViewItemFromGtaTexture(string name, Bitmap texture)
        {
            var lvi = new ListViewItem(name);
            lvi.Tag = texture;
            return lvi;
        }
        
        private void UpdatePictureBox()
        {
            var items = MainListView.SelectedItems;
            if(items.Count > 1 || items.Count == 0) { return; }
            var texture = items[0].Tag as Bitmap;

            MainPictureBox.Image = texture;
        }

        //debate
        private void UpdateSelectionPropertyGrid()
        {
        }

        private void ExtractTexture()
        {
            var image = MainPictureBox.Image;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                var bmp = new Bitmap(image);
                bmp.Save(fbd.SelectedPath + "\\" + CurrentTextureName + ".bmp");
            }
        }

        public void Pan(MouseEventArgs mouse)
        {
            if (mouse.Button == MouseButtons.Left)
            {
                Point mousePosNow = mouse.Location;

                int deltaX = mousePosNow.X - MouseDown.X;
                int deltaY = mousePosNow.Y - MouseDown.Y;

                int newX = MainPictureBox.Location.X + deltaX;
                int newY = MainPictureBox.Location.Y + deltaY;

                MainPictureBox.Location = new Point(newX, newY);
            }
        }
        public void Zoom(MouseEventArgs mouse)
        {
            if (MouseIsHovering == true)
            {
                int newWidth = MainPictureBox.Image.Width;
                int newHeight = MainPictureBox.Image.Height;
                int newX = MainPictureBox.Location.X;
                int newY = MainPictureBox.Location.Y;

                if (mouse.Delta > 0f)
                {
                    newWidth = MainPictureBox.Size.Width + (MainPictureBox.Size.Width / 10);
                    newHeight = MainPictureBox.Size.Height + (MainPictureBox.Size.Height / 10);
                    newX = MainPictureBox.Location.X - ((MainPictureBox.Size.Width / 10) / 2);
                    newY = MainPictureBox.Location.Y - ((MainPictureBox.Size.Height / 10) / 2);
                }

                else if (mouse.Delta < 0f)
                {
                    newWidth = MainPictureBox.Size.Width - (MainPictureBox.Size.Width / 10);
                    newHeight = MainPictureBox.Size.Height - (MainPictureBox.Size.Height / 10);
                    newX = MainPictureBox.Location.X + ((MainPictureBox.Size.Width / 10) / 2);
                    newY = MainPictureBox.Location.Y + ((MainPictureBox.Size.Height / 10) / 2);
                }

                MainPictureBox.Size = new Size(newWidth, newHeight);
                MainPictureBox.Location = new Point(newX, newY);
            }
        }
        private void UpdateZoom()
        {
            MainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MainPictureBox.Width = MainPictureBox.Width - 2;
            MainPictureBox.Height = MainPictureBox.Height - 2;
            MainPictureBox.Location = MainPictureBox.Location;
        }

        private void MainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePictureBox();
            UpdateSelectionPropertyGrid();
        }
        private void extractTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractTexture();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if(CanZoom == true)
            {
                Zoom(e);
            }
        }
        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Pan(e);
        }
        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDown = e.Location;
                CanZoom = false;
                Cursor.Current = Cursors.SizeAll;
            }
        }
        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            CanZoom = true;

            Cursor.Current = Cursors.Arrow;
        }
        private void MainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            MouseIsHovering = false;
        }
        private void MainPictureBox_MouseEnter(object sender, EventArgs e)
        {
            MouseIsHovering = true;
        }

        
    }
}
