using Breakout.Game_elements;
using System;
using System.Collections.Generic;

namespace Breakout.Subsystems
{
    internal class Renderer
    {
        List<GameElement> renderList;

        internal Renderer()
        {
            Console.WriteLine("Renderer subsystem in progress");

            renderList = new();
            
            //IN PROGRESS: Renderer (constructor)
        }

        internal List<GameElement> GetRenderList()
        {
            return renderList;
        }

        internal List<GameElement> AddToRenderList(GameElement element)
        {
            renderList.Add(element);
            return renderList;
        }

        internal void ClearRenderList()
        {
            renderList.Clear();
        }

        internal void DoRendering()
        {
            Console.WriteLine("Rendering in progress");

            //TODO: DoRendering()
            // Translate a list of GameElements into Draw() or DrawString() calls
        }
    }
}
