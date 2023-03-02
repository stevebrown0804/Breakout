using Breakout.Game_elements;
using Breakout.Subsystems.misc;
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

        //can this go in here?  TBD!
        public Spacing spacing;

        public SubsystemsHolder(GraphicsDeviceManager graphics) 
        {
            keyboard = new();
            renderer = new();
            stringRenderer = new();
            spacing = new(graphics);
        }

        /*public void InitializeSpacing(GraphicsDeviceManager graphics)
        {
            spacing = new(graphics);
        }*/

    }
}
