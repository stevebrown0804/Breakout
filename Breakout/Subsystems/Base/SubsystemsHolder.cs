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

    }
}
