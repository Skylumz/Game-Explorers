using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.RenderWare.Sections
{
    public class GeometryList : RenderWareSection
    {
        public GeometryListStruct Struct { get; set; }
        public List<Geometry> Geometries { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new GeometryListStruct();
            Struct.Read(br, this);

            Geometries = new List<Geometry>();
            
            for (int i = 0; i < Struct.NumberOfGeometries; i++)
            {
                var geo = new Geometry();
                geo.Read(br, this);
                Geometries.Add(geo);
            }
        }
    }

    public class GeometryListStruct : RenderWareSection
    {
        public int NumberOfGeometries { get; set; }
        
        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            NumberOfGeometries = br.ReadInt32();
        }
    }

    public class Geometry : RenderWareSection
    {
        public GeometryStruct Struct { get; set; }
        public MaterialList MaterialList { get; set; }
        public Extension Extension { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Struct = new GeometryStruct();
            Struct.Read(br, this);

            MaterialList = new MaterialList();
            MaterialList.Read(br, this);

            Extension = new Extension();
            Extension.Read(br, this);
        }
    }

    public enum GeometryFlag
    {
        none = 0x0000,
        isTristrip = 0x0001,
        hasVertexPositions = 0x0002,
        hasTextCoords = 0x0004,
        hasVertexColors = 0x0008,
        hasNormals = 0x0010,
        hasLights = 0x0020,
        modeulateMaterialColor = 0x00040,
        hasTextCoords2 = 0x0080
    }

    public class GeometryStruct : RenderWareSection
    {
        public GeometryFlag Flag { get; set; }
        public short Unknown1 { get; set; }
        public int FaceCount { get; set; }
        public int VertexCount { get; set; }
        public int MorphTargetCount { get; set; } //always 1 morphing not used for gta 

        //if version is less than 0x34000
        public float Ambient { get; set; }
        public float Specular { get; set; }
        public float Diffuse { get; set; }

        public bool HasVertexColors { get { return UseVertexColors(); } }
        public bool HasTextureCoords { get { return UseTextureCoords(); } }
        public bool HasTextureCoords2 { get { return UseTextureCoords2(); } }

        public List<List<short>> VertexColors { get; set; }
        public List<List<float>> TextureCoords1 { get; set; }
        public List<List<float>> TextureCoords2 { get; set; }
        public List<GeometryTriangle> Triangles { get; set; }
        public List<MorphTarget> MorphTargets { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            Flag = (GeometryFlag)br.ReadInt16();
            Unknown1 = br.ReadInt16();
            FaceCount = br.ReadInt32();
            VertexCount = br.ReadInt32();
            MorphTargetCount = br.ReadInt32();

            if (Version < 0x34000)
            {
                Ambient = br.ReadSingle();
                Specular = br.ReadSingle();
                Diffuse = br.ReadSingle();
            }

            if (HasVertexColors)
            {
                VertexColors = new List<List<short>>();
                for (int i = 0; i < VertexCount; i++)
                {
                    var clr = new List<short>();
                    clr.Add(br.ReadByte()); //r
                    clr.Add(br.ReadByte()); //g 
                    clr.Add(br.ReadByte()); //b
                    clr.Add(br.ReadByte()); //a
                }
            }

            if (HasTextureCoords)
            {
                TextureCoords1 = new List<List<float>>();
                for (int i = 0; i < VertexCount; i++)
                {
                    var tc = new List<float>();
                    tc.Add(br.ReadSingle());
                    tc.Add(br.ReadSingle());
                    TextureCoords1.Add(tc);
                }
            }
            else if (HasTextureCoords2)
            {
                TextureCoords2 = new List<List<float>>();
                for (int i = 0; i < VertexCount * 2; i++)
                {
                    var tc = new List<float>();
                    tc.Add(br.ReadSingle());
                    tc.Add(br.ReadSingle());
                    TextureCoords2.Add(tc);
                }
            }

            Triangles = new List<GeometryTriangle>();
            for (int i = 0; i < FaceCount; i++)
            {
                var t = new GeometryTriangle();
                t.Read(br, this);
                Triangles.Add(t);
            }

            MorphTargets = new List<MorphTarget>();
            for (int i = 0; i < MorphTargetCount; i++)
            {
                var m = new MorphTarget();
                m.Read(br, this);
                MorphTargets.Add(m);
            }
        }

        private bool UseVertexColors()
        {
            if((Flag & GeometryFlag.hasVertexColors) != 0) { return true; }
            else { return false; }
        }

        private bool UseTextureCoords()
        {
            if ((Flag & GeometryFlag.hasTextCoords) != 0) { return true; }
            else { return false; }
        }

        private bool UseTextureCoords2()
        {
            if ((Flag & GeometryFlag.hasTextCoords2) != 0) { return true; }
            else { return false; }
        }
    }

    public class GeometryTriangle : RenderWareSection
    {
        public ushort Vertex1 { get; set; }
        public ushort Vertex2 { get; set; }
        public ushort Vertex3 { get; set; }
        public ushort MaterialIndex { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            //base.Read(br); Not a actual section

            Vertex2 = br.ReadUInt16();
            Vertex1 = br.ReadUInt16();
            MaterialIndex = br.ReadUInt16();
            Vertex3 = br.ReadUInt16();
        }
    }

    public class MorphTarget : RenderWareSection
    {
        public List<float> BoundingSphere { get; set; } //x, y, z, radius
        public bool HasVertices { get; set; }
        public bool HasNormals { get; set; }

        public List<List<float>> Vertices { get; set; }
        public List<List<float>> Normals { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            //base.Read(br); Not a actual section

            BoundingSphere = new List<float>();
            for (int i = 0; i < 4; i++)
            {
                BoundingSphere.Add(br.ReadSingle());
            }

            HasVertices = br.ReadInt32() == 1 ? true : false;
            HasNormals = br.ReadInt32() == 1 ? true : false;

            var par = p as GeometryStruct;

            if (HasVertices)
            {
                Vertices = new List<List<float>>();
                for (int i = 0; i < par.VertexCount; i++)
                {
                    var vert = new List<float>();
                    vert.Add(br.ReadSingle()); //x
                    vert.Add(br.ReadSingle()); //y
                    vert.Add(br.ReadSingle()); //z
                    Vertices.Add(vert);
                }
            }

            if (HasNormals)
            {
                Normals = new List<List<float>>();
                for (int i = 0; i < par.VertexCount; i++)
                {
                    var norm = new List<float>();
                    norm.Add(br.ReadSingle()); //x
                    norm.Add(br.ReadSingle()); //y
                    norm.Add(br.ReadSingle()); //z
                    Normals.Add(norm);
                }
            }
        }
    }
}
