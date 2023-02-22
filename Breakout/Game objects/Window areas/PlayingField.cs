using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    // "Some kind of nice background image for the gameplay area."

    //TODO: Render the galaxy sprite (from project 2) to the playingfield (I think)

    internal class PlayingField : GameObject
    {
        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        internal PlayingField(Rectangle position) : base(position)
        {
        }

        /*internal PlayingField(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal PlayingField() : base()
        {
        }*/
    }
}
