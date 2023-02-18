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
            keyboard.InitializePreviousState(); //just to make it (the 'keyboard' variable) non-empty
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
            //sprites = new(this, spriteBatch);                          //again, waiting to determine if we need both this and ContentManager
            //...then use sprites' dictionar(y/ies) to get Texture2Ds

            //do loadContent for all views
            foreach (var state in states)
            {
                state.Value.loadContent(this.Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard.UpdateCurrentState();            
            gameStateEnum = currentState.processInput(gameTime, keyboard);

            //Now's our chance to exit! ...(/^^)/
            if (gameStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            keyboard.SetPreviousStateToCurrentState();

            currentState.update(gameTime, renderer);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(new Color(32, 32, 32));  //dark gray...which I couldn't find a name for
                                                          //  Although I didn't look super-hard

            currentState.render(gameTime, renderer);
            currentState = states[gameStateEnum];

            renderer.ClearRenderList(); //TODO: See if this is right...I'm still not super-sure
                                        //FOLLOW-UP: Seems right.  Let's leave the TODO in place, for now.

            base.Draw(gameTime);
        }
    }
}