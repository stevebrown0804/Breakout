﻿using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* "High Scores are persisted to the browser's local storage; keep and display up to the top 5 scores." */
//A browser, you say?

/* "Provide an option in the High Score display to reset the scores; this is for grading purposes." */

namespace Breakout.Game_states
{
    //also stolen
    public class HighScoresView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "These are the high scores";

        //TODO: Implement high scores (once the prof. has lectures on local storage, I suppose)

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/high-scores");
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)
        {
            if (/*Keyboard.GetState().IsKeyDown(Keys.Escape)*/ keyboard.IsKeyPressed(Keys.Escape)) //IN PROGRESS: Replace with OnKeyPress (or w/e we call it)
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime, Renderer renderer)
        {
            spriteBatch.Begin();

            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            spriteBatch.DrawString(m_font, MESSAGE,
                                   new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                             graphics.PreferredBackBufferHeight / 2 - stringSize.Y),                   Color.White);

            spriteBatch.End();
        }

        public override void update(GameTime gameTime, Renderer renderer)
        {
        }
    }
}
