using Breakout.Game_states;
using Microsoft.Xna.Framework;
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
        public int bottomAreaHeight;
        public int wallThickness;
        public int playingFieldPaddingOnAllFourSides;

        internal Spacing(GraphicsDeviceManager graphics)
        {
            //First up, stuff that's hard-coded
            playingFieldPaddingOnAllFourSides = 50; //100;
            wallThickness = 30;

            //Next up...other stuff  (although here's still hard-coded.  what's up with that?  TBD)            
            topAreaHeight = 170;    //just picking a number for the moment.  TODO: Actual topAreaHeight
            paddleAreaHeight = 50;  //TODO: Fix this formula so that it has no constants, if possible
            bottomAreaHeight = 180;  //TODO: Fix this forumula so that it has no constants, if possible

            //RecomputeValues(graphics, gamePlayView);            
        }

        internal void RecomputeValues(GraphicsDeviceManager graphics, GamePlayView gamePlayView)
        {
            //Here we'll put stuff that's dependant on other stuff
            //REMINDER: Order matters, here.

            //middleAreaHeight = graphics.PreferredBackBufferHeight -
            middleAreaHeight = gamePlayView.playingField.position.Height -
                                topAreaHeight - bottomAreaHeight - paddleAreaHeight;
        }
    }
}
