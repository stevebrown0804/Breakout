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

/* "When starting the game, provide a 3, 2, 1 count down timer, showing the numbers 3, 2, 1 in the middle of the screen for the count down.  Following the completion of the countdown, the ball starts from the paddle in a nice direction (nice meaning not too steep of an angle and not straight up)." */

/* "Player starts with three paddles; no way to earn any more.
    When the player doesn't hit the ball, subtract a paddle from the remaining paddles (or end the game if none left) and provide a 3, 2, 1 count down timer before starting with the new paddle.  Ball starts in the same way as the start of the game." */

/* "Show current score; place at bottom of the gameplay screen.
    Graphically (not text) show number of paddles left; place in the upper right or lower left of the gameplay screen." */

/* "Background music during the gameplay." */

//TODO: Make/find a sprite for the '# paddles remaining.'  (Pac Man sprite? hmm...)
//TODO: Find some BGM and integrate it

namespace Breakout.Game_states
{
    enum GamePlayState  //public/internal? TBD      //Also, I think this can go inside the class.  TBD
    {
        //nset = 0,
        Initializing,
        Countdown,
        InGame,
        Paused,
        GameOver  //what else?  TBD
    }
    //How about a stack to track this? (maybe) TBD


    public class GamePlayView : GameStateView
    {
        //Sprites & fonts
        private SpriteFont inGameMenuFont;  //Fonts
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
        private Texture2D white1x1;

        //private const string MESSAGE = "TODO: Game";

        //Game Objects
        List<Ball> balls = new();
        BottomAreaOfInteriorToWalls bottomAreaOfInteriorToWalls;
        //BottomAreaOfPlayingField bottomAreaOfPlayingField;
        List<Brick> bricks = new();
        BrickGrid brickGrid; // = new();
        InteriorToWalls interiorToWalls;
        MiddleAreaOfInteriorToWalls middleAreaOfInteriorToWalls;
        //MiddleAreaOfPlayingField middleAreaOfPlayingField;
        Paddle paddle = new();
        PaddleArea paddleArea;
        internal PlayingField playingField; // = new();
        PauseMenu pauseMenu = new();                        // <---necessary at this point? TBD
        RemainingLivesIcons remainingLivesIcons = new();
        Score score = new();
        TopAreaOfInteriorToWalls topAreaOfInteriorToWalls;
        //TopAreaOfPlayingField topAreaOfPlayingField;
        List<Wall> walls = new();
        WindowInterior windowInterior; // = new();
        Spacing spacing;

        //Misc. variables
        bool isPaused = false;
        int remainingLives = 3;  // 2?  TBD
        GamePlayState gamePlayState;

        public GamePlayView()
        {
            gamePlayState = GamePlayState.Initializing;
        }

        //IN PROGRESS: Implement GamePlayView.loadContent()
        public override void loadContent(ContentManager contentManager)  
        {
            inGameMenuFont = contentManager.Load<SpriteFont>("Fonts/ingame-menu");      //Fonts
            inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
            countdownFont = contentManager.Load<SpriteFont> ("Fonts/ingame-countdown");            
            blue1x1 = contentManager.Load<Texture2D>("Sprites/blue1x1");                //Bricks
            limeGreen1x1 = contentManager.Load<Texture2D>("Sprites/limeGreen1x1");
            orange1x1 = contentManager.Load<Texture2D>("Sprites/orange1x1");
            yellow1x1 = contentManager.Load<Texture2D>("Sprites/yellow1x1");
            darkgray1x1 = contentManager.Load<Texture2D>("Sprites/dark-gray1x1");      //Walls
            ball50x50 = contentManager.Load<Texture2D>("Sprites/ball50x50");            //Ball
            bluegray1x1 = contentManager.Load<Texture2D>("Sprites/bluegray1x1");        //Paddle
            purple1x1 = contentManager.Load<Texture2D>("Sprites/purple1x1");            //misc.
            white1x1 = contentManager.Load<Texture2D>("Sprites/white1x1");
        }

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);

            //and then...

            //...set up the game objects - IN PROGRESS
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
            //END Do we need these?

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
                playingField.position.Width - 2*spacing.wallThickness, 
                playingField.position.Height - spacing.wallThickness));

            //Then we'll split up the area within the walls
            //Top
            topAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X, interiorToWalls.position.Y,
                          interiorToWalls.position.Width, spacing.topAreaHeight));
            //Bottom
            bottomAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X, interiorToWalls.position.Y + interiorToWalls.position.Height - spacing.bottomAreaHeight, interiorToWalls.position.Width, spacing.bottomAreaHeight));
            //Paddle area
            paddleArea = new(new Rectangle(interiorToWalls.position.X, 
                interiorToWalls.position.Y + interiorToWalls.position.Height - bottomAreaOfInteriorToWalls.position.Height - spacing.paddleAreaHeight, interiorToWalls.position.Width, 
                spacing.paddleAreaHeight));
            //Middle
            middleAreaOfInteriorToWalls = new(new Rectangle(interiorToWalls.position.X,
                interiorToWalls.position.Y + topAreaOfInteriorToWalls.position.Height,
                interiorToWalls.position.Width,
                interiorToWalls.position.Height - topAreaOfInteriorToWalls.position.Height - bottomAreaOfInteriorToWalls.position.Height - paddleArea.position.Height));

            /*brickGrid = new(new Rectangle(middleAreaOfPlayingField.position.X, middleAreaOfPlayingField.position.Y, //Do this after adding walls
                            playingField.position.Width, spacing.paddleAreaHeight));*/
            padding = spacing.brickGridSpacingOnAllFourSides;
            brickGrid = new(new Rectangle(middleAreaOfInteriorToWalls.position.X + padding,
                middleAreaOfInteriorToWalls.position.Y + padding,
                middleAreaOfInteriorToWalls.position.Width - 2 * padding,
                middleAreaOfInteriorToWalls.position.Height - 2 * padding));
        }

        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)   
        {
            //TODO: Implement GamePlayView.processInput()
            //REMINDER: This is called at the beginning of BreakoutGame.Input()

            //TODO: Change this to have Esc bring up a pause window (and pause the game) with 'quit' and 'resume' options -- or change a state variable to 'paused' then call a method that renders the pause menu
            if (keyboard.IsKeyPressed(Keys.Escape))
           {
               return GameStateEnum.MainMenu;
           }

           //Controls: Spacebar (to release the ball), left, right...and that's it, right?  TBD
           //Oh, and Enter, to select an option during the pause menu

           return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime, Renderer renderer)
        {
             base.render(gameTime, renderer);
        }
                
        public override void update(GameTime gameTime, Renderer renderer)
        {
            //IN PROGRESS: GamePlayview.update()

            //Vector2 stringSize = inGameMenuFont.MeasureString(MESSAGE);
            GameElement el;
            /*el = new GameElement(RenderType.Text, inGameMenuFont, MESSAGE, 
                                 new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                                 graphics.PreferredBackBufferHeight / 2 - stringSize.Y),                      Color.Yellow);
            renderer.AddToRenderList(el);*/

            //TODO, FOR NOW: Draw each region of the screen as a solid color
            // We'll make sure we get the render-order right, plus it'll be fun to see.  *thumbs up*
            el = new GameElement(RenderType.UI, CallType.Rectangle, white1x1, windowInterior.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, limeGreen1x1, playingField.position, Color.White);
            renderer.AddToRenderList(el);

            foreach(Wall wall in walls)
            {
                el = new GameElement(RenderType.UI, CallType.Rectangle, darkgray1x1, wall.position, Color.White);
                renderer.AddToRenderList(el);
            }

            el = new GameElement(RenderType.UI, CallType.Rectangle, purple1x1, interiorToWalls.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, blue1x1, topAreaOfInteriorToWalls.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, orange1x1, bottomAreaOfInteriorToWalls.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, bluegray1x1, paddleArea.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, yellow1x1, middleAreaOfInteriorToWalls.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, limeGreen1x1, brickGrid.position, Color.White);
            renderer.AddToRenderList(el);


        }

    }//END class GamePlayView
}
