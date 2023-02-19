using Breakout.Game_objects.Base;
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
        //Reminder:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        //TODO: Wall class
        //Wooboy, that's gonna be a biggun'.  
        //(kidding, I think)

        internal Wall(Rectangle position) : base(position)
        {
        }

        internal Wall(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal Wall() : base()
        {
        }
    }
}
