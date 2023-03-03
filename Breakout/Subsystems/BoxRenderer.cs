using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Subsystems
{
    public class BoxRenderer
    {

        public float DrawRectangleHCentered(Rectangle rectToDraw, Rectangle renderSurface)
        {
            return renderSurface.X + (renderSurface.Width / 2) - (rectToDraw.Width / 2);
        }

        public float DrawRectangleVCentered(Rectangle rectToDraw, Rectangle renderSurface)
        {
            return renderSurface.Y + (renderSurface.Height / 2) - (rectToDraw.Height / 2);
        }

        public Vector2 DrawRectangleHVCentered(Rectangle rectToDraw, Rectangle renderSurface)
        {
            return new Vector2(DrawRectangleHCentered(rectToDraw, renderSurface),
                DrawRectangleVCentered(rectToDraw, renderSurface));
        }
    }
}
