using Breakout.Game_elements;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    enum GamePlayState  //public/internal? TBD
    {
        unset = 0,
        Initializing,
        Countdown,
        InGame,
        Paused,
        GameOver  //what else?  TBD
    }
    //How about a stack to track this? (maybe) TBD


    //again, this code was stolen.  stolen, I say!
    public class GamePlayView : GameStateView
    {
        private SpriteFont inGameMenuFont;  //Fonts
        private SpriteFont inGameScoreFont;
        private SpriteFont countdownFont;
        private Texture2D blue1x1;          //Bricks
        private Texture2D green1x1;
        private Texture2D orange1x1;
        private Texture2D yellow1x1;
        private Texture2D dark_gray1x1;     //Wall
        private Texture2D ball50x50;        //Ball
        private Texture2D bluegray1x1;      //Paddle

        private const string MESSAGE = "TODO: Game";  //TODO: Comment this out...eventually

        bool isPaused = false;
        int remainingLives = 3;  // 2?  TBD

        //DONE, I THINK: Implement GamePlayView.loadContent()
        public override void loadContent(ContentManager contentManager)  
        {
            inGameMenuFont = contentManager.Load<SpriteFont>("Fonts/ingame-menu");      //Fonts
            inGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score");
            countdownFont = contentManager.Load<SpriteFont> ("Fonts/ingame-countdown");            
            blue1x1 = contentManager.Load<Texture2D>("Sprites/blue1x1");                //Bricks
            green1x1 = contentManager.Load<Texture2D>("Sprites/green1x1");
            orange1x1 = contentManager.Load<Texture2D>("Sprites/orange1x1");
            yellow1x1 = contentManager.Load<Texture2D>("Sprites/yellow1x1");
            dark_gray1x1 = contentManager.Load<Texture2D>("Sprites/dark-gray1x1");      //Walls
            ball50x50 = contentManager.Load<Texture2D>("Sprites/ball50x50");            //Ball
            bluegray1x1 = contentManager.Load<Texture2D>("Sprites/bluegray1x1");        //Paddle
        }

        //TODO: Implement GamePlayView.processInput()
        //REMINDER: This is called at the beginning of BreakoutGame.Input()
        public override GameStateEnum processInput(GameTime gameTime, BO_Keyboard keyboard)   
        {
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
            //IN PROGRESS-ish: GamePlayview.update()

            Vector2 stringSize = inGameMenuFont.MeasureString(MESSAGE);
            GameElement el;
            el = new GameElement(RenderType.Text, inGameMenuFont, MESSAGE, 
                                 new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,                                 graphics.PreferredBackBufferHeight / 2 - stringSize.Y),                      Color.Yellow);
            renderer.AddToRenderList(el);
        }

    }//END class GamePlayView
}
