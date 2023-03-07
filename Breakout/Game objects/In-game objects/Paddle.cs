using Breakout.Game_objects.Base;
using Breakout.Game_objects.Window_areas;
using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;

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
        public int originalWidth;
        internal bool hasOneTopRowBrickBeenHitThisLife = false;
        public GamePlayState nextState;

        public bool isShrinkingToHalf = false;        
        public bool haveShrinkToHalfTimersBeenSet = false;        
        private TimeSpan shrinkingToHalfEndsAt;
        private TimeSpan shrinkToHalfStartTimer;
        private TimeSpan shrinkToHalfElapsedTime = TimeSpan.Zero;
        float shrinkToHalfShrinkagePerMS = 0f;
        float shrinkToHalfSizeTargetWidth;

        public bool isShrinkingToNothing = false;
        public bool haveShrinkToNothingTimersBeenSet = false;
        private TimeSpan shrinkingToNothingEndsAt;
        private TimeSpan shrinkToNothingStartTimer;
        private TimeSpan shrinkToNothingElapsedTime = TimeSpan.Zero;
        float shrinkToNothingShrinkagePerMS = 0f;        
        
        internal Paddle(Rectangle position) : base(position) { }

        //Animate the paddle
        public void Move(Direction direction, GameTime gameTime, GamePlayView gpv)
        {
            Vector2 velocity = new(0, 0);
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

        public void ShrinkToHalfSize(GameTime gameTime)
        {
            if (!haveShrinkToHalfTimersBeenSet)
            {
                TimeSpan shrinkageDuration = new System.TimeSpan(0, 0, 0, 0, 500);        //0.5sec shrinking
                double shrinkageDurationInMS = shrinkageDuration.TotalMilliseconds;

                shrinkToHalfStartTimer = gameTime.TotalGameTime;
                shrinkingToHalfEndsAt = gameTime.TotalGameTime + shrinkageDuration;
                shrinkToHalfSizeTargetWidth = position.Width / 2;
                shrinkToHalfShrinkagePerMS = (position.Width - shrinkToHalfSizeTargetWidth) / (float) shrinkageDurationInMS;
                haveShrinkToHalfTimersBeenSet = true;
                //Debug.Print("Paddle.ShrinkToHalfSize says: timers have been set");
            }

            if (shrinkToHalfStartTimer + shrinkToHalfElapsedTime < shrinkingToHalfEndsAt)
            {
                int shrinkage = (int)(shrinkToHalfShrinkagePerMS * gameTime.ElapsedGameTime.TotalMilliseconds);
                position.Width -= shrinkage;
                position.X += shrinkage / 2;
            }
            else
            {
                if (position.Width != (int)shrinkToHalfSizeTargetWidth)
                    position.Width = (int)shrinkToHalfSizeTargetWidth;

                //Debug.Print("Paddle.ShrinkToHalfSize says: Done shrinking to half");
                isShrinkingToHalf = false;
            }
            shrinkToHalfElapsedTime += gameTime.ElapsedGameTime;
        }
       
         public GamePlayState ShrinkToNothing(GameTime gameTime)
        {
            if (!haveShrinkToNothingTimersBeenSet)
            {
                TimeSpan shrinkageDuration = new System.TimeSpan(0, 0, 0, 0, 750);  //750ms of shrinking
                double shrinkageDurationInMS = shrinkageDuration.TotalMilliseconds;

                shrinkToNothingElapsedTime = TimeSpan.Zero;
                shrinkToNothingStartTimer = gameTime.TotalGameTime;
                shrinkingToNothingEndsAt = gameTime.TotalGameTime + shrinkageDuration;  

                //float targetWidth = 0;      //for 'shrink to nothing'
                shrinkToNothingShrinkagePerMS = (position.Width) / (float) shrinkageDurationInMS;
                haveShrinkToNothingTimersBeenSet = true;
                //Debug.Print("Paddle.ShrinkToNothing says: timers have been set");
            }

            if (shrinkToNothingStartTimer + shrinkToNothingElapsedTime < shrinkingToNothingEndsAt)
            {
                int shrinkage = (int)(shrinkToNothingShrinkagePerMS * gameTime.ElapsedGameTime.TotalMilliseconds);
                //Debug.Print($"Paddle.ShrinkToNothing says: shrinking the paddle by {shrinkage}");
                position.Width -= shrinkage;
                position.X += shrinkage / 2;
            }
            else
            {
                //Debug.Print("Paddle.ShrinkToNothing says: Done shrinking to nothing");
                isShrinkingToNothing = false;  //superfluous? probably, but *shrug*

                if (position.Width != 0)
                    position.Width = 0;

                return nextState;
            }
            shrinkToNothingElapsedTime += gameTime.ElapsedGameTime;

            return GamePlayState.PaddleShrinkingToNothing;

        }//END ShrinkToNothing()

        private void ResetShrinkingToHalf()
        {
            shrinkToHalfElapsedTime = TimeSpan.Zero;
            haveShrinkToHalfTimersBeenSet = false;
            isShrinkingToHalf = false;
        }

        private void ResetShrinkingToNothing()
        {
            shrinkToNothingElapsedTime = TimeSpan.Zero;
            haveShrinkToNothingTimersBeenSet = false;
            isShrinkingToNothing = false;
        }

        public void ResetPaddle(GamePlayView gpv)
        {
            hasOneTopRowBrickBeenHitThisLife = false;
            ResetShrinkingToHalf();
            ResetShrinkingToNothing();

            position.Width = originalWidth;
            position.X = gpv.interiorToWalls.position.X + gpv.interiorToWalls.position.Width / 2 - position.Width / 2;
        }

    }//END class Paddle
}
