using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal class Brick : GameObject
    {
        internal Color color;   //TODO: Make some 1x1 sprites of the colors required for the bricks
                                //"Starting from the bottom, two rows of yellow, two rows of orange, two rows of blue, and two rows of green."

        Brick(int x, int y, int width, int height, Color color)
        {
            position.X = x;
            position.Y = y;
            position.Width = width;
            position.Height = height;
            this.color = color;
        }
    }
}
