using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine.Rendering
{
    public class Grid
    {
        public int VaoID;
        public string ShaderName;
        public int GridCount;

        //transform
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale = new Vector3(1, 1, 1);

        //view
        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        //mesh data
        public List<Vector3> Verticies;
        public List<Vector3> VertexColors;

        public Grid(int count)
        {
            GridCount = count;
            BuildGrid();
        }

        private void BuildGrid()
        {
            Verticies = new List<Vector3>();
            Verticies.Add(new Vector3(0, -GridCount, 0));
            Verticies.Add(new Vector3(0, GridCount, 0));
            for (int i = -GridCount; i <= GridCount; i++)
            {
                Verticies.Add(new Vector3(i, 0, -GridCount));
                Verticies.Add(new Vector3(i, 0, GridCount));
                Verticies.Add(new Vector3(-GridCount, 0, i));
                Verticies.Add(new Vector3(GridCount, 0, i));
            }
            
            VertexColors = new List<Vector3>();
            VertexColors.Add(new Vector3(0, 1, 0));
            VertexColors.Add(new Vector3(0, 1, 0));
            for (int i = -GridCount; i <= GridCount; i++)
            {
                if (i == -2)
                {
                    VertexColors.Add(new Vector3(0, 0, 1));
                    VertexColors.Add(new Vector3(0, 0, 1));
                    VertexColors.Add(new Vector3(1, 0, 0));
                    VertexColors.Add(new Vector3(1, 0, 0));
                }
                else
                {
                    VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
                    VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
                    VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
                    VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
                    VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
                }
            }

            VaoID = GLManager.BindGrid(this);
        }

        public void UpdateGridCount(int count)
        {
            GridCount = count;
            BuildGrid();
        }

        public void CalculateModelViewProjectionMatrix(int width, int height, Camera cam, float FOV = 75)
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            ViewProjectionMatrix = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
            ModelViewProjectionMatrix = ModelMatrix * ViewProjectionMatrix;
        }
    }
}
