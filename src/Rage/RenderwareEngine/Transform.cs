using OpenTK;
using RenderwareEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine
{
    public class Transform
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        private Matrix4 ModelMatrix;
        private Matrix4 ViewProjectionMatrix;
        public Matrix4 ModelViewProjectionMatrix { get; private set; }

        public Transform()
        {
            Position = new Vector3();
            Rotation = new Vector3();
            Scale = new Vector3(1, 1, 1);

            ModelMatrix = Matrix4.Identity;
            ViewProjectionMatrix = Matrix4.Identity;
            ModelViewProjectionMatrix = Matrix4.Identity;
        }

        public void CalculateModelViewProjectionMatrix(int width, int height, Camera cam, float FOV = 75)
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            ViewProjectionMatrix = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
            ModelViewProjectionMatrix = ModelMatrix * ViewProjectionMatrix;
        }
    }
}
