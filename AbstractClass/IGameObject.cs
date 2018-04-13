using GP_Final_Catapult.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.GameObjects {
    abstract class IGameObject {
        protected Transform Transform;
        protected string Name;
        protected string Tag;
        protected bool Active;
        protected byte Layer;

        public virtual void Update(GameTime gameTime) {
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
        }
    }
}
