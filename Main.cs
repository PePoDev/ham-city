using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GP_Final_Catapult.Properties;
using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Utilities;
using System.Text;
using QuakeConsole;

namespace GP_Final_Catapult {

    public class Main : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ConsoleComponent console;

        private StringBuilder stringBuilder = new StringBuilder();
        private SpriteFont Jacklane;
        private Texture2D Cursor;

        private float deltaTime;
        public Main() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Set windows start posion to center screen
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            // Set Width and Height og game screen
            graphics.PreferredBackBufferWidth = Settings.Default.ScreenWidth;
            graphics.PreferredBackBufferHeight = Settings.Default.ScreenHeight;
            // For detect FPS in FramerateCounter.cs
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            // Restore fullscreen setting
            if (Settings.Default.FullScreen) {
                graphics.ToggleFullScreen();
            }
            graphics.ApplyChanges();

            // Initial QuakeConsole
            console = new ConsoleComponent(this);
            Components.Add(console);
            // Add interpreter for QuakeConsole
            var pythonInterpreter = new PythonInterpreter();
            var manualInterpreter = new ManualInterpreter();
            console.Interpreter = manualInterpreter;

            pythonInterpreter.AddVariable("name", graphics);
            manualInterpreter.RegisterCommand("Unlock", args => {  });
        }
        protected override void Initialize() {
            base.Initialize();
        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Jacklane = Content.Load<SpriteFont>("Fonts/Jacklane");
            Cursor = Content.Load<Texture2D>("TransitionEffect/Circle");
            ScreenManager.LoadContent(this);
        }
        protected override void UnloadContent() {
            ScreenManager.UnloadContent();
            Content.Unload();
        }
        protected override void Update(GameTime gameTime) {
            InputManager.Update(gameTime);
            ScreenManager.Update(gameTime);
            // ToggleConsole when press F12
            if (InputManager.OnKeyDown(Keys.F12))
                console.ToggleOpenClose();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // CAll ScreenManager
            ScreenManager.Draw(spriteBatch);
            // Draw cursor in game
            spriteBatch.Draw(Cursor,InputManager.GetMousePosition(),Color.White);
            // Draw FPS if steting enable
            if (Settings.Default.ShowFPS) {
                deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FramerateCounter.Update(deltaTime);
                stringBuilder.Clear();
                stringBuilder.Append("FPS : ");
                stringBuilder.Append(FramerateCounter.AverageFramesPerSecond.ToString("F"));
                spriteBatch.DrawString(Jacklane, stringBuilder, new Vector2(1020, 10), Color.Yellow);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
