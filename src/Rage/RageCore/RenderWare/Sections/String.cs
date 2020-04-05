using RageCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageCore.RenderWare.Sections
{
    public class String : RenderWareSection
    {
        public string DataString { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            DataString = BinaryReaderUtilities.ReadNullStartingString(br, Size);
        }
    }
}
