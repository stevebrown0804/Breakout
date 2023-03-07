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
            boundingBox = this.position;        //if bounding box isn't specified, have it copy position
        }

        //LATER: Delete this (GameObject.GameObject(Rect, Rect)), if it gets no references
        /*internal GameObject(Rectangle position, Rectangle boundingBox)  //for when boundingBox is different from position
        {
            this.position = position;
            this.boundingBox = boundingBox;
        }*/

        //LATER: Delete this (GameObject.GameObject()), if it gets no references
        /*internal GameObject()   //pretty sure this serves no real purpose; 
        {
            position = new();
            boundingBox = new();
        }*/
    }
}
