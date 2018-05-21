using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using GP_Final_Catapult.Utilities;
using GP_Final_Catapult.Managers;
using System;
using GP_Final_Catapult.Properties;

namespace GP_Final_Catapult.Screens {
	class GamePlayScreen : IScreen {
		private Texture2D enemyHumanTexture, enemyMonsterTexture, boss1Texture, boss2Texture, boss3Texture, bossSoulTexture, fairyTexture, PlayerTexture;
		private Texture2D catapultFrontTexture, catapultBackTexture, Star1, Star2, Star3;
		private Texture2D bulletTexture, wallCanDestroy, wallCanNotDestroy, Door, wallCanDestroyStand, wallCanNotDestroyStand, DoorStand, Trigger;
		private Texture2D Home, Restart, Next;
		private Texture2D BG_Normal, BG_Dark, BlackPanel;
		private SpriteFont font;

		private List<IGameObject> LightObj = new List<IGameObject>();
		private List<IGameObject> NightObj = new List<IGameObject>();
		private IGameObject BulletObj;

		public bool GodMode = false;
		private float time;
		private int star = 0;
		private int LevelNo;
		private bool isWin = false;

		public override void LoadContent() {
			base.LoadContent();
			enemyHumanTexture = Content.Load<Texture2D>("Sprites/Character/theif");
			enemyMonsterTexture = Content.Load<Texture2D>("Sprites/Character/monster");
			boss1Texture = Content.Load<Texture2D>("Sprites/Character/boss_monk");
			boss2Texture = Content.Load<Texture2D>("Sprites/Character/boss_watch");
			boss3Texture = Content.Load<Texture2D>("Sprites/Character/boss_blackpanter");
			bossSoulTexture = Content.Load<Texture2D>("Sprites/Character/Boss_soul");
			fairyTexture = Content.Load<Texture2D>("Sprites/Character/fariy");
			PlayerTexture = Content.Load<Texture2D>("Sprites/Character/player");
			catapultFrontTexture = Content.Load<Texture2D>("Sprites/CatapultFront");
			catapultBackTexture = Content.Load<Texture2D>("Sprites/CatapultBack");
			bulletTexture = Content.Load<Texture2D>("Sprites/Bullet");

			wallCanDestroy = Content.Load<Texture2D>("Sprites/WallCanDestroy");
			wallCanNotDestroy = Content.Load<Texture2D>("Sprites/WallCantDestroy");
			Door = Content.Load<Texture2D>("Sprites/WallTrigger");

			wallCanDestroyStand = Content.Load<Texture2D>("Sprites/WallCanDestroyStand");
			wallCanNotDestroyStand = Content.Load<Texture2D>("Sprites/WallCantDestroyStand");
			DoorStand = Content.Load<Texture2D>("Sprites/WallTriggerStand");

			Trigger = Content.Load<Texture2D>("Sprites/control-lever");

			BG_Normal = Content.Load<Texture2D>("Sprites/BG_game_normal");
			BG_Dark = Content.Load<Texture2D>("Sprites/BG_game_dark");
			font = Content.Load<SpriteFont>("fonts/Kaiju");

			Star1 = Content.Load<Texture2D>("Sprites/1start");
			Star2 = Content.Load<Texture2D>("Sprites/2start");
			Star3 = Content.Load<Texture2D>("Sprites/3start");
			BlackPanel = Content.Load<Texture2D>("Sprites/fadeBlack");

			Home = Content.Load<Texture2D>("Sprites/home");
			Restart = Content.Load<Texture2D>("Sprites/lv8-1");
			Next = Content.Load<Texture2D>("Sprites/next");

			Initial();
		}
		public void Initial() {
			ScreenTransitions.FadeOUT();
			LevelNo = Settings.Default.LevelSelected;

			// Create bullet
			BulletObj = ObjectCreate.CreateBullet(new Vector2(250, 500), bulletTexture);

			switch (LevelNo) {
				case 0:
					// Create Enemy
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(500, 600), enemyHumanTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(700, 500), enemyMonsterTexture));
					LightObj.Add(ObjectCreate.CreateBoss1(new Vector2(900, 500), boss1Texture));
					LightObj.Add(ObjectCreate.CreateBoss2(new Vector2(1100, 500), boss2Texture));
					LightObj.Add(ObjectCreate.CreateBoss3(new Vector2(500, 200), boss3Texture));

					// Create wall
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(700, 200), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(900, 200), wallCanNotDestroy, "CanNotDestroy"));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(700, 200), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(900, 200), wallCanNotDestroyStand, "CanNotDestroy"));

					// Create door
					var door = ObjectCreate.CreateWall(new Vector2(1100, 500), Door, "Door");
					NightObj.Add(door);

					var doorList = new List<IGameObject>();
					doorList.Add(door);

					// Create trigger
					NightObj.Add(ObjectCreate.CreateTrigger(new Vector2(900, 500), Trigger, doorList));

					break;
				case 1:

					break;
				case 2:
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(794, 249)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(771, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(832, 499), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWall(new Vector2(795, 444), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(772, 444), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(989, 444), wallCanDestroyStand, "CanDestroy"));
					break;
				case 3:

					break;
				case 4:

					break;
				case 5:

					break;
				case 6:

					break;
				case 7:

					break;
				case 8:

					break;
				case 9:

					break;
				case 10:

					break;
			}
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {
			if (!isWin) {
				time += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

				if (InputManager.OnKeyDown(Keys.Space) && !((Bullet)BulletObj).HitedObj && !((Bullet)BulletObj).isFly) GodMode = !GodMode;

				if (GodMode) {
					BulletObj.Update(gameTime, NightObj);
					NightObj.ForEach(obj => obj.Update(gameTime, NightObj));
				} else {
					BulletObj.Update(gameTime, LightObj);
					LightObj.ForEach(obj => obj.Update(gameTime, LightObj));
				}

				WinDetect();

			} else {
				// Restart
				if (InputManager.OnMouseDown(new Rectangle(100, 500, 300, 110))) {
					var levelStatus = Settings.Default.LevelStatus.Split('/');
					levelStatus[LevelNo - 1] = "3";
					levelStatus[LevelNo] = "0";
					Settings.Default.LevelStatus = String.Join("/",levelStatus);
					Settings.Default.Save();
					ScreenManager.LoadScreen(new GamePlayScreen());
				}
				// Home
				if (InputManager.OnMouseDown(new Rectangle(450, 500, 300, 110))) {
					var levelStatus = Settings.Default.LevelStatus.Split('/');
					levelStatus[LevelNo - 1] = "3";
					levelStatus[LevelNo] = "0";
					Settings.Default.LevelStatus = String.Join("/", levelStatus);
					Settings.Default.Save();
					ScreenManager.LoadScreen(new MainMenuScreen());
				}
				// Next Level
				if (InputManager.OnMouseDown(new Rectangle(800, 500, 300, 110))) {
					var levelStatus = Settings.Default.LevelStatus.Split('/');
					levelStatus[LevelNo - 1] = "3";
					levelStatus[LevelNo] = "0";
					Settings.Default.LevelStatus = String.Join("/", levelStatus);
					Settings.Default.LevelSelected = LevelNo + 1;
					Settings.Default.Save();
					ScreenManager.LoadScreen(new GamePlayScreen());
				}
			}
		}
		private void WinDetect() {
			if (!hasEnemy(NightObj) && !hasEnemy(LightObj)) {
				star++;
				// AudioManager.Play("win");
				switch (LevelNo) {
					case 1:
						star += 2;
						isWin = true;
						break;
					case 2:
						star += 2;
						isWin = true;
						break;
				}
			}
		}
		private bool hasEnemy(List<IGameObject> objects) {
			var HasEnemy = false;
			objects.ForEach(GO => {
				if (GO.Name.Equals("enemy")) {
					HasEnemy = true;
				}
			});
			return HasEnemy;
		}
		public override void Draw(SpriteBatch spriteBatch) {
			// Draw game BG between Dark and Normal mode
			spriteBatch.Draw(GodMode ? BG_Dark : BG_Normal, Vector2.Zero, Color.White);

			if (GodMode) {
				NightObj.ForEach(obj => obj.Draw(spriteBatch));
			} else {
				LightObj.ForEach(obj => obj.Draw(spriteBatch));
			}

			spriteBatch.DrawString(font, "Time : " + time.ToString("0.00"), new Vector2(25, 25), Color.Black);

			spriteBatch.Draw(catapultBackTexture, new Vector2(200, 470), Color.White);
			BulletObj.Draw(spriteBatch);
			spriteBatch.Draw(catapultFrontTexture, new Vector2(200, 470), Color.White);

			if (isWin) {
				spriteBatch.Draw(BlackPanel, Vector2.Zero, new Color(Color.Black, 0.8f));
				if (star == 1) {
					spriteBatch.Draw(Star1, new Vector2(640 - Star1.Width / 2, 360 - Star1.Height), Color.White);
				} else if (star == 2) {
					spriteBatch.Draw(Star2, new Vector2(640 - Star1.Width / 2, 360 - Star1.Height), Color.White);
				} else if (star == 3) {
					spriteBatch.Draw(Star3, new Vector2(640 - Star1.Width / 2, 360 - Star1.Height), Color.White);
				}
				spriteBatch.Draw(Restart, new Vector2(100, 500), Color.White);
				spriteBatch.Draw(Home, new Vector2(450, 500), Color.White);
				spriteBatch.Draw(Next, new Vector2(800, 500), Color.White);
			}
		}
	}
}