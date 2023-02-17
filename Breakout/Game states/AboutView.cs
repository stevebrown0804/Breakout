using Breakout.Subsystems;
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
        private const string MESSAGE = "Game by Steve Brown\n\nPress ESC to return to menu";

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/about-screen");
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)
        {
            //If the user presses Escape, exit to main menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) //TODO: Replace this with OnKeyPress or w/e we call it
            {
                return GameStateEnum.MainMenu;
            }
            //otherwise, keeps showing the About screen
            return GameStateEnum.About;
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            spriteBatch.DrawString(m_font, MESSAGE,
                                   new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                             graphics.PreferredBackBufferHeight / 2 - stringSize.Y),                   Color.White);

            spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
