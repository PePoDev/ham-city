using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP_Final_Catapult.Components {
    class Transform : IComponent{
        public Vector2 Position, Scale;
        public float Rotation;

        public Transform() {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
        }

    }
}
