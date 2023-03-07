using Microsoft.Xna.Framework.Input;

namespace Breakout.@Subsystems
{
    public class BO_Keyboard
    {
        KeyboardState prevKeyboardState;
        KeyboardState currentKeyboardState;

        public void InitializePreviousState()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public void SetPreviousStateToCurrentState()
        {
            prevKeyboardState = currentKeyboardState;
        }

        public void UpdateCurrentState()
        {
            currentKeyboardState = Keyboard.GetState();
        }

        internal bool IsKeyPressed(Keys key)
        {
            return (!prevKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));
        }

        public bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

    }//END class BO_Keyboard
}
