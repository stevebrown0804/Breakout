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

        //private Texture2D white1x1;
        private SpriteFont highScoresFont;
        private SpriteFont highScoresHeaderFont;
        private const string highScoresHeaderMsg = "High scores";
        private const string highScoresResetMsg = "Press 'r' to reset the high scores";
        private const string highScoresEscapeMsg = "Press Escape to return to the main menu";

        bool areHighScoresSetUp = false;

        //IN PROGRESS: Implement high scores (class HighScoresView) (Remaining: update the list, reset the list, persist the list)

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
            //white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
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

        public override void update(GameTime gameTime)
        {
            if (!areHighScoresSetUp)
            {
                DetermineHighScoresRegionPosition();
                //highScores.SetupHighScores(renderer, highScoresHeaderFont, highScoresFont); //One-time call? TBD!
                areHighScoresSetUp = true;
            }

            float x, y;
            GameElement el;
            List<string> list;
            Vector2 vec;

            //First, render the background  //<--possibly optional; TBD
            /*el = new(RenderType.UI, CallType.Rectangle, white1x1, highScoresRegion.position, Color.White);
            renderer.AddToRenderList(el);*/

            //Find the position of the header message and queue it for rendering
            (_, vec) = stringRenderer.RenderStringHVCentered(highScoresHeaderMsg, highScoresHeaderFont, highScoresRegion.position);
            x = vec.X;
            y = highScoresRegion.position.Y + spacing.highScoresRegionInternalTopSpacing;
            el = new(RenderType.Text, highScoresHeaderFont, highScoresHeaderMsg, new Vector2(x,y), Color.White);
            renderer.AddToRenderList(el);

            //Next up, find the positions of the list of high scores and queue them for rendering
            vec = highScoresHeaderFont.MeasureString(highScoresHeaderMsg);
            y += vec.Y + spacing.highScoresRegionSubHeaderSpacing;
            list = highScores.GetHighScoresListOfStrings();
            list.Reverse();
            for(int i = 0; i < list.Count; i++)
            {
                x = stringRenderer.RenderStringHCentered(list[i], highScoresFont, highScoresRegion.position);

                y += spacing.highScoresRegionIntraLineSpacing;
                el = new(RenderType.Text, highScoresFont, list[i], new Vector2(x, y), Color.White);
                renderer.AddToRenderList(el);
                vec = highScoresFont.MeasureString(list[i]);
                y += vec.Y;
            }

            //find the positions of the reset message and queue it for rendering
            vec = highScoresFont.MeasureString(highScoresResetMsg);
            x = stringRenderer.RenderStringHCentered(highScoresResetMsg, highScoresFont, highScoresRegion.position);
            y += 2 * spacing.highScoresRegionIntraLineSpacing;
            el = new(RenderType.Text, highScoresFont, highScoresResetMsg, new Vector2(x, y), Color.White);
            renderer.AddToRenderList(el);

            //and then the escape message
            x = stringRenderer.RenderStringHCentered(highScoresEscapeMsg, highScoresFont, highScoresRegion.position);
            y += vec.Y;
            y += spacing.highScoresRegionIntraLineSpacing;
            el = new(RenderType.Text, highScoresFont, highScoresEscapeMsg, new Vector2(x, y), Color.White);
            renderer.AddToRenderList(el);
        }

        private void resetHighScores()
        {
            highScores.ReinitializeHighScores();
        }

        private void DetermineHighScoresRegionPosition()
        {
            float x, y, w, h;

            //A THOUGHT: x positions don't need to be centered within highScoresRegion; they can simply be H-centered on the screen
            Rectangle theScreen = new(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
  
            //determine the region's width & height
            //First w...
            Vector2 vec = highScoresHeaderFont.MeasureString(highScoresHeaderMsg);
            float possibleMaxX = vec.X;
            List<string> list = highScores.GetHighScoresListOfStrings();
            list.Add(highScoresResetMsg);
            float possibleMaxX_2 = stringRenderer.GetStringSizeMaxX(highScoresFont, list);
            w = MathHelper.Max(possibleMaxX, possibleMaxX_2);
            w += 2 * spacing.highScoresRegionInternalSideSpacing;

            //...and then h
            h = vec.Y;
            h += spacing.highScoresRegionIntraLineSpacing;
            h += FindTotalHeight(highScoresFont, list, spacing.highScoresRegionIntraLineSpacing);
            highScoresRegion.UpdatePosition(new Rectangle(0, 0, (int)w, (int)h));

            //figure out the region's (x,y)            
            x = boxRenderer.DrawRectangleHCentered(highScoresRegion.position, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));            
            y = boxRenderer.DrawRectangleVCentered(highScoresRegion.position, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            y -= spacing.highScoresRegionExternalBottomSpacing;

            highScoresRegion.UpdatePosition(new Rectangle((int) x, (int) y, highScoresRegion.position.Width, highScoresRegion.position.Height));
        }

        /*private void ResizeHighScoresRegion(Rectangle pos)
        {
            //TODO! (HighScoresView.ResizeHighScoresRegion())
            throw new System.Exception("What's this HighScoresView.ResizeHighScoresRegion() function all about yo");
        }*/

        //Necessary? TBD
        private float FindTotalHeight(SpriteFont font, List<string> list, int intraLineSpacing)
        {
            Vector2 vec;
            float h = 0f;
            for (int i = 0; i < list.Count; i++)
            {
                vec = font.MeasureString(list[i]);
                h += vec.Y + intraLineSpacing;
            }
            return h;
        }

    }//END class HighScoresView
}
