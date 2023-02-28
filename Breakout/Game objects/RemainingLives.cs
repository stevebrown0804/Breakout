using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class RemainingLives : GameObject
    {
         public int remainingLives = 3;  // 2?  TBD  //<--nah, 3 works

        internal RemainingLives(Rectangle position) : base(position)
        {
        }
    }
}
