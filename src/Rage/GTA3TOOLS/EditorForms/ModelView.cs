using GTA3TOOLS.Controls;
using GTA3TOOLS.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using RenderwareEngine;
using RenderwareEngine.Shaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA3TOOLS.EditorForms
{
    public partial class ModelView : Form
    {
        GLControl Viewport;
        Scene scene;

        public ModelView()
        {
            InitializeComponent();
        }

        // Called on load
        public void Init()
        {
            Viewport = new GLControl();
            Controls.Add(Viewport);

            Viewport.Paint += Viewport_Paint;
            Viewport.Resize += Viewport_Resize;
            Viewport.Dock = DockStyle.Fill;
            scene = new Scene(Viewport);

            //dont like doing this need a Renderer.InitShaders() or something
            ColorShader colorShader = new ColorShader("Shaders\\ColorVertexShader.glsl", "Shaders\\ColorFragmentShader.glsl");
            scene.Renderer.ShaderManager.AddShader("Color", colorShader);

            scene.Renderer.Start();

            Application.Idle += Application_Idle;

            Viewport_Resize(Viewport, EventArgs.Empty);
        }

        private void RenderFrame()
        {
            scene.Render();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            while (Viewport.IsIdle)
            {
                RenderFrame();
            }
        }

        private void Viewport_Resize(object sender, EventArgs e)
        {
            if(scene == null) { return; }
            scene.Renderer.Resize();
        }

        private void Viewport_Paint(object sender, PaintEventArgs e)
        {
            RenderFrame();
        }

        private void ModelView_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void UpdateSceneUI()
        {
            var startLoc = new Point();
            foreach (var obj in scene.SceneObjects)
            {
                var transformcontrol = new SceneObjectEditor();
                transformcontrol.model = obj;
                transformcontrol.Parent = this;
                transformcontrol.BringToFront();
                transformcontrol.Location = startLoc;
                startLoc.Y += transformcontrol.Height;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count;
            bool parsed = int.TryParse(textBox1.Text, out count);
            if (parsed)
            {
                scene.Grid.UpdateGridCount(count);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scene.AddCube();
            UpdateSceneUI();
        }
    }
}
