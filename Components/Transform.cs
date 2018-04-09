using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP_Final_Catapult.Components {
    class Transform {
        protected Vector2 transform, rotation, scale;
        Transform(Vector2 transform, Vector2 rotation,Vector2 scale) {
            this.transform = transform; this.rotation = rotation; this.scale = scale;
        }


    }
}
