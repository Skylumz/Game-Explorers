using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO
//Multiline byte selection
//Display Caret Offset Position
//

namespace GameCore
{
    public partial class HexEditorForm : Form
    {
        private string FilePath { get; set; }
        private string FileName { get { return Path.GetFileName(FilePath); } }
        private byte[] Data { get; set; }

        public HexEditorForm(ExplorerForm ef, string fp, byte[] data)
        {
            InitializeComponent();

            Owner = ef;
            FilePath = fp;
            Data = data;

            InitForm();
            UpdateHexTextBox();
            UpdatePropertyGrid();
        }

        private void InitForm()
        {
            Icon = Owner.Icon;
            Text = "Hex Editor - Skylumz - " + FileName;
            HexTextBox.Font = new Font(FontFamily.GenericMonospace, HexTextBox.Font.Size); //makes everything aligned 
            BytePerLineComboBox.SelectedIndex = 0;
        }

        private void UpdateHexTextBox()
        {
            if (Data == null) { return; }

            HexTextBox.Text = HexUtilties.ByteArrayToString(Data, int.Parse(BytePerLineComboBox.Text));
        }
        
        private void UpdatePropertyGrid()
        {
            var selectedText = HexTextBox.SelectedText;

            var selectedBytes = HexUtilties.ConvertHexStringToByteArray(selectedText);

            if (selectedBytes == null) { selectedBytes = new byte[0]; }
            
            var hb = new HexUtilties.ByteObject(selectedBytes);
            SelectionPropertyGrid.SelectedObject = hb;
            AmountOfBytesSelectedStatusStripLabel.Text = "Length: " + hb.Bytes.Length.ToString();
        }

        private void HexTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (HexTextBox.SelectedText.Replace(" ", string.Empty).Length % 2 == 0) { UpdatePropertyGrid(); }
        }
        private void BytePerLineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHexTextBox();
        }
        
        //move to Utils/HexUtilties.cs?
        public static class HexUtilties
        {
            public class ByteObject
            {
                public byte[] Bytes { get; set; }
                public string Binary { get { try { return Convert.ToString(Bytes[0], 2).PadLeft(8, '0'); } catch { return "Invalid"; } } }
                public string Int8 { get { try { return Bytes[0].ToString(); } catch { return "Invalid"; } } } //
                public string Uint8 { get { try { return Bytes[0].ToString(); } catch { return "Invalid"; } } } //idk if this is the correct way to do this for unsigned or signed
                public string Int16 { get { try { return BitConverter.ToInt16(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Uint16 { get { try { return BitConverter.ToUInt16(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Int32 { get { try { return BitConverter.ToInt32(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Uint32 { get { try { return BitConverter.ToUInt32(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Int64 { get { try { return BitConverter.ToInt64(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Uint64 { get { try { return BitConverter.ToUInt64(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Float { get { try { return BitConverter.ToSingle(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string Double { get { try { return BitConverter.ToDouble(Bytes, 0).ToString(); } catch { return "Invalid"; } } }
                public string ASCII { get { try { return Encoding.ASCII.GetString(Bytes) == string.Empty ? "Invalid" : Encoding.ASCII.GetString(Bytes); } catch { return "Invalid"; } } }
                public string Unicode { get { try { return Encoding.Unicode.GetString(Bytes) == string.Empty ? "Invalid" : Encoding.Unicode.GetString(Bytes); } catch { return "Invalid"; } } }

                public ByteObject(byte[] bs)
                {
                    Bytes = bs;
                }
            }

            public static string ByteArrayToString(byte[] data, int bytePerLine = 16, bool withOffsets = true, bool withDecodedText = true)
            {
                StringBuilder sb = new StringBuilder();

                int lineCount = 0;
                if (withOffsets)
                {
                    string os = "Offset  ";
                    for (int i = 0; i < bytePerLine; i++)
                    {
                        os += " " + i.ToString("X2");
                    }

                    if (withDecodedText)
                    {
                        os += " Decoded Text";
                    }

                    sb.AppendLine(os);
                }

                StringBuilder hexStr = new StringBuilder();
                StringBuilder texStr = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    if (i % bytePerLine == 0)
                    {
                        hexStr.Append(texStr.ToString());
                        if (i != 0) { sb.AppendLine(hexStr.ToString()); }
                        hexStr.Clear();
                        texStr.Clear();
                        texStr.Append(" ");
                        if (withOffsets)
                        {
                            hexStr.Append(lineCount.ToString("X8"));
                            lineCount++;
                        }
                    }
                    hexStr.Append(" " + data[i].ToString("X2"));
                    if (withDecodedText)
                    {
                        //dont really like this
                        if (data[i] == 0) { texStr.Append("."); }
                        else
                        {
                            var cRes = Convert.ToChar(data[i]);
                            if (char.IsWhiteSpace(cRes)) { cRes = '.'; }
                            texStr.Append(cRes);
                        }
                    }
                }

                return sb.ToString();
            }

            //stolen: https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array/321404
            public static byte[] ConvertHexStringToByteArray(string inputString)
            {
                var hexString = inputString.Replace(" ", string.Empty);
                
                try
                {
                    byte[] retval = new byte[hexString.Length / 2];
                    for (int i = 0; i < hexString.Length; i += 2)
                        retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                    return retval;
                }
                catch
                {
                    return null;
                }
            }
        }

        
    }
}
