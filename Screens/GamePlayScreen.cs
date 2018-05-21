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
				case 1:
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(832, 570), enemyHumanTexture));
					break;
				case 2:
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(794, 249)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(771, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(832, 499) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWall(new Vector2(795, 444) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));

					var door21 = ObjectCreate.CreateWallStand(new Vector2(771, 444) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					LightObj.Add(door21);
					var door22 = ObjectCreate.CreateWall(new Vector2(795, 444) + Door.Bounds.Center.ToVector2(), Door, "Door");
					LightObj.Add(door22);

					var doorList21 = new List<IGameObject>();
					doorList21.Add(door21);
					doorList21.Add(door22);

					NightObj.Add(ObjectCreate.CreateTrigger(new Vector2(863 + 29, 364 + 41), Trigger, doorList21));
					break;
				case 3:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(770, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(771, 443) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(794, 249)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(795, 444)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(990, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1207, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWall(new Vector2(1013, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(831 + 55, 128 + 55), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(831 + 55, 323 + 55), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(831 + 55, 499 + 55), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateBoss1(new Vector2(1029 + 120, 233 + 120), boss1Texture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(795, 249)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(795, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(558 + 80, 538 + 60), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(784 + 80, 137 + 60), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(812 + 80, 332 + 60), enemyMonsterTexture));
					break;
				case 4:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(749, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(966, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(749, 443) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(772, 248)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(989, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWall(new Vector2(795, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(839, 127) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(839, 323) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(832, 499) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(796, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(795, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateWall(new Vector2(993, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1190, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(866, 333) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(960, 511) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(560, 525) + (new Vector2(80, 60)), enemyMonsterTexture));

					var door41 = ObjectCreate.CreateWallStand(new Vector2(772, 443) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					LightObj.Add(door41);

					var doorList41 = new List<IGameObject>();
					doorList41.Add(door41);

					NightObj.Add(ObjectCreate.CreateTrigger(new Vector2(1114, 355) + (new Vector2(29, 41)), Trigger, doorList41));
					break;
				case 5:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(942, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1159, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(942, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(965, 248)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1159, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWall(new Vector2(965, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(965, 127) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(788, 489) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1015, 498) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWall((new Vector2(818, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(1014, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(795, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1210, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(1053, 135) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(830, 175) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(930, 330) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(843, 525) + (new Vector2(80, 60)), enemyMonsterTexture));
					var door51 = ObjectCreate.CreateWallStand(new Vector2(728, 443) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					var door52 = ObjectCreate.CreateWall(new Vector2(748, 445) + Door.Bounds.Center.ToVector2(), Door, "Door");
					LightObj.Add(door51);
					LightObj.Add(door52);

					var doorList51 = new List<IGameObject>();
					doorList51.Add(door51);
					doorList51.Add(door52);

					NightObj.Add(ObjectCreate.CreateTrigger(new Vector2(1102, 545) + (new Vector2(29, 41)), Trigger, doorList51));



					var door511 = ObjectCreate.CreateWallStand(new Vector2(795, 445) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					var door512 = ObjectCreate.CreateWallStand(new Vector2(1206, 445) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");

					var door513 = ObjectCreate.CreateWall(new Vector2(818, 445) + Door.Bounds.Center.ToVector2(), Door, "Door");
					var door514 = ObjectCreate.CreateWall(new Vector2(1012, 444) + Door.Bounds.Center.ToVector2(), Door, "Door");
					NightObj.Add(door511);
					NightObj.Add(door512);
					NightObj.Add(door513);
					NightObj.Add(door514);

					var doorList511 = new List<IGameObject>();
					doorList511.Add(door511);
					doorList511.Add(door512);
					doorList511.Add(door514);
					doorList511.Add(door513);

					LightObj.Add(ObjectCreate.CreateTrigger(new Vector2(1034, 363) + (new Vector2(29, 41)), Trigger, doorList511));
					break;
				case 6:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(510, 250) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(510, 446) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(942, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(533, 446)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1159, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWall(new Vector2(965, 444) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(559, 323) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(700, 323) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(841, 323) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(975, 317) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(559, 498) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(777, 498) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateBoss2(new Vector2(991 + 30, 485 + 80), boss2Texture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1210, 52) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1210, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(552, 445) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(576, 445)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(621, 288)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(818, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(1014, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(795, 181) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(1006, 181) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(552, 338) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(759, 338) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(983, 326) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(874, 496) + (new Vector2(80, 60)), enemyMonsterTexture));
					var door61 = ObjectCreate.CreateWallStand(new Vector2(748, 445) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					var door62 = ObjectCreate.CreateWall(new Vector2(728, 443) + Door.Bounds.Center.ToVector2(), Door, "Door");
					LightObj.Add(door61);
					LightObj.Add(door62);

					var doorList61 = new List<IGameObject>();
					doorList61.Add(door61);
					doorList61.Add(door62);

					NightObj.Add(ObjectCreate.CreateTrigger(new Vector2(1103, 548) + (new Vector2(29, 41)), Trigger, doorList61));



					var door611 = ObjectCreate.CreateWallStand(new Vector2(795, 445) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");
					var door612 = ObjectCreate.CreateWallStand(new Vector2(1206, 445) + DoorStand.Bounds.Center.ToVector2(), DoorStand, "Door");

					var door613 = ObjectCreate.CreateWall(new Vector2(818, 445) + Door.Bounds.Center.ToVector2(), Door, "Door");
					var door614 = ObjectCreate.CreateWall(new Vector2(1012, 444) + Door.Bounds.Center.ToVector2(), Door, "Door");
					NightObj.Add(door611);
					NightObj.Add(door612);
					NightObj.Add(door613);
					NightObj.Add(door614);

					var doorList611 = new List<IGameObject>();
					doorList611.Add(door611);
					doorList611.Add(door612);
					doorList611.Add(door614);
					doorList611.Add(door613);

					LightObj.Add(ObjectCreate.CreateTrigger(new Vector2(1102, 545) + (new Vector2(29, 41)), Trigger, doorList611));

					break;
				case 7:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(725, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(942, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1160, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(942, 423) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1160, 423) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(747, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(965, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(965, 423)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(725, 423) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1063, 105) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(816, 302) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(978, 300) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(978, 481) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(936, 94) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(514, 231) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(514, 430) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1210, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(540, 232)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(738, 231)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(818, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(1014, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(543, 419)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(741, 419)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(772, 444) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(514, 121) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(724, 115) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(987, 181) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(577, 310) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(830, 315) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(560, 505) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(830, 491) + (new Vector2(80, 60)), enemyMonsterTexture));
					break;
				case 8:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(620, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(840, 30) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(840, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1058, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(644, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(862, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(1082, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(1083, 423)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(621, 423) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(886, 105) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1010, 105) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1134, 105) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(680, 300) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(898, 3001) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1121, 300) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(900, 481) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1120, 481) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(574, 119) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(994, 94) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(794, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1211, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(597, 225)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(794, 225)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(597, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(817, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(1014, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(596, 447)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(570, 445) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(789, 120) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(1029, 97) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(582, 335) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(844, 335) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(358, 527) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(594, 527) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(994, 476) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(830, 589) + (new Vector2(80, 60)), enemyMonsterTexture));
					break;
				case 9:
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(648, 195) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(844, 30) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(624, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(844, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1062, 228) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(648, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(866, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(1086, 228)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWall((new Vector2(1087, 423)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					LightObj.Add(ObjectCreate.CreateWallStand(new Vector2(625, 423) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateWall(new Vector2(433, 423) + wallCanNotDestroy.Bounds.Center.ToVector2(), wallCanNotDestroy, "CanNotDestroy"));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(700, 93) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(902, 96) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(481, 290) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(671, 300) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(907, 300) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1125, 290) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(467, 481) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(904, 481) + (new Vector2(55, 55)), enemyHumanTexture));
					LightObj.Add(ObjectCreate.CreateEnemyHuman(new Vector2(1125, 485) + (new Vector2(55, 55)), enemyHumanTexture));

					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(597, 25) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(324, 240) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(794, 249) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(1014, 248) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(324, 444) + wallCanDestroyStand.Bounds.Center.ToVector2(), wallCanDestroyStand, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(597, 225)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(794, 225)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(817, 253)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(597, 289)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(1041, 341)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWall((new Vector2(596, 447)) + wallCanDestroy.Bounds.Center.ToVector2(), wallCanDestroy, "CanDestroy"));
					NightObj.Add(ObjectCreate.CreateWallStand(new Vector2(570, 445) + wallCanNotDestroyStand.Bounds.Center.ToVector2(), wallCanNotDestroyStand, "CanNotDestroy"));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(637, 119) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(849, 117) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(1061, 119) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(1051, 233) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(582, 335) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(358, 417) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(358, 527) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(594, 527) + (new Vector2(80, 60)), enemyMonsterTexture));
					NightObj.Add(ObjectCreate.CreateEnemyMonster(new Vector2(829, 576) + (new Vector2(80, 60)), enemyMonsterTexture));
					LightObj.Add(ObjectCreate.CreateBoss3(new Vector2(1041, 506), boss3Texture));
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
					case 3:
						star += 2;
						isWin = true;
						break;
					case 4:
						star += 2;
						isWin = true;
						break;
					case 5:
						star += 2;
						isWin = true;
						break;
					case 6:
						star += 2;
						isWin = true;
						break;
					case 7:
						star += 2;
						isWin = true;
						break;
					case 8:
						star += 2;
						isWin = true;
						break;
					case 9:
						star += 2;
						isWin = true;
						break;
					case 10:
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