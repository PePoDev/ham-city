using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GP_Final_Catapult.Screens;

namespace GP_Final_Catapult.Managers {
    static class ScreenManager {
        public static ContentManager Content { private set; get; }
        public static IScreen currentScreen = new SplashScreen();

        public static void LoadContent() {
            Content = new ContentManager(Main.self.Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }
        public static void UnloadContent() {
            currentScreen.UnloadContent();
        }
        public static void Update(GameTime gameTime) {
            currentScreen.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch) {
            currentScreen.Draw(spriteBatch);
        }
        public static void LoadScreen(IScreen screenName) {
            currentScreen = screenName;
            currentScreen.LoadContent();
        }
	}
}
