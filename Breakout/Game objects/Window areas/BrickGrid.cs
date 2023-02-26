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
    internal class BrickGrid : GameObject
    {
        //Base class:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/
                
        internal Dictionary<int, List<Brick>> brickGrid;

        internal int numBricksHit = 0;

        internal BrickGrid(Rectangle position, int numRowsofBricks) : base(position)
        {
            //Initalize the dictionary
            brickGrid = new();

            //Initialize the (numRowsofBricks) lists of bricks within the dictionary
            for (int i = 0; i < numRowsofBricks; i++)
            {
                brickGrid[i] = new();
            }
        }

        /*internal BrickGrid(Rectangle position, Rectangle boundingBox) : base(position, boundingBox)
        {
        }*/

        /*internal BrickGrid() : base()
        {
        }*/
    }
}
