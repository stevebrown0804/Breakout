using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Breakout.Subsystems;
using Breakout.Game_elements;
using System.Diagnostics;
using Breakout.Subsystems.Base;

namespace Breakout.Game_states
{
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        protected Dictionary<string, ISubsystem> subsystems;
        //ISubsystem keyboard;
        ISubsystem renderer;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Dictionary<string, ISubsystem> subsystems)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.subsystems = subsystems;

            //keyboard = subsystems["keyboard"];
            renderer = subsystems["renderer"];
        }

        public abstract void loadContent(ContentManager contentManager);
        
        public abstract GameStateEnum processInput(GameTime gameTime);
        
        public virtual void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            var renderList = renderer.GetRenderList();

            for (int i = 0; i < renderList.Count; i++)
            {
                GameElement el = renderList[i];
                if (el.renderType == RenderType.Text)
                    spriteBatch.DrawString(el.font, el.text, el.vec, el.color);
                else //el.renderType == UI
                {
                    if (el.callType == CallType.Vector2)
                        spriteBatch.Draw(el.texture, el.vec, el.color);
                    else //el.callType == Rectangle
                        spriteBatch.Draw(el.texture, el.rect, el.color);
                }
            }

            spriteBatch.End();
        }
        
        public abstract void update(GameTime gameTime);
    }
}
