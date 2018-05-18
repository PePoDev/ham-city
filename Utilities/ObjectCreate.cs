using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Utilities {
	static class ObjectCreate {

		public static Enemy CreateEnemyHuman(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 149, 189);
			enemySprite.CreateAnimmtion("idle", (0, 0), (1, 0), (2, 0));
			enemySprite.PlayAnimation("idle");

			var enemyPhysics = new Physics();

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;

			return enemy;
		}
		public static Enemy CreateEnemyMonster(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 149, 189);
			enemySprite.CreateAnimmtion("idle", (0, 0), (1, 0), (2, 0));
			enemySprite.PlayAnimation("idle");

			var enemyPhysics = new Physics();

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;

			return enemy;
		}
		public static Bullet CreateBullet() {


			var bullet = new Bullet();

			return bullet;
		}
	}
}
