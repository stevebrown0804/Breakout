using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout
{
    public enum GameStateEnum
    {
        //unset = 0  //enum's definition stolen from prof. mathias!
        MainMenu,
        GamePlay,
        HighScores,
        Options,
        //Help,
        About,
        Exit
    }

    public class Breakout_Game : Game
    {
        private GraphicsDeviceManager graphics;
        //private SpriteBatch spriteBatch;

        private IGameState currentState;
        private GameStateEnum gameStateEnum = GameStateEnum.MainMenu;  //we'll start with the main menu
        private Dictionary<GameStateEnum, IGameState> states;

        //Subsystems
        //InputProcessor inputProcessor;
        BO_Keyboard keyboard;
        //Sprites sprites;  //TODO: See if we need to use this AND ContentManager
        Renderer renderer;

        public Breakout_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Initialize subsystems!
            //inputProcessor = new();
            keyboard = new();
            renderer = new();

            //the prof's code
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            // Create all the game states here
            states = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, new GamePlayView() },
                { GameStateEnum.HighScores, new HighScoresView() },
                //{ GameStateEnum.Help, new HelpView() },
                { GameStateEnum.About, new AboutView() }
            };

            // Give all game states a chance to initialize, other than constructor
            foreach (var item in states)
            {
                item.Value.initialize(this.GraphicsDevice, graphics);
            }

            // We are starting with the main menu - as defined by the value set in gameStateEnum
            currentState = states[gameStateEnum];
            //END the prof's code

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);            //one of these will be made within each View

            //sprites = new(this, spriteBatch);                          //again, waiting to determine if we need both this and ContentManager
            //...then use sprites' dictionar(y/ies) to get Texture2Ds

            //the prof's code
            // Give all game states a chance to load their content
            foreach (var state in states)
            {
                state.Value.loadContent(this.Content);
            }
            //END
        }

        protected override void Update(GameTime gameTime)
        {
            //inputProcessor.ProcessInput(gameTime, keyboard);      //this'll happen within each View

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //The prof's code
            gameStateEnum = currentState.processInput(gameTime);
            // Special case for exiting the game
            if (gameStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            currentState.update(gameTime);
            //END

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //  Retort: Ok!
            //renderer.DoRendering();
                        
            GraphicsDevice.Clear(/*Color.SlateGray*/ new Color(32, 32, 32));

            //The prof's code
            currentState.render(gameTime);
            currentState = states[gameStateEnum];
            //END


            base.Draw(gameTime);
        }
    }
}