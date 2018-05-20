using GP_Final_Catapult.Components;
using GP_Final_Catapult.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GP_Final_Catapult.GameObjects {
	public class IGameObject {
		protected Dictionary<string, IComponent> Components = new Dictionary<string, IComponent>();

		public Transform transform = new Transform();
		public String Name;
		public string Tag;
		public bool isActive;

		public IGameObject gameObject {
			get {
				return this;
			}
		}
		public Rectangle Rectangle {
			get {
				return new Rectangle((int)transform.position.X - GetComponent<Sprite>().SpriteSheet.CellSize.X / 2,
									(int)transform.position.Y - GetComponent<Sprite>().SpriteSheet.CellSize.Y / 2,
									GetComponent<Sprite>().SpriteSheet.CellSize.X,
									GetComponent<Sprite>().SpriteSheet.CellSize.Y);
			}
		}
		public void AddComponent(IComponent component) => Components.Add(component.GetType().Name, component);
		public T GetComponent<T>() {
			return (T)Convert.ChangeType(Components[typeof(T).Name], typeof(T));
		}
		public virtual void Update(GameTime gameTime, List<IGameObject> gameObjects) {
			
			foreach (var component in Components) {
				switch (component.Value.GetType().Name) {
					case "Sprite":
						(component.Value as Sprite).Update(gameTime);
						break;
					case "Physics":
						(component.Value as Physics).Update(gameTime, ref transform.position, this, gameObjects);
						break;
				}
			}
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
			foreach (var component in Components) {
				switch (component.Value.GetType().Name) {
					case "Sprite":
						(component.Value as Sprite).Draw(spriteBatch, transform);
						break;
				}
			}
		}
	}
}
