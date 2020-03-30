﻿//Stolen : https://github.com/igorseabra4/RenderWareFile
namespace RenderWareFile
{
    public enum Section : int
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
