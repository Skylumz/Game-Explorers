//https://github.com/dennisyolkin/gta_gameworld_renderer/blob/master/GTA%20World%20Renderer/Scenes/Loaders/TXDArchive.cs hacked and wacked

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RageCore.Common.GameFiles;
using System.Drawing;
using System.Drawing.Imaging;

namespace RageCore.GTA3.GameFiles
{
    public class TxdFile : RenderWareFile
    {

        public int TextureCount { get { return Textures.Count; } }
        public Dictionary<string, GtaTexture> Textures { get; set; }

        public TxdFile(string fp) : base(fp) { }

        private int readTextures { get; set; }
        private int amountOfTextures { get; set; }

        public override void Load(byte[] data)
        {
            var br = new BinaryReader(new MemoryStream(data));
            Textures = new Dictionary<string, GtaTexture>();
            Read(br);
        }

        private void Read(BinaryReader br)
        {
            SectionType type = (SectionType)br.ReadInt32();
            int size = br.ReadInt32();
            br.BaseStream.Seek(4, SeekOrigin.Current);
            
            readTextures = 0;
            
            ReadSection(br, size, type);
        }
        
        public override void ReadSection(BinaryReader br, int size, SectionType parentSection)
        {
            int positionEnd = (int)br.BaseStream.Position + size;
            
            while (br.BaseStream.Position < positionEnd && br.BaseStream.Position < br.BaseStream.Length && readTextures <= amountOfTextures)
            {
                SectionType type = (SectionType)br.ReadInt32();
                int newSize = br.ReadInt32();
                br.BaseStream.Seek(4, SeekOrigin.Current);

                switch (type)
                {
                    case SectionType.Data:
                        if (parentSection == SectionType.TextureNative)
                        {
                            ReadTexture(br, newSize, parentSection);
                            readTextures++;
                        }
                        else if (parentSection == SectionType.Dictionary)
                        {
                            amountOfTextures = br.ReadInt16();
                            br.BaseStream.Seek(sizeof(short), SeekOrigin.Current);
                        }
                        else
                            br.BaseStream.Seek(newSize, SeekOrigin.Current);
                        break;
                    case SectionType.TextureNative:
                        ReadSection(br, newSize, type);
                        break;
                    default:
                        //br.BaseStream.Seek(newSize, SeekOrigin.Current);
                        break;
                }
            }
        }

        private void ReadTexture(BinaryReader br, int size, SectionType type)
        {
            int position = (int)br.BaseStream.Position;
            br.BaseStream.Seek(8, SeekOrigin.Current);

            byte[] diffuseTextureName = new byte[32];
            byte[] alphaTextureName = new byte[32];
            br.Read(diffuseTextureName, 0, diffuseTextureName.Length);
            br.Read(alphaTextureName, 0, alphaTextureName.Length);

            var texture = new GtaTexture();
            texture.Load(br);
            
            //change this buillshit
            Func<byte[], string> ToFullName = delegate (byte[] name)
            {
                int nilIdx = Array.IndexOf(name, (byte)0);
                int nameLen = nilIdx == -1 ? name.Length : nilIdx;
                return Encoding.ASCII.GetString(name, 0, nameLen);//.ToLower();
            };

            Textures[ToFullName(diffuseTextureName)] = texture;

            if (alphaTextureName[0] != 0)
                Textures[ToFullName(alphaTextureName)] = texture;
        }
    }

    public class GtaTexture
    {
        public int RasterFormatSource { get; set; }
        public RasterFormat RasterFormat { get; set; }
        public RasterFormatEx RasterFormatEx { get; set; }
        public byte BitsPerPixel { get; set; }
        public byte DXTnumber { get; set; }
        public int AlphaUsed { get; set; }
        public short TextureWidth { get; set; }
        public short TextureHeight { get; set; }
        public byte MipMaps { get; set; }

        public bool UsePallete { get; set; }

        public Bitmap Bitmap { get; set; }

        public void Load(BinaryReader br)
        {
            ReadHeader(br);
            ReadTextureData(br);
        }

        private void ReadHeader(BinaryReader br)
        {
            RasterFormatSource = br.ReadInt32();

            RasterFormat = (RasterFormat)(RasterFormatSource % 0x1000);
            RasterFormatEx = (RasterFormatEx)(RasterFormatSource - RasterFormatSource % 0x1000);

            AlphaUsed = br.ReadInt32();
            TextureWidth = br.ReadInt16();

            TextureHeight = br.ReadInt16();
            BitsPerPixel = br.ReadByte();
            MipMaps = br.ReadByte();

            br.BaseStream.Seek(HeaderFieldOffset.DxtCompressionType - HeaderFieldOffset.RasterType, SeekOrigin.Current);
            DXTnumber = br.ReadByte();

            if ((RasterFormat == RasterFormat.R8_G8_B8_A8 || RasterFormat == RasterFormat.R8_G8_B8) && RasterFormatEx == RasterFormatEx.Pal8 || (BitsPerPixel == 8))
            {
                UsePallete = true;
            }
            else { UsePallete = false; }
        }

        private void ReadTextureData(BinaryReader br)
        {
            GtaColor[] palette = new GtaColor[0];
            if (UsePallete)
            {
                palette = ReadPalette(br);
            }

            Bitmap = new Bitmap(TextureWidth, TextureHeight, PixelFormat.Format24bppRgb);

            int dataSize = br.ReadInt32();

            if (UsePallete)
            {
                for (int i = 0; i < TextureWidth; ++i)
                {
                    for (int j = 0; j < TextureHeight; ++j)
                    {
                        int idx = br.ReadByte();

                        Bitmap.SetPixel(i, j, palette[idx].Color);
                    }
                }
                //for some reason
                Bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
            {
                byte[] data = br.ReadBytes(dataSize);
                var amountOfClrs = data.Length / 4;
                List<byte[]> dataclrs = new List<byte[]>();

                for (int i = 4; i < data.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        dataclrs.Add(new byte[] { data[i - 4], data[i - 3], data[i - 2], data[i - 1] });
                    }
                }

                //add last color
                dataclrs.Add(new byte[] { data[data.Length - 4], data[data.Length - 3], data[data.Length - 2], data[data.Length - 1] });

                palette = new GtaColor[data.Length / 4];

                for (int i = 0; i < dataclrs.Count; i++)
                {
                    palette[i] = new GtaColor(dataclrs[i][0], dataclrs[i][1], dataclrs[i][2], dataclrs[i][3]);
                }

                //setting top mipmap data
                int idx = 0;
                for (int i = 0; i < TextureWidth; ++i)
                {
                    for (int j = 0; j < TextureHeight; ++j)
                    {
                        Bitmap.SetPixel(i, j, palette[idx].Color);
                        idx++;
                    }
                }

                //deal with other mip maps?
                //dont know how :)
            }
        }

        private GtaColor[] ReadPalette(BinaryReader reader)
        {
            GtaColor[] Pallete = new GtaColor[256];
            for (int i = 0; i != 256; ++i)
            {
                var tmp = new byte[4];
                for (int j = 0; j != 4; ++j)
                    tmp[j] = reader.ReadByte();
                Pallete[i] = new GtaColor(tmp[0], tmp[1], tmp[2], tmp[3]);
            }
            return Pallete;
        }

        private static class HeaderFieldOffset
        {
            public const int RasterFormat = 0; // uint32
            public const int AlphaOrFourCC = 4; // uint32
            public const int ImageWidth = 8; // uint16
            public const int ImageHeight = 10; // uint16
            public const int BitsPerPixel = 12; // byte
            public const int MipMapCount = 13; // byte
            public const int RasterType = 14; // byte
            public const int DxtCompressionType = 15; // byte
        }

        public class GtaColor
        {
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
            public int A { get; set; }

            public Color Color { get { return Color.FromArgb(A, R, G, B); } }

            public GtaColor(int r, int g, int b, int a)
            {
                R = r;
                G = g;
                B = b;
                A = a;
            }
        }
    }
}
