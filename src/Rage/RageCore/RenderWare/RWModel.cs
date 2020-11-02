using RageCore.RenderWare.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageCore.RenderWare
{
    public class RWModel
    {
        public float[] Verticies { get; set; }
        public int[] Indicies { get; set; }
        
        public static RWModel ClumpToRWModel(Clump c)
        {
            RWModel result = new RWModel();

            //only gonna go after the first geometry in clump
            var geo = c.GeometryList.Geometries[0].Struct;
            var morphTarget = geo.MorphTargets[0];

            result.Verticies = new float[morphTarget.Vertices.Count];
            result.Indicies = new int[geo.Triangles.Count];

            int j = 0;
            int a = 0;
            foreach(var v in morphTarget.Vertices)
            {
                result.Verticies[j] = v[0];
                result.Verticies[j] = v[1];
                result.Verticies[j] = v[2];
                j++;
            }
            for (int i = 0; i < geo.FaceCount; i++)
            {
                result.Indicies[i] = geo.Triangles[a].Vertex3;
                result.Indicies[i] = geo.Triangles[a].Vertex2;
                result.Indicies[i] = geo.Triangles[a].Vertex1;
            }

            return result;
        }
    }
}
