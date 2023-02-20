using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects.non_derived
{
    internal class Spacing
    {
        //TODO: Figure out the spacing of the various regions of the game screen
        // ...and give stuff (public) variable names.  *thumbs up*

        public int topAreaHeight;
        public int middleAreaHeight;
        public int paddleAreaHeight;

        internal Spacing()
        {
            //REMINDER: Order matters, here.

            topAreaHeight = 200;    //just picking a number for the moment.  TODO: Actual topAreaHeight
            paddleAreaHeight = 50;  //TODO: Fix this formula so that it has no constants, if possible
            middleAreaHeight = 1080 - topAreaHeight - 100 - paddleAreaHeight;  //100->bottomAreaHeight; //TODO: Fix this formula, once we can.  (It should have no constants...I think)
        }
    }
}
