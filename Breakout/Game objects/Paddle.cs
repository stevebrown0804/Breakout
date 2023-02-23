using Breakout.Game_objects.Base;
using Breakout.Game_states;
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

    public enum Direction
    {
        Left,
        Right
    }

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

        //IN PROGRESS: Animate the paddle
        //TODO: Have the L/R motion be based on the velocity vector
        public void Move(Direction direction, GameTime gameTime, GamePlayView gpv)
        {
            int deltaX;
            if(direction == Direction.Left)
            {
                deltaX = -10;  //TMP
            }
            else //direction == Direction.Right
            {
                deltaX = 10;  //TMP
            }

            position.X += deltaX;
            MoveAttachedBalls(deltaX, gpv);
        }

        private void MoveAttachedBalls(int deltaX, GamePlayView gpv)
        {
            for(int i = 0; i < gpv.balls.Count; i++)
            {
                if (gpv.balls[i].AtRest())
                {
                    gpv.balls[i].position.X += deltaX;
                }
            }
        }

    }
}
