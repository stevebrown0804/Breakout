using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using System;

/* "Every time a brick is hit, a tasteful explosion of particles occurs.  The explosion happens by having particles take up the same space (surface area) as the brick and then explode out based upon their position relative to the center.  Alternatively, the explosion could be the particles falling down, with a stickiness based upon their relative position on the brick (higher in the brick, the stickier).
"*/

namespace Breakout.Game_elements
{
    //IN PROGRESS: Brick class (Remaining: exploding bricks, ..?)
    internal class Brick : GameObject
    {
        public bool hasBeenHit = false;
        public bool isExploding = false;
        private bool timersSet = false;
        private TimeSpan explosionEndsAt;
        private TimeSpan startTimer;
        private TimeSpan elapsedTime = TimeSpan.FromSeconds(0);

        internal Brick(Rectangle position) : base(position) { }

        public void Explode(GameTime gameTime, GamePlayView gpv, Renderer renderer)
        {
            if (isExploding)
            {
                if(!timersSet)
                {
                    startTimer = gameTime.TotalGameTime;
                    explosionEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 3);  //3 second explosion
                    timersSet = true;
                }

                if(startTimer + elapsedTime < explosionEndsAt)
                {
                    //TODO: do the particle animation for an exploding brick

                    //TMP (Adding a white brick to the render list for 3sec)
                    renderer.AddToRenderList(new(RenderType.UI, CallType.Rectangle, gpv.white1x1, position, Color.White));
                    //END TMP
                }
                else
                {
                    isExploding = false;
                    //Debug.Print("Brick.Explode says: isExploding has been set to false (after 3sec elapsed)");
                }
                elapsedTime += gameTime.ElapsedGameTime;

            }//END if (isExploding)

        }//END Explode()

    }//END class Brick
}
