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
        //TODO: Render the ACTUAL pauseMenu object
        //TODO: Make the pause menu functional

        public bool isPaused = false;
        public Dictionary<string, string> pauseMenuPrompts = new();

        internal PauseMenu(Rectangle position) : base(position)
        {
            Intialize();
        }

        private void Intialize()
        {
            pauseMenuPrompts["header"] = "Game paused";
            pauseMenuPrompts["resume"] = "Resume";
            pauseMenuPrompts["exit"] = "Quit";
        }

        //DONE, I THINK: figuring out the pause menu rectangle; TODO: Actually try the pause menu out
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
            int pauseMenuX;
            int pauseMenuY;
            InteriorToWalls intr = gamePlay.interiorToWalls;
            Spacing sp = gamePlay.spacing;

            float pauseMenuMaxFontWidth = GetStringSizeMaxX(gamePlay.pauseMenuFont);
            pauseMenuWidth = (int)pauseMenuMaxFontWidth + gamePlay.spacing.pauseMenuInternalSideSpacing;
            pauseMenuX = intr.position.X + intr.position.Width / 2 - pauseMenuWidth / 2;
            pauseMenuY = intr.position.Y + sp.pauseMenuTopSpacing;
            pauseMenuHeight = ComputeTotalHeight(sp.pauseMenuInternalTopSpacing, sp.pauseMenuInternalBottomSpacing, sp.pauseMenuIntraLineSpacing, gamePlay.pauseMenuFont);

            UpdatePosition(new Rectangle(pauseMenuX, pauseMenuY, pauseMenuWidth,
                pauseMenuHeight));
        }

        private int ComputeTotalHeight(int topSpacing, int bottomSpacing, int intraLineSpacing, SpriteFont font)
        {
            int height = topSpacing;
            Vector2 stringSize;
            foreach (string k in pauseMenuPrompts.Keys)
            {
                stringSize = font.MeasureString(pauseMenuPrompts[k]);
                height += (int)stringSize.Y + intraLineSpacing;
            }
            height += bottomSpacing;

            return height; // 100; //TMP
        }

        private void UpdatePosition(Rectangle rect)
        {
            position = rect;
            boundingBox = rect; //let's do this too...just in case? *shrug*
        }

        private float GetStringSizeMaxX(SpriteFont font)
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
