using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Breakout;
using Breakout.Subsystems;
using System.Diagnostics;
using Breakout.Game_elements;
using System.ComponentModel;

namespace Breakout.Game_states
{
    //and, again, stolen.
    public class MainMenuView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private enum MenuState
        {
            NewGame,
            HighScores,
            //Help,
            About,
            Quit
        }

        private MenuState m_currentSelection = MenuState.NewGame;
        private bool m_waitForKeyRelease = false;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);

            //TODO: Was I going to do something else in MainMenuView.initialize()?  TBD

            Brick brick2 = new(new Rectangle(0, 0, 100, 100), new Rectangle(200, 200, 150, 150));
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)
        {
            // This is the technique I'm using to ensure one keypress makes one menu navigation move - the prof

            //TODO: Write the BO_Keyboard class then replace these calls with IsKeyPress() or w/e we call it

            if (!m_waitForKeyRelease)
            {
                //TODO: Add logic to not scroll up past New Game or scroll down past Quit

                // Arrow keys to navigate the menu
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    m_currentSelection = m_currentSelection + 1;
                    m_waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    m_currentSelection = m_currentSelection - 1;
                    m_waitForKeyRelease = true;
                }
                
                // If enter is pressed, return the appropriate new state
                if (keyboard.IsKeyPressed(Keys.Enter))
                {
                    switch (m_currentSelection)
                    {
                        case (MenuState.NewGame): return GameStateEnum.GamePlay;
                        case (MenuState.HighScores): return GameStateEnum.HighScores;
                        case (MenuState.About): return GameStateEnum.About;
                        case (MenuState.Quit): return GameStateEnum.Exit;
                        default:
                            throw new System.Exception("MainMenuview.processInput says: Unrecognized MenuState");
                    }
                }

                //And if ESC is pressed, exit
                if (keyboard.IsKeyPressed(Keys.Escape))
                    return GameStateEnum.Exit;

            }
            //else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
            else if (keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Up))
            {
                m_waitForKeyRelease = false;
            }

            //Otherwise, continue doing the main menu
            return GameStateEnum.MainMenu;
        }

        public override void update(GameTime gameTime, Renderer renderer)
        {
        }

        public override void render(GameTime gameTime, Renderer renderer)
        {
            spriteBatch.Begin();

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = drawMenuItem(
                m_currentSelection == MenuState.NewGame ? m_fontMenuSelect : m_fontMenu, 
                "New Game",
                200, 
                m_currentSelection == MenuState.NewGame ? Color.Yellow : Color.White);
            bottom = drawMenuItem(m_currentSelection == MenuState.HighScores ? m_fontMenuSelect : m_fontMenu, "High Scores", bottom, m_currentSelection == MenuState.HighScores ? Color.Yellow : Color.White);
            //bottom = drawMenuItem(m_currentSelection == MenuState.Help ? m_fontMenuSelect : m_fontMenu, "Help", bottom, m_currentSelection == MenuState.Help ? Color.Yellow : Color.White);
            bottom = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.Yellow : Color.White);
            drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.White);

            spriteBatch.End();
        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                font,
                text,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }
    }
}