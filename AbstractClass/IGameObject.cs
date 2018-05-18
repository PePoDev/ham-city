using GP_Final_Catapult.Components;
using GP_Final_Catapult.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GP_Final_Catapult.GameObjects {
	abstract class IGameObject : ICloneable{
        protected Dictionary<string,IComponent> Components = new Dictionary<string, IComponent>();

		public Transform transform = new Transform();
        public string name;
        public string tag;
        public bool active;
        public byte layer;

        public void AddComponent(IComponent component) {
            Components.Add(component.GetType().Name, component);
        }
        public T GetComponent<T>() {
			return (T)Convert.ChangeType(Components[typeof(T).Name], typeof(T));
        }
		public object Clone() {
			return (IGameComponent)MemberwiseClone();
		}
		public virtual void Update(GameTime gameTime, List<IGameObject> gameObjects) {
			foreach (var component in Components) {
				switch (component.Value.GetType().Name) {
					case "Sprite":
						(component.Value as Sprite).Update(gameTime);
						break;
					case "Rigibody2D":
						//( component.Value as Rigibody2D ).Update(gameTime,transformposition);
						break;
				}
			}
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
			foreach (var component in Components) {
				switch (component.Value.GetType().Name) {
					case "Sprite":
						(component.Value as Sprite).Draw(spriteBatch, transform.position);
						break;
				}
			}
		}
	}
}
