using Breakout.Game_objects.Base;
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

        //TODO: Animate the ball
        //TODO: Have the ball track its velocity
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

        /*internal Ball(Vector2 position) : base(position)      //let's just use a Rectangle
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

        internal bool AtRest()
        {
            return velocity.X == 0 && velocity.Y == 0;  
        }

        //TODO: Have the ball speed up when a certain # of bricks are destroyed
        internal void SpeedUp(int bricksDestroyed)
        {
            velocity.X *= speedupFactor[bricksDestroyed];
            velocity.Y *= speedupFactor[bricksDestroyed];
        }
    }
}
