using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* "The paddle shrinks to half size when the player breaks through (makes a hole) in the top green row.  On start of new paddle, if top row already has a hole, start with the full size paddle and then shrink to half size when a new brick in the top green row is destroyed.
 
  When the player misses the ball, the paddle performs a shrink animation to make it disappear." */

namespace Breakout.Game_elements
{
    internal class Paddle : GameObject
    {
        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        internal Paddle(Rectangle position) : base(position)
        {
        }

        /*internal Paddle(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal Paddle() : base()
        {
        }*/

        public void Shrink(GameTime gameTime)
        {
            //Shrink!
            //TODO: Shrink the paddle (as an animation)
        }

        //TODO: Animate the paddle
        public void MoveLeft(GameTime gameTime)
        {
            //Move left!
        }

        public void MoveRight(GameTime gameTime)
        {
            //Move right!
        }

    }
}
