using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Breakout.Subsystems;

namespace Breakout.Game_states
{
    //code stolen from prof. mathias!
    //then TOTALLY adapted by me! \(^ ^ )/
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        public abstract void loadContent(ContentManager contentManager);
        public abstract GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard);
        public abstract void render(GameTime gameTime);
        public abstract void update(GameTime gameTime);
    }
}
