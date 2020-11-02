using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RenderwareEngine.Rendering
{
    public class RenderableModel
    {
        public string ShaderName;
        public int VaoID;
        
        //mesh data
        public List<Vector3> Verticies;
        public List<Vector3> VertexColors;
        public List<int> Indicies;

        public RenderableModel(List<Vector3> verts, List<Vector3> vertcolors, List<int> indicies, string shadername)
        {
            Verticies = verts;
            VertexColors = vertcolors;
            Indicies = indicies;
            ShaderName = shadername;
            VaoID = GLManager.BindRenderableModel(this);
        }
    }
}
