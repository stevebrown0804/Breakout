using Breakout.Game_elements;
using System;
using System.Collections.Generic;

namespace Breakout.Subsystems
{
    public class Renderer
    {
        readonly List<GameElement> renderList;

        internal Renderer()
        {
            renderList = new();            
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

     }//END class Renderer
}
