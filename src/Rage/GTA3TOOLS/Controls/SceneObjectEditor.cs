using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RenderwareEngine;

namespace GTA3TOOLS.Controls
{
    public partial class SceneObjectEditor : UserControl
    {
        public GameObject GameObject;

        public SceneObjectEditor()
        {
            InitializeComponent();

            PositionXTextbox.TextChanged += PositionXTextbox_TextChanged;
            PositionYTextbox.TextChanged += PositionYTextbox_TextChanged;
            PositionZTextbox.TextChanged += PositionZTextbox_TextChanged;

            RotationXTextbox.TextChanged += RotationXTextbox_TextChanged;
            RotationYTextbox.TextChanged += RotationYTextbox_TextChanged;
            RotationZTextbox.TextChanged += RotationZTextbox_TextChanged;

            ScaleXTextbox.TextChanged += ScaleXTextbox_TextChanged;
            ScaleYTextbox.TextChanged += ScaleYTextbox_TextChanged;
            ScaleZTextbox.TextChanged += ScaleZTextbox_TextChanged;

        }

        private void UpdateText()
        {
            PositionXTextbox.Text = GameObject.Transform.Position.X.ToString();
            PositionYTextbox.Text = GameObject.Transform.Position.Y.ToString();
            PositionZTextbox.Text = GameObject.Transform.Position.Z.ToString();
            RotationXTextbox.Text = GameObject.Transform.Rotation.X.ToString();
            RotationYTextbox.Text = GameObject.Transform.Rotation.Y.ToString();
            RotationZTextbox.Text = GameObject.Transform.Rotation.Z.ToString();
            ScaleXTextbox.Text = GameObject.Transform.Scale.X.ToString();
            ScaleYTextbox.Text = GameObject.Transform.Scale.Y.ToString();
            ScaleZTextbox.Text = GameObject.Transform.Scale.Z.ToString();
        }

        private void ScaleZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleZTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Scale.Z = value;
            }
        }

        private void ScaleYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleYTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Scale.Y = value;
            }
        }

        private void ScaleXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleXTextbox.Text, out value);;
            if (isFloat == true)
            {
                GameObject.Transform.Scale.X = value;
            }
        }

        private void RotationZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationZTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Rotation.Z = value;
            }
        }

        private void RotationYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationYTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Rotation.Y = value;
            }
        }

        private void RotationXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationXTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Rotation.X = value;
            }
        }

        private void PositionZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionZTextbox.Text, out value);
            if (value != 0)
            {
                GameObject.Transform.Position.Z = value;
            }
        }

        private void PositionYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionYTextbox.Text, out value);
            if (isFloat == true)
            {
                GameObject.Transform.Position.Y = value;
            }
        }

        private void PositionXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionXTextbox.Text, out value);
            if(isFloat == true)
            {
                GameObject.Transform.Position.X = value;
            }
        }

        private void SceneObjectEditor_Load(object sender, EventArgs e)
        {
            UpdateText();
        }
    }
}
