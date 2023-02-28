using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class RemainingLives : GameObject
    {
        //stuff to stash
        GamePlayView gpv;
        Renderer renderer;
        ContentManager contentManager;

        //sprites, fonts
        private Texture2D remainingLife100x20;  //remaining life icon
        //SpriteFont inGameScoreFont;  //borrowing this

        //other variables
        //string remainingLivesStr = "TODO: Remaining Lives";
        public int remainingLives = 3;


        internal RemainingLives(Rectangle position, SubsystemsHolder subsystems, GamePlayView gpv) : base(position)
        {
            //stash subsystems
            this.gpv = gpv;
            this.renderer = subsystems.renderer;
        }

        internal void loadContent()
        {
            //stash this
            contentManager = gpv.contentManager;

            //load the sprite
            remainingLife100x20 = contentManager.Load<Texture2D>("Sprites/remainingLife100x20");
            //and the font
            //inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
        }

        internal void DrawRemainingLives()
        {
            GameElement el;

            Vector2 vec = new(position.X + gpv.spacing.remainingLivesLeftSpacing, position.Y + gpv.spacing.remainingLivesTopSpacing);
            float initX = vec.X;
            for (int i = 0; i < remainingLives - 1; i++)
            {
                vec.X = initX + i * (remainingLife100x20.Width + gpv.spacing.remainingLivesIntraSpriteSpacing);
                el = new(RenderType.UI, CallType.Vector2, remainingLife100x20, vec, Color.White);
                renderer.AddToRenderList(el);                
            }
        }

    }//END class RemainingLives
}
