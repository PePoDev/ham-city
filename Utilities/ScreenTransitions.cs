using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.Properties;
using System.Collections.Generic;

namespace GP_Final_Catapult.Utilities{
    static class ScreenTransitions {
        private static Texture2D Circle;
        private static float scale;
        private static List<FadeItem> items = new List<FadeItem>();

        public static void SetTexture(Texture2D _texture) {
            Circle = _texture;
        }
        public static void Initialize() {
            int width = Settings.Default.ScreenWidth;
            int height = Settings.Default.ScreenHeight;
            for(int xPixel = 32; xPixel - 32 < width; xPixel += 64) {
                for(int yPixel = 32; yPixel - 32 < height; yPixel += 64) {
                    items.Add(new FadeItem() {
                        Xpos = xPixel,
                        Ypos = yPixel,
                        Delay = xPixel + yPixel
                    });
                }
            }
        }
        public static void Update(GameTime gameTime) {
            foreach(var item in items) {
                item.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            //scale = (float)System.Math.Cos(gameTime.TotalGameTime.TotalMilliseconds / 200.0) + 1;
        }
        public static void Draw(SpriteBatch spriteBatch) {
            foreach(var item in items) {
                spriteBatch.Draw(Circle, new Vector2(item.Xpos, item.Ypos), null, Color.White, 0f, new Vector2(32, 32), item.Scale, SpriteEffects.None, 0);
            }
        }
    }
}