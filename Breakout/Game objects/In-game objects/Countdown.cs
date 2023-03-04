using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects
{
    internal class Countdown : GameObject
    {
        //stuff to stash
        Renderer renderer;
        StringRenderer stringRenderer;

        private bool areInitialTimersSet = false;
        private bool doneWithCountdown = false;

        private TimeSpan startTimer;
        private TimeSpan numberEndsAt;
        private int numbersRemaining = 3;  //as in "3..2...1...Go!"
        private TimeSpan elapsedTime = TimeSpan.Zero;

        internal Countdown(Rectangle position, SubsystemsHolder subsystems) : base(position)
        {
            renderer = subsystems.renderer;
            stringRenderer = subsystems.stringRenderer;
        }

        //IN PROGRESS: Countdown.DoCountdown()
        internal void DoCountdown(int durationInMS, GameTime gameTime, GamePlayView gpv)
        {
            int numberDurationInMS = durationInMS; //500;  //0.5 per number?  TBD  //<--also, superfluous

            if (!doneWithCountdown)
            {
                if (!areInitialTimersSet)
                {
                    startTimer = gameTime.TotalGameTime;
                    numberEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 0, 0, numberDurationInMS);  
                    areInitialTimersSet = true;
                }

                if (numbersRemaining >= 0)
                {
                    if (startTimer + elapsedTime < numberEndsAt)
                    {
                        int x = (int) stringRenderer.RenderStringHCentered($"{numbersRemaining}", gpv.countdownFont, position);
                        int y = position.Y; 
                        renderer.AddToRenderList(new(RenderType.Text, gpv.countdownFont, $"{numbersRemaining}", new(x, y), Color.Red));
                    }
                    else
                    {
                        elapsedTime = TimeSpan.Zero;
                        startTimer = gameTime.TotalGameTime;
                        numberEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 0, 0, numberDurationInMS);
                        numbersRemaining--;
                    }
                }
                else
                {
                    doneWithCountdown = true;
                }
                elapsedTime += gameTime.ElapsedGameTime;
            }
            else
            {
                //Debug.Print($"Setting gamePlayState to: InGame; current value is: {gamePlayState}");
                gpv.gamePlayState = GamePlayState.InGame;
            }
        }

        //IN PROGRESS: Countdown.ResetCountdown()
        internal void ResetCountdown()
        {
            areInitialTimersSet = false;
            doneWithCountdown = false;
            numbersRemaining = 3;
            elapsedTime = TimeSpan.Zero;
            startTimer = TimeSpan.Zero;
            numberEndsAt = TimeSpan.Zero;
        }

        //TODO (POSSIBLY; TBD): Countdown.ExtendTimers()
        internal void ExtendTimers(TimeSpan timeSpan)
        {
            //Hey, couldn't we just NOT add to the elapsed time while paused?  TBD
        }
    }
}
