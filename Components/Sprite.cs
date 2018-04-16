using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Spritesheet;

namespace GP_Final_Catapult.Components {
    class Sprite : IComponent {
        public SpriteSheet SpriteSheet { get; private set; }
        public Dictionary<string, Animation> animation = new Dictionary<string, Animation>();
        public string animationName;

		public Sprite(Texture2D Texture, int GridWidth, int GridHeight) => SpriteSheet = new SpriteSheet(Texture).WithGrid((GridWidth, GridHeight));
        public void CreateAnimmtion(string _animationName, params (int x, int y)[] frames) => animation.Add(_animationName, SpriteSheet.CreateAnimation(frames).FlipX());
        public void PlayAnimation(string _animationName) {
            animationName = _animationName;
            animation[animationName].Start(Repeat.Mode.Loop);
        }
        public void Update(GameTime gameTime) => animation[animationName].Update(gameTime);
        public void Draw(SpriteBatch spriteBatch, Vector2 Position) => spriteBatch.Draw(animation[animationName], Position);
	}
}
