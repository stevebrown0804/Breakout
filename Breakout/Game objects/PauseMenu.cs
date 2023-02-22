using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class PauseMenu : GameObject
    {
        //TODO: PauseMenu class
        //ALSO TODO: Think of any other classes we may need, in-game

        public bool showPauseMenu = false;

        internal PauseMenu(Rectangle position) : base(position)
        {
        }

        //the pause menu probably doesn't need a bounding box
        /*internal PauseMenu(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal PauseMenu() : base()
        {
        }*/
    }
}
