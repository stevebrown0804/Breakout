using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* "The ball speed increases (you can choose the rate) at the following intervals; start over when starting a new paddle:
    4 bricks removed
    12 bricks removed
    36 bricks removed
    62 bricks removed" */

namespace Breakout.Game_elements
{
    internal class Ball : GameObject
    {
        //IN PROGRESS: Ball class

        //TODO: Have the ball update its velocity in response to a collisions

        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        public Vector2 velocity; // = new();
         Dictionary<int, float> speedupFactor;

        internal Ball(Rectangle position) : base(position)
        {
            Initialize();
        }

        internal Ball(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
            Initialize();
        }

        /*internal Ball(Vector2 position) : base(position)      //let's just use a Rectangle  //<---actually...TBD
        {
            Initialize();
        }*/

        /*internal Ball() : base()
        {
        }*/

        private void Initialize()
        {
            speedupFactor = new Dictionary<int, float> {
                {4, 2f },  //just to pick some numbers atm;  TODO: Update these later, as needed
                {12, 2f },
                {36, 2f },
                {62, 2f }
            };

            velocity = new Vector2(0, 0);  //Initially at rest -> moving with the paddle
         }

        public void GiveVelocity()
        {
            //EVENTUALLY: screw around with these values until they feel right
            velocity.X = 1f; //0.3f; //45 degrees to the right, I think. <--left, atm
            velocity.Y = -1f; //-0.3f; 
        }

        internal bool IsAtRest()
        {
            return velocity.X == 0 && velocity.Y == 0;  
        }

        //IN PROGRESS: Ball.Move()
        internal void Move(GameTime gameTime, GamePlayView gpv)
        {
            TimeSpan time = gameTime.ElapsedGameTime;
            float deltaX = velocity.X * (float)time.TotalMilliseconds;
            float deltaY = velocity.Y * (float)time.TotalMilliseconds;
            
            //IN PROGRESS check for collisions and bounce accordingly
            //CD with the walls
            foreach(Wall w in gpv.walls)
            {
                //https://gamedev.stackexchange.com/questions/13774/how-do-i-detect-the-direction-of-2d-rectangular-object-collisions?noredirect=1&lq=1

                Rectangle test_position = position;
                test_position.X += (int)deltaX;
                test_position.Y += (int)deltaY;
                if (CollisionDetection.DoTheyIntersect(w.position, test_position)) //position))
                {
                    //Left/right wall
                    if(CollisionDetection.FromTheRight(position, deltaX, w.position) || 
                       CollisionDetection.FromTheLeft(position, deltaX, w.position))
                        velocity.X = -(velocity.X);

                    //Top wall
                    //else if (position.Y >= w.position.Y + w.position.Height && test_position.Y < w.position.Y + w.position.Height)
                    else if(CollisionDetection.FromTheBottom(position, deltaY, w.position))
                    {
                        velocity.Y = -(velocity.Y);
                    }
                    else if(true) //TMP -- bottom wall?  *shrug*
                    {
                        //No bottom wall, yo
                    }
                }
                /*CollisionType type = CollisionDetection.GetIntersectType(w.position, position);
                if(type == CollisionType.horizontal)
                {
                    velocity.X = -(velocity.X);
                }
                else if(type == CollisionType.vertical)
                {
                    velocity.Y = -(velocity.Y);
                }
                else if(type == CollisionType.none)
                {
                    //No collision
                }
                else
                {
                    throw new Exception("Uh...is somebody using softICE?  c'mon.");
                }*/
            }
            
            //and the bricks

            //and the paddls



            //For the moment...  TMP
            position.X += (int)deltaX;
            position.Y += (int)deltaY;
        }

        //DONE, MAYBE?: Have the ball speed up when a certain # of bricks are destroyed
        internal void SpeedUp(int bricksDestroyed)
        {
            velocity.X *= speedupFactor[bricksDestroyed];
            velocity.Y *= speedupFactor[bricksDestroyed];
        }
    }
}
