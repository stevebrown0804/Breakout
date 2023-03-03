using Breakout.Game_states;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Subsystems.misc
{
    public class Spacing
    {
        //ONGOING: Figure out the spacing of the various regions of the game screen
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
        public int remainingLivesLeftSpacing;
        public int remainingLivesRightSpacing;
        public int remainingLivesTopSpacing;
        public int remainingLivesBottomSpacing;
        public int remainingLivesIntraSpriteSpacing;
        public int scoreSectionLeftSpacing;
        public int scoreSectionRightSpacing;
        public int scoreSectionTopSpacing;
        public int scoreSectionIntraWordSpacing;
        public int scoreSectionBottomSpacing;
        public int paddleWidth;
        public int paddleHeight;
        public int ballHeight;
        public int ballWidth;
        public int countdownSideSpacing;
        public int countdownTopSpacing;
        public int countdownBottomSpacing;
        public int pauseMenuExteriorTopSpacing;
        public int pauseMenuInternalTopSpacing;
        public int pauseMenuInternalBottomSpacing;
        public int pauseMenuInternalSideSpacing;
        public int pauseMenuIntraLineSpacing;
        public int pauseMenuPostHeaderSpacing;
        public int gameOverIntraLineSpacing;
        public int gameOverSideSpacing;
        public int gameOverTopSpacing;
        public int gameOverBottomSpacing;
        public int highScoresRegionExternalTopSpacing;
        //public int highScoresRegionExternalSideSpacing;
        public int highScoresRegionExternalBottomSpacing;
        public int highScoresRegionInternalSideSpacing;


        internal Spacing(GraphicsDeviceManager graphics)
        {
            //Set various hard-coded values
            playingFieldPaddingOnAllFourSides = 0; // 20; // 30; //50;

            wallThickness = 24;

            brickGridSpacingOnAllFourSides = 20;
            brickGridBottomSpacing = 220;

            intraBrickHorizontalSpacing = 10;
            intraBrickVerticalSpacing = 10;

            topAreaHeight = 70;
            bottomAreaHeight = 110;

            remainingLivesLeftSpacing = 20;
            remainingLivesRightSpacing = 60;  //unused? TBD
            remainingLivesTopSpacing = 20;
            remainingLivesBottomSpacing = 20;  //? TBD
            remainingLivesIntraSpriteSpacing = 24;  //initial guess

            scoreSectionLeftSpacing = 270;
            scoreSectionRightSpacing = 60;  //?
            scoreSectionTopSpacing = 10;
            scoreSectionBottomSpacing = 20; //?
            scoreSectionIntraWordSpacing = 30;

            paddleAreaHeight = 40;
            paddleWidth = 300;
            paddleHeight = paddleAreaHeight;

            ballWidth = 50;
            ballHeight = 50;

            countdownSideSpacing = 600;
            countdownTopSpacing = 100;
            countdownBottomSpacing = 300;

            pauseMenuExteriorTopSpacing = 150;
            pauseMenuInternalTopSpacing = 30;
            pauseMenuInternalBottomSpacing = 40;
            pauseMenuInternalSideSpacing = 60;
            pauseMenuIntraLineSpacing = 20;
            pauseMenuPostHeaderSpacing = 40;

            gameOverIntraLineSpacing = 70;
            gameOverSideSpacing = 120;
            gameOverTopSpacing = 200;
            gameOverBottomSpacing = 230;

            highScoresRegionExternalTopSpacing = 100;  //IN PROGRESS
            //highScoresRegionExternalSideSpacing = 0;
            highScoresRegionExternalBottomSpacing = 100;
            highScoresRegionInternalSideSpacing = 100; //TEMP-ish
        }

        internal void RecomputeValues(GraphicsDeviceManager graphics, GamePlayView gamePlayView)
        {
            //Here we'll put stuff that's dependant on other stuff
            middleAreaHeight = gamePlayView.playingField.position.Height -
                                topAreaHeight - bottomAreaHeight - paddleAreaHeight;
        }
    }
}
