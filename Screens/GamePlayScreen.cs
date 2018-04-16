using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GP_Final_Catapult.Components;
using GP_Final_Catapult.GameObjects;

namespace GP_Final_Catapult.Screens {
    class GamePlayScreen : IScreen{
		private Texture2D enemyTexture;

		private GameObject Enemy = new GameObject();

        public override void LoadContent() {
            base.LoadContent();
			enemyTexture = Content.Load<Texture2D>("TransitionEffect/Circle");

			var Transform = new Transform(0, 0);
			Enemy.AddComponent(Transform);
			var enemySprite = new Sprite(enemyTexture, 64, 64, Transform.Position);
			enemySprite.CreateAnimmtion("idle",(0,0));
			enemySprite.PlayAnimation("idle");
			Enemy.AddComponent(enemySprite);

			Enemy.GetComponent<Sprite>().Position += new Vector2(50,50);
		}
		public override void UnloadContent() => base.UnloadContent();
		public override void Update(GameTime gameTime) {
			Enemy.Update(gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch) {
			Enemy.Draw(spriteBatch);
		}
	}
}
