using Breakout.Game_objects.Base;
using Breakout.Game_objects.non_derived;
using Breakout.Game_objects.Window_areas;
using Breakout.Game_states;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class PauseMenu : GameObject
    {
        //TODO: PauseMenu class
        //ALSO TODO: Think of any other classes we may need, in-game

        public bool showPauseMenu = false;
        public Dictionary<string, string> pauseMenuPrompts = new();
        /*public string pauseMenuHeader = "Game Paused";
        public string pauseMenuResumePrompt = "Resume";
        public string pauseMenuExitPrompt = "Quit";*/

        internal PauseMenu(Rectangle position) : base(position)
        {
            Intialize();
        }

        //the pause menu probably doesn't need a bounding box
        /*internal PauseMenu(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal PauseMenu() : base()
        {
        }*/

        private void Intialize()
        {
            pauseMenuPrompts["header"] = "Game paused";
            pauseMenuPrompts["resume"] = "Resume";
            pauseMenuPrompts["exit"] = "Quit";
        }

        public void SecondInitialize(GamePlayView gamePlay)
        {
            //spacing.
            /*pauseMenuTopSpacing = 100;
            pauseMenuInternalTopSpacing = 10;
            pauseMenuInternalBottomSpacing = 10;
            pauseMenuInternalSideSpacing = 10;
            pauseMenuIntraLineSpacing = 5;*/

            int pauseMenuWidth;
            int pauseMenuHeight;
            int pauseMenuX = 100; //TMP
            int pauseMenuY = 100;  //TMP
            float pauseMenuMaxFontWidth = GetStringSizeMaxX(gamePlay.pauseMenuFont);
            pauseMenuWidth = (int)pauseMenuMaxFontWidth + gamePlay.spacing.pauseMenuInternalSideSpacing;
            pauseMenuHeight = 400;  //TMP


            int pauseMenuXCoord = gamePlay.interiorToWalls.position.X + gamePlay.spacing.pauseMenuTopSpacing;
            int pauseMenuYCoord;

            UpdatePosition(new Rectangle(pauseMenuX, pauseMenuY, pauseMenuWidth,
                pauseMenuHeight));
        }

        private void UpdatePosition(Rectangle rect)
        {
            position = rect;
            boundingBox = rect; //let's do this too...just in case? *shrug*
        }

        internal float GetStringSizeMaxX(SpriteFont font)
        {
            float maxSizeX = 0f;
            Vector2 stringSize;
            foreach (string k in pauseMenuPrompts.Keys)
            {
                stringSize = font.MeasureString(pauseMenuPrompts[k]);
                if(stringSize.X > maxSizeX)
                {
                    maxSizeX = stringSize.X;
                }
            }
            return maxSizeX; 
        }
    }
}
