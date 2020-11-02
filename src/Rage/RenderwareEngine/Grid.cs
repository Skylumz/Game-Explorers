using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine
{
    public class Grid
    {
        public int VaoID;
        public string ShaderName;

        //transform
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale = new Vector3(1, 1, 1);

        public List<Vector3> Verticies;
        public List<Vector3> VertexColors;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public int GridCount;


        public Grid(int count)
        {
            GridCount = count;
            Init();
        }

        private void Init()
        {
            Verticies = new List<Vector3>();
            for (int i = -GridCount; i < GridCount + 1; i++)
            {
                Verticies.Add(new Vector3(i, 0, GridCount));
                Verticies.Add(new Vector3(i, 0, -GridCount));
                Verticies.Add(new Vector3(GridCount, 0, i));
                Verticies.Add(new Vector3(-GridCount, 0, i));
            }
            //Verticies = new List<Vector3>();

            //Verticies.Add(new Vector3(0, 0, -25));
            //Verticies.Add(new Vector3(0, 0, 25));

            //for (int i = -25; i < 25; i++)
            //{
            //    Verticies.Add(new Vector3(i, -25, 0));
            //    Verticies.Add(new Vector3(i, 25, 0));
            //    Verticies.Add(new Vector3(-25, i, 0));
            //    Verticies.Add(new Vector3(25, -i, 0));
            //}

            //VertexColors = new List<Vector3>();

            //VertexColors.Add(new Vector3(1, 1, 1));
            //VertexColors.Add(new Vector3(0, 0, 1));

            //for (int i = -25; i < 25; i++)
            //{
            //    if(i == 0)
            //    {
            //        VertexColors.Add(new Vector3(1, 1, 1));
            //        VertexColors.Add(new Vector3(0, 1, 0));
            //        VertexColors.Add(new Vector3(1, 1, 1));
            //        VertexColors.Add(new Vector3(1, 0, 0));
            //    }
            //    else
            //    {
            //        VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
            //        VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
            //        VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
            //        VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
            //        VertexColors.Add(new Vector3(0.6f, 0.6f, 0.6f));
            //    }
            //}


            VaoID = GLManager.BindGrid(this);
        }

        public void UpdateGridCount(int count)
        {
            GridCount = count;
            Init();
        }

        public void CalculateModelViewProjectionMatrix(int width, int height, Camera cam, float FOV = 75)
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            ViewProjectionMatrix = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (FOV / 180f), width / (float)height, 0.2f, 256.0f);
            ModelViewProjectionMatrix = ModelMatrix * ViewProjectionMatrix;
        }
    }
}
