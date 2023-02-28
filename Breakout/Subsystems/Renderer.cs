using Breakout.Game_elements;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout.@Subsystems
{
    public class Renderer
    {
        readonly List<GameElement> renderList;

        internal Renderer()
        {
            renderList = new();            
        }

        public List<GameElement> GetRenderList()
        {
            return renderList;
        }

        public List<GameElement> AddToRenderList(GameElement element)
        {
            renderList.Add(element);
            return renderList;
        }

        public void ClearRenderList()
        {
            renderList.Clear();
        }

    }//END class Renderer
}
