using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.RenderWare.Sections
{
    public class FrameList : RenderWareSection
    {
        public FrameListStruct Struct { get; set; }
        public List<Extension> Extensions { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new FrameListStruct();
            Struct.Read(br, this);

            Extensions = new List<Extension>();
            for (int i = 0; i < Struct.FrameCount; i++)
            {
                var ext = new Extension();
                ext.Read(br, this);
                Extensions.Add(ext);
            }
        }
    }

    public class FrameListStruct : RenderWareSection
    {
        public int FrameCount { get; set; }
        public List<Frame> Frames { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            FrameCount = br.ReadInt32();

            Frames = new List<Frame>();
            for (int i = 0; i < FrameCount; i++)
            {
                Frame f = new Frame();
                f.Read(br, this);
                Frames.Add(f);        
            }
        }
    }

    public class Frame : RenderWareSection
    {
        public List<float> RotationMatrix { get; set; } //rotation matrix
        public List<float> Position { get; set; } //vector3
        public int ParentFrame { get; set; }
        public int Unknown { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            //base.Read(br); no section identifier on this one

            RotationMatrix = new List<float>();
            for (int i = 0; i < 9; i++)
            {
                RotationMatrix.Add(br.ReadSingle());
            }

            Position = new List<float>();
            for (int i = 0; i < 3; i++)
            {
                Position.Add(br.ReadSingle());
            }

            ParentFrame = br.ReadInt32();
            Unknown = br.ReadInt32();
        }
    }
}
