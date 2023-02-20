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

    internal class PlayingField : GameObject
    {
        //Reminder:
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
