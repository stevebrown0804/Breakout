using Breakout.Game_elements;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout.Game_states
{
    public class AboutView : GameStateView
    {
        private SpriteFont m_font;
        private SpriteFont m_font_smaller;
        private const string MESSAGE = "Game by Steve Brown";
        private const string MESSAGE2 = "Press ESC to return to menu";

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            base.initialize(graphicsDevice, graphics, subsystems);
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/about-screen");
            m_font_smaller = contentManager.Load<SpriteFont>("Fonts/about-screen-smaller");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            //If the user presses Escape, exit to main menu
            if (keyboard.IsKeyPressed(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            //otherwise, keep showing the About screen
            return GameStateEnum.About;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
        }

        //MAYBE: Change the contents to use StringRenderer
        public override void update(GameTime gameTime)
        {
            GameElement el;
            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            el = new(RenderType.Text, m_font, MESSAGE,
                     new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                                     graphics.PreferredBackBufferHeight / 2 - stringSize.Y), 
                     Color.White);
            renderer.AddToRenderList(el);

            Vector2 stringSize2 = m_font_smaller.MeasureString(MESSAGE2); 
            el = new(RenderType.Text, m_font_smaller, MESSAGE2,
                     new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize2.X / 2,
                                 graphics.PreferredBackBufferHeight / 2 + stringSize2.Y),
                     Color.White);
            renderer.AddToRenderList(el);
        }
    }
}
