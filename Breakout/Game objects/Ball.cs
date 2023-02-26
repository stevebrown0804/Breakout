﻿using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems.@static;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

/* "The ball speed increases (you can choose the rate) at the following intervals; start over when starting a new paddle:
    4 bricks removed
    12 bricks removed
    36 bricks removed
    62 bricks removed" */

namespace Breakout.Game_elements
{
    internal class Ball : GameObject
    {
        //DONE, I THINK: Ball class

        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        public Vector2 velocity; // = new();
         Dictionary<int, float> speedupFactor;

        internal Ball(Rectangle position) : base(position)
        {
            Initialize();
        }

        /*internal Ball(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
            Initialize();
        }*/

        /*internal Ball(Vector2 position) : base(position)      //let's just use a Rectangle  //<---actually...TBD
        {
            Initialize();
        }*/

        /*internal Ball() : base()
        {
        }*/

        private void Initialize()
        {
            //MAYBE: Keep messing these values (in Ball.Initialize()) later, as needed
            speedupFactor = new Dictionary<int, float> {
                {4, 1.2f },
                {12, 1.3f },
                {36, 1.4f },
                {62, 1.5f }
            };

            velocity = new Vector2(0, 0);  //Initially at rest -> moving with the paddle
         }

        public void GiveVelocity()
        {
            //MAYBE: keep messing around with these values (in Ball.GiveVelocity()), as needed
            velocity.X = 0.3f; //45 degrees to the right, I think. <--positive is right, negative is left
            velocity.Y = -0.3f; 
        }

        internal bool IsAtRest()
        {
            return velocity.X == 0 && velocity.Y == 0;  
        }

        //
        //DONE, I THINK: Ball.Move()
        internal void Move(GameTime gameTime, GamePlayView gpv)
        {
            TimeSpan time = gameTime.ElapsedGameTime;
            float deltaX = velocity.X * (float)time.TotalMilliseconds;
            float deltaY = velocity.Y * (float)time.TotalMilliseconds;
            Rectangle test_position = position;
            test_position.X += (int)deltaX;
            test_position.Y += (int)deltaY;

            //CD with the walls
            foreach (Wall w in gpv.walls)
            {                
                if (CollisionDetection.DoTheyIntersect(w.position, test_position))
                {
                    //Left/right wall
                    if(CollisionDetection.FromTheRight(position, deltaX, w.position) || 
                       CollisionDetection.FromTheLeft(position, deltaX, w.position))
                        velocity.X = -(velocity.X);

                    //Top wall
                    if(CollisionDetection.FromTheBottom(position, deltaY, w.position))
                        velocity.Y = -(velocity.Y);
                    //else //bottom wall
                        //No bottom wall, yo
                }
            }

            //...and the paddle
            if (CollisionDetection.DoTheyIntersect(gpv.paddle.position, test_position))
            {
                //Left/right side of paddle
                if (CollisionDetection.FromTheRight(position, deltaX, gpv.paddle.position) ||
                   CollisionDetection.FromTheLeft(position, deltaX, gpv.paddle.position))
                    velocity.X = -(velocity.X);

                //Top of paddle
                if(CollisionDetection.FromTheTop(position, deltaY, gpv.paddle.position))
                    velocity.Y = -(velocity.Y);
            }

            //...and the bricks
            if(CollisionDetection.DoTheyIntersect(gpv.brickGrid.position, test_position))
            {
                //do CD with each 'row region'
                var rr = gpv.rowRegions;
                for (int i = 0; i < rr.Count; i++)
                {
                    if (CollisionDetection.DoTheyIntersect(rr[i].position, test_position))
                    {
                        //Debug.Print($"RowRegion intersection found: ball: {test_position}; rowRegion[{i}]: {rr[i].position}");

                        //Having found a 'row region' that we collided with...
                        // Do CD with the individual bricks of the row (from aforementioned row region)
                        var bg = gpv.brickGrid.brickGrid;                        
                        for (int j = 0; j < bg[i].Count; j++)  //rowRegion and brickGrid.brickGrid appear to use the same row indices!  (...I hope)
                        {
                            if (!bg[i][j].hasBeenHit)
                            {
                                if (CollisionDetection.DoTheyIntersect(bg[i][j].position, test_position))
                                {
                                    //We seem to have a found a brick that's being collided with!
                                    // From the sides
                                    if (CollisionDetection.FromTheRight(position, deltaX, bg[i][j].position) ||
                                        CollisionDetection.FromTheLeft(position, deltaX, bg[i][j].position))
                                    {
                                        //Debug.Print($"Collision from the side: test_position:{test_position}, bg[{i}][{j}] position: {bg[i][j].position}");
                                        velocity.X = -(velocity.X);
                                    }

                                    // From the Top/bottom
                                    if (CollisionDetection.FromTheBottom(position, deltaY, bg[i][j].position) ||
                                        CollisionDetection.FromTheTop(position, deltaY, bg[i][j].position))
                                    {
                                        //Debug.Print($"Collision from the top/bottom: test_position:{test_position}, bg[{i}][{j}] position: {bg[i][j].position}");
                                        velocity.Y = -(velocity.Y);
                                    }                                   

                                    //Hide the brick and trigger the explosion animation
                                    bg[i][j].hasBeenHit = true;
                                    bg[i][j].isExploding = true;
                                    gpv.brickGrid.numBricksHit++;   //keep a tally of the number of bricks that have been hit
                                    if(speedupFactor.ContainsKey(gpv.brickGrid.numBricksHit))
                                    {
                                        SpeedUp(gpv.brickGrid.numBricksHit);
                                    }
                                    

                                }//END if (CollisionDetection.DoTheyIntersect(bg[i][j].position, test_position))
                            
                            }//END if (!bg[i][j].hasBeenHit) / else

                        }//END for (int j = 0; j < bg[i].Count; j++) 

                    }//END if (CollisionDetection.DoTheyIntersect(rr[i].position, test_position))

                }//END for (int i = 0; i < rr.Count; i++)

            }//END if(CollisionDetection.DoTheyIntersect(gpv.brickGrid.position, test_position))


            //Then we'll actually move the ball
            position.X = test_position.X; //+= (int)deltaX;
            position.Y = test_position.Y; //+= (int)deltaY;
        }

        internal void SpeedUp(int bricksDestroyed)
        {
            //Debug.Print($"Speeding up ball by a factor of {speedupFactor[bricksDestroyed]} (for {bricksDestroyed} bricks)");
            velocity.X *= speedupFactor[bricksDestroyed];
            velocity.Y *= speedupFactor[bricksDestroyed];
        }
    }
}
