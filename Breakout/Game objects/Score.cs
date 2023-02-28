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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

/* "Scoring
    1 point for each yellow brick
    2 points for each orange brick
    3 points for each blue brick
    5 points for each green brick
    25 points when a line is cleared
    Every 100 points the player earns a second ball that automatically starts from the middle of the paddle (no space bar to release it).  This new ball starts at the initial slow speed and increases in speed according to the above pattern.  In other words, each ball has its own speed and own state for speed increases." */

namespace Breakout.Game_objects
{
    //TODO: Implement class Score
    internal class Score : GameObject
    {

        GamePlayView gpv;
        Renderer renderer;
        SpriteFont inGameScoreFont;

        internal Score(Rectangle position, SubsystemsHolder subsystems, GamePlayView gpv) : base(position)
        {
            this.gpv = gpv;
            renderer = subsystems.renderer;
        }

        internal void loadContent()
        {
            inGameScoreFont = gpv.contentManager.Load<SpriteFont>("Fonts/ingame-score");
        }

        internal void DrawScore()
        {
            GameElement el;

            Vector2 vec = new(position.X, position.Y);
            el = new(RenderType.Text, gpv.inGameScoreFont, "TODO: Score", vec, Color.White);

            renderer.AddToRenderList(el);
        }

    }
}
