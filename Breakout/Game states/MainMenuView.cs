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
    public class MainMenuView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private enum MenuState
        {
            NewGame,
            HighScores,
            About,
            Quit
        }

        private MenuState m_currentSelection = MenuState.NewGame;
        private bool m_waitForKeyRelease = false;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);

            //TODO: Was I going to do something else in MainMenuView.initialize()?  TBD
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)
        {
            // This is the technique I'm using to ensure one keypress makes one menu navigation move - the prof

            if (!m_waitForKeyRelease)
            {
                //TODO: Add logic to not scroll up past New Game or scroll down past Quit

                // Arrow keys to navigate the menu
                if (keyboard.IsKeyHeld(Keys.Down))  //Any reason we can't do IsKeyPRessed?  TBD
                {
                    m_currentSelection = m_currentSelection + 1;
                    m_waitForKeyRelease = true;
                }
                  if (keyboard.IsKeyHeld(Keys.Up))
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

        public override void render(GameTime gameTime, Renderer renderer)
        {
            /*spriteBatch.Begin();
            spriteBatch.End();*/

            base.render(gameTime, renderer);
        }

        private (float, GameElement) drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);

            GameElement el = new(RenderType.Text, font, text,
                                 new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                                 color);

            return (y + stringSize.Y, el);
        }

        public override void update(GameTime gameTime, Renderer renderer)
        {
            (float bottom, GameElement el) = drawMenuItem(
                m_currentSelection == MenuState.NewGame ? m_fontMenuSelect : m_fontMenu,
                "New Game",
                200,
                m_currentSelection == MenuState.NewGame ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);

            (bottom, el) = drawMenuItem(m_currentSelection == MenuState.HighScores ? m_fontMenuSelect : m_fontMenu, "High Scores", bottom, m_currentSelection == MenuState.HighScores ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);

            (bottom, el) = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);

            (bottom, el) = drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);
        }
    }
}