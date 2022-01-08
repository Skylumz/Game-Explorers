using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.RenderWare.Sections
{
    public class Extension : RenderWareSection
    {
        public List<RenderWareSection> Extensions { get; set; }

        public override void Read(BinaryReader br, RenderWareSection p)
        {
            base.Read(br, p);

            br.ReadBytes(Size);

        }
    }
}
