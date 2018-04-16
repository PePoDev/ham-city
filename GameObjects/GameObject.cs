using GP_Final_Catapult.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GP_Final_Catapult.GameObjects {
	class GameObject {
        protected Dictionary<string,IComponent> Components = new Dictionary<string, IComponent>();
        public string Name;
        public string Tag;
        public bool Active;
        public byte Layer;

        public void AddComponent(IComponent component) {
            Components.Add(component.GetType().Name, component);
        }
        public T GetComponent<T>() {
			return (T)Convert.ChangeType(Components[typeof(T).Name], typeof(T));
        }
		public virtual void Update(GameTime gameTime) {
			foreach (var component in Components) {
				component.Value.Update(gameTime);
			}
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
			foreach (var component in Components) {
				component.Value.Draw(spriteBatch);
			}
		}
	}
}
