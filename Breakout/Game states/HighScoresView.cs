using Breakout.Game_elements;
using Breakout.Game_objects.Base;
using Breakout.Game_objects.non_derived;
using Breakout.Game_objects.Window_areas.HighScoreView;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Breakout.Subsystems.misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

/* "High Scores are persisted to the browser's local storage; keep and display up to the top 5 scores." */
//A browser, you say?

/* "Provide an option in the High Score display to reset the scores; this is for grading purposes." */

namespace Breakout.Game_states
{
    public class HighScoresView : GameStateView
    {
        BoxRenderer boxRenderer;

        HighScoresRegion highScoresRegion;
        HighScores highScores;

        private Texture2D white1x1;
        private SpriteFont highScoresFont;
        private SpriteFont highScoresHeaderFont;
        private const string highScoresHeaderMsg = "High scores";
        private const string highScoresResetMsg = "Press 'r' to reset the high scores";

        bool areHighScoresSetUp = false;

        //IN PROGRESS: Implement high scores (class HighScoresView)

        public HighScoresView()
        {
            //TODO: Something here? not sure atm (HighScoresView constructor)
        }

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            base.initialize(graphicsDevice, graphics, subsystems);

            //stash stuff
            boxRenderer = subsystems.boxRenderer;

            //Create highScores -- which I THINK will initialize its list (and populate it).  TBD!
            highScores = new();
            // ...and highScoresRegion, which I'm not sure if this step is necessary
            highScoresRegion = new(new Rectangle(0, 0, 0, 0));  //dare we create this here? TBD!
        }

        public override void loadContent(ContentManager contentManager)
        {
            white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
            highScoresFont = contentManager.Load<SpriteFont>("Fonts/high-scores");
            highScoresHeaderFont = contentManager.Load<SpriteFont>("Fonts/highScoresHeader");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (keyboard.IsKeyPressed(Keys.R))
            {
                resetHighScores();
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
        }

        //IN PROGRESS: HighScoresView.update()
        //Print a message like so:
        //  High Scores
        //  10000
        //  9000
        //  (etc.)
        //
        //  Press 'r' to reset the high scores
        //  Press Escape to return to the main menu
        
        public override void update(GameTime gameTime)
        {
            if (!areHighScoresSetUp)
            {
                DetermineHighScoresRegionPosition();
                highScores.SetupHighScores(renderer, highScoresHeaderFont, highScoresFont); //One-time call? TBD!
                areHighScoresSetUp = true;
            }

            GameElement el;
            el = new(RenderType.UI, CallType.Rectangle, white1x1, highScoresRegion.position, Color.White);
            renderer.AddToRenderList(el);

            (_, Vector2 vec) = stringRenderer.RenderStringHVCentered(highScoresHeaderMsg, highScoresHeaderFont, highScoresRegion.position);
            el = new(RenderType.Text, highScoresHeaderFont, highScoresHeaderMsg, vec, Color.Black);
            renderer.AddToRenderList(el);
        }

        //TODO: Reset the high scores (in resetHighScores())
        private void resetHighScores()
        {
            //Debug.Print("TODO: Reset the high scores");
            highScores.ReinitializeHighScores();
        }

        //IN PROGRESS! (HighScoresView.DetermineHighScoresRegionPosition())
        private void DetermineHighScoresRegionPosition()
        {
            float x, y, w, h;  //TODO: fill in w,h (of HighScoresView.initialize()) with non-literals
  
            //determine the region's width & height
            Vector2 vec = highScoresHeaderFont.MeasureString(highScoresHeaderMsg);
            float possibleMaxX = vec.X;
            List<string> list = highScores.GetHighScoresListOfStrings();
            list.Add(highScoresResetMsg);
            float possibleMaxX_2 = stringRenderer.GetStringSizeMaxX(highScoresFont, list);
            w = MathHelper.Max(possibleMaxX, possibleMaxX_2);
            w += 2 * spacing.highScoresRegionInternalSideSpacing;

            //...and h
            h = 500;  //TMP
            highScoresRegion.UpdatePosition(new Rectangle(0, 0, (int)w, (int)h));



            //figure out the region's (x,y)            
            x = boxRenderer.DrawRectangleHCentered(highScoresRegion.position, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));            
            y = boxRenderer.DrawRectangleVCentered(highScoresRegion.position, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            y -= spacing.highScoresRegionExternalBottomSpacing;

            highScoresRegion.UpdatePosition(new Rectangle((int) x, (int) y, highScoresRegion.position.Width, highScoresRegion.position.Height));
        }

        private void ResizeHighScoresRegion(Rectangle pos)
        {
            //TODO! (HighScoresView.ResizeHighScoresRegion())
            throw new System.Exception("What this HighScoresView.ResizeHighScoresRegion() function all about yo");
        }

    }//END class HighScoresView
}
