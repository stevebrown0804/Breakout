using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_states
{
    public interface IGameState
    {
        void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Dictionary<string, ISubsystem> subsystems);

        void loadContent(ContentManager contentManager);
        
        GameStateEnum processInput(GameTime gameTime);
        
        void update(GameTime gameTime);
        
        void render(GameTime gameTime);
    }
}
