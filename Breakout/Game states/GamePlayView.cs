using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_states
{
    //again, this code was stolen.  stolen, I say!
    public class GamePlayView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "Isn't this game fun!";  //TODO: Comment this out...eventually

        public override void loadContent(ContentManager contentManager)  //TODO: Implement GamePlayView.loadContent()
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu"); //TODO: Make other fonts for this view
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)   //TODO: Implement GamePlayView.processInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime)  //TODO: Add 'renderer' object to parameters
        {                                               //TODO: Also, implement GamePlayView.render()
            spriteBatch.Begin();

            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            spriteBatch.DrawString(m_font, MESSAGE,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);

            spriteBatch.End();
        }

        public override void update(GameTime gameTime) //MAYBE: Use this?  I'm not sure we need to, atm
        {
        }
    }
}
