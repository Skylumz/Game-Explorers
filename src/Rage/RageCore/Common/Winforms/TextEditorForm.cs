using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RageCore.Common.Winforms
{
    public partial class TextEditorForm : Form
    {
        private string FilePath { get; set; }
        private string FileName { get { return Path.GetFileName(FilePath); } }
        private byte[] Data { get; set; }
        private bool IsXml { get; set; }

        private Color NodeColor = Color.Brown;
        private Color CommentColor = Color.Green;
        private Color AttributeColor = Color.Red;
        private Color StringColor = Color.Blue;
        private Color InnerTextColor = Color.Black;

        public TextEditorForm(ExplorerForm ef, string fp, byte[] data, bool isxml = false)
        {
            InitializeComponent();

            Owner = ef;
            FilePath = fp;
            Data = data;
            IsXml = isxml;

            InitForm();
            UpdateTextTextBox();
        }

        private void InitForm()
        {
            Icon = Owner.Icon;
            Text = "Text Editor - Skylumz - " + FileName;
        }

        private void UpdateTextTextBox()
        {
            TextTextBox.Text = Encoding.UTF8.GetString(Data);
            UpdateSyntaxHighlighting();
        }

        //needs work with nested nodes
        //also super slow lol
        private void UpdateSyntaxHighlighting()
        {
            if (IsXml)
            {
                string nodes = "<.+?>|\t<.+?>";
                MatchCollection nodeMatches = Regex.Matches(TextTextBox.Text, nodes);
                string comments = "<!--.+?-->";
                MatchCollection commentMatches = Regex.Matches(TextTextBox.Text, comments);
                string attributeNames = " .+?=\"" ;
                MatchCollection attributeMatches = Regex.Matches(TextTextBox.Text, attributeNames);
                string strings = "\".+?\"";
                MatchCollection stringMatches = Regex.Matches(TextTextBox.Text, strings);
                
                int originalIndex = TextTextBox.SelectionStart;
                int originalLength = TextTextBox.SelectionLength;
                Color originalColor = Color.Black;
                MainMenuStrip.Focus();
                TextTextBox.SelectionStart = 0;
                TextTextBox.SelectionLength = TextTextBox.Text.Length;
                TextTextBox.SelectionColor = originalColor;

                if(nodeMatches != null)
                {
                    foreach (Match m in nodeMatches)
                    {
                        TextTextBox.SelectionStart = m.Index;
                        TextTextBox.SelectionLength = m.Length;
                        TextTextBox.SelectionColor = NodeColor;
                    }
                }
                
                if(attributeMatches != null)
                {
                    foreach (Match m in attributeMatches)
                    {
                        TextTextBox.SelectionStart = m.Index;
                        TextTextBox.SelectionLength = m.Length;
                        TextTextBox.SelectionColor = AttributeColor;
                    }
                }
                
                if (stringMatches != null)
                {
                    foreach (Match m in stringMatches)
                    {
                        TextTextBox.SelectionStart = m.Index;
                        TextTextBox.SelectionLength = m.Length;
                        TextTextBox.SelectionColor = StringColor;
                    }
                }
                
                if (commentMatches != null)
                {
                    foreach (Match m in commentMatches)
                    {
                        TextTextBox.SelectionStart = m.Index;
                        TextTextBox.SelectionLength = m.Length;
                        TextTextBox.SelectionColor = CommentColor;
                    }
                }
                

                TextTextBox.SelectionStart = originalIndex;
                TextTextBox.SelectionLength = originalLength;
                TextTextBox.SelectionColor = originalColor;
                TextTextBox.Focus();
            }
        }

        private void TextTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSyntaxHighlighting();
        }
    }
}
