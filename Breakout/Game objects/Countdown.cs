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
                                            // assuming we even us it, that is.  TBD

        private bool timersSet = false;
        private TimeSpan startTimer;
        private TimeSpan explosionEndsAt;        
        private TimeSpan elapsedTime = TimeSpan.FromSeconds(0);

        internal Countdown(Rectangle position) : base(position)
        {
        }

        //TODO: Countdown.DoCountdown()
        internal void DoCountdown(GameTime gameTime)
        {

        }

        //TODO: Countdown.ExtendTimers()
        internal void ExtendTimers(TimeSpan timeSpan)
        {
            //Hey, couldn't we just NOT add to the elapsed time while paused?  TBD
        }
    }
}
