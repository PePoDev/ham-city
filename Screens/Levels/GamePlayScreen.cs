using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using GP_Final_Catapult.Utilities;
using GP_Final_Catapult.Managers;


namespace GP_Final_Catapult.Screens {
	class GamePlayScreen : IScreen {
		private Texture2D enemyHumanTexture, enemyMonsterTexture, bulletTexture;
		private Texture2D BG_Normal, BG_Dark;

		private List<IGameObject> NormalObj = new List<IGameObject>();
		private List<IGameObject> HumanObj = new List<IGameObject>();
		private List<IGameObject> MonsterObj = new List<IGameObject>();
		private IGameObject BulletObj;

		private bool GodMode = false;
		private int shootedBullet = 0;

		public override void LoadContent() {
			base.LoadContent();
			enemyHumanTexture = Content.Load<Texture2D>("Sprites/Character/Theif_Idle");
			enemyMonsterTexture = Content.Load<Texture2D>("Sprites/Character/Theif_Idle");
			bulletTexture = Content.Load<Texture2D>("TransitionEffect/Circle");
			BG_Normal = Content.Load<Texture2D>("Sprites/BG_game_normal");
			BG_Dark = Content.Load<Texture2D>("Sprites/BG_game_dark");

			Initial();
		}
		public void Initial() {
			var bulletSprite = new Sprite(bulletTexture, 64, 64);
			bulletSprite.CreateAnimmtion("idle", (0, 0));
			bulletSprite.PlayAnimation("idle");

			BulletObj = new Bullet();
			BulletObj.AddComponent(bulletSprite);
			BulletObj.transform.position = new Vector2(200, 600);

			HumanObj.Add(ObjectCreate.CreateEnemy(new Vector2(0, 0), enemyHumanTexture));
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {
			if (InputManager.OnKeyDown(Keys.Space)) GodMode = !GodMode;
			if (GodMode) {
				BulletObj.Update(gameTime, MonsterObj);
				MonsterObj.ForEach(Monster => Monster.Update(gameTime, MonsterObj));
			} else {
				BulletObj.Update(gameTime, HumanObj);
				HumanObj.ForEach(Human => Human.Update(gameTime, HumanObj));
			}

			// Drag and drop  for shoot

			NormalObj.ForEach(Normal => Normal.Update(gameTime, NormalObj));
		}
		public override void Draw(SpriteBatch spriteBatch) {
			// Draw game BG between Dark and Normal mode
			spriteBatch.Draw(GodMode ? BG_Dark : BG_Normal, Vector2.Zero, Color.White);

			if (GodMode) {
				MonsterObj.ForEach(Monster => Monster.Draw(spriteBatch));
			} else {
				HumanObj.ForEach(Human => Human.Draw(spriteBatch));
			}

			BulletObj.Draw(spriteBatch);
			NormalObj.ForEach(Normal => Normal.Draw(spriteBatch));
		}
	}
}
