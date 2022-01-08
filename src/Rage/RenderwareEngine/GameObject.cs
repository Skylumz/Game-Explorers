using RenderwareEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderwareEngine
{
    public class GameObject
    {
        public string Name { get; set; }
        public Transform Transform { get; set; }
        public RenderableModel Model { get; set; }

        public GameObject(Transform t, RenderableModel m)
        {
            Transform = t;
            Model = m;
        }
    }
}
