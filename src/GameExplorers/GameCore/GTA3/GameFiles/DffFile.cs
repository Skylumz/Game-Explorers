using GameCore.RenderWare;
using GameCore.RenderWare.Sections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.GTA3
{
    public class DffFile : GameFile
    {
        public Clump Clump { get; set; }

        //for rendering model
        public RWModel Model { get; set; }

        public DffFile(string fp) : base(fp) { }

        public override void Load(byte[] data)
        {
            if (FileName == "loplyguy.dff") { return; }//throw new Exception("Unreadable for now!"); }

            Clump = new Clump();
            Clump.Read(new BinaryReader(new MemoryStream(data)), null);

            Model = RWModel.ClumpToRWModel(Clump);
        }
    }
}
