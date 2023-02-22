using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* "Every time a brick is hit, a tasteful explosion of particles occurs.  The explosion happens by having particles take up the same space (surface area) as the brick and then explode out based upon their position relative to the center.  Alternatively, the explosion could be the particles falling down, with a stickiness based upon their relative position on the brick (higher in the brick, the stickier).
"*/

namespace Breakout.Game_elements
{
    //IN PROGRESS: Brick class
    internal class Brick : GameObject
    {
        //"Starting from the bottom, two rows of yellow, two rows of orange, two rows of blue, and two rows of green."

        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        public bool beenHit = false;    //TODO: change this based on a collision

        internal Brick(Rectangle position) : base(position)
        {
        }

        internal Brick(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal Brick() : base()
        {
        }

        public void Explode()
        {
            //Boom!
            //TODO: Exploding bricks
        }
    }
}
