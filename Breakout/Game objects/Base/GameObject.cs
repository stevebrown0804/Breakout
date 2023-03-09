using Microsoft.Xna.Framework;

namespace Breakout.Game_objects.Base
{
    internal abstract class GameObject
    {
        internal Rectangle position;
        internal Rectangle boundingBox;

        internal GameObject(Rectangle position)
        {
            this.position = position;
            boundingBox = this.position;        // if bounding box isn't specified, have it copy position
        }

    }
}
