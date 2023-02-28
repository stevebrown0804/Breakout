using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal class Wall : GameObject
    {
        internal Wall(Rectangle position) : base(position) { }
    }
}
