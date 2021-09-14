using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace DragonQuest1.Script
{
    public static class InputManager
    {
        static KeyboardState previousKeyboardState, currentKeyboardState;
        static MouseState previousMouseState, currentMouseState;
        public static Rectangle globalMousePosition;
        public static Rectangle mousePosition;
        static int holdTrashold = 150;
        static double holdTimeLeft = 0f, holdTimeRight = 0f;
        public static void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousePosition = new Rectangle(currentMouseState.Position, new Point(1, 1));
            if (currentMouseState.LeftButton == ButtonState.Pressed)
                holdTimeLeft += gameTime.ElapsedGameTime.TotalMilliseconds;
            else holdTimeLeft = 0f;
            if (currentMouseState.RightButton == ButtonState.Pressed)
                holdTimeRight += gameTime.ElapsedGameTime.TotalMilliseconds;
            else holdTimeRight = 0f;
        }
        //change mouse pos to global position if camera moves
        public static void MousePos(Matrix invertedMatrix)
        {
            Vector2 temp = Vector2.Transform(new Vector2(currentMouseState.X, currentMouseState.Y), invertedMatrix);
            globalMousePosition = new Rectangle((int)temp.X, (int)temp.Y, 1, 1);
        }
        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        public static bool isKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }
        public static bool isKeyPressed(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key));
        }
        public static bool isKeyReleased(Keys key)
        {
            return (currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key));
        }
        public static bool isLeftClicked()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released);
        }
        public static bool isRightClicked()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released);
        }
        public static bool isLeftHold()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed && holdTimeLeft > holdTrashold);
        }
        public static bool isRightHold()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed && holdTimeRight > holdTrashold);
        }
    }
}
