using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Screens {
	class MainMenuScreen : IScreen {
		private Texture2D BG, AboutTexture, SelectLevelTexture, ExitTexture, OptionTexture, CreateMapTexture;
		private Texture2D SelectLevelPanel, LockTexture, Star1, Star2, Star3;
		private SpriteFont font;

		private int SelectID = 0;
		private int PanelID = 0;

		public override void LoadContent() {
			base.LoadContent();
			BG = Content.Load<Texture2D>("Sprites/Menu/BG_game_menu");
			SelectLevelTexture = Content.Load<Texture2D>("Sprites/Menu/Select-level_click");
			CreateMapTexture = Content.Load<Texture2D>("Sprites/Menu/Create-maps_click");
			OptionTexture = Content.Load<Texture2D>("Sprites/Menu/Option_click");
			AboutTexture = Content.Load<Texture2D>("Sprites/Menu/About_click");
			ExitTexture = Content.Load<Texture2D>("Sprites/Menu/Exit_click");

			SelectLevelPanel = Content.Load<Texture2D>("Sprites/Level");
			LockTexture = Content.Load<Texture2D>("Sprites/lock");
			Star1 = Content.Load<Texture2D>("Sprites/1start");
			Star2 = Content.Load<Texture2D>("Sprites/2start");
			Star3 = Content.Load<Texture2D>("Sprites/3start");

			font = Content.Load<SpriteFont>("Fonts/Baffled");

			Initial();
		}
		private void Initial() {
			ScreenTransitions.FadeOUT();
			var LevelStatus = Properties.Settings.Default.LevelStatus.Split('/');
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
			var mousePosition = InputManager.GetMousePosition();

			// OnMouseHover SeletLevel Button
			if ((mousePosition.X > 776 && mousePosition.Y > 344) && (mousePosition.X < 973 && mousePosition.Y < 454)) {
				SelectID = 1;
				if (InputManager.OnMouseDown(new Rectangle(776, 344, 197, 100))) {
					PanelID = 1;
				}
			} /* CreateMap */ else if ((mousePosition.X > 998 && mousePosition.Y > 426) && (mousePosition.X < 1217 && mousePosition.Y < 559)) {
				SelectID = 2;
				if (InputManager.OnMouseDown(new Rectangle(998, 426, 219, 133))) {
					PanelID = 2;
				}
			} /* Option */ else if ((mousePosition.X > 849 && mousePosition.Y > 446) && (mousePosition.X < 974 && mousePosition.Y < 575)) {
				SelectID = 3;
				if (InputManager.OnMouseDown(new Rectangle(849, 446, 125, 129))) {
					PanelID = 3;
				}
			} /* Exit */ else if ((mousePosition.X > 998 && mousePosition.Y > 559) && (mousePosition.X < 1181 && mousePosition.Y < 715)) {
				SelectID = 4;
				if (InputManager.OnMouseDown(new Rectangle(998, 559, 183, 156))) {
					Main.self.Exit();
				}
			} /* About */ else if ((mousePosition.X > 805 && mousePosition.Y > 604) && (mousePosition.X < 965 && mousePosition.Y < 690)) {
				SelectID = 5;
				if (InputManager.OnMouseDown(new Rectangle(805, 604, 160, 86))) {
					PanelID = 4;
				}
			} else {
				SelectID = 0;
			}

			// Panal OnMouseDown
			if (PanelID != 0) {
				if (InputManager.OnMouseDown(new Rectangle(998, 559, 183, 156))) {
					PanelID = 0;
				}
			}
		}
		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw(BG, Vector2.Zero, Color.White);
			switch (SelectID) {
				case 1:
					spriteBatch.Draw(SelectLevelTexture, new Vector2(771, 339), Color.White);
					break;
				case 2:
					spriteBatch.Draw(CreateMapTexture, new Vector2(998, 426), Color.White);
					break;
				case 3:
					spriteBatch.Draw(OptionTexture, new Vector2(844, 466), Color.White);
					break;
				case 4:
					spriteBatch.Draw(ExitTexture, new Vector2(998, 559), Color.White);
					break;
				case 5:
					spriteBatch.Draw(AboutTexture, new Vector2(805, 604), Color.White);
					break;
			}
			switch (PanelID) {
				case 1:
					spriteBatch.Draw(SelectLevelPanel, new Vector2(75, 360 - SelectLevelPanel.Height/2), new Color(Color.White,0.4f));

					break;
				case 2:
					spriteBatch.Draw(LockTexture, new Vector2(100, 360 - LockTexture.Height / 2), Color.White); 
					spriteBatch.DrawString(font, "Coming Soon . . .", new Vector2(250, 360), Color.Black);
					break;
				case 3:

					break;
				case 4:

					break;
			}
		}
	}
}
