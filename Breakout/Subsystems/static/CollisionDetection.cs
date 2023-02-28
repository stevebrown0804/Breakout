using Microsoft.Xna.Framework;


namespace Breakout.@Subsystems.@static
{
    //DONE, I THINK: CollisionDetection class

    //The "FromThe..." algorithms came from here (after I'd tried unsuccessfully to implement them myself):
    //https://gamedev.stackexchange.com/questions/13774/how-do-i-detect-the-direction-of-2d-rectangular-object-collisions?noredirect=1&lq=1

    internal static class CollisionDetection
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
                return true;

            return false;
        }

        public static bool FromTheLeft(Rectangle spriteRect, float deltaX, Rectangle objRect)
        {
            if (spriteRect.X + spriteRect.Width < objRect.X && spriteRect.X + deltaX + spriteRect.Height >= objRect.X)
                return true;

            return false;
        }

        public static bool FromTheBottom(Rectangle spriteRect, float deltaY, Rectangle objRect)
        {
            if (spriteRect.Y >= objRect.Y + objRect.Height && spriteRect.Y + deltaY < objRect.Y + objRect.Height)
                return true;

            return false;
        }

        public static bool FromTheTop(Rectangle spriteRect, float deltaY, Rectangle objRect)
        {
            //oldBoxBottom < otherObj.Top && boxBottom >= otherObj.Top;
            if (spriteRect.Y + spriteRect.Height < objRect.Y && spriteRect.Y + deltaY + spriteRect.Height >= objRect.Y)
                return true;

            return false;
        }

    }//END static class CollisionDetection
}
