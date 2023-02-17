using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal class Brick
    {
        int x;
        int y;
        int width;
        int height;
        Color color;

        Brick(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }
    }
}
