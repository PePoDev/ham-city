using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.Properties;
using System.Collections.Generic;

namespace GP_Final_Catapult.Utilities{
    static class ScreenTransitions {
        private static Texture2D Circle;
        private static List<FadeItem> items = new List<FadeItem>();

		private static bool fading = false;
		private static bool FadeIn = false;

		public static void SetTexture(Texture2D _texture) => Circle = _texture;
        public static void Initialize() {
			if (FadeIn) {
				var width = Settings.Default.ScreenWidth;
				var height = Settings.Default.ScreenHeight;
				for (int xPixel = 32; xPixel - 32 < width; xPixel += 64) {
					for (int yPixel = 32; yPixel - 32 < height; yPixel += 64) {
						items.Add(new FadeItem() {
							Xpos = xPixel,
							Ypos = yPixel,
							Delay = 2000 - (xPixel + yPixel)
						});
					}
				}
			} else {
				var width = Settings.Default.ScreenWidth;
				var height = Settings.Default.ScreenHeight;
				for (int xPixel = 32; xPixel - 32 < width; xPixel += 64) {
					for (int yPixel = 32; yPixel - 32 < height; yPixel += 64) {
						items.Add(new FadeItem() {
							Xpos = xPixel,
							Ypos = yPixel,
							Delay = xPixel + yPixel
						});
					}
				}
			}
        }
        public static void Update(GameTime gameTime) {
			if (fading) {
				foreach (var item in items) {
					item.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds,FadeIn);
				}
			}
        }
        public static void Draw(SpriteBatch spriteBatch) {
            foreach(var item in items) {
                spriteBatch.Draw(Circle, new Vector2(item.Xpos, item.Ypos), null, new Color(0xB4EDD2), 0f, new Vector2(32, 32), item.GetScale(FadeIn), SpriteEffects.None, 0);
            }
        }
		public static void FadeIN() {
			fading = true;
			FadeIn = true;
			items.Clear();
			Initialize();
		}
		public static void FadeOUT() {
			fading = true;
			FadeIn = false;
			items.Clear();
			Initialize();
		}
		public static void StopFading() {
			fading = false;
		}
	}
}