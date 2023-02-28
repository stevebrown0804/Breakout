using Breakout.Game_elements;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout.@Subsystems
{
    public class StringRenderer
    {
        public (float, Vector2) RenderStringHVCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            Vector2 stringSize = font.MeasureString(str);
            Rectangle rs = renderSurface;

            float rs_y = rs.Y + rs.Height / 2 - stringSize.Y / 2;

            return (rs_y + stringSize.Y, new Vector2(rs.X + rs.Width / 2 - stringSize.X / 2, rs_y));
        }

        //IN PROGRESS, if needed (StringRenderer.RenderStringHCentered())  //<--eh?  (j/k)
        public float RenderStringHCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            Vector2 stringSize = font.MeasureString(str);
            Rectangle rs = renderSurface;

            return rs.X + rs.Width / 2 - stringSize.X / 2;
        }

        /*//other interface methods (which throw)
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
        }*/
    }//END class StringRenderer
}
