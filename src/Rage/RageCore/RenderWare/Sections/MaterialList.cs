using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageCore.RenderWare.Sections
{
    public class MaterialList : RenderWareSection
    {
        public MaterialListStruct Struct { get; set; }
        public List<Material> Materials { get; set; }
            
        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new MaterialListStruct();
            Struct.Read(br, this);

            Materials = new List<Material>();
            for (int i = 0; i < Struct.MaterialCount; i++)
            {
                var mat = new Material();
                mat.Read(br, this);
                Materials.Add(mat);
            }
        }
    }

    public class MaterialListStruct : RenderWareSection
    {
        public int MaterialCount { get; set; }
        public List<int> UnusedInts { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            MaterialCount = br.ReadInt32();

            UnusedInts = new List<int>();
            for (int i = 0; i < MaterialCount; i++)
            {
                UnusedInts.Add(br.ReadInt32());
            }
        }
    }

    public class Material : RenderWareSection
    {
        public MaterialStruct Struct { get; set; }
        public Texture Texture { get; set; }
        public Extension Extension { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new MaterialStruct();
            Struct.Read(br, this);

            if (Struct.IsTextured)
            {
                Texture = new Texture();
                Texture.Read(br, this);
            }

            Extension = new Extension();
            Extension.Read(br, this);
        }
    }

    public class MaterialStruct : RenderWareSection
    {
        public int Flags { get; set; }
        public List<byte> Color { get; set; }
        public int Unused { get; set; }
        public bool IsTextured { get; set; }
        public float Ambient { get; set; }
        public float Specular { get; set; }
        public float Diffuse { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            var endPos = br.BaseStream.Position + Size;

            Flags = br.ReadInt32();
            Color = new List<byte>();
            for (int i = 0; i < 4; i++)
            {
                Color.Add(br.ReadByte());
            }
            Unused = br.ReadInt32();
            IsTextured = br.ReadInt32() == 1 ? true : false;

            if (Version > 0x30400)
            {
                Ambient = br.ReadSingle();
                Specular = br.ReadSingle();
                Diffuse = br.ReadSingle();
            }

            //material struct has extra data in some files not reported here : 
            br.BaseStream.Position = endPos;
        }
    }
}
