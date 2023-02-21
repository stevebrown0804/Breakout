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

    //IN PROGRESS: Get the pencil & paper out and diagram the screen, figuring out where stuff will go

    internal class BrickGrid : GameObject
    {
        //Reminder:
        /*internal Rectangle position;
        internal Rectangle boundingBox;*/
                
        internal Dictionary<int, List<Brick>> brickGrid;

        internal BrickGrid(Rectangle position) : base(position)
        {
            //Initalize the dictionary
            brickGrid = new();
            
            //Initialize the 8 lists of bricks within the dictionary
            for(int i = 0; i < 8; i++)
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
