using Breakout.Game_elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.@Subsystems.Base
{
    public class SubsystemsHolder
    {
        public BO_Keyboard keyboard;
        public Renderer renderer;
        public StringRenderer stringRenderer;

        public SubsystemsHolder() 
        {
            keyboard = new();
            renderer = new();
            stringRenderer = new();
        }

        /*//Keyboard
        void InitializePreviousState();

        void UpdateCurrentState();

        void SetPreviousStateToCurrentState();

        bool IsKeyPressed(Keys key);

        bool IsKeyHeld(Keys key);

        //Renderer
        List<GameElement> GetRenderList();

        List<GameElement> AddToRenderList(GameElement element);

        void ClearRenderList();

        //StringRenderer
        (float, Vector2) RenderStringHVCentered(string str, SpriteFont font, Rectangle renderSurface);

        float RenderStringHCentered(string str, SpriteFont font, Rectangle renderSurface);*/
    }
}
