
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GP_Final_Catapult.Components {
    interface IComponent {

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
	}
}
