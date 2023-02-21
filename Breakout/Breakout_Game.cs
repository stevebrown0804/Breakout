using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Breakout
{
    public enum GameStateEnum
    {
        MainMenu,
        GamePlay,
        HighScores,
        Options,
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
        BO_Keyboard keyboard;
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
            keyboard = new();
            keyboard.InitializePreviousState(); //just to make it (the 'keyboard' variable) non-empty
            renderer = new();

            //Then do other stuff!
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            states = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, new GamePlayView() },
                { GameStateEnum.HighScores, new HighScoresView() },
                { GameStateEnum.About, new AboutView() }
            };

            foreach (var key in states.Keys)
            {
                states[key].initialize(this.GraphicsDevice, graphics);
            }

            currentState = states[gameStateEnum];

            base.Initialize();
        }

        protected override void LoadContent()
        {            
            foreach (var key in states.Keys)
            {
                states[key].loadContent(this.Content);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.Clear(new Color(64, 64, 64));  //dark-ish gray...which I couldn't find a name for
                                                          //  Although I didn't look super-hard *thumbs up*
            //GraphicsDevice.Clear(Color.Black);
            //GraphicsDevice.Clear(Color.White);

            //Do some drawing
            currentState.render(gameTime, renderer);
            currentState = states[gameStateEnum];

            //And, with the drawing done, clear out the render list in preparation for next time
            renderer.ClearRenderList();

            base.Draw(gameTime);
        }
    }
}