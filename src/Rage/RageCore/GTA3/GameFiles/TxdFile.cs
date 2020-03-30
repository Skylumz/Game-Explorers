using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RageCore.Common.GameFiles;

namespace RageCore.GTA3.GameFiles
{
    public class TxdFile : GameFile
    {
        public SectionType type { get; set; }
        public int AmountOfTextures { get; set; }

        public TxdFile(string fp) : base(fp)
        {
        }

        public override void Load(byte[] data)
        {
            var br = new BinaryReader(new MemoryStream(data));
            Read(br);
        }

        private void Read(BinaryReader br)
        {
            type = (SectionType)br.ReadInt32();
            int size = br.ReadInt32();
            br.BaseStream.Seek(4, SeekOrigin.Current);


        }

        private void ParseSection(BinaryReader br, int size, SectionType parentType)
        {
            int positionEnd = (int)br.BaseStream.Position + size;

            while (br.BaseStream.Position < positionEnd && br.BaseStream.Position < br.BaseStream.Length)
            //&& processedTextures < texturesAmount)
            {
                SectionType sectionType = (SectionType)br.ReadInt32();

                int sectionSize = br.ReadInt32();
                br.BaseStream.Seek(4, SeekOrigin.Current);

                switch (sectionType)
                {
                    case SectionType.Data:
                        if (parentType == SectionType.TextureNative) { }
                        //ReadTexture(sectionSize, parentType);
                        else if (parentType == SectionType.Dictionary)
                        {
                            AmountOfTextures = br.ReadInt16();
                            br.BaseStream.Seek(sizeof(short), SeekOrigin.Current);
                        }
                        else
                            br.BaseStream.Seek(sectionSize, SeekOrigin.Current);
                        break;

                    case SectionType.Extension:
                    case SectionType.Dictionary:
                    case SectionType.TextureNative:
                        ParseSection(br, sectionSize, sectionType);
                        break;

                    default:
                        br.BaseStream.Seek(sectionSize, SeekOrigin.Current);
                        break;
                }

            }
        }
    }

    public enum SectionType
    {
        Data = 1,
        Extension = 3,
        TextureNative = 21,
        Dictionary = 22,
        Unknown = 42134213 
    }
}
