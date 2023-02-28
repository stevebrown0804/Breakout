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

            //Note: The first value of the tuple is a float containing the bottom coordinate of the text
            return (rs_y + stringSize.Y, new Vector2(rs.X + rs.Width / 2 - stringSize.X / 2, rs_y));
        }

        public float RenderStringHCentered(string str, SpriteFont font, Rectangle renderSurface)
        {
            Vector2 stringSize = font.MeasureString(str);
            Rectangle rs = renderSurface;

            return rs.X + rs.Width / 2 - stringSize.X / 2;
        }

    }//END class StringRenderer
}
