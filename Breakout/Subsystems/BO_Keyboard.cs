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

        //TODO: Declare a couple keyboard state variables...w/e data types those were
        // One for the previous state, one for the current state
        KeyboardState prevKeyboardState;
        KeyboardState currentKeyboardState;
        //Reminder: KeyboardState currentState = Keyboard.GetState();
        // prevState = currentState;  //at the 'end' ...whatever that means, in this context

        internal BO_Keyboard()
        {
            Console.WriteLine("Keyboard subsystem in progress");

            prevKeyboardState = Keyboard.GetState();    //Dare we do this here? TBD

            //TODO: BO_Keyboard subsystem
            //Note: Let's differentiate between keypress events (prev==false, cur==true) and 'key hold' events (ie. a key being held down)
        }
    }
}
