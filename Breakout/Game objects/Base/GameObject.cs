using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects.Base
{
    internal abstract class GameObject
    {
        internal Rectangle position;
        internal Rectangle boundingBox;

        internal GameObject(Rectangle position)
        {
            this.position = position;
            boundingBox = new();
         }

        internal GameObject(Rectangle position, Rectangle boundingBox)
        {
            this.position = position;
            this.boundingBox = boundingBox;
        }

        internal GameObject()
        {
            position = new();
            boundingBox = new();
        }
    }
}
