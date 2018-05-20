using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Utilities {
	static class ObjectCreate {

		public static Enemy CreateEnemyHuman(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "human";

			return enemy;
		}
		public static Enemy CreateEnemyMonster(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "enemy";

			return enemy;
		}
		public static Enemy CreateBoss1(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "human";

			return enemy;
		}
		public static Enemy CreateBoss2(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "human";

			return enemy;
		}
		public static Enemy CreateBoss3(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "human";

			return enemy;
		}
		public static Enemy CreateTrigger(Vector2 position, Texture2D enemyTexture, IGameObject door) {
			var enemySprite = new Sprite(enemyTexture, 200, 200);
			enemySprite.CreateAnimmtion("idle", (2, 1), (2, 1), (2, 1), (0, 2), (0, 2), (0, 2), (1, 2), (1, 2), (1, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (0, 2));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.STATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.SURFACE;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "trigger";

			return enemy;
		}
		public static Bullet CreateBullet(Vector2 position, Texture2D bulletTexture) {
			var bulletSprite = new Sprite(bulletTexture, 64, 64);
			bulletSprite.CreateAnimmtion("idle", (0, 0));
			bulletSprite.PlayAnimation("idle");

			var bulletPhysics = new Physics();
			bulletPhysics.GRAVITY = 0;
			bulletPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.CIRCLE;
			bulletPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			bulletPhysics.EntityImpluseType = Physics.ImpluseType.SURFACE;

			var bullet = new Bullet();
			bullet.AddComponent(bulletSprite);
			bullet.AddComponent(bulletPhysics);
			bullet.transform.position = position;
			bullet.Name = "bullet";

			return bullet;
		}
		public static Wall CreateWall(Vector2 position, Texture2D bulletTexture, float rotation, string WallType) {
			var wallSprite = new Sprite(bulletTexture,195, 23);
			wallSprite.CreateAnimmtion("idle", (0, 0));
			wallSprite.PlayAnimation("idle");

			var wallPhysics = new Physics();
			var groundPhysics = new Physics();
			groundPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			groundPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;
			groundPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;

			var wall = new Wall();
			wall.AddComponent(wallSprite);
			wall.AddComponent(wallPhysics);
			wall.transform.position = position;
			wall.Name = WallType;
			wall.transform.rotation = rotation;

			return wall;
		}
	}
}
