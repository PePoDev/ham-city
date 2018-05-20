using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using GP_Final_Catapult.Utilities;
using GP_Final_Catapult.Managers;
using System;

namespace GP_Final_Catapult.Screens {
	class GamePlayScreen : IScreen {
		private Texture2D enemyHumanTexture, enemyMonsterTexture, boss1Texture, boss2Texture, boss3Texture, bossSoulTexture, fairyTexture, PlayerTexture;
		private Texture2D catapultFrontTexture, catapultBackTexture;
		private Texture2D bulletTexture, wallCanDestroy, wallCanNotDestroy, Door, Trigger;
		private Texture2D BG_Normal, BG_Dark;

		private List<IGameObject> GameObj = new List<IGameObject>();
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
			Trigger = Content.Load<Texture2D>("Sprites/Bullet");
			BG_Normal = Content.Load<Texture2D>("Sprites/BG_game_normal");
			BG_Dark = Content.Load<Texture2D>("Sprites/BG_game_dark");

			Initial();
		}
		public void Initial() {
			LevelNo = int.Parse(Properties.Settings.Default.LevelSelected);
			switch (LevelNo) {
				case 1:
					// Create bullet
					BulletObj = ObjectCreate.CreateBullet(new Vector2(250, 500), bulletTexture);
					((Bullet)BulletObj).GetParentScreen(this);

					// Create Enemy
					GameObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(500, 600), enemyHumanTexture));
					GameObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(700, 500), enemyMonsterTexture));
					GameObj.Add(ObjectCreate.CreateBoss1(new Vector2(900, 500), boss1Texture));
					GameObj.Add(ObjectCreate.CreateBoss2(new Vector2(1100, 500), boss2Texture));
					GameObj.Add(ObjectCreate.CreateBoss3(new Vector2(500, 200), boss3Texture));
					
					// Create wall
					GameObj.Add(ObjectCreate.CreateWall(new Vector2(500, 200), wallCanDestroy, 0f,"CanDestroy"));
					GameObj.Add(ObjectCreate.CreateWall(new Vector2(500, 200), wallCanNotDestroy, 0f, "CanNotDestroy"));

					// Create trigger
					var door = ObjectCreate.CreateWall(new Vector2(500, 200), Door, 0f, "Door");
					GameObj.Add(door);

					// Create trigger
					GameObj.Add(ObjectCreate.CreateTrigger(new Vector2(500, 200), Trigger, door));
					break;
				case 2:

					break;
			}
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {
			if (!isWin) {
				time += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

				if (InputManager.OnKeyDown(Keys.Space)) GodMode = !GodMode;

				BulletObj.Update(gameTime, GameObj);

				GameObj.ForEach(Obj => {
					if (!GodMode && Obj.Name.Equals("human")) {
						Obj.Update(gameTime, GameObj);
					} else if (GodMode && Obj.Name.Equals("enemy")) {
						Obj.Update(gameTime, GameObj);
					}
				});

				WinDetect();
			} else {

			}
		}
		private void WinDetect() {
			if (!hasEnemy(GameObj)) {
				star++;
				switch (LevelNo) {
					case 1:
						star += 2;
						isWin = true;
						break;
				}
			}
		}
		private bool hasEnemy(List<IGameObject> objects) {
			var HasEnemy = false;
			objects.ForEach(GO => {
				if (GO.Name.Equals("enemy") || GO.Name.Equals("human")) {
					HasEnemy = true;
				}
			});
			return HasEnemy;
		}
		public override void Draw(SpriteBatch spriteBatch) {
			// Draw game BG between Dark and Normal mode
			spriteBatch.Draw(GodMode ? BG_Dark : BG_Normal, Vector2.Zero, Color.White);

			GameObj.ForEach(Obj => {
				if (!GodMode && Obj.Name.Equals("human")) {
					Obj.Draw(spriteBatch);
				} else if (GodMode && Obj.Name.Equals("enemy")) {
					Obj.Draw(spriteBatch);
				}
			});

			spriteBatch.Draw(catapultBackTexture, new Vector2(200, 470), Color.White);
			BulletObj.Draw(spriteBatch);
			spriteBatch.Draw(catapultFrontTexture, new Vector2(200, 470), Color.White);
		}
	}
}
