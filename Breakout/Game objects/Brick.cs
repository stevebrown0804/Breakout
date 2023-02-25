using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
//using System;
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

        public bool hasBeenHit = false;
        public bool isExploding = false;
        private GameTime explosionEndsAt;

        internal Brick(Rectangle position) : base(position)
        {
        }

        /*internal Brick(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal Brick() : base()
        {
        }*/

        public void Explode(GameTime gameTime)
        {
            if (isExploding)
            {
                //Boom!
                //Debug.Print($"The brick at {position} says: Boom!");

                //TODO: Exploding bricks!
                //explosionEndsAt = gameTime.ElapsedGameTime + new System.TimeSpan(0, 0, 3);  //3 second explosion.

                //TODO: When it hits the end of its 3sec (or w/e we settle on) if exploding, disable
                isExploding = false;    //TMP (isExploding = false;)
            }
            
        }
    }
}
