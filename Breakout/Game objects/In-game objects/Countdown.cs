using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using System;


namespace Breakout.Game_objects
{
    internal class Countdown : GameObject
    {
        //stuff to stash
        Renderer renderer;
        StringRenderer stringRenderer;

        //flags
        private bool areInitialTimersSet = false;
        private bool doneWithCountdown = false;

        //timer stuff
        private TimeSpan startTimer;
        private TimeSpan numberEndsAt;
        private int numbersRemaining = 3;  //as in "3..2...1...Go!"
        private TimeSpan elapsedTime = TimeSpan.Zero;

        internal Countdown(Rectangle position, SubsystemsHolder subsystems) : base(position)
        {
            renderer = subsystems.renderer;
            stringRenderer = subsystems.stringRenderer;
        }

        internal void DoCountdown(int durationInMS, GameTime gameTime, GamePlayView gpv)
        {
            if (!doneWithCountdown)
            {
                if (!areInitialTimersSet)
                {
                    startTimer = gameTime.TotalGameTime;
                    numberEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 0, 0, durationInMS);  
                    areInitialTimersSet = true;
                }

                string str = numbersRemaining > 0 ? $"{numbersRemaining}" : "Go!";
                if (numbersRemaining >= 0)
                {
                    if (startTimer + elapsedTime < numberEndsAt)
                    {
                        int x = (int) stringRenderer.RenderStringHCentered(str, gpv.countdownFont, position);
                        int y = position.Y; 
                        renderer.AddToRenderList(new(RenderType.Text, gpv.countdownFont, str, new(x, y), Color.Red));
                    }
                    else
                    {
                        elapsedTime = TimeSpan.Zero;
                        startTimer = gameTime.TotalGameTime;
                        numberEndsAt = gameTime.TotalGameTime + new System.TimeSpan(0, 0, 0, 0, durationInMS);
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

        internal void ResetCountdown()
        {
            areInitialTimersSet = false;
            doneWithCountdown = false;
            numbersRemaining = 3;
            elapsedTime = TimeSpan.Zero;
            startTimer = TimeSpan.Zero;
            numberEndsAt = TimeSpan.Zero;
        }

    }//END class Countdown
}
