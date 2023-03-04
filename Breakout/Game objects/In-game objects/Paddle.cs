using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        internal Paddle(Rectangle position) : base(position)
        {
        }

        //TODO: Shrink the paddle (as an animation)
        public void Shrink(GameTime gameTime)
        {
            Debug.Print("TODO: Shrink the paddle");        
        }

        //Animate the paddle
        public void Move(Direction direction, GameTime gameTime, GamePlayView gpv)
        {
            Vector2 velocity = new();
            TimeSpan time = gameTime.ElapsedGameTime;
            float deltaX;
            if (direction == Direction.Left)
            {
                velocity.X = -(GetPaddleSpeed());                  
                deltaX = velocity.X * (float)time.TotalMilliseconds;
            }
            else //direction == Direction.Right
            {
                velocity.X = GetPaddleSpeed();
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

        private static float GetPaddleSpeed()
        {
            //MAYBE: apply the balls' multiplier to the paddle (or some kind of speedup)

            //LATER: Mess around with this value until we get something that feels right
            return 1.5f;  
        }

        private static void MoveAttachedBalls(int deltaX, GamePlayView gpv)
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
