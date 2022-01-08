using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine.Shaders
{
    public class ShaderManager
    {
        private Dictionary<string, ShaderProgram> shaders;

        public ShaderManager()
        {
            shaders = new Dictionary<string, ShaderProgram>();
        }

        //remove shader?

        public void AddShader(string name, ShaderProgram shader)
        {
            shaders.Add(name, shader);
        }

        public ShaderProgram GetShader(string name)
        {
            return shaders[name];
        }
    }
}
