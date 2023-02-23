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

        //Animate the paddle
        public void Move(Direction direction, GameTime gameTime, GamePlayView gpv)
        {
            Vector2 velocity = new();
            TimeSpan time = gameTime.ElapsedGameTime;
            float deltaX;
            if (direction == Direction.Left)
            {
                velocity.X = -1f;   //Turn out, this is perfect.  who knew!                
                deltaX = velocity.X * (float)time.TotalMilliseconds;
            }
            else //direction == Direction.Right
            {
                velocity.X = 1f;                
                deltaX = velocity.X * (float)time.TotalMilliseconds;
            }

            //Check that deltaX won't put the position out of bounds
            if (position.X + deltaX < gpv.interiorToWalls.position.X)
            {
                position.X = gpv.interiorToWalls.position.X;
            }
            else if (position.X + deltaX > gpv.interiorToWalls.position.X + gpv.interiorToWalls.position.Width - position.Width)
            {
                position.X = gpv.interiorToWalls.position.X + gpv.interiorToWalls.position.Width - position.Width;
            }
            else  //if all is well, add deltaX to position.X (and move any attached balls)
            {
                position.X += (int)deltaX;
                MoveAttachedBalls((int)deltaX, gpv);
            }
        }//END Move()

        private void MoveAttachedBalls(int deltaX, GamePlayView gpv)
        {
            for(int i = 0; i < gpv.balls.Count; i++)
            {
                if (gpv.balls[i].IsAtRest())
                {
                    gpv.balls[i].position.X += deltaX;
                }
            }
        }

    }
}
