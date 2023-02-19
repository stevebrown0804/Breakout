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
        //TODO: Ball class

        //Reminder:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        internal Ball(Rectangle position) : base(position)
        {
        }

        internal Ball(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal Ball() : base()
        {
        }
    }
}
