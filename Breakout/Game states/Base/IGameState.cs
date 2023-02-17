using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_states
{
    public interface IGameState
    {
        //TODO: look for the prof's presentation and implement this accordingly

        //note: code stolen from prof. mathias! \(^^ )/
        void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics);
        void loadContent(ContentManager contentManager);
        GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard);
        void update(GameTime gameTime);
        void render(GameTime gameTime);
    }
}
