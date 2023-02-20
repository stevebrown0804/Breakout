using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class RemainingLivesIcons : GameObject
    {
        //TODO: RemainingLivesObject class
        //This will be the area that the paddle icons are rendered into

        internal RemainingLivesIcons(Rectangle position) : base(position)
        {
        }

        //the pause menu probably doesn't need a bounding box
        internal RemainingLivesIcons(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal RemainingLivesIcons() : base()
        {
        }
    }
}
