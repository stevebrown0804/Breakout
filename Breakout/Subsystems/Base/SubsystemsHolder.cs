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
        public BoxRenderer boxRenderer;

        //is this a subsystem?  *shrug* I suppose if we say that it is, then it is  *thumbs up*
        public HighScores highScores;

        //can this go in here?  TBD!  //Follow-up: yep!
        public Spacing spacing;

        public SubsystemsHolder(GraphicsDeviceManager graphics) 
        {
            keyboard = new();
            renderer = new();
            stringRenderer = new();
            boxRenderer = new();
            highScores = new();  //TODO: Load highscores from file (if it exists)  or new()
            spacing = new(graphics);
        }

    }//END class SubsystemsHolder
}
