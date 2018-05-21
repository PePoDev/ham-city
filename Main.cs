using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GP_Final_Catapult.Properties;
using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Utilities;
using System.Text;
using QuakeConsole;
using GP_Final_Catapult.Screens;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GP_Final_Catapult {

    public class Main : Game {
		public static Game self;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ConsoleComponent console;

		private StringBuilder stringBuilder = new StringBuilder();
        private SpriteFont Jacklane;
        private Texture2D Cursor;
        private Texture2D Circle;
		private Song BGM;

		public PythonInterpreter pythonInterpreter;
		public ManualInterpreter manualInterpreter;

		private float deltaTime;
        public Main() {
			self = this;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // Set Width and Height og game screen
            graphics.PreferredBackBufferWidth = Settings.Default.ScreenWidth;
            graphics.PreferredBackBufferHeight = Settings.Default.ScreenHeight;
            
            // Set windows start posion to center screen
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            
            // For detect FPS in FramerateCounter.cs
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            // Restore fullscreen setting
            if (Settings.Default.FullScreen)
                graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            // Initial ScreenTransition
			ScreenTransitions.FadeOUT();

			// Initial QuakeConsole
			console = new ConsoleComponent(this);
            Components.Add(console);
            
            // Add interpreter for QuakeConsole
            pythonInterpreter = new PythonInterpreter();
            manualInterpreter = new ManualInterpreter();
            console.Interpreter = manualInterpreter;
            
            // Add variable for PythonInterpreter
            pythonInterpreter.AddVariable("console", console);
            pythonInterpreter.AddVariable("manual", manualInterpreter);
            
            // Add command for ManualInterpreter
            manualInterpreter.RegisterCommand("fullscreen", args => {
                if (args.Length == 0)
                    return;
                else if (graphics.IsFullScreen && args[0].Equals("off"))
                    graphics.ToggleFullScreen();
                else if (!graphics.IsFullScreen && args[0].Equals("on"))
                    graphics.ToggleFullScreen();
            });
            manualInterpreter.RegisterCommand("fps", args => {
                if (args.Length == 0)
                    return;
                else if (args[0].Equals("on")) {
                    Settings.Default.ShowFPS = true;
                    Settings.Default.Save();
                } else if (args[0].Equals("off")) {
                    Settings.Default.ShowFPS = false;
                    Settings.Default.Save();
                }
            });
            manualInterpreter.RegisterCommand("console.Interpreter", args => {
                if (args.Length < 2)
                    return;
                else if (args[0].Equals("=") & args[1].Equals("python"))
                    console.Interpreter = pythonInterpreter;
            });
            manualInterpreter.RegisterCommand("exit", args => { Exit(); });
			manualInterpreter.RegisterCommand("ResetLevel", args => { ScreenManager.LoadScreen(new GamePlayScreen()); });

			BGM = Content.Load<Song>("Audios/POL-mad-run-short");
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = Settings.Default.BGMVolume;
			MediaPlayer.Play(BGM);

			AudioManager.AddAudioEffect("click", Content.Load<SoundEffect>("Audios/NFF-select").CreateInstance());
			AudioManager.AddAudioEffect("shoot", Content.Load<SoundEffect>("Audios/NFF-rasp").CreateInstance());
			AudioManager.AddAudioEffect("die", Content.Load<SoundEffect>("Audios/NFF-lose").CreateInstance());
			AudioManager.AddAudioEffect("switch", Content.Load<SoundEffect>("Audios/NFF-click-switch").CreateInstance());
		}
        protected override void Initialize() {
            base.Initialize();
        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Jacklane = Content.Load<SpriteFont>("Fonts/Jacklane");
            Cursor = Content.Load<Texture2D>("Sprites/cursor");
            Circle = Content.Load<Texture2D>("TransitionEffect/Circle");
            ScreenTransitions.SetTexture(Circle);
            ScreenManager.LoadContent();
        }
        protected override void UnloadContent() {
            ScreenManager.UnloadContent();
            Content.Unload();
        }
        protected override void Update(GameTime gameTime) {
			MediaPlayer.Volume = Settings.Default.BGMVolume;

			// CALL InputManager
			InputManager.Update(gameTime);

            // CALL ScreenManager
            ScreenManager.Update(gameTime);

            // CALL ScreenTransition effect
            ScreenTransitions.Update(gameTime);

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
            
            // Draw FPS if steting enable
            if (Settings.Default.ShowFPS) {
                deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FramerateCounter.Update(deltaTime);
                stringBuilder.Clear();
                stringBuilder.Append("FPS : ");
                stringBuilder.Append(FramerateCounter.AverageFramesPerSecond.ToString("F"));
                spriteBatch.DrawString(Jacklane, stringBuilder, new Vector2(1020, 10), Color.Yellow);
			}

			// CALL ScreenTransition effect
			ScreenTransitions.Draw(spriteBatch);

			// Draw cursor in game
			spriteBatch.Draw(Cursor, InputManager.GetMousePosition(), Color.White);

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
