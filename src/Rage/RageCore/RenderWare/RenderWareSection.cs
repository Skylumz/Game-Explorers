using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace GameCore.RenderWare
{
    [TypeConverter(typeof(ExpandableObjectConverter))]   
    public class RenderWareSection
    {
        public SectionType Type { get; set; }
        public int Size { get; set; }
        public int LibraryID { get; set; }
        public int Version { get { return GetVersion(); } }
        public int Build { get { return GetBuild(); } }

        public RenderWareSection Parent { get; set; }
        
        public virtual void Read(BinaryReader br, RenderWareSection p) 
        {
            Type = (SectionType)br.ReadInt32(); //section type
            Size = br.ReadInt32();
            LibraryID = br.ReadInt32();
            Parent = p;
        }

        //work on later :P
        public virtual string DumpTree(int level)
        {
            StringBuilder sb = new StringBuilder();
            level++;
            string tab = "";
            for (int i = 0; i < level; i++)
            {
                tab += "\t";
            }
            string header = tab + "Type: " + Type.ToString() + " Size: " + Size.ToString() + " LibraryID: " + LibraryID.ToString();
            sb.AppendLine(header);
            sb.AppendLine(tab + "{");
            //if (Parent != null) { sb.AppendLine(tab + Parent.DumpTree(level)); }
            //foreach(var p in GetType().GetProperties())
            //{
            //    if(p.GetValue(this).GetType().BaseType == typeof(RenderWareSection)) 
            //    { 
            //        //var obj = p.GetValue(this) as RenderWareSection;
            //        //sb.AppendLine(obj.ToString());
            //    }
            //    if(p.Name == "Parent" || p.Name == "Version" || p.Name == "Build") { continue; }
            //    //sb.AppendLine(tab + "\t" + p.Name + " Value:" + p.GetValue(this));
            //}

            sb.AppendLine(tab + "}");

            return sb.ToString();
        }

        private int GetVersion()
        {
            if ((LibraryID & 0xFFFF0000) != 0)
            {
                return (LibraryID >> 14 & 0x3FF00) + 0x30000 | (LibraryID >> 16 & 0x3F);
            }
            else { return LibraryID << 8; }
        }

        private int GetBuild()
        {
            if ((LibraryID & 0xFFFF0000) != 0)
            {
                return LibraryID & 0xFFFF;
            }
            else { return 0; }
        }
    }

    public enum SectionType
    {
        None = 0x0,
        Struct = 0x1,
        String = 0x2,
        Extension = 0x3,
        Texture = 0x6,
        Material = 0x7,
        MaterialList = 0x8,
        AtomicSector = 0x9,
        PlaneSector = 0xA,
        World = 0xB,
        FrameList = 0xE,
        Geometry = 0xF,
        Clump = 0x10,
        Atomic = 0x14,
        TextureNative = 0x15,
        TextureDictionary = 0x16,
        GeometryList = 0x1A,
        ChunkGroupStart = 0x29,
        ChunkGroupEnd = 0x2A,
        ColTree = 0x2C,
        MorphPLG = 0x105,
        SkyMipmapVal = 0x110,
        CollisionPLG = 0x11D,
        UserDataPLG = 0x11F,
        MaterialEffectsPLG = 0x120,
        BinMeshPLG = 0x50E,
        NativeDataPLG = 0x510,
        BFBB_CollisionData_Section1 = 0x00BEEF01,
        BFBB_CollisionData_Section2 = 0x00BEEF02,
        BFBB_CollisionData_Section3 = 0x00BEEF03,
    }
}
