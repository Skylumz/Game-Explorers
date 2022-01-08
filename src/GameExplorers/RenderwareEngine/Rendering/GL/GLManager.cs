using RenderwareEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

//cam cam cam https://stackoverflow.com/questions/28676300/opengl-with-opentk-wrapper-cannot-get-projections-to-work

namespace RenderwareEngine.Rendering
{
    public static class GLManager
    {
        public static int BindRenderableModel(RenderableModel model)
        {
            int vaoID = CreateVAO();
            BindIndiciesBuffer(model.Indicies.ToArray());
            StoreVector3DataInAttributeList(0, 3, model.Verticies.ToArray());
            UnbindVAO();
            return vaoID;
        }

        public static int BindGrid(Grid grid)
        {
            int vaoID = CreateVAO();
            StoreVector3DataInAttributeList(0, 3, grid.Verticies.ToArray());
            StoreVector3DataInAttributeList(1, 3, grid.VertexColors.ToArray());
            UnbindVAO();
            return vaoID;
        }

        private static int CreateVAO()
        {
            int vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private static void StoreVector3DataInAttributeList(int attNumber, int coordinateSize, Vector3[] data)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector3.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private static void StoreVector2DataInAttributeList(int attNumber, int coordinateSize, Vector2[] data)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector2.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private static void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private static void BindIndiciesBuffer(int[] indicies)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData<int>(BufferTarget.ElementArrayBuffer, indicies.Length * sizeof(int), indicies, BufferUsageHint.StaticDraw);
        }
    }
}
