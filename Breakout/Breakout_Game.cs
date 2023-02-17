using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

//TODO: Incorporate the prof's code into this file

namespace Breakout
{
    public enum GameStateEnum
    {
        //unset = 0  //enum's definition stolen from prof. mathias!
        MainMenu,
        GamePlay,
        HighScores,
        Options,
        Help,
        About,
        Exit
    }

    public class Breakout_Game : Game
    {
        //private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //the prof's stuff
        private GraphicsDeviceManager m_graphics;
        private IGameState m_currentState;
        private GameStateEnum m_nextStateEnum = GameStateEnum.MainMenu;
        private Dictionary<GameStateEnum, IGameState> m_states;
        //END

        //Game state stuff
        //GameStateEnum gameStateEnum;
        //IGameState gameState;

        //Subsystems
        //InputProcessor inputProcessor;
        //BO_Keyboard keyboard;
        //Sprites sprites;  //TODO: See if we need to use this AND ContentManager
        //Renderer renderer;

        public Breakout_Game()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Initialize subsystems!
            //inputProcessor = new();
            //keyboard = new();
            //renderer = new();

            //the prof's code
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;

            m_graphics.ApplyChanges();

            // Create all the game states here
            m_states = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, new GamePlayView() },
                { GameStateEnum.HighScores, new HighScoresView() },
                { GameStateEnum.Help, new HelpView() },
                { GameStateEnum.About, new AboutView() }
            };

            // Give all game states a chance to initialize, other than constructor
            foreach (var item in m_states)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }

            // We are starting with the main menu - as defined by the value set in m_nextStateEnum
            m_currentState = m_states[m_nextStateEnum];
            //END the prof's code

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //sprites = new(this, spriteBatch);
            //...then use sprites' dictionar(y/ies) to get Texture2Ds

            //the prof's code
            // Give all game states a chance to load their content
            foreach (var item in m_states)
            {
                item.Value.loadContent(this.Content);
            }
            //END
        }

        protected override void Update(GameTime gameTime)
        {
            //inputProcessor.ProcessInput(gameTime, keyboard);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //The prof's code
            m_nextStateEnum = m_currentState.processInput(gameTime);
            // Special case for exiting the game
            if (m_nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            m_currentState.update(gameTime);
            //END

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //  Retort: Ok!
            //renderer.DoRendering();

            //The prof's code
            GraphicsDevice.Clear(Color.Black);

            m_currentState.render(gameTime);

            m_currentState = m_states[m_nextStateEnum];
            //END


            base.Draw(gameTime);
        }
    }
}