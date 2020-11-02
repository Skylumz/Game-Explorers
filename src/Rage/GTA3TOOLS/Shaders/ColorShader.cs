using RenderwareEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA3TOOLS.Shaders
{
    public class ColorShader : ShaderProgram
    {
        public ColorShader(string vs, string fs) : base(vs, fs) 
        { 
            
        }

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
        }

        protected override void GetAllUniformLocations()
        {
            //throw new NotImplementedException();
        }
    }
}
