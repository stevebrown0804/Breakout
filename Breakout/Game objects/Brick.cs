using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* "Every time a brick is hit, a tasteful explosion of particles occurs.  The explosion happens by having particles take up the same space (surface area) as the brick and then explode out based upon their position relative to the center.  Alternatively, the explosion could be the particles falling down, with a stickiness based upon their relative position on the brick (higher in the brick, the stickier).
"*/

//MAYBE: Should we make an 'exploding brick' class, maybe?  TBD

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
