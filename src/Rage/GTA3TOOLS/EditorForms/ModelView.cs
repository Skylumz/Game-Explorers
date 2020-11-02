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
            Viewport.BringToFront();

            Viewport.Paint += Viewport_Paint;
            Viewport.Resize += Viewport_Resize;
            Viewport.Dock = DockStyle.Fill;
            scene = new Scene(Viewport);

            //dont like doing this need a Renderer.InitShaders() or something
            ColorShader colorShader = new ColorShader("Shaders\\ColorVertexShader.glsl", "Shaders\\ColorFragmentShader.glsl");
            scene.Renderer.ShaderManager.AddShader("Color", colorShader);

            scene.Renderer.Start();
            UpdateSceneUI();

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
            SceneTreeView.Nodes.Clear();

            foreach (var obj in scene.GameObjects)
            {
                TreeNode n = new TreeNode();
                n.Text = "Cube";
                n.Tag = obj;
                SceneTreeView.Nodes.Add(n);
            }
        }

        private void SceneTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null || e.Node.Tag == null) { return; }

            var go = e.Node.Tag as GameObject;

            var transformcontrol = new SceneObjectEditor();
            transformcontrol.GameObject = go;
            transformcontrol.Parent = InspectorPanel;
            transformcontrol.BringToFront();
        }

        private void cubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.AddCube();
            UpdateSceneUI();
        }
    }
}
