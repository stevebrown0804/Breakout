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
        //Reminder:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        //TODO: Is this class necessary?  I'm thinking that it'll just be Rect(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), but we'll see I guess
        //And hey...ya never know when we'll decide to put the whole game within another region or something *shrug*

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
