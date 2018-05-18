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
		private Texture2D enemyHumanTexture, enemyMonsterTexture, bulletTexture;
		private Texture2D catapultFrontTexture, catapultBackTexture;
		private Texture2D BG_Normal, BG_Dark;

		private List<IGameObject> NormalObj = new List<IGameObject>();
		private List<IGameObject> HumanObj = new List<IGameObject>();
		private List<IGameObject> MonsterObj = new List<IGameObject>();
		private IGameObject BulletObj;
		private Vector2 oldPosition = Vector2.Zero;

		private bool GodMode = false;
		private int shootedBullet = 0;
		private bool isDraging = false;

		public override void LoadContent() {
			base.LoadContent();
			enemyHumanTexture = Content.Load<Texture2D>("Sprites/Character/Theif_Idle");
			enemyMonsterTexture = Content.Load<Texture2D>("Sprites/Character/Theif_Idle");
			catapultFrontTexture = Content.Load<Texture2D>("Sprites/CatapultFront");
			catapultBackTexture = Content.Load<Texture2D>("Sprites/CatapultBack");
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

			HumanObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(0, 0), enemyHumanTexture));
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
			var bulletPosition = BulletObj.GetComponent<Sprite>().SpriteSheet.CellSize;
			if (InputManager.OnMouseDown(new Rectangle((int)BulletObj.transform.position.X - BulletObj.GetComponent<Sprite>().SpriteSheet.CellSize.X / 2, (int)BulletObj.transform.position.Y - BulletObj.GetComponent<Sprite>().SpriteSheet.CellSize.Y / 2, bulletPosition.X, bulletPosition.Y)) && !isDraging) {
				isDraging = true;
				oldPosition = BulletObj.transform.position;
			}
			if (InputManager.OnMouseUp(new Rectangle(0, 0, 1280, 720)) && isDraging) {
				isDraging = false;
				var mousePosition = InputManager.GetMousePosition();
				if (mousePosition.X < 300 && mousePosition.Y > 500)
					;
				else
					BulletObj.transform.position = oldPosition; ;
			}
			if (isDraging) {
				var mousePosition = InputManager.GetMousePosition();
				if (mousePosition.X < 300 && mousePosition.Y > 500)
					BulletObj.transform.position = InputManager.GetMousePosition();
			}
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
			spriteBatch.Draw(catapultBackTexture, new Vector2(200, 470), Color.White);
			BulletObj.Draw(spriteBatch);
			spriteBatch.Draw(catapultFrontTexture, new Vector2(200, 470), Color.White);
			NormalObj.ForEach(Normal => Normal.Draw(spriteBatch));
		}
	}
}
