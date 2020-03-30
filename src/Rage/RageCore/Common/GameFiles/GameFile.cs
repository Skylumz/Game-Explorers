using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RageCore.Common.GameFiles
{
    public class GameFile
    {
        public string FilePath { get; set; }
        public string Name { get; set; }

        public int Version { get; set; }

        public GameFile(string fp) { FilePath = fp; }
        public virtual void Load() { }
        public virtual void Load(byte[] data) { }
    }
}
