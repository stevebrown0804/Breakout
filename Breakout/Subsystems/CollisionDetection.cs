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
        public static bool DoTheyIntersect(Rectangle r1, Rectangle r2)
        {
            bool theyDo = !(r2.X > r1.X + r1.Width || 
                            r2.X + r2.Width < r1.X || 
                            r2.Y > r1.Y + r1.Height || 
                            r2.Y + r2.Height < r1.Y);
            return theyDo;
        }

        public static bool FromTheRight(Rectangle spriteRect, float deltaX, Rectangle objRect)
        {
            if (spriteRect.X >= objRect.X + objRect.Width && spriteRect.X + deltaX < objRect.X + objRect.Width)
            {
                return true;
            }
            return false;
        }

        public static bool FromTheLeft(Rectangle spriteRect, float deltaX, Rectangle objRect)
        {
            if(spriteRect.X + spriteRect.Width < objRect.X && spriteRect.X + deltaX + spriteRect.Height >= objRect.X)
            {
                return true;
            }
            return false;
        }

        public static bool FromTheBottom(Rectangle spriteRect, float deltaY, Rectangle objRect)
        {
            //if (spriteRect.X >= objRect.X + objRect.Width && spriteRect.X + deltaX < objRect.X + objRect.Width)
            if(spriteRect.Y >= objRect.Y + objRect.Height && spriteRect.Y + deltaY < objRect.Y + objRect.Height)
            {
                return true;
            }
            return false;
        }


        //TODO: Test this
        public static CollisionType GetIntersectType(Rectangle r1, Rectangle r2)
        {
            if (!(r2.X > r1.X + r2.Width || r2.X + r2.Width < r1.X))
                return CollisionType.horizontal;

            if (!(r2.Y > r1.Y + r1.Height || r2.Y + r2.Height < r1.Y))
                return CollisionType.vertical;

            return CollisionType.none;
        }
    }
}
