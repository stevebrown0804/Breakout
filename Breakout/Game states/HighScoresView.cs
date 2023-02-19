using Breakout.Game_elements;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* "High Scores are persisted to the browser's local storage; keep and display up to the top 5 scores." */
//A browser, you say?

/* "Provide an option in the High Score display to reset the scores; this is for grading purposes." */

namespace Breakout.Game_states
{
    public class HighScoresView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "TODO: High scores";

        //TODO: Implement high scores (once the prof. has lectured on local storage, I suppose)

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/high-scores");  //MAYBE: More HighScores fonts?
                                                                            // Or just the one?  TBD
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)
        {

            //TODO: HighScoresView.processInput()

            if (keyboard.IsKeyPressed(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime, Renderer renderer)
        {
            base.render(gameTime, renderer);
        }

        public override void update(GameTime gameTime, Renderer renderer)
        {
            //IN PROGRESS-ish: HighScoresView.update()

            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            GameElement el = new(RenderType.Text, m_font, MESSAGE,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 2 - stringSize.Y),
                Color.White);
            renderer.AddToRenderList(el);
        }
    }
}
