﻿using Breakout.Game_elements;
using Breakout.Game_objects;
using Breakout.Game_objects.Window_areas;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Drawing;

/* "When starting the game, provide a 3, 2, 1 count down timer, showing the numbers 3, 2, 1 in the middle of the screen for the count down.  Following the completion of the countdown, the ball starts from the paddle in a nice direction (nice meaning not too steep of an angle and not straight up)." */

/* "Player starts with three paddles; no way to earn any more.
    When the player doesn't hit the ball, subtract a paddle from the remaining paddles (or end the game if none left) and provide a 3, 2, 1 count down timer before starting with the new paddle.  Ball starts in the same way as the start of the game." */

/* "Show current score; place at bottom of the gameplay screen.
    Graphically (not text) show number of paddles left; place in the upper right or lower left of the gameplay screen." */

/* "Background music during the gameplay." */


namespace Breakout.Game_states
{
    enum GamePlayState
    {
        //unset = 0,
        OutOfGame,
        Initializing,
        Countdown,
        InGame,
        Paused,
        ResettingLevel,
        GameOver,
        Cleanup //what else?  TBD
    }


    public class GamePlayView : GameStateView
    {
        //Some stuff to stash
        GraphicsDevice graphicsDevice;
        internal ContentManager contentManager;
        
        //Some constants
        const int numRowsOfBricks = 8;
        const int numBricksPerRow = 14;

        //Sprites & fonts
        internal SpriteFont pauseMenuFont;  //Fonts
        internal SpriteFont inGameScoreFont;
        private SpriteFont countdownFont;
        private SpriteFont gameOverFont;
        private SpriteFont gameOverEscapePromptFont;
        private Texture2D blue1x1;          //Bricks
        private Texture2D limeGreen1x1;
        private Texture2D orange1x1;
        private Texture2D yellow1x1;
        private Texture2D darkgray1x1;      //Wall
        private Texture2D ball50x50;        //Ball
        private Texture2D bluegray1x1;      //Paddle
        private Texture2D galaxy;           //BG Image
        private Texture2D purple1x1;        //misc.  //MAYBE: Remove, once we're done with it.
        internal Texture2D white1x1;
        internal Texture2D black1x1;

        //Game Objects -- Everything below here (I think) is initialized in initialize()
        // Lists
        internal List<Ball> balls;
        internal List<Wall> walls;
        internal List<RowRegion> rowRegions;

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
        internal PauseMenu pauseMenu;
        internal RemainingLives remainingLives;
        RightHalfOfBottomArea rightHalfOfBottomArea;
        internal Score score;
        TopAreaOfInteriorToWalls topAreaOfInteriorToWalls;
        //TopAreaOfPlayingField topAreaOfPlayingField;        
        WindowInterior windowInterior;

        //Misc. variables
        bool showRegions;
        bool showRowRegions;
        bool showCountdownRegion;
        bool showPauseMenuRegion;

        //Variables that do NOT need to be reinitialize in Reinitalize()
        bool isContentLoaded = false;
        bool areSubsystemsStashed = false;
        internal GamePlayState gamePlayState;

        //and then..other stuff?  *shrug*
        internal bool waitingOnRender = false;
        internal bool waitingToReinitializeBalls = false;
        bool isBGMPlaying = false;

        public GamePlayView() { }

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, SubsystemsHolder subsystems)
        {
            //Debug.Print("Now in GamePlayView.initialize");
            //Debug.Print($"Setting gamePlayState to: Initializing; current value is: {gamePlayState}");
            gamePlayState = GamePlayState.Initializing;

            //stash these
            if (!areSubsystemsStashed)
            {
                base.initialize(graphicsDevice, graphics, subsystems);
                this.graphicsDevice = graphicsDevice;
                highScores = subsystems.highScores;

                areSubsystemsStashed = true;
            }

            //new the lists
            balls = new();
            walls = new();
            rowRegions = new();

            //initialize some variables
            showRegions = false;
            showRowRegions = false;
            showCountdownRegion = false;
            showPauseMenuRegion = false;

            //Set up the game objects
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
                middleAreaOfInteriorToWalls.position.Height - 2 * padding - spacing.brickGridBottomSpacing), numRowsOfBricks);

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
            remainingLives = new(new Rectangle(leftHalfOfBottomArea.position.X + spacing.remainingLivesLeftSpacing, leftHalfOfBottomArea.position.Y + spacing.remainingLivesTopSpacing, leftHalfOfBottomArea.position.Width - spacing.remainingLivesLeftSpacing - spacing.remainingLivesRightSpacing, leftHalfOfBottomArea.position.Height - spacing.remainingLivesTopSpacing - spacing.remainingLivesBottomSpacing), subsystems, this);

            //And the score section
            score = new(new Rectangle(rightHalfOfBottomArea.position.X + spacing.scoreSectionLeftSpacing, rightHalfOfBottomArea.position.Y + spacing.scoreSectionTopSpacing, rightHalfOfBottomArea.position.Width - spacing.scoreSectionLeftSpacing - spacing.scoreSectionRightSpacing, rightHalfOfBottomArea.position.Height - spacing.scoreSectionTopSpacing - spacing.scoreSectionBottomSpacing), subsystems, this);

            //and the paddle
            paddle = new(new Rectangle(paddleArea.position.X + paddleArea.position.Width / 2 - spacing.paddleWidth / 2, paddleArea.position.Y, spacing.paddleWidth, spacing.paddleHeight));

            //And add ball #1
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

            //and the..pause menu?
            pauseMenu.Initialize(subsystems);
        }

        public override void loadContent(ContentManager contentManager)  
        {
            //Debug.Print("Now in GamePlayView.loadContent");

            if (!isContentLoaded)
            {
                //stash this
                this.contentManager = contentManager;

                pauseMenuFont = contentManager.Load<SpriteFont>("Fonts/ingame-menu");      //Fonts
                inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
                countdownFont = contentManager.Load<SpriteFont>("Fonts/ingame-countdown");
                gameOverFont = contentManager.Load<SpriteFont>("Fonts/game-over");
                gameOverEscapePromptFont = contentManager.Load<SpriteFont>("Fonts/game-over-prompt");
                countdownFont = contentManager.Load<SpriteFont>("Fonts/countdown");
                blue1x1 = contentManager.Load<Texture2D>("Sprites/blue1x1");                //Bricks
                limeGreen1x1 = contentManager.Load<Texture2D>("Sprites/limeGreen1x1");
                orange1x1 = contentManager.Load<Texture2D>("Sprites/orange1x1");
                yellow1x1 = contentManager.Load<Texture2D>("Sprites/yellow1x1");
                darkgray1x1 = contentManager.Load<Texture2D>("Sprites/dark-gray1x1");      //Walls
                ball50x50 = contentManager.Load<Texture2D>("Sprites/ball50x50");            //Ball
                bluegray1x1 = contentManager.Load<Texture2D>("Sprites/bluegray1x1");        //Paddle
                galaxy = contentManager.Load<Texture2D>("Sprites/galaxy");                  //BG image
                purple1x1 = contentManager.Load<Texture2D>("Sprites/purple1x1");            //misc.
                white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
                black1x1 = contentManager.Load<Texture2D>("Sprites/black1x1");

                remainingLives.loadContent();

                isContentLoaded = true;
            }

            pauseMenu.SecondInitialize(this); //this needs pauseMenuFont to be non-null
                                              //so we'll do it here
        }

        public override GameStateEnum processInput(GameTime gameTime)   
        {
            //Debug.Print("Now in GamePlayView.processInput");

            //Controls: Spacebar (to release the ball), left, right...and that's it, right?  TBD
            //Oh, and Enter, to select an option during the pause menu
            //And Escape, to bring up the pause menu
            if(gamePlayState != GamePlayState.OutOfGame)
            {
                if (!waitingOnRender)
                {
                    //MAYBE: Remove states from this OR as necessary
                    if (/*gamePlayState == GamePlayState.Initializing ||*/ gamePlayState == GamePlayState.Countdown
                    || gamePlayState == GamePlayState.InGame /*|| gamePlayState == GamePlayState.Paused */
                    || gamePlayState == GamePlayState.GameOver /*|| gamePlayState == GamePlayState.ResettingLevel*/ /*|| GamePlayState.Cleanup*/)
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

                    if (gamePlayState == GamePlayState.InGame || gamePlayState == GamePlayState.Countdown /*|| gamePlayState == GamePlayState.ResettingLevel*/)
                    {
                        if (gamePlayState == GamePlayState.InGame)
                        {
                            //in-game keys
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
                                        //Debug.Print($"Ball spawned; hitBricksAtSpawnTime set to: {brickGrid.numBricksHit}");
                                        balls[i].SetHitBricksAtSpawnTime(brickGrid.numBricksHit);
                                        balls[i].GiveVelocity();
                                    }
                                }
                            }
                        }

                        if (keyboard.IsKeyPressed(Keys.Escape))
                        {
                            pauseMenu.prevStateBeforePaused = gamePlayState;
                            gamePlayState = GamePlayState.Paused;
                            audioPlayer.PauseBGM();
                            pauseMenu.isPaused = true;

                            /*Debug.Print($"Setting gamePlayState to: Cleanup; current value is: {gamePlayState}");
                            gamePlayState = GamePlayState.Cleanup;*/
                        }
                    }
                    else if (gamePlayState == GamePlayState.Paused)
                    {   
                        if(keyboard.IsKeyPressed(Keys.Up))
                        {
                            if (pauseMenu.pauseMenuOptions != PauseMenu.PauseMenuOptions.resume)
                                pauseMenu.pauseMenuOptions--;
                        }
                        else if (keyboard.IsKeyPressed(Keys.Down))
                        {
                            if (pauseMenu.pauseMenuOptions != PauseMenu.PauseMenuOptions.exit)
                                pauseMenu.pauseMenuOptions++;
                        }
                        else if (keyboard.IsKeyPressed(Keys.Enter))
                        {
                            if (pauseMenu.pauseMenuOptions == PauseMenu.PauseMenuOptions.resume)
                            {
                                gamePlayState = pauseMenu.prevStateBeforePaused;
                                audioPlayer.ResumeBGM();
                                pauseMenu.isPaused = false;
                            }
                            else if (pauseMenu.pauseMenuOptions == PauseMenu.PauseMenuOptions.exit)
                            {
                                gamePlayState = GamePlayState.Cleanup;
                                //ReinitializeGame(graphicsDevice, graphics);
                                audioPlayer.StopBGM();
                                isBGMPlaying = false;
                                return GameStateEnum.MainMenu;
                            }
                        }
                        else if (keyboard.IsKeyPressed(Keys.Escape))
                        {
                            gamePlayState = pauseMenu.prevStateBeforePaused;
                            audioPlayer.ResumeBGM();
                            pauseMenu.isPaused = false;
                        }
                    }
                    else if (gamePlayState == GamePlayState.GameOver)
                    {  
                        if (keyboard.IsKeyPressed(Keys.Escape))
                        {   
                            //Debug.Print($"Setting gamePlayState to: Cleanup; current value is: {gamePlayState}");
                            gamePlayState = GamePlayState.Cleanup;
                            audioPlayer.StopBGM();
                            isBGMPlaying = false;
                            return GameStateEnum.MainMenu;
                        }
                    }

                }//END if(!waitingOnRender)

            }//END if(gamePlayState != GamePlayState.OutOfGame)

            return GameStateEnum.GamePlay;
        }

        //NOTE: GamePlayView.render() contains a line that skips base.render() on certain states
        //ONGOING: Check any new GamePlayStates to see if it should be added to the conditional
        public override void render(GameTime gameTime)
        {
            //Debug.Print("Now in GamePlayView.render()");

            if (gamePlayState != GamePlayState.Initializing || gamePlayState != GamePlayState.Cleanup || gamePlayState != GamePlayState.ResettingLevel)
                base.render(gameTime);

            waitingOnRender = false;
        }

        public override void update(GameTime gameTime)
        {
            //Debug.Print("Now in GamePlayView.update()");

            if (!isBGMPlaying && gamePlayState != GamePlayState.Cleanup)
            {
                //TODO: uncomment this before submitting
                //audioPlayer.PlayBGM();        
                isBGMPlaying = true;
            } 

            /*if (gamePlayState == GamePlayState.Initializing || gamePlayState == GamePlayState.Countdown
                || gamePlayState == GamePlayState.InGame || gamePlayState == GamePlayState.Paused
                || gamePlayState == GamePlayState.ResettingLevel || gamePlayState == GamePlayState.GameOver
                || gamePlayState != GamePlayState.Cleanup)*/

            if (gamePlayState != GamePlayState.OutOfGame)
            {
                if (gamePlayState == GamePlayState.Initializing)
                {
                    if (isContentLoaded)
                    {
                        //Debug.Print($"Setting gamePlayState to: Countdown; current value is: {gamePlayState}");
                        gamePlayState = GamePlayState.Countdown;
                    }
                }
                else if (gamePlayState == GamePlayState.Countdown)
                {
                    //FOR NOW: we'll skip the countdown state and go directly to inGame
                    //Debug.Print($"Setting gamePlayState to: InGame; current value is: {gamePlayState}");
                    gamePlayState = GamePlayState.InGame;

                }
                else if (gamePlayState == GamePlayState.InGame)
                {
                    DrawGame(gameTime);
                }
                else if (gamePlayState == GamePlayState.Paused)
                {
                    DrawGame(gameTime);
                    //TODO: extend timers (so that the ending timer gets pushed forward by a cycle)
                    //      That means: exploding bricks, countdown....what else?

                    // Or we could simply not call the methods that add to elapsedTime; TBD

                    pauseMenu.DrawPauseMenu(this);

                }
                else if (gamePlayState == GamePlayState.ResettingLevel)
                {
                    DrawGame(gameTime);
                    ResetLevel(gameTime);
                }
                else if (gamePlayState == GamePlayState.GameOver)
                {
                    DrawGame(gameTime);
                    DrawGameOver(gameTime);
                }
                else if (gamePlayState == GamePlayState.Cleanup)
                {
                    //REMINDER: ReinitializeGame() sets gamePlayState to initialize (in Initalize())
                    ReinitializeGame(graphicsDevice, graphics); 
                }
                else
                {
                    throw new Exception("GamePlayView.update says: Invalid gamePlayState");
                }
            }

            waitingOnRender = true;

        }//END update()

        private void ReinitializeBall()
        {
            //Reinitialize the list of balls
            balls = new();

            //And add ball #1
            Ball ball = new(new Rectangle(paddle.position.X + paddle.position.Width / 2 - spacing.ballWidth / 2, paddle.position.Y - spacing.ballHeight, spacing.ballWidth, spacing.ballHeight));
            balls.Add(ball);

            waitingToReinitializeBalls = false;
        }

        private void DrawGame(GameTime gameTime)
        {
            GameElement el;

            /*if (!waitingOnRender)
                {*/

            //Vector2 stringSize = pauseMenuFont.MeasureString(highScoresHeaderMsg);

            /*el = new GameElement(RenderType.Text, pauseMenuFont, highScoresHeaderMsg, 
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

            //interiorToWalls w/ BG image
            el = new GameElement(RenderType.UI, CallType.Rectangle, galaxy, interiorToWalls.position, Color.White);
            renderer.AddToRenderList(el);

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
                    else  //if the brick has been hit
                    { 
                        if (bg[i][j].isExploding && !pauseMenu.isPaused) //check to see if it's still exploding
                                                                            // and that the game's not paused
                        {
                            bg[i][j].Explode(gameTime, this, renderer); //the 2nd/3rd argument(s) is/are temporary
                                                                        // ...maybe.  TBD
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

                //remainingLives
                el = new GameElement(RenderType.UI, CallType.Rectangle, orange1x1, remainingLives.position, Color.White);
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
                if (!pauseMenu.isPaused)
                    balls[i].Move(gameTime, this);

                if (balls[i].isActive)
                {
                    el = new GameElement(RenderType.UI, CallType.Rectangle, ball50x50, balls[i].position, Color.White);
                    renderer.AddToRenderList(el);
                }
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

            //remaining lives
            remainingLives.DrawRemainingLives();

            //Score
            score.DrawScore();

            /*waitingOnRender = true;

            }//END if (!waitingOnRender)*/

        }//END DrawGame()

        private void ResetLevel(GameTime gameTime)
        {
            //Debug.Print("Inside GamePlayView.ResetLevel()");

            //reset the ball's (and paddle's) position & velocity
            ReinitializeBall();

            //and then...
            //Debug.Print($"Setting gamePlayState to: InGame; current value is: {gamePlayState}");
            gamePlayState = GamePlayState.InGame;
        }

        public void ReinitializeGame(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            //Debug.Print("Now in GamePlayView.ReinitializeGame()");
            initialize(graphicsDevice, graphics, subsystems);
            loadContent(contentManager);

            remainingLives.loadContent();
            score.loadContent();
        }

        private void DrawGameOver(GameTime gameTime)
        {
            string str = "Game Over";
            string str2 = "Press Escape to return to the main menu";

            //Debug.Print("GamePlayView.DrawGameOver() says: TODO!");
            (float bottom, Vector2 vec) = stringRenderer.RenderStringHVCentered(str, gameOverFont, interiorToWalls.position);
            vec.Y -= 150;   //<--manually adding an offset, for the sake of appearance
            GameElement el = new(RenderType.Text, gameOverFont, str, vec, Color.Red);

            vec = new(stringRenderer.RenderStringHCentered(str2, gameOverEscapePromptFont, interiorToWalls.position), bottom + spacing.gameOverIntraLineSpacing);
            vec.Y -= 40;  //<--manually adding an offset here too
            GameElement el2 = new(RenderType.Text, gameOverEscapePromptFont, str2, vec, Color.Red);

            int x, y, h, w;
            x = windowInterior.position.X + spacing.gameOverSideSpacing;
            y = windowInterior.position.Y + spacing.gameOverTopSpacing;
            w = windowInterior.position.X + windowInterior.position.Width - 2 * spacing.gameOverSideSpacing;
            h = windowInterior.position.Y + windowInterior.position.Height - spacing.gameOverTopSpacing -  spacing.gameOverBottomSpacing;
            Rectangle r = new(x, y, w, h);
            GameElement el3 = new(RenderType.UI, CallType.Rectangle, black1x1, r, Color.White);
 
            renderer.AddToRenderList(el3);  //<-- note! UI element gets rendered first
            renderer.AddToRenderList(el);
            renderer.AddToRenderList(el2);
        }

    }//END class GamePlayView
}
