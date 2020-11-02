using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine
{
    public class Scene
    {
        public GLControl Viewport;
        public Renderer Renderer;
        public List<RenderableModel> SceneObjects;
        public Camera Camera;
        public Grid Grid;

        public Scene(GLControl vp)
        {
            Viewport = vp;

            Init();
        }

        //called On_Load on form
        public void Init()
        {
            Renderer = new Renderer(Viewport);
            SceneObjects = new List<RenderableModel>();
            Camera = new Camera(Viewport);
            Camera.MoveSpeed = 0.2f;
            Camera.Position = new Vector3(0, 0, 0);
            Grid = new Grid(10);
            Grid.Position = new Vector3(0, 0, 0);
            Grid.ShaderName = "Color";
        }

        public void AddCube()
        {
            var verts = new List<Vector3> {
                new Vector3(-0.8f, -0.8f,  -0.8f),
                new Vector3(0.8f, -0.8f,  -0.8f),
                new Vector3(0.8f, 0.8f,  -0.8f),
                new Vector3(-0.8f, 0.8f,  -0.8f),
                new Vector3(-0.8f, -0.8f,  0.8f),
                new Vector3(0.8f, -0.8f,  0.8f),
                new Vector3(0.8f, 0.8f,  0.8f),
                new Vector3(-0.8f, 0.8f,  0.8f),
            };

            var inds = new List<int>
            {
                //front
                0, 7, 3,
                0, 4, 7,
                //back
                1, 2, 6,
                6, 5, 1,
                //left
                0, 2, 1,
                0, 3, 2,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };

            var model = new RenderableModel(verts, inds, "Color");
            model.Scale = new Vector3(1, 1, 1);
            model.Position = new Vector3(0, 0, 0);
            SceneObjects.Add(model);
        }

        public void Render()
        {
            Renderer.PrepareFrame();
            
            Camera.ProcessInput();
            Renderer.RenderGrid(Grid, Camera);

            foreach (var obj in SceneObjects)
            {
                Renderer.RenderRenderableModel(obj, Camera);
            }

            Viewport.SwapBuffers();
        }
    }
}
