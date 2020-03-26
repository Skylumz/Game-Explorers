using RageCore.Common.GameFiles;
using RageCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RageCore.GTA1.GameFiles
{
    public class ImgFile1 : ArchiveFile
    {
        public int NumberOfDirectorys { get; set; }
        public List<Img1DirectoryEntry> DirectoryEntries { get; set;}

        public ImgFile1(string filepath) : base(filepath)
        {
            FilePath = filepath;
        }

        public override void Load()
        {
            var br = new BinaryReader(new MemoryStream(File.ReadAllBytes(FilePath)));
            Read(br);
        }

        public void Read(BinaryReader br)
        {
            //read .dir file
            var dirfp = FilePath.Replace("img", "dir");
            var dirbytes = File.ReadAllBytes(dirfp);
            NumberOfDirectorys = dirbytes.Length / 32;
            var dirbr = new BinaryReader(new MemoryStream(dirbytes));

            DirectoryEntries = new List<Img1DirectoryEntry>();
            for (int i = 0; i < NumberOfDirectorys; i++)
            {
                Img1DirectoryEntry e = new Img1DirectoryEntry();
                e.Read(dirbr);
                br.BaseStream.Seek(e.Offset, SeekOrigin.Current);
                e.Data = br.ReadBytes((int)e.Size);
                DirectoryEntries.Add(e);
            }
        }

        public class Img1DirectoryEntry : ArchiveFileEntry
        {
            public UInt32 Offset { get; set; }
            public UInt32 Size { get; set; }
            public string Name { get; set; }
            public byte[] Data { get; set; }

            public void Read(BinaryReader br)
            {
                Offset = br.ReadUInt32();
                Size = br.ReadUInt32();
                Name = BinaryReaderUtilities.ReadNullTerminatedString(br, 24);
            }
        }
    }
}
