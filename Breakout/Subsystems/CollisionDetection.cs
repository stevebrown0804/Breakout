using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Subsystems
{
    //IN PROGRESS: CollisionDetection class

    internal enum CollisionType
    {
        none,
        horizontal,
        vertical
    }

    //TODO: Test this
    internal class CollisionDetection
    {
        public bool DoTheyIntersect(Rectangle r1, Rectangle r2)
        {
            bool theyDo = !(r2.X > r1.X + r2.Width || 
                            r2.X + r2.Width < r1.X || 
                            r2.Y > r1.Y + r1.Height || 
                            r2.Y + r2.Height < r1.Y);
            return theyDo;
        }

        //TODO: Test this
        public CollisionType GetIntersectType(Rectangle r1, Rectangle r2)
        {
            if (!(r2.X > r1.X + r2.Width || r2.X + r2.Width < r1.X))
                return CollisionType.horizontal;

            if (!(r2.Y > r1.Y + r1.Height || r2.Y + r2.Height < r1.Y))
                return CollisionType.vertical;

            return CollisionType.none;
        }
    }
}
