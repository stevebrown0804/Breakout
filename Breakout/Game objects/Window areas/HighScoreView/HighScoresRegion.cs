using Breakout.Game_objects.Base;
//using Breakout.Game_objects.non_derived;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects.Window_areas.HighScoreView
{
    internal class HighScoresRegion : GameObject
    {

        internal HighScoresRegion(Rectangle position) : base(position) { }

        internal void UpdatePosition(Rectangle position)
        {
            this.position = position;
        }
    }
}
