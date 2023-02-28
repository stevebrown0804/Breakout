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
        GamePlayView gpv;
        Renderer renderer;
        public int remainingLives = 3;  // 2?  TBD  //<--nah, 3 works
        private Texture2D remainingLife100x20;  //remaining life icon
        
        

        internal RemainingLives(Rectangle position, SubsystemsHolder subsystems, GamePlayView gpv) : base(position)
        {
            //stash subsystems
            this.gpv = gpv;
            this.renderer = subsystems.renderer;

            
        }

        internal void loadContent()
        {
            //load the sprite
            remainingLife100x20 = gpv.contentManager.Load<Texture2D>("Sprites/remainingLife100x20");    //remaining life icon
        }

        internal void DrawRemainingLives()
        {
            GameElement el;

            Vector2 vec = new(position.X, position.Y);
            el = new(RenderType.Text, gpv.inGameScoreFont, "TODO: Remaining lives", vec, Color.White);

            renderer.AddToRenderList(el);
        }
    }
}
