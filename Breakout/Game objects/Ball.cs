﻿using Breakout.Game_objects.Base;
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

        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        public Vector2 velocity; // = new();
         Dictionary<int, float> speedupFactor;

        /*internal Ball(Rectangle position) : base(position)
        {
        }*/

        internal Ball(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
            speedupFactor = new Dictionary<int, float> {
                {4, 2f },  //just to pick some numbers atm;  TODO: Update these later, as needed
                {12, 2f },
                {36, 2f },
                {62, 2f }
            };

            velocity = new Vector2(1, 1);  //for a total of: sqrt(2) (units?) as the initial speed.  TODO: maybe change this; we'll see
        }

        /*internal Ball() : base()
        {
        }*/

        internal void SpeedUp(int bricksDestroyed)
        {
            velocity.X *= speedupFactor[bricksDestroyed];
            velocity.Y *= speedupFactor[bricksDestroyed];
        }
    }
}
