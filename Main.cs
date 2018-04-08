using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.Properties;
using GP_Final_Catapult.Screens;
using GP_Final_Catapult.Managers;
using Microsoft.Xna.Framework.Media;

namespace GP_Final_Catapult {

    public class Main : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Song BGM;

        public Main() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Settings.Default.ScreenWidth;
            graphics.PreferredBackBufferHeight = Settings.Default.ScreenHeight;
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            graphics.ApplyChanges();
        }
        protected override void Initialize() {
            base.Initialize();
            BGM = Content.Load<Song>("Audios/2000_Town3");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Settings.Default.BGMVolume;
            MediaPlayer.Play(BGM);
        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.LoadContent(this);
        }
        protected override void UnloadContent() {
            ScreenManager.UnloadContent();
            Content.Unload();
        }
        protected override void Update(GameTime gameTime) {
            ScreenManager.Update(gameTime);
            MediaPlayer.Volume = Settings.Default.BGMVolume;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            ScreenManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
