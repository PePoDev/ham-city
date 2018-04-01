using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.Screens;

namespace GP_Final_Catapult.ScreenManager {
    static class Screen {
        public static Main main;

        public static IScreen currentScreen = new MainMenuScreen();
        public static void LoadContent(Main _main) {
            main = _main;
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

        public static void LoadScene(IScreen screenName) {
            currentScreen = screenName;
            currentScreen.LoadContent();
        }
    }
}
