﻿using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal class Wall : GameObject
    {
        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        internal Wall(Rectangle position) : base(position)
        {
        }

/*        internal Wall(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal Wall() : base()
        {
        }*/
    }
}
