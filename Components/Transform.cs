using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Components {
    class Transform : IComponent{
		public Vector2 position;
		public Vector2 scale;
        public float rotation;

        public Transform() {
			position = Vector2.Zero;
			scale = Vector2.One;
            rotation = 0f;
        }
		public Transform(Vector2 position) {
			this.position = position;
			scale = Vector2.One;
			rotation = 0f;
		}
    }
}
