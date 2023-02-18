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
        //Reminder: KeyboardState currentState = Keyboard.GetState();
        // prevState = currentState;  //at the 'end' ...whatever that means, in this context

        internal BO_Keyboard()
        {
            //Console.WriteLine("Keyboard subsystem in progress");

            //IN PROGRESS: BO_Keyboard subsystem
            //Note: Let's differentiate between keypress events (prev==false, cur==true) and 'key hold' events (ie. a key being held down)
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
            return (!prevKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyDown(key));  //TODO: see if this is right
        }

        internal bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key); //TODO: see if this is right
        }
    }
}
