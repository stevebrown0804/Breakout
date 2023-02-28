using Breakout.Game_objects.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* "Scoring
    1 point for each yellow brick
    2 points for each orange brick
    3 points for each blue brick
    5 points for each green brick
    25 points when a line is cleared
    Every 100 points the player earns a second ball that automatically starts from the middle of the paddle (no space bar to release it).  This new ball starts at the initial slow speed and increases in speed according to the above pattern.  In other words, each ball has its own speed and own state for speed increases." */

namespace Breakout.Game_objects
{
    //TODO: Implement class Score
    internal class Score : GameObject
    {
        internal Score(Rectangle position) : base(position)
        {
        }
    }
}
