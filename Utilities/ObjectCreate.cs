using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GP_Final_Catapult.Utilities {
	static class ObjectCreate {

		public static Enemy CreateEnemyHuman(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 120, 120);
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
			enemy.hp = 1;

			return enemy;
		}
		public static Enemy CreateEnemyMonster(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 200, 100);
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
			enemy.hp = 1;

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
			enemy.Name = "enemy";
			enemy.hp = 2;

			return enemy;
		}
		public static Enemy CreateBoss2(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 150, 160);
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
			enemy.hp = 2;

			return enemy;
		}
		public static Enemy CreateBoss3(Vector2 position, Texture2D enemyTexture) {
			var enemySprite = new Sprite(enemyTexture, 160, 170);
			enemySprite.CreateAnimmtion("idle", (2, 1), (0, 2), (2, 1), (0, 2), (2, 1), (0, 2));
			enemySprite.PlayAnimation("idle");
			enemySprite.CreateAnimmtion("die", (0, 0), (1, 0), (2, 0), (0, 1), (1, 1));

			var enemyPhysics = new Physics();
			enemyPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			enemyPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;
			enemyPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;

			var enemy = new Enemy();
			enemy.AddComponent(enemySprite);
			enemy.AddComponent(enemyPhysics);
			enemy.transform.position = position;
			enemy.Name = "enemy";
			enemy.hp = 2;

			return enemy;
		}
		public static Trigger CreateTrigger(Vector2 position, Texture2D enemyTexture, List<IGameObject> door) {
			var triggerSprite = new Sprite(enemyTexture, 58, 80);
			triggerSprite.CreateAnimmtion("idle", (0,0));
			triggerSprite.PlayAnimation("idle");
			triggerSprite.CreateAnimmtion("switch", (1, 0));

			var triggerPhysics = new Physics();
			triggerPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			triggerPhysics.EntityPhysicsType = Physics.PhysicsType.STATICS;
			triggerPhysics.EntityImpluseType = Physics.ImpluseType.SURFACE;

			var trigger = new Trigger();
			trigger.AddComponent(triggerSprite);
			trigger.AddComponent(triggerPhysics);
			trigger.transform.position = position;
			trigger.Name = "trigger";
			trigger.doorList = door;

			return trigger;
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
		public static Wall CreateWall(Vector2 position, Texture2D bulletTexture, string WallType) {
			var wallSprite = new Sprite(bulletTexture,195, 23);
			wallSprite.CreateAnimmtion("idle", (0, 0));
			wallSprite.PlayAnimation("idle");
			
			var wallPhysics = new Physics();
			wallPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			wallPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;
			wallPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;

			var wall = new Wall();
			wall.AddComponent(wallSprite);
			wall.AddComponent(wallPhysics);
			wall.transform.position = position;
			wall.Name = WallType;

			return wall;
		}
		public static Wall CreateWallStand(Vector2 position, Texture2D bulletTexture, string WallType) {
			var wallSprite = new Sprite(bulletTexture, 23, 195);
			wallSprite.CreateAnimmtion("idle", (0, 0));
			wallSprite.PlayAnimation("idle");

			var wallPhysics = new Physics();
			wallPhysics.EntityBoundingBoxType = Physics.BoundingBoxType.AABB;
			wallPhysics.EntityImpluseType = Physics.ImpluseType.NORMAL;
			wallPhysics.EntityPhysicsType = Physics.PhysicsType.KINEMATICS;

			var wall = new Wall();
			wall.AddComponent(wallSprite);
			wall.AddComponent(wallPhysics);
			wall.transform.position = position;
			wall.Name = WallType;

			return wall;
		}
	}
}
