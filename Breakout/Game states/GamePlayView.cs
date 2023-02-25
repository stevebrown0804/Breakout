using Breakout.Game_elements;
using Breakout.Game_objects;
using Breakout.Game_objects.non_derived;
using Breakout.Game_objects.Window_areas;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

/* "When starting the game, provide a 3, 2, 1 count down timer, showing the numbers 3, 2, 1 in the middle of the screen for the count down.  Following the completion of the countdown, the ball starts from the paddle in a nice direction (nice meaning not too steep of an angle and not straight up)." */

/* "Player starts with three paddles; no way to earn any more.
    When the player doesn't hit the ball, subtract a paddle from the remaining paddles (or end the game if none left) and provide a 3, 2, 1 count down timer before starting with the new paddle.  Ball starts in the same way as the start of the game." */

/* "Show current score; place at bottom of the gameplay screen.
    Graphically (not text) show number of paddles left; place in the upper right or lower left of the gameplay screen." */

/* "Background music during the gameplay." */

//TODO: Make/find a sprite for the '# paddles remaining' and render it  (Pac Man sprite? hmm...)

//TODO: Detect when the ball goes off the screen at the bottom and subtract a life (and start over or do game over)

//TODO: Find some BGM and integrate it

namespace Breakout.Game_states
{
    enum GamePlayState  //public/internal? TBD      //Also, I think this can go inside the class.  TBD
    {
        //unset = 0,
        Initializing,
        Countdown,
        InGame,
        Paused,
        GameOver,
        Cleanup //what else?  TBD
    }


    public class GamePlayView : GameStateView
    {
        //Some stuff to stash
        GraphicsDevice graphicsDevice;
        ContentManager contentManager;

        //Some constants
        const int numRowsOfBricks = 8;
        const int numBricksPerRow = 14;

        //Sprites & fonts
        internal SpriteFont pauseMenuFont;  //Fonts
        private SpriteFont inGameScoreFont;
        private SpriteFont countdownFont;
        private Texture2D blue1x1;          //Bricks
        private Texture2D limeGreen1x1;
        private Texture2D orange1x1;
        private Texture2D yellow1x1;
        private Texture2D darkgray1x1;     //Wall
        private Texture2D ball50x50;        //Ball
        private Texture2D bluegray1x1;      //Paddle
        private Texture2D purple1x1;        //misc.  //MAYBE: Remove, once we're done with it.
        internal Texture2D white1x1;
        private Texture2D black1x1;

        //Game Objects -- Everything below here (I think) is initialized in initialize()
        // Lists
        internal List<Ball> balls; // = new();
        internal List<Wall> walls; // = new();
        internal List<RowRegion> rowRegions; // = new();

        // Non-list types
        BottomAreaOfInteriorToWalls bottomAreaOfInteriorToWalls;
        //BottomAreaOfPlayingField bottomAreaOfPlayingField;        
        internal BrickGrid brickGrid;
        Countdown countdown;
        internal InteriorToWalls interiorToWalls;
        LeftHalfOfBottomArea leftHalfOfBottomArea;
        MiddleAreaOfInteriorToWalls middleAreaOfInteriorToWalls;
        //MiddleAreaOfPlayingField middleAreaOfPlayingField;
        internal Paddle paddle;
        internal PaddleArea paddleArea;
        internal PlayingField playingField;
        PauseMenu pauseMenu;
        RemainingLivesIcons remainingLivesIcons;
        RightHalfOfBottomArea rightHalfOfBottomArea;
        Score score;
        TopAreaOfInteriorToWalls topAreaOfInteriorToWalls;
        //TopAreaOfPlayingField topAreaOfPlayingField;        
        WindowInterior windowInterior;

        //the one that's not derived from GameObject
        internal Spacing spacing;

        //Variables that do NOT need to be reinitialize in Reinitalize()
        bool contentIsLoaded = false;
        GamePlayState gamePlayState;

        //Misc. variables  (REMINDER: If we add anything to this list, reinitialize it in Reinitialize()
        bool showRegions = false;
        bool showRowRegions = false;
        bool showCountdownRegion = false;
        bool showPauseMenuRegion = false;
        
        //internal bool waitingOnRender = false;

        public GamePlayView()
        {
            gamePlayState = GamePlayState.Initializing;
        }

        //DONE, I THINK - GamePlayView.initialize()
        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            Debug.Print("Now in GamePlayView.initialize");

            base.initialize(graphicsDevice, graphics);

            //stash this
            this.graphicsDevice = graphicsDevice;

            //new the lists
            balls = new();
            walls = new();
            rowRegions = new();

            //Set up the game objects
            spacing = new(graphics);
                windowInterior = new(new Rectangle(0, 0,
                                     graphics.PreferredBackBufferWidth,
                                     graphics.PreferredBackBufferHeight));

            int padding = spacing.playingFieldPaddingOnAllFourSides;
            playingField = new(new Rectangle(padding, padding, graphics.PreferredBackBufferWidth - 2 * padding, graphics.PreferredBackBufferHeight - 2 * padding));

            spacing.RecomputeValues(graphics, this);  //re-compute the values, after setting playingField

            //Do we need these?  We're re-computing them as the space interior to the walls, I think.  TBD
            /*topAreaOfPlayingField = new(new Rectangle(playingField.position.X, playingField.position.Y,
                          playingField.position.Width, spacing.topAreaHeight));
            
            middleAreaOfPlayingField = new(new Rectangle(playingField.position.X, topAreaOfPlayingField.position.Y + topAreaOfPlayingField.position.Height,
                             playingField.position.Width, spacing.middleAreaHeight));*/

            /*bottomAreaOfPlayingField = new(new Rectangle(playingField.position.X, paddleArea.position.Y + paddleArea.position.Height,
                             playingField.position.Width, spacing.bottomAreaHeight));*/
            //END

            //Next up, walls
            Wall topWall = new(new Rectangle(playingField.position.X, playingField.position.Y,
                playingField.position.Width, spacing.wallThickness));
            walls.Add(topWall);
            Wall leftWall = new(new Rectangle(playingField.position.X, playingField.position.Y + spacing.wallThickness, spacing.wallThickness, playingField.position.Height - spacing.wallThickness));
            walls.Add(leftWall);
            Wall rightWall = new(new Rectangle(playingField.position.X + playingField.position.Width - spacing.wallThickness, playingField.position.Y + spacing.wallThickness, spacing.wallThickness, playingField.position.Height - spacing.wallThickness));
            walls.Add(rightWall);

            //And then, stuff within the walls
            interiorToWalls = new(new Rectangle(playingField.position.X + spacing.wallThickness,
                playingField.position.Y + spacing.wallThickness,
                playingField.position.Width - 2 * spacing.wallThickness,
                playingField.position.Height - spacing.wallThickness));

            //Then we'll split up the area within the walls
            // Top
            topAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X, interiorToWalls.position.Y,
                          interiorToWalls.position.Width, spacing.topAreaHeight));
            // Bottom
            bottomAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X, interiorToWalls.position.Y + interiorToWalls.position.Height - spacing.bottomAreaHeight, interiorToWalls.position.Width, spacing.bottomAreaHeight));
            // Paddle area
            paddleArea = new(new Rectangle(interiorToWalls.position.X,
                interiorToWalls.position.Y + interiorToWalls.position.Height - bottomAreaOfInteriorToWalls.position.Height - spacing.paddleAreaHeight, interiorToWalls.position.Width,
                spacing.paddleAreaHeight));
            // Middle
            middleAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X,
                interiorToWalls.position.Y + topAreaOfInteriorToWalls.position.Height,
                interiorToWalls.position.Width,
                interiorToWalls.position.Height - topAreaOfInteriorToWalls.position.Height - bottomAreaOfInteriorToWalls.position.Height - paddleArea.position.Height));

            padding = spacing.brickGridSpacingOnAllFourSides;
            brickGrid = new(new Rectangle(middleAreaOfInteriorToWalls.position.X + padding,
                middleAreaOfInteriorToWalls.position.Y + padding,
                middleAreaOfInteriorToWalls.position.Width - 2 * padding,
                middleAreaOfInteriorToWalls.position.Height - 2 * padding - spacing.brickGridBottomSpacing));

            //Add the bricks to brickGrid
            //Figure out each brick's size (and spacing within the brickgrid)
            int h = brickGrid.position.Height;
            int w = brickGrid.position.Width;
            h -= (numRowsOfBricks + 1) * spacing.intraBrickHorizontalSpacing;
            w -= (numBricksPerRow + 1) * spacing.intraBrickVerticalSpacing;
            h /= numRowsOfBricks;
            w /= numBricksPerRow;

            int x; // this'll get set inside the for loop
            int y = brickGrid.position.Y + spacing.intraBrickVerticalSpacing;
            var bg = brickGrid.brickGrid;  //shorthand
            Brick brick;
            for (int i = 0; i < numRowsOfBricks; i++)
            {
                //Reset x
                x = brickGrid.position.X + spacing.intraBrickHorizontalSpacing;

                for (int j = 0; j < numBricksPerRow; j++)
                {
                    //Create a brick and add it
                    brick = new(new Rectangle(x, y, w, h));
                    bg[i].Add(brick);
                    //Then compute the new x (within brickGrid)
                    x += w + spacing.intraBrickVerticalSpacing;
                }
                //After each internal for loop, compute the new y (for the next row)
                y += h + spacing.intraBrickHorizontalSpacing;
            }

            //the 'row regions'
            // NOTE: we'll reuse x,y,w,h.  (h, in particular.  don't change h from line 179!)
            for (int i = 0; i < numRowsOfBricks; i++)
            {
                x = brickGrid.position.X; // + spacing.intraBrickHorizontalSpacing;
                y = brickGrid.position.Y + (i * h) + (spacing.intraBrickVerticalSpacing * (i+1));
                w = brickGrid.position.Width; // - spacing.intraBrickHorizontalSpacing;
                //h =  <--already set (from line 179) 
                rowRegions.Add(new(new Rectangle(x,y,w,h)));
            }

            //We'll split up the bottom area into left/right halves
            leftHalfOfBottomArea = new(new Rectangle(bottomAreaOfInteriorToWalls.position.X, bottomAreaOfInteriorToWalls.position.Y, bottomAreaOfInteriorToWalls.position.Width / 2, bottomAreaOfInteriorToWalls.position.Height));

            rightHalfOfBottomArea = new(new Rectangle(interiorToWalls.position.X + leftHalfOfBottomArea.position.Width, bottomAreaOfInteriorToWalls.position.Y, bottomAreaOfInteriorToWalls.position.Width / 2, bottomAreaOfInteriorToWalls.position.Height));

            //Next up, the 'lives remaining' section
            remainingLivesIcons = new(new Rectangle(leftHalfOfBottomArea.position.X + spacing.remainingLivesLeftSpacing, leftHalfOfBottomArea.position.Y + spacing.remainingLivesTopSpacing, leftHalfOfBottomArea.position.Width - spacing.remainingLivesLeftSpacing - spacing.remainingLivesRightSpacing, leftHalfOfBottomArea.position.Height - spacing.remainingLivesTopSpacing - spacing.remainingLivesBottomSpacing));

            //And the score section
            score = new(new Rectangle(rightHalfOfBottomArea.position.X + spacing.scoreSectionLeftSpacing, rightHalfOfBottomArea.position.Y + spacing.scoreSectionTopSpacing, rightHalfOfBottomArea.position.Width - spacing.scoreSectionLeftSpacing - spacing.scoreSectionRightSpacing, rightHalfOfBottomArea.position.Height - spacing.scoreSectionTopSpacing - spacing.scoreSectionBottomSpacing));

            //And the paddle
            paddle = new(new Rectangle(paddleArea.position.X + paddleArea.position.Width / 2 - spacing.paddleWidth / 2, paddleArea.position.Y, spacing.paddleWidth, spacing.paddleHeight));

            //And ball #1
            Ball ball = new(new Rectangle(paddle.position.X + paddle.position.Width / 2 - spacing.ballWidth / 2, paddle.position.Y - spacing.ballHeight, spacing.ballWidth, spacing.ballHeight));
            balls.Add(ball);

            //and the countdown
            int countdownXCoord = interiorToWalls.position.X + spacing.countdownSideSpacing;
            int countdownYCoord = interiorToWalls.position.Y + spacing.countdownTopSpacing;
            countdown = new(new Rectangle(countdownXCoord, countdownYCoord,
                interiorToWalls.position.X + interiorToWalls.position.Width - countdownXCoord - spacing.countdownSideSpacing,
                interiorToWalls.position.Y + interiorToWalls.position.Height - countdownYCoord - spacing.countdownBottomSpacing));

            //and the pause menu            
            pauseMenu = new(new Rectangle(0, 0, 0, 0));  //so we can access pauseMenu's helper functions

        }

        //DONE, FOR THE MOST PART: Implement GamePlayView.loadContent()
        //IMPORTANT:  Contains the line where we skip countdown
        public override void loadContent(ContentManager contentManager)  
        {
            Debug.Print("Now in GamePlayView.loadContent");

            if (!contentIsLoaded)
            {
                //stash this
                this.contentManager = contentManager;

                pauseMenuFont = contentManager.Load<SpriteFont>("Fonts/ingame-menu");      //Fonts
                inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
                countdownFont = contentManager.Load<SpriteFont>("Fonts/ingame-countdown");
                blue1x1 = contentManager.Load<Texture2D>("Sprites/blue1x1");                //Bricks
                limeGreen1x1 = contentManager.Load<Texture2D>("Sprites/limeGreen1x1");
                orange1x1 = contentManager.Load<Texture2D>("Sprites/orange1x1");
                yellow1x1 = contentManager.Load<Texture2D>("Sprites/yellow1x1");
                darkgray1x1 = contentManager.Load<Texture2D>("Sprites/dark-gray1x1");      //Walls
                ball50x50 = contentManager.Load<Texture2D>("Sprites/ball50x50");            //Ball
                bluegray1x1 = contentManager.Load<Texture2D>("Sprites/bluegray1x1");        //Paddle
                purple1x1 = contentManager.Load<Texture2D>("Sprites/purple1x1");            //misc.
                white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
                black1x1 = contentManager.Load<Texture2D>("Sprites/black1x1");

                contentIsLoaded = true;
            }

            pauseMenu.SecondInitialize(this); //this needs pauseMenuFont to be non-null
                                              //so we'll do it here
            gamePlayState = GamePlayState.InGame; //GamePlayState.Countdown;    //<--note: skipping countdown
        }

        //IN PROGRESS: Implement GamePlayView.processInput()
        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)   
        {
            //Debug.Print("Now in GamePlayView.processInput");

            //Controls: Spacebar (to release the ball), left, right...and that's it, right?  TBD
            //Oh, and Enter, to select an option during the pause menu
            //And Escape, to bring up the pause menu

            /*if (!waitingOnRender)
            {*/

            //MAYBE: Remove states from this OR as necessary
            if (/*gamePlayState == GamePlayState.Initializing ||*/ gamePlayState == GamePlayState.Countdown
                || gamePlayState == GamePlayState.InGame /*|| gamePlayState == GamePlayState.Paused */
                || gamePlayState == GamePlayState.GameOver /* || GamePlayState.Cleanup */)
            {
                if (keyboard.IsKeyPressed(Keys.S))  //toggle showRegions
                {
                    if (showRegions)
                    {
                        showRegions = false;
                        showCountdownRegion = false;
                        showPauseMenuRegion = false;
                        showRowRegions = false;
                    }
                    else
                        showRegions = true;
                }

                if (keyboard.IsKeyPressed(Keys.A))  //toggle showCountdownRegion
                {
                    if (showRegions)
                    {
                        if (showCountdownRegion)
                        {
                            showCountdownRegion = false;
                        }
                        else
                        {
                            showPauseMenuRegion = false;
                            showCountdownRegion = true;
                        }
                    }
                }

                if (keyboard.IsKeyPressed(Keys.D))  //toggle showPauseMenuRegion
                {
                    if (showRegions)
                    {
                        if (showPauseMenuRegion)
                        {
                            showPauseMenuRegion = false;
                        }
                        else
                        {
                            showCountdownRegion = false;
                            showPauseMenuRegion = true;
                        }
                    }
                }

                if (keyboard.IsKeyPressed(Keys.R))
                {
                    if (showRegions)
                    {
                        if (showRowRegions)
                            showRowRegions = false;
                        else
                        {
                            showRowRegions = true;
                        }
                    }
                }
            }   
           

            if(gamePlayState == GamePlayState.Paused)
            {
                //TODO: Enter/arrow keys during the pause menu
            }

            //TODO: Change this to have Esc bring up a pause menu
            // That is...a menu with 'quit' and 'resume' options
            if (gamePlayState == GamePlayState.InGame || gamePlayState == GamePlayState.Countdown)
            {
                if (keyboard.IsKeyPressed(Keys.Escape))
                {
                    //For the moment, this just cleans up the game and exits to the menu
                    gamePlayState = GamePlayState.Cleanup;
                    Reinitialize(graphicsDevice, graphics);
                    return GameStateEnum.MainMenu;
                }
            }

            //In GameOver, we'll just have the user press Escape to return to the menu
            if (gamePlayState == GamePlayState.GameOver)
            {
                if (keyboard.IsKeyPressed(Keys.Escape))
                {
                    //For the moment, this just cleans up the game and exits to the menu
                    gamePlayState = GamePlayState.Cleanup;
                    Reinitialize(graphicsDevice, graphics);
                    return GameStateEnum.MainMenu;
                }
            }

            //in-game keys
            if (gamePlayState == GamePlayState.InGame)
            {
                if (keyboard.IsKeyHeld(Keys.Left))
                {
                    paddle.Move(Direction.Left, gameTime, this);
                }
                if (keyboard.IsKeyHeld(Keys.Right))
                {
                    paddle.Move(Direction.Right, gameTime, this);
                }
                if (keyboard.IsKeyPressed(Keys.Space))
                {
                    //Find balls that're at rest and give them an initial velocity
                    for (int i = 0; i < balls.Count; i++)
                    {
                        if (balls[i].IsAtRest())
                        {
                            balls[i].GiveVelocity();
                        }
                    }
                }
            }

            //}//END if(!waitingOnRender)

            return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime, Renderer renderer)
        {
            //Debug.Print("Now in GamePlayView.render()");

            if (gamePlayState != GamePlayState.Initializing)
                base.render(gameTime, renderer);

            //waitingOnRender = false;
        }

        //DONE FOR NOW, I THINK: GamePlayview.update()
        public override void update(GameTime gameTime, Renderer renderer)
        {
            //Debug.Print("Now in GamePlayView.update()");

            /*if (gamePlayState == GamePlayState.Initializing || gamePlayState == GamePlayState.Countdown
                || gamePlayState == GamePlayState.InGame || gamePlayState == GamePlayState.Paused
                || gamePlayState == GamePlayState.GameOver)*/

            GameElement el;

            if (gamePlayState != GamePlayState.Initializing || gamePlayState != GamePlayState.Cleanup)
            {
                /*if (!waitingOnRender)
                {*/

                //Vector2 stringSize = pauseMenuFont.MeasureString(MESSAGE);

                /*el = new GameElement(RenderType.Text, pauseMenuFont, MESSAGE, 
                                        new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                                 graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);
                renderer.AddToRenderList(el);*/

                //Draw each region of the screen as a solid color
                // We'll make sure we get the render-order right, plus it'll be fun to see.  *thumbs up*
                if (showRegions)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, white1x1, windowInterior.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, limeGreen1x1, playingField.position, Color.White);
                    renderer.AddToRenderList(el);
                }

                foreach (Wall wall in walls)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, darkgray1x1, wall.position, Color.White);
                    renderer.AddToRenderList(el);
                }

                if (showRegions)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, purple1x1, interiorToWalls.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, blue1x1, topAreaOfInteriorToWalls.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, orange1x1, bottomAreaOfInteriorToWalls.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, white1x1, paddleArea.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, purple1x1, middleAreaOfInteriorToWalls.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, black1x1, brickGrid.position, Color.White);
                    renderer.AddToRenderList(el);
                }//END if(showRegions)

                //Add the bricks from brickGrid
                var bg = brickGrid.brickGrid;
                for (int i = 0; i < numRowsOfBricks; i++)
                {
                    Texture2D tx;
                    switch (i)
                    {
                        case 0:
                        case 1:
                            tx = limeGreen1x1;
                            break;
                        case 2:
                        case 3:
                            tx = blue1x1;
                            break;
                        case 4:
                        case 5:
                            tx = orange1x1;
                            break;
                        case 6:
                        case 7:
                            tx = yellow1x1;
                            break;
                        default:
                            throw new System.Exception("Unrecognized row number");

                    }
                    for (int j = 0; j < numBricksPerRow; j++)
                    {
                        if (!bg[i][j].hasBeenHit)
                        {
                            el = new GameElement(RenderType.UI, CallType.Rectangle, tx, bg[i][j].position, Color.White);
                            renderer.AddToRenderList(el);
                        }
                        else
                        {
                            if (bg[i][j].isExploding)
                            {
                                bg[i][j].Explode(gameTime, this, renderer); //the 2nd/3rd argument(s) is/are temporary
                            }
                        }
                    }
                }

                if (showRegions)
                {
                    //the 'row regions'
                    if (showRowRegions)
                    {
                        for (int i = 0; i < rowRegions.Count; i++)
                        {
                            el = new GameElement(RenderType.UI, CallType.Rectangle, white1x1, rowRegions[i].position, Color.White);
                            renderer.AddToRenderList(el);
                        }
                    }

                    //Bottom area, left and right
                    el = new GameElement(RenderType.UI, CallType.Rectangle, blue1x1, leftHalfOfBottomArea.position, Color.White);
                    renderer.AddToRenderList(el);

                    el = new GameElement(RenderType.UI, CallType.Rectangle, limeGreen1x1, rightHalfOfBottomArea.position, Color.White);
                    renderer.AddToRenderList(el);

                    //remainingLivesIcons
                    el = new GameElement(RenderType.UI, CallType.Rectangle, orange1x1, remainingLivesIcons.position, Color.White);
                    renderer.AddToRenderList(el);

                    //score section
                    el = new GameElement(RenderType.UI, CallType.Rectangle, yellow1x1, score.position, Color.White);
                    renderer.AddToRenderList(el);

                }//END if(showRegions)

                //paddle
                el = new GameElement(RenderType.UI, CallType.Rectangle, bluegray1x1, paddle.position, Color.White);
                renderer.AddToRenderList(el);

                //ball
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Move(gameTime, this);
                    el = new GameElement(RenderType.UI, CallType.Rectangle, ball50x50, balls[i].position, Color.White);
                    renderer.AddToRenderList(el);
                }

                //countdown
                if (showRegions && showCountdownRegion)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, yellow1x1, countdown.position, Color.White);
                    renderer.AddToRenderList(el);
                }

                //pause menu
                if (showRegions && showPauseMenuRegion)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, orange1x1, pauseMenu.position, Color.White);
                    renderer.AddToRenderList(el);
                }

                /*waitingOnRender = true;

                }//END if (!waitingOnRender)*/

            }//END if (gamePlayState != GamePlayState.Initializing || gamePlayState != GamePlayState.Cleanup)

        }//END update()


        //DONE, I THINK: reinitializing the GamePlayView
        public void Reinitialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            Debug.Print("Now in GamePlayView.Reinitialize()");

            gamePlayState = GamePlayState.Initializing;
            initialize(graphicsDevice, graphics);
            loadContent(contentManager);

            showRegions = false;
            showRowRegions = false;
            showCountdownRegion = false;
            showPauseMenuRegion = false;
        }

    }//END class GamePlayView
}
