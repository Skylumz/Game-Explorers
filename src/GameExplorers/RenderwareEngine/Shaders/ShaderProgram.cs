using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//stolen from thinmatrix
namespace RenderwareEngine.Shaders
{
    public abstract class ShaderProgram
    {
        public int ProgramID { get; set; }
        public int VertexShaderID { get; set; }
        public int FragmentShaderID { get; set; }

        public int ModelViewLocation { get; set; }

        //fix this retardation!
        public string Error { get; set; }
        public string Error1 { get; set; }

        public ShaderProgram(string vertexShaderPath, string fragmentShaderPath)
        {
            VertexShaderID = loadShader(vertexShaderPath, ShaderType.VertexShader);
            FragmentShaderID = loadShader(fragmentShaderPath, ShaderType.FragmentShader);
            ProgramID = GL.CreateProgram();
            GL.AttachShader(ProgramID, VertexShaderID);
            GL.AttachShader(ProgramID, FragmentShaderID);
            BindAttributes();
            GL.LinkProgram(ProgramID);
            GL.ValidateProgram(ProgramID);
            GetAllUniformLocations();

            Error = GL.GetShaderInfoLog(VertexShaderID);
            Error1 = GL.GetShaderInfoLog(FragmentShaderID);
        }

        protected abstract void GetAllUniformLocations();

        protected int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(ProgramID, uniformName);
        }

        public void Start()
        {
            GL.UseProgram(ProgramID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void CleanUp()
        {
            Stop();
            GL.DetachShader(ProgramID, VertexShaderID);
            GL.DetachShader(ProgramID, FragmentShaderID);
            GL.DeleteShader(VertexShaderID);
            GL.DeleteShader(FragmentShaderID);
            GL.DeleteProgram(ProgramID);
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(ProgramID, attribute, variableName);
        }

        protected void LoadUniformFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        protected void LoadUniformVector(int location, Vector3 value)
        {
            GL.Uniform3(location, value);
        }

        protected void LoadUniformBoolean(int location, bool value) 
        {
            float toLoad = 0;
            if (value) { toLoad = 1; }
            GL.Uniform1(location, toLoad);
        }

        protected void LoadUniformMatrix(int location, Matrix4 value)
        {
            GL.UniformMatrix4(location, false, ref value);
        }

        public void LoadModelViewMatrix(Matrix4 value)
        {
            //for first call
            if(ModelViewLocation == 0) { GetUniformLocation("modelview"); }
            GL.UniformMatrix4(ModelViewLocation, false, ref value);
        }

        private static int loadShader(string shaderPath, ShaderType type)
        {
            StringBuilder shaderSource = new StringBuilder();

            try
            {
                StreamReader reader = new StreamReader(shaderPath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    shaderSource.Append(line).Append("\n");
                }
                reader.Close();
            }
            catch { }

            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, shaderSource.ToString());
            GL.CompileShader(shaderID);

            int j;
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out j);;
            if(j == 0) { /*big error*/ }

            return shaderID;
        }
    }
}
