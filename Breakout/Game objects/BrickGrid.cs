using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Breakout.Game_objects.Base;

namespace Breakout.Game_elements
{

    /* "Game starts with 8 rows of bricks.
        Each row is made up of at least 14 bricks.
        Have a small space between each of the rows and bricks." */

    //TODO: Get the pencil & paper out and diagram the screen, figuring out where stuff will go

    internal class BrickGrid : GameObject
    {
        //Reminder:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/

        //TODO: Pick a data structure to represent the BrickGrid.  Dictionary<int(=row number, starting at the (top/bottom)), List<Brick>>, perhaps?

        internal BrickGrid(Rectangle position) : base(position)
        {
        }

        internal BrickGrid(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }

        internal BrickGrid() : base()
        {
        }
    }
}
