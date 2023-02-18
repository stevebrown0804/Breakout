using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* "Player movement is controlled by using the arrow keys." */

namespace Breakout.Subsystems
{
    public class BO_Keyboard
    {
        KeyboardState prevKeyboardState;
        KeyboardState currentKeyboardState;

        internal BO_Keyboard()
        {
            //nothing to see here!
        }

        internal void InitializePreviousState()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        internal void SetPreviousStateToCurrentState()
        {
            prevKeyboardState = currentKeyboardState;
        }

        internal void UpdateCurrentState()
        {
            currentKeyboardState = Keyboard.GetState();
        }

        internal bool IsKeyPressed(Keys key)
        {
            return (!prevKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));
        }

        internal bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        internal bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

    }//END class BO_Keyboard
}
