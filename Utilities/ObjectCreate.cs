using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Utilities {
	static class ObjectCreate {

		public static Enemy CreateEnemy(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 149, 189);
			enemySprite.CreateAnimmtion("idle", (0, 0), (1, 0), (2, 0));
			enemySprite.PlayAnimation("idle");

			var enemyRigibody = new Rigibody2D();

			var enemyBoxCollision2D = new BoxColision2D();

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyRigibody);
			enemy.AddComponent(enemyBoxCollision2D);
			enemy.transform.position = position;

			return enemy;
		}

	}
}
