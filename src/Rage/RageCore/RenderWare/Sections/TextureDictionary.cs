using RageCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RageCore.RenderWare.Sections
{
    public class TextureDictionary : RenderWareSection
    {
        public TextureDictionaryStruct Struct { get; set; }
        public List<TextureNative> TextureNatives { get; set; }
        public Extension Extension { get; set; }

        public int TextureCount { get { return Struct.TextureCount; } }
        public Dictionary<string, Bitmap> Textures { get { return GetAllTextures(); } }

        public override void Read(BinaryReader br)
        {
            base.Read(br);

            Struct = new TextureDictionaryStruct();
            Struct.Read(br);

            TextureNatives = new List<TextureNative>();
            for (int i = 0; i < TextureCount; i++)
            {
                var tn = new TextureNative();
                tn.Read(br);
                TextureNatives.Add(tn);
            }

            Extension = new Extension();
            Extension.Read(br);
        }

        private Dictionary<string, Bitmap> GetAllTextures()
        {
            Dictionary<string, Bitmap> texs = new Dictionary<string, Bitmap>();
            foreach(var t in TextureNatives)
            {
                var tex = t.Struct.GetImage();
                texs[t.Struct.DiffuseTextureName] = tex;
            }
            return texs;
        }
    }

    public class TextureDictionaryStruct : RenderWareSection
    {
        public short TextureCount { get; set; }
        public short Unk1 { get; set; }

        public override void Read(BinaryReader br)
        {
            base.Read(br);

            TextureCount = br.ReadInt16();
            Unk1 = br.ReadInt16();
        }
    }
    
    public class TextureNative : RenderWareSection
    {
        public TextureNativeStruct Struct { get; set; }
        public Extension Extension { get; set; }

        public override void Read(BinaryReader br)
        {
            base.Read(br);

            Struct = new TextureNativeStruct();
            Struct.Read(br);

            Extension = new Extension();
            Extension.Read(br);
        }
    }

    public class TextureNativeStruct : RenderWareSection
    {
        public int PlatformID { get; set; } // 8 = Gta 3, Gta VC // 9 = Gta SA // PS2\0 = Ps2 Gta 3, VC, SA // 5 = Xbox Gta 3, VC, SA
        public TextureFilterMode FilterMode { get; set; }
        public TextureAdressingMode AddressMode { get; set; }
        public short Pad { get; set; }
        public string DiffuseTextureName { get; set; }
        public string AlphaTextureName { get; set; }
        public RasterFormat RasterFormat { get; set; }
        public bool UseAlpha { get; set; }
        public short TextureWidth { get; set; }
        public short TextureHeight { get; set; }
        public byte BitDepth { get; set; }
        public byte MipMapCount { get; set; }
        public RasterFormat RasterType { get; set; }
            
        //3, VC
        public bool Compression { get; set; }

        //SA?
        //public byte Alpha { get; set; }
        //public byte CubeTexture { get; set; }
        //public byte AutoMipMaps { get; set; }
        //public byte Compressed { get; set; }
        //public byte Pad { get; set; }

        public List<Color[]> MipMaps { get; set; }

        public Color[] Palette { get; set; }
        public bool UsePalette { get; set; }
        public Bitmap PaletteBitmap { get; set; }
        public int PaletteDataSize { get; set; }
        
        public override void Read(BinaryReader br)
        {
            base.Read(br);

            var startPos = br.BaseStream.Position;

            PlatformID = br.ReadInt32();
            
            switch (PlatformID)
            {
                case 5:
                    throw new NotImplementedException("Cannot read this txd file!");
                case 8:
                    ReadGta3VCFormat(br);
                    break;
                case 9:
                    ReadGtaSAFormat(br);
                    break;
                default:
                    break;
            }

            //have to do this until extensions are implemented
            if (br.BaseStream.Position != startPos + Size) 
            {
                MessageBox.Show("Cur Pos: " + br.BaseStream.Position.ToString() + " Should Pos: " + (startPos + Size).ToString()); //missing 4 bytes?
                br.BaseStream.Position = startPos + Size; 
            }
        } 

        private void ReadGta3VCFormat(BinaryReader br)
        {
            FilterMode = (TextureFilterMode)br.ReadByte();
            AddressMode = (TextureAdressingMode)br.ReadByte();
            Pad = br.ReadInt16();
            DiffuseTextureName = BinaryReaderUtilities.ReadNullTerminatedString(br, 32);
            AlphaTextureName = BinaryReaderUtilities.ReadNullTerminatedString(br, 32);
            RasterFormat = (RasterFormat)br.ReadInt32();
            UseAlpha = br.ReadInt32() == 1 ? true : false;

            TextureWidth = br.ReadInt16();
            TextureHeight = br.ReadInt16();
            BitDepth = br.ReadByte();
            MipMapCount = br.ReadByte();
            RasterType = (RasterFormat)br.ReadByte();
            Compression = br.ReadByte() == 1 ? true : false;

            int paletteSize = BitDepth == 8 ? 256 : 0; //gotta fix this

            if (paletteSize != 0)
            {
                UsePalette = true;
                //would like to create the bit map in GetImage();
                Palette = ReadPalette(br);
                PaletteBitmap = new Bitmap(TextureWidth, TextureHeight);
                for (int i = 0; i < TextureWidth; i++)
                {
                    for (int j = 0; j < TextureHeight; j++)
                    {
                        var idx = br.ReadByte();

                        PaletteBitmap.SetPixel(i, j, Palette[idx]);
                    }
                }
                PaletteBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                PaletteDataSize = br.ReadInt32();
            }
            else
            {
                MipMaps = new List<Color[]>();

                for (int i = 0; i < MipMapCount; i++)
                {
                    var size = br.ReadInt32();
                    var data = br.ReadBytes(size);
                    List<byte[]> dataclrs = new List<byte[]>();

                    for (int j = 4; j < data.Length; j++)
                    {
                        if (j % 4 == 0)
                        {
                            dataclrs.Add(new byte[] { data[j - 4], data[j - 3], data[j - 2], data[j - 1] });
                        }
                    }

                    //add last color
                    dataclrs.Add(new byte[] { data[data.Length - 4], data[data.Length - 3], data[data.Length - 2], data[data.Length - 1] });

                    var palette = new Color[size / 4];

                    for (int j = 0; j < size / 4; j++)
                    {
                        var byteA = dataclrs[j];
                        //if (UseAlpha) { palette[j] = Color.FromArgb(byteA[3], byteA[0], byteA[1], byteA[2]); }
                        //else { palette[j] = Color.FromArgb(255, byteA[0], byteA[1], byteA[2]); }
                        palette[j] = Color.FromArgb(255, byteA[0], byteA[1], byteA[2]);
                    }

                    MipMaps.Add(palette);
                }
            }
        }

        private Color[] ReadPalette(BinaryReader br)
        {
            Color[] palette = new Color[256];
            for (int i = 0; i != 256; ++i)
            {
                var tmp = new byte[4];
                for (int j = 0; j != 4; ++j)
                    tmp[j] = br.ReadByte();
                palette[i] = Color.FromArgb(tmp[3], tmp[0], tmp[1], tmp[2]);
            }
            return palette;
        }

        private void ReadGtaSAFormat(BinaryReader br) { throw new NotImplementedException(); }

        public Bitmap GetImage(int mipMapLevel = 0)
        {
            var bmp = new Bitmap(TextureWidth, TextureHeight);
            if (UsePalette)
            {
                return PaletteBitmap;
            }
            else
            {
                var level = MipMaps[mipMapLevel];

                int idx = 0;
                for (int i = 0; i < TextureWidth; ++i)
                {
                    for (int j = 0; j < TextureHeight; ++j)
                    {
                        bmp.SetPixel(i, j, level[idx]);
                        idx++;
                    }
                }
            }
            return bmp;
        }
    }

    public enum TextureFilterMode
    {
        NONE = 0,
        NEAREST = 1,
        LINEAR = 2,
        MIP_NEAREST = 3,
        MIP_LINEAR = 4,
        LINEAR_MIP_NEAREST = 5,
        LINEAR_MIP_LINEAR = 6
    }

    public enum TextureAdressingMode
    {
        NONE = 0,
        WRAP = 1, 
        MIRROR = 2,
        CLAMP = 3
    }

    public enum RasterFormat
    {
        FORMAT_DEFAULT = 0,        
        FORMAT_1555 = 0x0100, //(1 bit alpha, RGB 5 bits each; also used for DXT1 with alpha)
        FORMAT_565 = 0x0200, //(5 bits red, 6 bits green, 5 bits blue; also used for DXT1 without alpha)
        FORMAT_4444 = 0x0300, //(RGBA 4 bits each; also used for DXT3)
        FORMAT_LUM8 = 0x0400, //(gray scale, D3DFMT_L8)
        FORMAT_8888 = 0x0500, //(RGBA 8 bits each)
        FORMAT_888 = 0x0600, //(RGB 8 bits each, D3DFMT_X8R8G8B8)
        FORMAT_555 = 0x0A00, //(RGB 5 bits each - rare, use 565 instead, D3DFMT_X1R5G5B5)
        FORMAT_EXT_AUTO_MIPMAP = 0x1000, //(RW generates mipmaps, see special section below)
        FORMAT_EXT_PAL8 = 0x2000, //(2^8 = 256 palette colors)
        FORMAT_EXT_PAL4 = 0x4000, //(2^4 = 16 palette colors)
        FORMAT_EXT_MIPMAP = 0x8000 //(mipmaps included)
    }
}
