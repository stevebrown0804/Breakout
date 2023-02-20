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
        private Texture2D dark_gray1x1;     //Wall
        private Texture2D ball50x50;        //Ball
        private Texture2D bluegray1x1;      //Paddle

        private const string MESSAGE = "TODO: Game";  //TODO: Comment this out...eventually

        //Game Objects
        List<Ball> balls = new();
        List<Brick> bricks = new();
        BrickGrid brickGrid = new();
        Paddle paddle = new();
        PlayingField playingField; // = new();
        PauseMenu pauseMenu = new();                        // <---necessary at this point? TBD
        RemainingLivesIcons remainingLivesIcons = new();
        Score score = new();
        List<Wall> walls = new();
        WindowInterior windowInterior; // = new();
        Spacing spacing = new();

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
            dark_gray1x1 = contentManager.Load<Texture2D>("Sprites/dark-gray1x1");      //Walls
            ball50x50 = contentManager.Load<Texture2D>("Sprites/ball50x50");            //Ball
            bluegray1x1 = contentManager.Load<Texture2D>("Sprites/bluegray1x1");        //Paddle

            //And now, set up the game objects - IN PROGRESS
            windowInterior = new(new Rectangle(0, 0, 
                                 graphics.PreferredBackBufferWidth, 
                                 graphics.PreferredBackBufferHeight));
            playingField = new(windowInterior.position);        //for now, copy windowInterior.position
            TopArea topArea = new(new Rectangle(playingField.position.X, playingField.position.Y,
                playingField.position.Width, spacing.topAreaHeight));

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
            el = new GameElement(RenderType.UI, CallType.Rectangle, yellow1x1, windowInterior.position, Color.White);
            renderer.AddToRenderList(el);

            el = new GameElement(RenderType.UI, CallType.Rectangle, limeGreen1x1, playingField.position, Color.White);
            renderer.AddToRenderList(el);




        }

    }//END class GamePlayView
}
