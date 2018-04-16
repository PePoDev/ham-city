using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Components {
    class Transform : IComponent{
        public Vector2 Position, Scale;
        public float Rotation;

        public Transform() {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
        }
		public Transform(int x, int y) {
			Position = new Vector2(x,y);
			Scale = Vector2.One;
			Rotation = 0f;
		}
		public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }
    }
}
