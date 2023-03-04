using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

/* "Scoring
    1 point for each yellow brick
    2 points for each orange brick
    3 points for each blue brick
    5 points for each green brick
    25 points when a line is cleared
    Every 100 points the player earns a second ball that automatically starts from the middle of the paddle (no space bar to release it).  This new ball starts at the initial slow speed and increases in speed according to the above pattern.  In other words, each ball has its own speed and own state for speed increases." */

namespace Breakout.Game_objects
{
    internal class Score : GameObject
    {
        //stuff to stash
        GamePlayView gpv;
        Renderer renderer;
        ContentManager contentManager;

        //sprites, fonts
        //SpriteFont inGameScoreFont;

        string scoreStr = "Score: ";
        internal int score = 0;
        int blockOf100 = 0;

        internal Score(Rectangle position, SubsystemsHolder subsystems, GamePlayView gpv) : base(position)
        {
            this.gpv = gpv;
            renderer = subsystems.renderer;
        }

        internal void loadContent()
        {
            //stash this
            this.contentManager = gpv.contentManager;

            //inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
        }

        internal void IncreaseScore(int increase)
        {
            score += increase;

            int newScoreDiv100 = score / 100;
            if(blockOf100 < newScoreDiv100)
            {
                //Debug.Print($"Another 100pts has been reached ({score}pts, {newScoreDiv100}); releasing a ball");
                //Release a ball
                Ball ball = new(new Rectangle(gpv.paddle.position.X + gpv.paddle.position.Width / 2 - gpv.spacing.ballWidth / 2, gpv.paddle.position.Y - gpv.spacing.ballHeight, gpv.spacing.ballWidth, gpv.spacing.ballHeight));
                //Debug.Print($"Ball spawned; hitBricksAtSpawnTime set to: {gpv.brickGrid.numBricksHit}");
                ball.SetHitBricksAtSpawnTime(gpv.brickGrid.numBricksHit);
                ball.GiveVelocity();
                gpv.balls.Add(ball);
                blockOf100 = newScoreDiv100;
            }
        }

        internal void DrawScore()
        {
            GameElement el;
            Vector2 spriteSize = gpv.inGameScoreFont.MeasureString(scoreStr);
            Vector2 vec = new(position.X + gpv.spacing.scoreSectionLeftSpacing, position.Y + gpv.spacing.scoreSectionTopSpacing);
            el = new(RenderType.Text, gpv.inGameScoreFont, scoreStr, vec, Color.White);
            renderer.AddToRenderList(el);
            vec.X += spriteSize.X + gpv.spacing.scoreSectionIntraWordSpacing;
            el = new(RenderType.Text, gpv.inGameScoreFont, $"{score}", vec, Color.White);
            renderer.AddToRenderList(el);
        }

    }//END class Score
}
