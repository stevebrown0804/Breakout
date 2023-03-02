using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_objects.non_derived;
using Breakout.Game_objects.Window_areas.HighScoreView;
using Breakout.Subsystems.Base;
using Breakout.Subsystems.misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

/* "High Scores are persisted to the browser's local storage; keep and display up to the top 5 scores." */
//A browser, you say?

/* "Provide an option in the High Score display to reset the scores; this is for grading purposes." */

namespace Breakout.Game_states
{
    public class HighScoresView : GameStateView
    {
        HighScoresRegion highScoresRegion;
        HighScores highScores;

        private Texture2D white1x1;
        private SpriteFont highScoresFont;
        private SpriteFont highScoresHeaderFont;
        private const string MESSAGE = "High scores";
        private const string resetHighScoresMsg = "Press 'r' to reset the high scores";

        bool areHighScoresSetUp = false;

        //IN PROGRESS: Implement high scores (class HighScoresView)

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            base.initialize(graphicsDevice, graphics, subsystems);

            //Then create the highScoresRegion
            int w, h;  //TODO: fill in w,h (of HighScoresView.initialize()) with non-literals
            w = 1000; h = 700;  //TMP!
            highScoresRegion = new(new Rectangle(spacing.highScoresRegionExternalTopSpacing, spacing.highScoresRegionExternalSideSpacing, w, h));

            highScores = new();

            //Let's see if we can put this here:
            //highScores.SetupHighScores();  //nm!  we put it in update()
        }

        public override void loadContent(ContentManager contentManager)
        {
            white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
            highScoresFont = contentManager.Load<SpriteFont>("Fonts/high-scores");
            highScoresHeaderFont = contentManager.Load<SpriteFont>("Fonts/highScoresHeader");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (keyboard.IsKeyPressed(Keys.R))
            {
                resetHighScores();
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
        }

        //IN PROGRESS: HighScoresView.update()
        //Print a message like so:
        //  High Scores
        //  10000
        //  9000
        //  (etc.)
        //
        //  Press 'r' to reset the high scores
        //  Press Escape to return to the main menu
        
        public override void update(GameTime gameTime)
        {
            if (!areHighScoresSetUp)
            {
                highScores.SetupHighScores(renderer, highScoresHeaderFont, highScoresFont); //One-time call? TBD!
                areHighScoresSetUp = true;
            }

            GameElement el;
            el = new(RenderType.UI, CallType.Rectangle, white1x1, highScoresRegion.position, Color.White);
            renderer.AddToRenderList(el);

            //Rectangle r = new(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            (_, Vector2 vec) = stringRenderer.RenderStringHVCentered(MESSAGE, highScoresHeaderFont, highScoresRegion.position);

            el = new(RenderType.Text, highScoresHeaderFont, MESSAGE, vec, Color.Black);
            renderer.AddToRenderList(el);
        }

        //IN PROGRESS: Reset the high scores (in resetHighScores())
        private void resetHighScores()
        {
            //Debug.Print("TODO: Reset the high scores");
            highScores.ReinitializeHighScores();
        }

    }//END class HighScoresView
}
