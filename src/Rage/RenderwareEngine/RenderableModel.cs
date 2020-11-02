using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RenderwareEngine
{
    public class RenderableModel
    {
        public string ShaderName;
        public int VaoID;

        //transform
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        //mesh data
        public List<Vector3> Verticies;
        public List<int> Indicies;

        public RenderableModel(List<Vector3> verts, List<int> indicies, string shadername)
        {
            Verticies = verts;
            Indicies = indicies;
            ShaderName = shadername;
            VaoID = GLManager.BindRenderableModel(this);
        }

        public void CalculateModelViewProjectionMatrix(int width, int height, Camera cam, float FOV = 75)
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            ViewProjectionMatrix = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
            ModelViewProjectionMatrix = ModelMatrix * ViewProjectionMatrix;
        }
    }
}
