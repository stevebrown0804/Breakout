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
        //IN PROGRESS: Figure out the spacing of the various regions of the game screen
        // ...and give stuff (public) variable names.  *thumbs up*

        public int topAreaHeight;
        public int middleAreaHeight;
        public int paddleAreaHeight;
        public int bottomAreaHeight;
        public int wallThickness;
        public int playingFieldPaddingOnAllFourSides;
        public int brickGridSpacingOnAllFourSides;
        public int brickGridBottomSpacing;
        public int intraBrickHorizontalSpacing;
        public int intraBrickVerticalSpacing;

        internal Spacing(GraphicsDeviceManager graphics)
        {
            //Set various hard-coded values
            playingFieldPaddingOnAllFourSides = 200; // 30; //50;
            wallThickness = 30;
            brickGridSpacingOnAllFourSides = 20;
            brickGridBottomSpacing = 210;
            intraBrickHorizontalSpacing = 10;
            intraBrickVerticalSpacing = 10;

            topAreaHeight = 70;     //Can we do away with any of these literals?  TBD
            paddleAreaHeight = 20;
            bottomAreaHeight = 110;         
        }

        internal void RecomputeValues(GraphicsDeviceManager graphics, GamePlayView gamePlayView)
        {
            //Here we'll put stuff that's dependant on other stuff
            middleAreaHeight = gamePlayView.playingField.position.Height -
                                topAreaHeight - bottomAreaHeight - paddleAreaHeight;
        }
    }
}
