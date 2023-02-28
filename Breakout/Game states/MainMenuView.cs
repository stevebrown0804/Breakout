using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Breakout;
using Breakout.Subsystems;
using System.Diagnostics;
using Breakout.Game_elements;
using System.ComponentModel;
using Breakout.Subsystems.Base;
using System.Collections.Generic;

namespace Breakout.Game_states
{
    public class MainMenuView : GameStateView
    {
        //subsystems
        /*Subsystems.Base.Subsystems keyboard;
        Subsystems.Base.Subsystems renderer;*/

        //Fonts
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        //An enum!
        private enum MenuState
        {
            NewGame,
            HighScores,
            About,
            Quit
        }
        private MenuState m_currentSelection = MenuState.NewGame;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            //Debug.Print("Now in MainMenuView.initialize()");

            base.initialize(graphicsDevice, graphics, subsystems);

            /*keyboard = subsystems["keyboard"];
            renderer = subsystems["renderer"];*/
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            // Arrow keys to navigate the menu
            if (keyboard.IsKeyPressed(Keys.Down))
            {
                if(m_currentSelection != MenuState.Quit)
                {
                    m_currentSelection = m_currentSelection + 1;
                }
            }
            if (keyboard.IsKeyPressed(Keys.Up))
            {
                if(m_currentSelection != MenuState.NewGame)
                {
                    m_currentSelection = m_currentSelection - 1;
                }
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

            //Otherwise, continue doing the main menu
            return GameStateEnum.MainMenu;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
        }

        private (float, GameElement) drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);

            //TODO: Change this to use StringRenderer
            GameElement el = new(RenderType.Text, font, text,
                                 new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                                 color);

            return (y + stringSize.Y, el);
        }

        public override void update(GameTime gameTime)
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

            (_, el) = drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.White);
            renderer.AddToRenderList(el);
        }
    }
}