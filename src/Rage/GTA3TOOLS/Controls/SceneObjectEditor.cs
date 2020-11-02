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
        public RenderableModel model;

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
            PositionXTextbox.Text = model.Position.X.ToString();
            PositionYTextbox.Text = model.Position.Y.ToString();
            PositionZTextbox.Text = model.Position.Z.ToString();
            RotationXTextbox.Text = model.Rotation.X.ToString();
            RotationYTextbox.Text = model.Rotation.Y.ToString();
            RotationZTextbox.Text = model.Rotation.Z.ToString();
            ScaleXTextbox.Text = model.Scale.X.ToString();
            ScaleYTextbox.Text = model.Scale.Y.ToString();
            ScaleZTextbox.Text = model.Scale.Z.ToString();
        }

        private void ScaleZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleZTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Scale.Z = value;
            }
        }

        private void ScaleYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleYTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Scale.Y = value;
            }
        }

        private void ScaleXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(ScaleXTextbox.Text, out value);;
            if (isFloat == true)
            {
                model.Scale.X = value;
            }
        }

        private void RotationZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationZTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Rotation.Z = value;
            }
        }

        private void RotationYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationYTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Rotation.Y = value;
            }
        }

        private void RotationXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(RotationXTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Rotation.X = value;
            }
        }

        private void PositionZTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionZTextbox.Text, out value);
            if (value != 0)
            {
                model.Position.Z = value;
            }
        }

        private void PositionYTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionYTextbox.Text, out value);
            if (isFloat == true)
            {
                model.Position.Y = value;
            }
        }

        private void PositionXTextbox_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            bool isFloat = float.TryParse(PositionXTextbox.Text, out value);
            if(isFloat == true)
            {
                model.Position.X = value;
            }
        }

        private void SceneObjectEditor_Load(object sender, EventArgs e)
        {
            UpdateText();
        }
    }
}
