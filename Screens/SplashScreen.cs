using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Properties;
using GP_Final_Catapult.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GP_Final_Catapult.Screens {
	class SplashScreen : IScreen {
		private Texture2D โลโก้;

		private float time = 0f;
		private bool CallFaded = false;

		public override void LoadContent() {
			base.LoadContent();
			โลโก้ = Content.Load<Texture2D>("Sprites/logo");
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {
			time += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
			if (time > 3f && !CallFaded) {
				CallFaded = true;
				ScreenTransitions.FadeIN();
			} else if (time > 6f) {
				ScreenManager.LoadScreen(new MainMenuScreen());
			}
			Console.WriteLine(time);
		}
		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.GraphicsDevice.Clear(Color.Black);
			spriteBatch.Draw(โลโก้, new Vector2((Settings.Default.ScreenWidth / 2) - โลโก้.Width / 2, (Settings.Default.ScreenHeight / 2) - โลโก้.Height / 2),Color.White);
		}
	}
}
