using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_states
{
    //also stolen  -- except everything's commented out!  bwahahaha we'll delete this, eventually (I think)

   /* public class HelpView : GameStateView
    {
        private SpriteFont inGameMenuFont;
        private const string MESSAGE = "This is how to play the game\nAnd this is line 2!";

        public override void loadContent(ContentManager contentManager)
        {
            inGameMenuFont = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.Help;
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 stringSize = inGameMenuFont.MeasureString(MESSAGE);
            spriteBatch.DrawString(inGameMenuFont, MESSAGE,
                                   new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, 
                                               graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);

            spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
        }
    }*/
}
