using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects.Base
{
    internal abstract class GameObject
    {
        internal Rectangle position;
        internal Rectangle boundingBox;

        internal GameObject()
        {
            position = new();
            boundingBox = new();
        }
    }
}
