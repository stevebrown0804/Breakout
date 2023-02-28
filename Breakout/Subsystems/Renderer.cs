using Breakout.Game_elements;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout.Subsystems
{
    public class Renderer : ISubsystem
    {
        readonly List<GameElement> renderList;

        internal Renderer()
        {
            renderList = new();            
        }

        public List<GameElement> GetRenderList()
        {
            return renderList;
        }

        public List<GameElement> AddToRenderList(GameElement element)
        {
            renderList.Add(element);
            return renderList;
        }

        public void ClearRenderList()
        {
            renderList.Clear();
        }

        //Other interface methods (which throw)
        public void InitializePreviousState()
        {
            throw new Exception("Renderer subsystem does not implement InitializePreviousState().  BO_Keyboard, perhaps?");
        }

        public void UpdateCurrentState()
        {
            throw new Exception("Renderer subsystem does not implement UpdateCurrentState().  BO_Keyboard, perhaps?");
        }

        public void SetPreviousStateToCurrentState()
        {
            throw new Exception("Renderer subsystem does not implement SetPreviousStateToCurrentState().  BO_Keyboard, perhaps?");
        }

        public bool IsKeyPressed(Keys key)
        {
            throw new Exception("Renderer subsystem does not implement IsKeyPressed().  BO_Keyboard, perhaps?");
        }

        public bool IsKeyHeld(Keys key)
        {
            throw new Exception("Renderer subsystem does not implement IsKeyHeld().  BO_Keyboard, perhaps?");
        }

        public (float, Vector2) RenderStringHVCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            throw new Exception("Renderer subsystem does not implement RenderStringHVCentered().  stringRenderer, perhaps?");
        }

        public float RenderStringHCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            throw new NotImplementedException();
        }
    }//END class Renderer
}
