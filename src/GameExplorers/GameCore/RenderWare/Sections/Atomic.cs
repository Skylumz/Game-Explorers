using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.RenderWare.Sections
{
    public class Atomic : RenderWareSection
    {
        public AtomicStruct Struct { get; set; }
        public Extension Extension { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new AtomicStruct();
            Struct.Read(br, this);

            Extension = new Extension();
            Extension.Read(br, this);
        }
    }

    public class AtomicStruct : RenderWareSection
    {
        public int FrameIndex { get; set; }
        public int GeometryIndex { get; set; }
        public AtomicFlag Flag { get; set; }
        public int Unused { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            FrameIndex = br.ReadInt32();
            GeometryIndex = br.ReadInt32();
            Flag = (AtomicFlag)br.ReadInt32();
            Unused = br.ReadInt32();
        }
    }

    public enum AtomicFlag
    {
        None = 0,
        CollisionTest = 1,
        Render = 4,
        Both = 5
    }
}
