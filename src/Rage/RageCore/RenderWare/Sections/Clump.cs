using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.RenderWare.Sections
{
    public class Clump : RenderWareSection
    {
        public ClumpStruct Struct { get; set; }
        public FrameList FrameList { get; set; }
        public GeometryList GeometryList { get; set; }
        public List<Atomic> Atomics { get; set; }
        public Extension Extension { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new ClumpStruct();
            Struct.Read(br, this);

            FrameList = new FrameList();
            FrameList.Read(br, this);

            GeometryList = new GeometryList();
            GeometryList.Read(br, this);

            Atomics = new List<Atomic>();
            for (int i = 0; i < Struct.AtomicCount; i++)
            {
                var a = new Atomic();
                a.Read(br, this);
                Atomics.Add(a);
            }

            Extension = new Extension();
            Extension.Read(br, this);
        }
    }

    public class ClumpStruct : RenderWareSection
    {
        public int AtomicCount { get; set; }
        public int LightCount { get; set; }
        public int CameraCount { get; set; } //always 0

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            if (Version < 0x33000)
            {
                AtomicCount = br.ReadInt32();
            }
            else
            {
                AtomicCount = br.ReadInt32();
                LightCount = br.ReadInt32();
                CameraCount = br.ReadInt32();
            }
        }
    }
}
