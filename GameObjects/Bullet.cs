using GP_Final_Catapult.Components;
using GP_Final_Catapult.Managers;
using GP_Final_Catapult.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GP_Final_Catapult.GameObjects {
	class Bullet : IGameObject {
		private IGameObject ObjectHited = null, WallObj = null, triggerObj = null;
		private Vector2 oldObjectHitPosition = Vector2.Zero;
		private Vector2 oldPosition = Vector2.Zero;

		private float time = 0f;
		private bool Hited = false;
		public bool HitedObj = false;
		private bool isDraging = false;
		public bool isFly = false;
		private int shootedBullet = 0;

		public override void Update(GameTime gameTime, List<IGameObject> gameObjects) {
			// Drag and drop  for shoot
			var bulletPosition = GetComponent<Sprite>().SpriteSheet.CellSize;
			if (!isFly && InputManager.OnMouseDown(new Rectangle((int)transform.position.X - GetComponent<Sprite>().SpriteSheet.CellSize.X / 2, (int)transform.position.Y - GetComponent<Sprite>().SpriteSheet.CellSize.Y / 2, bulletPosition.X, bulletPosition.Y)) && !isDraging) {
				isDraging = true;
				oldPosition = transform.position;
			}
			if (InputManager.OnMouseUp(new Rectangle(0, 0, 1280, 720)) && isDraging) {
				isDraging = false;
				isFly = true;
				var mousePosition = InputManager.GetMousePosition();
				GetComponent<Physics>().Velocity.X = (oldPosition.X - mousePosition.X) * 5;
				GetComponent<Physics>().Velocity.Y = -((mousePosition.Y - oldPosition.Y) * 5);
				GetComponent<Physics>().Acceleration.Y = 981;
				GetComponent<Physics>().GRAVITY = 200;
				shootedBullet++;
				AudioManager.PlayAudio("shoot");
			}
			if (isDraging) {
				var mousePosition = InputManager.GetMousePosition();
				transform.position = InputManager.GetMousePosition();
			}
			if (isFly) {
				transform.rotation += 0.0001f;
				if (transform.rotation > 360f) {
					transform.rotation = -360f;
				}
			}

			// Detect collition
			gameObjects.ForEach(GO => {
				if (GO.Name.Equals("enemy") && gameObject.GetComponent<Physics>().IsTouching(GO) && !Hited) {
					Hited = true;
					if (--((Enemy)GO).hp == 0) {
						HitedObj = true;
						isFly = false;
						transform.rotation = 0f;
						ObjectHited = GO;
						oldObjectHitPosition = new Vector2(ObjectHited.transform.position.X, ObjectHited.transform.position.Y);
						GO.GetComponent<Sprite>().PlayAnimation("die");
						AudioManager.PlayAudio("die");
					} else {
						GetComponent<Physics>().Velocity = Vector2.Zero;
						GetComponent<Physics>().Acceleration = Vector2.Zero;
					}
				} else if (GO.Name.Equals("CanDestroy") && gameObject.GetComponent<Physics>().IsTouching(GO) && !Hited) {
					WallObj = GO;
					GetComponent<Physics>().Velocity = Vector2.Zero;
					GetComponent<Physics>().Acceleration = Vector2.Zero;
					transform.position = new Vector2(250, 500);
					isFly = false;
					transform.rotation = 0f;
				} else if ((GO.Name.Equals("CanNotDestroy") || GO.Name.Equals("Door")) && gameObject.GetComponent<Physics>().IsTouching(GO) && !Hited) {
					GetComponent<Physics>().Velocity = Vector2.Zero;
					GetComponent<Physics>().Acceleration = Vector2.Zero;
					transform.position = new Vector2(250, 500);
					isFly = false;
					transform.rotation = 0f;
				} else if (GO.Name.Equals("trigger") && gameObject.GetComponent<Physics>().IsTouching(GO) && !Hited) {
					GetComponent<Physics>().Velocity = Vector2.Zero;
					GetComponent<Physics>().Acceleration = Vector2.Zero;
					transform.position = new Vector2(250, 500);
					isFly = false;
					transform.rotation = 0f;
					GO.GetComponent<Sprite>().PlayAnimation("switch");
					triggerObj = GO;
				}
			});

			if (WallObj != null) {
				gameObjects.Remove(WallObj);
				WallObj = null;
			}

			if (triggerObj != null) {
				((Trigger)triggerObj).doorList.ForEach( obj => gameObjects.Remove(obj));
			}

			// Delay for play die animation when bullet hit enemy
			if (Hited) {
				time += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				if (time > 0.8f) {
					Hited = false;
					time = 0f;
					GetComponent<Physics>().Velocity = Vector2.Zero;
					GetComponent<Physics>().Acceleration = Vector2.Zero;
					transform.position = new Vector2(250, 500);
					isFly = false;
					transform.rotation = 0f;
					if (HitedObj) {
						gameObjects.Remove(ObjectHited);
						ObjectHited = null;
						HitedObj = false;
					}
				}
			} else if ((transform.position.X > 1280 + 32 || transform.position.Y > 720 + 32) && !Hited) {
				GetComponent<Physics>().Velocity = Vector2.Zero;
				GetComponent<Physics>().Acceleration = Vector2.Zero;
				transform.position = new Vector2(250, 500);
				isFly = false;
				transform.rotation = 0f;
			}
			if (ObjectHited != null) {
				if (oldObjectHitPosition.X + 25 < ObjectHited.transform.position.X || oldObjectHitPosition.Y + 10 < ObjectHited.transform.position.Y) {
					GetComponent<Physics>().Velocity = Vector2.Zero;
					GetComponent<Physics>().Acceleration = Vector2.Zero;
				}
			}
			base.Update(gameTime, gameObjects);
		}
		public override void Draw(SpriteBatch spriteBatch) {

			base.Draw(spriteBatch);
		}
	}
}
