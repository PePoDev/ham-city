using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.ScreenManager;

namespace GP_Final_Catapult {

    public class Main : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Main() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (int)Singleton.Dimension.X;
            graphics.PreferredBackBufferHeight = (int)Singleton.Dimension.Y;
            IsMouseVisible = true;

        }
        protected override void Initialize() {
            base.Initialize();
        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Screen.LoadContent(this);
        }
        protected override void UnloadContent() {
            Content.Unload();
            Screen.UnloadContent();
        }
        protected override void Update(GameTime gameTime) {
            Screen.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            BeginDraw();
            Screen.Draw(spriteBatch);
            EndDraw();
            base.Draw(gameTime);
        }
    }
}
