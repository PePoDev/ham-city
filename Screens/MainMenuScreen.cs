using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Properties;
using GP_Final_Catapult.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GP_Final_Catapult.Screens {
	class MainMenuScreen : IScreen {
		private Texture2D BG, AboutTexture, SelectLevelTexture, ExitTexture, OptionTexture, CreateMapTexture;
		private Texture2D LockTexture, Star1, Star2, Star3, LockLevel, BossLevel, NormalLevel;
		private SpriteFont font;

		private string[] LevelStatus;
		private int SelectID = 0;
		private int PanelID = 0;
		private float time = 0f;
		private bool Fade = false;

		public override void LoadContent() {
			base.LoadContent();
			BG = Content.Load<Texture2D>("Sprites/Menu/BG_game_menu");
			SelectLevelTexture = Content.Load<Texture2D>("Sprites/Menu/Select-level_click");
			CreateMapTexture = Content.Load<Texture2D>("Sprites/Menu/Create-maps_click");
			OptionTexture = Content.Load<Texture2D>("Sprites/Menu/Option_click");
			AboutTexture = Content.Load<Texture2D>("Sprites/Menu/About_click");
			ExitTexture = Content.Load<Texture2D>("Sprites/Menu/Exit_click");
			
			LockTexture = Content.Load<Texture2D>("Sprites/lock");
			Star1 = Content.Load<Texture2D>("Sprites/Level/1star");
			Star2 = Content.Load<Texture2D>("Sprites/Level/2star");
			Star3 = Content.Load<Texture2D>("Sprites/Level/3star");
			LockLevel = Content.Load<Texture2D>("Sprites/Level/lock");
			BossLevel = Content.Load<Texture2D>("Sprites/Level/boss");
			NormalLevel = Content.Load<Texture2D>("Sprites/Level/normal");
			font = Content.Load<SpriteFont>("Fonts/Baffled");

			Initial();
		}
		private void Initial() {
			ScreenTransitions.FadeOUT();
			LevelStatus = Settings.Default.LevelStatus.Split('/');
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
			var mousePosition = InputManager.GetMousePosition();

			if (!Fade) {
				// OnMouseHover SeletLevel Button
				if ((mousePosition.X > 776 && mousePosition.Y > 344) && (mousePosition.X < 973 && mousePosition.Y < 454)) {
					SelectID = 1;
					if (InputManager.OnMouseDown(new Rectangle(776, 344, 197, 100))) {
						AudioManager.PlayAudio("click");
						PanelID = 1;
					}
				} /* CreateMap */ else if ((mousePosition.X > 998 && mousePosition.Y > 426) && (mousePosition.X < 1217 && mousePosition.Y < 559)) {
					SelectID = 2;
					if (InputManager.OnMouseDown(new Rectangle(998, 426, 219, 133))) {
						AudioManager.PlayAudio("click");
						PanelID = 2;
					}
				} /* Option */ else if ((mousePosition.X > 849 && mousePosition.Y > 446) && (mousePosition.X < 974 && mousePosition.Y < 575)) {
					SelectID = 3;
					if (InputManager.OnMouseDown(new Rectangle(849, 446, 125, 129))) {
						AudioManager.PlayAudio("click");
						PanelID = 3;
					}
				} /* Exit */ else if ((mousePosition.X > 998 && mousePosition.Y > 559) && (mousePosition.X < 1181 && mousePosition.Y < 715)) {
					SelectID = 4;
					if (InputManager.OnMouseDown(new Rectangle(998, 559, 183, 156))) {
						AudioManager.PlayAudio("click");
						Main.self.Exit();
					}
				} /* About */ else if ((mousePosition.X > 805 && mousePosition.Y > 604) && (mousePosition.X < 965 && mousePosition.Y < 690)) {
					SelectID = 5;
					if (InputManager.OnMouseDown(new Rectangle(805, 604, 160, 86))) {
						AudioManager.PlayAudio("click");
						PanelID = 4;
					}
				} else {
					SelectID = 0;
				}

				// Panal OnMouseDown
				if (PanelID != 0) {
					if (InputManager.OnMouseDown(new Rectangle(0, 0, 100, 100))) {
						AudioManager.PlayAudio("click");
						PanelID = 0;
					}
				}

				// Panel Select Level
				if (InputManager.OnMouseDown(new Rectangle(100, 300, 101, 84)) && !LevelStatus[0].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 1;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(200, 300, 101, 84)) && !LevelStatus[1].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 2;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(300, 300, 101, 84)) && !LevelStatus[2].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 3;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(400, 300, 101, 84)) && !LevelStatus[3].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 4;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(500, 300, 101, 84)) && !LevelStatus[4].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 5;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(100, 425, 101, 84)) && !LevelStatus[5].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 6;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(200, 425, 101, 84)) && !LevelStatus[6].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 7;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(300, 425, 101, 84)) && !LevelStatus[7].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 8;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(400, 425, 101, 84)) && !LevelStatus[8].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 9;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
				if (InputManager.OnMouseDown(new Rectangle(500, 425, 101, 84)) && !LevelStatus[9].Equals("-1")) {
					AudioManager.PlayAudio("click");
					Settings.Default.LevelSelected = 10;
					Settings.Default.Save();
					Fade = true;
					ScreenTransitions.FadeIN();
				}
			} else {
				time += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				if (time > 3f) ScreenManager.LoadScreen(new GamePlayScreen());
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
					var point = new Vector2(100,300);
					var i = 1;
					var ii = 1;
					foreach (string level in LevelStatus) {
						switch (level) {
							case "-1":
								spriteBatch.Draw(LockLevel, new Vector2(point.X * i, point.Y), Color.White);
								break;
							case "0":
								if (ii % 3 == 0 || ii == 10)
									spriteBatch.Draw(BossLevel, new Vector2(point.X * i, point.Y), Color.White);
								else
									spriteBatch.Draw(NormalLevel, new Vector2(point.X * i, point.Y), Color.White);
								break;
							case "1":
								spriteBatch.Draw(Star1, new Vector2(point.X * i, point.Y), Color.White);
								break;
							case "2":
								spriteBatch.Draw(Star2, new Vector2(point.X * i, point.Y), Color.White);
								break;
							case "3":
								spriteBatch.Draw(Star3, new Vector2(point.X * i, point.Y), Color.White);
								break;
						}
						i++;
						ii++;
						if (i == 6) {
							i = 1;
							point.Y += 125;
						}
					}
					break;
				case 2:
					spriteBatch.Draw(LockTexture, new Vector2(100, 360 - LockTexture.Height / 2), Color.White); 
					spriteBatch.DrawString(font, "Coming Soon . . .", new Vector2(250, 360), Color.Black);
					break;
				case 3:
					spriteBatch.DrawString(font, "Don't setting, Just play.", new Vector2(200, 360), Color.Black);
					break;
				case 4:
					spriteBatch.DrawString(font, "Heyyyy just play please.", new Vector2(150, 360), Color.Black);
					break;
			}
		}
	}
}
