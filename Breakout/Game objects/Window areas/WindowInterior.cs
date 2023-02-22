using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal class WindowInterior : GameObject
    {
        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        internal WindowInterior(Rectangle position) : base(position)
        {
        }

        /*internal WindowInterior(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
            //Window interior probably won't have a bounding box
        }*/

        /*internal WindowInterior() : base()
        {
        }*/
    }
}
