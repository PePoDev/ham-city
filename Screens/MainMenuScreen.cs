
using GP_Final_Catapult.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Screens {
    class MainMenuScreen : IScreen {

        public override void LoadContent() {
            base.LoadContent();

			Initial();
        }
		private void Initial() {
			ScreenTransitions.FadeOUT();
		}
        public override void UnloadContent() {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime) {
            
        }
        public override void Draw(SpriteBatch spriteBatch) {
            
        }
    }
}
