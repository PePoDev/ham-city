using GP_Final_Catapult.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Screens {
    abstract class IScreen {
        protected ContentManager Content;

        public virtual void Initial() {

        }
        public virtual void LoadContent() {
            Content = new ContentManager(ScreenManager.Content.ServiceProvider, "Content");
            Initial();
        }
        public virtual void UnloadContent() {
            Content.Unload();
        }
        public virtual void Update(GameTime gameTime) {

        }
        public virtual void Draw(SpriteBatch spriteBatch) {

        }
    }
}
