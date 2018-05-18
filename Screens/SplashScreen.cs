using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Screens {
	class SplashScreen : IScreen {
		private Texture2D โลโก้;

		public override void LoadContent() {
			base.LoadContent();
			โลโก้ = Content.Load<Texture2D>("");
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {

		}
		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw(โลโก้, new Vector2(โลโก้.Width, โลโก้.Height), Color.White);
		}
	}
}
