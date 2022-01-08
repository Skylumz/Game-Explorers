using GameCore.RenderWare.Sections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.GTA3
{
    public class TxdFile : GameFile
    {
        public TextureDictionary TextureDictionary { get; set; }

        public TxdFile(string fp) : base(fp) { }

        public override void Load(byte[] data)
        {
            TextureDictionary = new TextureDictionary();
            TextureDictionary.Read(new BinaryReader(new MemoryStream(data)), null);
        }
    }
}
