using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class RemainingLivesIcons : GameObject
    {
         public int remainingLives = 3;  // 2?  TBD

        internal RemainingLivesIcons(Rectangle position) : base(position)
        {
        }

        /*internal RemainingLivesIcons(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal RemainingLivesIcons() : base()
        {
        }*/
    }
}
