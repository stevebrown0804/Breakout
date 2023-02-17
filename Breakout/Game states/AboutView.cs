using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_states
{
    //this code was also stolen!
    public class AboutView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "Game by: Steve Brown";

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.About;
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            spriteBatch.DrawString(m_font, MESSAGE,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);

            spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
