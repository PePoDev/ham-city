using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GP_Final_Catapult.Managers {
    static class InputManager {
        private static MouseState mousePreviousState;
        private static MouseState mouseCurrentState;
        private static KeyboardState keyboardPreviousState;
        private static KeyboardState keyboardCurrentState;

        public static void Update(GameTime gameTime) {
            mousePreviousState = mouseCurrentState;
            mouseCurrentState = Mouse.GetState();
            keyboardPreviousState = keyboardCurrentState;
            keyboardCurrentState = Keyboard.GetState();
        }
        public static Vector2 GetMousePosition() {
            return new Vector2(mouseCurrentState.X, mouseCurrentState.Y);
        }
        public static bool OnKeyDown(Keys key) {

            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
