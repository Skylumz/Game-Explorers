using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RenderwareEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine.Rendering
{
    public class Renderer
    {
        public ShaderManager ShaderManager;
        public GLControl Viewport;

        public Renderer(GLControl vp)
        {
            Viewport = vp;
            ShaderManager = new ShaderManager();
        }

        public void ChangeColor(Color color)
        {
            GL.ClearColor(color);
        }

        public void Start()
        {
            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
        }

        public void PrepareFrame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        //dont use? since scene has a RenderMethod ?
        public void DoFrame()
        {
            PrepareFrame();

            Viewport.SwapBuffers();
        }

        public void Resize()
        {
            GL.Viewport(0, 0, Viewport.Width, Viewport.Height);
        }

        public void RenderGameObject(GameObject go, Camera cam)
        {
            go.Transform.CalculateModelViewProjectionMatrix(Viewport.Width, Viewport.Height, cam);

            RenderRenderableModel(go.Model, go.Transform.ModelViewProjectionMatrix);
        }

        public void RenderRenderableModel(RenderableModel model, Matrix4 vpmatrix)
        {
            var shader = ShaderManager.GetShader(model.ShaderName);
            shader.Start();
            
            shader.LoadModelViewMatrix(vpmatrix);

            GL.BindVertexArray(model.VaoID);
            GL.EnableVertexArrayAttrib(model.VaoID, 0);

            GL.DrawElements(PrimitiveType.Triangles, model.Indicies.Count, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexArrayAttrib(model.VaoID, 0);

            GL.BindVertexArray(0);
            shader.Stop();
        }

        public void RenderGrid(Grid grid, Camera cam)
        {
            var shader = ShaderManager.GetShader(grid.ShaderName);
            shader.Start();

            grid.CalculateModelViewProjectionMatrix(Viewport.Width, Viewport.Height, cam);
            shader.LoadModelViewMatrix(grid.ModelViewProjectionMatrix);

            GL.BindVertexArray(grid.VaoID);
            GL.EnableVertexArrayAttrib(grid.VaoID, 0);
            GL.EnableVertexArrayAttrib(grid.VaoID, 1);
            GL.DrawArrays(PrimitiveType.Lines, 0, grid.Verticies.Count);
            GL.DisableVertexArrayAttrib(grid.VaoID, 0);
            GL.DisableVertexArrayAttrib(grid.VaoID, 1);
            GL.BindVertexArray(0);
            shader.Stop();
        }
    }
}
