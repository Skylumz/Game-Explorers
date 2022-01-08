using OpenTK;
using RenderwareEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine
{
    public class Scene
    {
        public GLControl Viewport;
        public Renderer Renderer;
        public List<GameObject> GameObjects;
        public Camera Camera;
        public Grid Grid;

        public bool renderGrid = true;

        public Scene(GLControl vp)
        {
            Viewport = vp;

            Init();
        }

        //called On_Load on form
        public void Init()
        {
            Renderer = new Renderer(Viewport);
            GameObjects = new List<GameObject>();
            Camera = new Camera(Viewport);
            Camera.MoveSpeed = 0.2f;
            Camera.Position = new Vector3(0, 0, 0);
            Grid = new Grid(10, 10);
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

            var colors = new List<Vector3>
            {
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f),
                new Vector3(1f, 1f,  1f)
            };

            var model = new RenderableModel(verts, colors, inds, "Color");
            var transform = new Transform();

            var go = new GameObject(transform, model);

            GameObjects.Add(go);
        }

        public void Render()
        {
            Renderer.PrepareFrame();
            
            Camera.ProcessInput();

            if (renderGrid)
            {
                Renderer.RenderGrid(Grid, Camera);
            }

            foreach (var obj in GameObjects)
            {
                Renderer.RenderGameObject(obj, Camera);
            }

            Viewport.SwapBuffers();
        }
    }
}
