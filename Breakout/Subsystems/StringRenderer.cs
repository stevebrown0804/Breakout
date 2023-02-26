using Breakout.Game_elements;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout.Subsystems
{
    internal class StringRenderer : ISubsystem
    {
        public static Vector2 RenderStringHVCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            Vector2 stringSize = font.MeasureString(str);
            Rectangle rs = renderSurface;

            return new Vector2(rs.X + rs.Width / 2 - stringSize.X / 2, /*graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2 */ rs.Y + rs.Height / 2 - stringSize.Y / 2 /* graphics.PreferredBackBufferHeight / 2 - stringSize.Y */);
        }

        //TODO, if needed (StringRenderer.RenderStringHCentered())
        public static float RenderStringHCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            

            //TMP
            return 0f;
        }

        //other interface methods (which throw)
        public void InitializePreviousState()
        {
            throw new Exception("StringRenderer subsystem does not implement this interface.  BO_Keyboard, perhaps?");
        }

        public List<GameElement> GetRenderList()
        {
            throw new Exception("StringRenderer subsystem does not implement this interface.  Renderer, perhaps?");
        }

        public void UpdateCurrentState()
        {
            throw new Exception("StringRenderer subsystem does not implement this interface.  BO_Keyboard, perhaps?");
        }

        public void SetPreviousStateToCurrentState()
        {
            throw new Exception("StringRenderer subsystem does not implement SetPreviousStateToCurrentState().  BO_Keyboard, perhaps?");
        }

        public List<GameElement> AddToRenderList(GameElement element)
        {
            throw new Exception("StringRenderer subsystem does not implement AddToRenderList().  Renderer, perhaps?");
        }

        public bool IsKeyPressed(Keys key)
        {
            throw new Exception("StringRenderer subsystem does not implement IsKeyPressed().  BO_Keyboard, perhaps?");
        }

        public bool IsKeyHeld(Keys key)
        {
            throw new Exception("StringRenderer subsystem does not implement IsKeyHeld().  BO_Keyboard, perhaps?");
        }

        public void ClearRenderList()
        {
            throw new Exception("StringRenderer subsystem does not implement ClearRenderList().  Renderer, perhaps?");
        }
    }//END class StringRenderer
}
