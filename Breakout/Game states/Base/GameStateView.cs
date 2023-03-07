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
using Breakout.Subsystems.misc;
using System.Drawing;

namespace Breakout.Game_states
{
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;

        internal SubsystemsHolder subsystems;
        internal BO_Keyboard keyboard;
        internal Renderer renderer;
        internal StringRenderer stringRenderer;
        internal Spacing spacing;
        internal HighScoresIOManager hsiom;
        internal HighScores highScores;
        internal AudioPlayer audioPlayer;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.subsystems = subsystems;

            renderer = subsystems.renderer;
            keyboard = subsystems.keyboard;
            stringRenderer = subsystems.stringRenderer;
            spacing = subsystems.spacing;
            hsiom = subsystems.hsiom;
            highScores = subsystems.highScores;
            audioPlayer = subsystems.audioPlayer;
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
                else if (el.renderType == RenderType.UI)
                {
                    if (el.callType == CallType.Vector2)
                        spriteBatch.Draw(el.texture, el.vec, el.color);
                    else if (el.callType == CallType.Rectangle)
                        spriteBatch.Draw(el.texture, el.rect, el.color);
                    else if (el.callType == CallType.mixed)
                        spriteBatch.Draw(el.texture, el.destRect, el.sourceRect, el.color, el.rotation, el.origin, el.effects, el.layerDepth);
                    else
                        throw new Exception($"GameStateView.render() says: Unrecognized CallType: {el.callType}");
                }
                else
                {
                    throw new Exception($"GameStateView.render() says: Unrecognized RenderType: {el.renderType}");
                }
            }

            spriteBatch.End();
        }
        
        public abstract void update(GameTime gameTime);
    }
}
