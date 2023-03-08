using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

/* "Every time a brick is hit, a tasteful explosion of particles occurs.  The explosion happens by having particles take up the same space (surface area) as the brick and then explode out based upon their position relative to the center.  Alternatively, the explosion could be the particles falling down, with a stickiness based upon their relative position on the brick (higher in the brick, the stickier).
"*/

namespace Breakout.Game_elements
{
    //IN PROGRESS: Brick class (Remaining: exploding bricks, ..?)
    internal class Brick : GameObject
    {
        bool areSubsystemsStashed = false;
        bool isParticleEmitterCreated = false;
        //bool hasSpriteBatchBeenCreated = false;
        bool hasSpriteBatchBeenStashed = false;

        Renderer renderer;
        ProfsRandom profsRandom;
        ParticleEmitter particleEmitter1;
        ParticleEmitter particleEmitter2;
        ParticleEmitter particleEmitter3;

        //GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        public bool hasBeenHit = false;
        public bool isExploding = false;
        private bool timersSet = false;
        private TimeSpan explosionEndsAt;
        private TimeSpan startTimer;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        internal Brick(Rectangle position) : base(position) { }

        public void Explode(GameTime gameTime, GamePlayView gpv, SubsystemsHolder subsystems)
        {
            if (!areSubsystemsStashed)
            {
                renderer = subsystems.renderer;
                profsRandom = subsystems.profsRandom;
                areSubsystemsStashed = true;
            }
            if (!hasSpriteBatchBeenStashed)
            {
                spriteBatch = gpv.spriteBatch;
                hasSpriteBatchBeenStashed = true;
            }

            if (!isParticleEmitterCreated)
            {
                int middleX = position.X + position.Width / 2;
                int middleY = position.Y + position.Height / 2;
                particleEmitter3 = new ParticleEmitter(
                    gpv.contentManager,
                    new TimeSpan(0, 0, 0, 0, 35), //new TimeSpan(0, 0, 0, 0, 5),                //rate
                    middleX, middleY,
                    2 * position.Height, //10, //position.Width / 8, //20,                      //size
                    1, //2,                                                                     //speed
                    new TimeSpan(0, 0, 0, 0, 450), //new TimeSpan(0, 0, 4),                     //lifetime
                    new TimeSpan(0, 0, 0, 0, 425)); //new TimeSpan(0, 0, 0, 0, 3000));          //switchover
                particleEmitter2 = new ParticleEmitter(
                    gpv.contentManager,
                    new TimeSpan(0, 0, 0, 0, 10), // new TimeSpan(0, 0, 0, 0, 25),
                    middleX, middleY,
                    100,
                    3,
                    new TimeSpan(0, 0, 0, 0, 100),
                    new TimeSpan(0, 0, 0, 0, 50)); //new TimeSpan(0, 0, 0, 0, 5000));
                particleEmitter1 = new ParticleEmitter(
                    gpv.contentManager,
                    new TimeSpan(0, 0, 0, 0, 1),
                    middleX, middleY,
                    8,
                    10,
                    new TimeSpan(0, 0, 10),
                    new TimeSpan(0, 0, 0));
                isParticleEmitterCreated = true;
            }

            if (isExploding)
            {
                if(!timersSet)
                {
                    startTimer = gameTime.TotalGameTime;
                    explosionEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 0, 0, 750);  //explosion duration
                    timersSet = true;
                }

                if(startTimer + elapsedTime < explosionEndsAt)
                {
                    particleEmitter1.update(gameTime);
                    particleEmitter2.update(gameTime);
                    particleEmitter3.update(gameTime);
                }
                else
                {
                    isExploding = false;
                    //Debug.Print("Brick.Explode says: isExploding has been set to false (after 3sec elapsed)");
                }
                elapsedTime += gameTime.ElapsedGameTime;

            }//END if (isExploding)

        }//END Explode()

        public void DrawExplosion(/*SpriteBatch spriteBatch*/)
        {            
            particleEmitter1.draw(spriteBatch, renderer);
            particleEmitter2.draw(spriteBatch, renderer);
            particleEmitter3.draw(spriteBatch, renderer);
        }

    }//END class Brick
}
