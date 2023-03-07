using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_objects.Window_areas;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Breakout.Subsystems.misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Breakout.Game_objects
{
    internal class PauseMenu : GameObject
    {
        //stuff to stash
        //BO_Keyboard keyboard;
        Renderer renderer;
        StringRenderer stringRenderer;
        Spacing spacing;

        public bool isPaused = false;
        //public bool wasJustPaused = false;
        public GamePlayState prevStateBeforePaused;
        public Dictionary<string, string> pauseMenuPrompts = new();

        internal enum PauseMenuOptions
        {
            resume,
            exit
        }
        internal PauseMenuOptions pauseMenuOptions;


        internal PauseMenu(Rectangle position) : base(position)
        {
            //Initialize();
        }

        internal void Initialize(SubsystemsHolder subsystems)
        {
            //stash stuff
            renderer = subsystems.renderer;
            stringRenderer = subsystems.stringRenderer;
            spacing = subsystems.spacing;

            pauseMenuPrompts["header"] = "Game paused";
            pauseMenuPrompts["resume"] = "Resume";
            pauseMenuPrompts["exit"] = "Exit to main menu";

            pauseMenuOptions = PauseMenuOptions.resume;
        }

        public void SecondInitialize(GamePlayView gpv)
        {
            int pauseMenuWidth;
            int pauseMenuHeight;            
            int pauseMenuX;
            int pauseMenuY;
            InteriorToWalls intr = gpv.interiorToWalls;
            Spacing sp = spacing;

            float pauseMenuMaxFontWidth = stringRenderer.GetStringSizeMaxX(gpv.pauseMenuFont, pauseMenuPrompts);
            pauseMenuWidth = (int)pauseMenuMaxFontWidth + 2 * gpv.spacing.pauseMenuInternalSideSpacing;
            pauseMenuX = intr.position.X + intr.position.Width / 2 - pauseMenuWidth / 2;
            pauseMenuY = intr.position.Y + sp.pauseMenuExteriorTopSpacing;
            pauseMenuHeight = ComputeTotalHeight(sp.pauseMenuInternalTopSpacing, sp.pauseMenuInternalBottomSpacing, sp.pauseMenuPostHeaderSpacing, sp.pauseMenuIntraLineSpacing, gpv.pauseMenuFont);

            UpdatePosition(new Rectangle(pauseMenuX, pauseMenuY, pauseMenuWidth,
                pauseMenuHeight));
        }

        private int ComputeTotalHeight(int topSpacing, int bottomSpacing, int postHeaderSpacing, int intraLineSpacing, SpriteFont font)
        {
            int height = topSpacing;
            Vector2 stringSize;
            foreach (string k in pauseMenuPrompts.Keys)
            {
                stringSize = font.MeasureString(pauseMenuPrompts[k]);
                height += (int)stringSize.Y + intraLineSpacing;
            }
            height -= intraLineSpacing; //subtract out the last one
            height += bottomSpacing + postHeaderSpacing;

            return height;
        }

        private void UpdatePosition(Rectangle rect)
        {
            position = rect;
            boundingBox = rect; //let's do this too...just in case? *shrug*
        }

        internal void DrawPauseMenu(GamePlayView gpv)
        {
            GameElement el;

            int x, y, w, h;
            x = position.X;
            y = position.Y; 
            w = position.Width;
            h = position.Height;
            el = new(RenderType.UI, CallType.Rectangle, gpv.black1x1, new Rectangle(x,y,w,h), Color.White);
            renderer.AddToRenderList(el);
            
            y += spacing.pauseMenuInternalTopSpacing;
            Vector2 vec = new(stringRenderer.RenderStringHCentered(pauseMenuPrompts["header"], gpv.pauseMenuFont, position), y);
            el = new(RenderType.Text, gpv.pauseMenuFont, pauseMenuPrompts["header"], vec, Color.White);
            renderer.AddToRenderList(el);

            Vector2 spriteSize = gpv.pauseMenuFont.MeasureString(pauseMenuPrompts["resume"]);
            y += (int)spriteSize.Y + spacing.pauseMenuIntraLineSpacing + spacing.pauseMenuPostHeaderSpacing;
            vec = new(stringRenderer.RenderStringHCentered(pauseMenuPrompts["resume"], gpv.pauseMenuFont, position), y);
            el = new(RenderType.Text, gpv.pauseMenuFont, pauseMenuPrompts["resume"], vec, pauseMenuOptions == PauseMenuOptions.resume ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);

            spriteSize = gpv.pauseMenuFont.MeasureString(pauseMenuPrompts["exit"]);
            y += (int)spriteSize.Y + spacing.pauseMenuIntraLineSpacing;
            vec = new(stringRenderer.RenderStringHCentered(pauseMenuPrompts["exit"], gpv.pauseMenuFont, position), y);
            el = new(RenderType.Text, gpv.pauseMenuFont, pauseMenuPrompts["exit"], vec, pauseMenuOptions == PauseMenuOptions.exit ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);
        
        }//END DrawPauseMenu()

    }//END class PauseMenu
}
