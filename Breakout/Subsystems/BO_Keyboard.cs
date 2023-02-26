using System;
using System.Collections.Generic;
using Breakout.Game_elements;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* "Player movement is controlled by using the arrow keys." */

namespace Breakout.Subsystems
{
    public class BO_Keyboard : ISubsystem
    {
        KeyboardState prevKeyboardState;
        KeyboardState currentKeyboardState;

        internal BO_Keyboard()
        {
            //nothing to see here!
        }

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

        public bool IsKeyPressed(Keys key)
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

        //Other interface methods (which throw)
        public List<GameElement> GetRenderList()
        {
            throw new Exception("BO_Keyboard does not implement GetRenderList(). Renderer, perhaps?");
        }

        public List<GameElement> AddToRenderList(GameElement element)
        {
            throw new Exception("BO_Keyboard does not implement AddToRenderList(). Renderer, perhaps?");
        }

        public void ClearRenderList()
        {
            throw new Exception("BO_Keyboard does not implement ClearRenderList(). Renderer, perhaps?");
        }
    }//END class BO_Keyboard
}
