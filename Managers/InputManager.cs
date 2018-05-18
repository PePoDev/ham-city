using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GP_Final_Catapult.Managers {
    static class InputManager {
        private static MouseState mousePreviousState;
        private static MouseState mouseCurrentState;
        private static KeyboardState keyboardPreviousState;
        private static KeyboardState keyboardCurrentState;

        private static Vector2 mousePosition;
        public static void Update(GameTime gameTime) {
            mousePreviousState = mouseCurrentState;
            mouseCurrentState = Mouse.GetState();
            keyboardPreviousState = keyboardCurrentState;
            keyboardCurrentState = Keyboard.GetState();
        }
        public static Vector2 GetMousePosition() {
            mousePosition.X = mouseCurrentState.X;
            mousePosition.Y = mouseCurrentState.Y;
            return mousePosition;
        }
        public static bool OnKeyDown(Keys key) {
            return (keyboardCurrentState.IsKeyDown(key) != keyboardPreviousState.IsKeyDown(key) )&& !keyboardCurrentState.IsKeyUp(key);
        }
		public static bool OnMouseDown(Rectangle area) {
			if (mouseCurrentState.LeftButton == ButtonState.Pressed && mousePreviousState.LeftButton == ButtonState.Released)
				if (area.Contains(mouseCurrentState.X, mouseCurrentState.Y))
					return true;
			return false;
		}
		public static bool OnMouseUp(Rectangle area) {
			if (mouseCurrentState.LeftButton == ButtonState.Released && mousePreviousState.LeftButton == ButtonState.Pressed)
				if (area.Contains(mouseCurrentState.X,mouseCurrentState.Y))
					return true;
			return false;
		}
	}
}
