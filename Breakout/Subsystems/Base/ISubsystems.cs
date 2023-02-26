using Breakout.Game_elements;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Subsystems.Base
{
    public interface ISubsystem
    {
        //Keyboard
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

    }
}
