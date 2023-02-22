using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class Countdown : GameObject
    {
        public bool showCountdown = false;   //we COULD set this to true here.  maybe.  TBD

        internal Countdown(Rectangle position) : base(position)
        {
        }
    }
}
