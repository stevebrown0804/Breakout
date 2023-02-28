using Breakout.Game_elements;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

/* "High Scores are persisted to the browser's local storage; keep and display up to the top 5 scores." */
//A browser, you say?

/* "Provide an option in the High Score display to reset the scores; this is for grading purposes." */

namespace Breakout.Game_states
{
    public class HighScoresView : GameStateView
    {
        //Dictionary<string, Subsystems> subsystems;
        /*Subsystems.Base.Subsystems renderer;
        Subsystems.Base.Subsystems keyboard;*/

        private SpriteFont highScoresFont;
        private SpriteFont highScoresHeaderFont;
        private const string MESSAGE = "TODO: High scores";
        private const string resetHighScoresMsg = "Press 'r' to reset the high scores";

        //TODO: Implement high scores

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            base.initialize(graphicsDevice, graphics, subsystems);

            /*renderer = subsystems["renderer"];
            keyboard = subsystems["keyboard"];*/
        }

        public override void loadContent(ContentManager contentManager)
        {
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

        //TODO: HighScoresView.update()
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
            //TODO: Change this to use StringRenderer
            Vector2 stringSize = highScoresFont.MeasureString(MESSAGE);
            GameElement el = new(RenderType.Text, highScoresFont, MESSAGE,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 2 - stringSize.Y),
                Color.White);
            renderer.AddToRenderList(el);
        }

        //TODO: Reset the high scores (in resetHighScores())
        private void resetHighScores()
        {
            
        }

    }//END class HighScoresView
}
