using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageCore.Common.GameFiles
{
    public class RenderWareFile : GameFile
    {
        public RenderWareFile(string fp) : base(fp) { }
        public virtual void ReadSection(BinaryReader br, int size, SectionType parentSection) { }
    }

    public enum SectionType
    {
        Data = 1,
        Extension = 3,
        TextureNative = 21,
        Dictionary = 22,
        Unknown = 42134213
    }

    public enum RasterFormat
    {
        Default = 0x0000,
        R5_G5_B5_A1 = 0x0100,
        R5_G6_B5 = 0x0200,
        R4_G4_B4_A4 = 0x0300,
        LUM8 = 0x0400, // (gray scale)
        R8_G8_B8_A8 = 0x0500,
        R8_G8_B8 = 0x0600,
        fmt555 = 0x0A00, // (RGB 5 bits each - rare, use 565 instead)
    }

    public enum RasterFormatEx
    {
        Default = 0,
        AutoMipMap = 0x1000, // (RW generates mipmaps)
        Pal8 = 0x2000, // (2^8 = 256 palette colors)
        Pal4 = 0x4000, // (2^4 = 16 palette colors)
        MipMap = 0x8000, // (mipmaps included)
    }
}
