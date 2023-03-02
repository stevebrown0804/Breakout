using Breakout.Game_objects;
using Breakout.Game_states;
using Breakout.Subsystems;
using Breakout.Subsystems.Base;
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
        private GameStateEnum gameStateEnum = GameStateEnum.MainMenu;  //<--the starting point
        private Dictionary<GameStateEnum, IGameState> states;

        //Subsystems
        SubsystemsHolder subsystems;
        BO_Keyboard keyboard;
        Renderer renderer;
        StringRenderer stringRenderer;

        public Breakout_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Initialize subsystems!            
            subsystems = new();
            keyboard = subsystems.keyboard;
            keyboard.InitializePreviousState();
            renderer = subsystems.renderer;
            stringRenderer = subsystems.stringRenderer;

            //Then do other stuff
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
                states[key].initialize(this.GraphicsDevice, graphics, subsystems);
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
            gameStateEnum = currentState.processInput(gameTime);

            //Now's our chance to exit! ...(/^^)/
            if (gameStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            keyboard.SetPreviousStateToCurrentState();
            currentState.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(gameStateEnum == GameStateEnum.GamePlay)
                GraphicsDevice.Clear(Color.Black);
            else if(gameStateEnum == GameStateEnum.MainMenu)
                GraphicsDevice.Clear(Color.Black);
            else if(gameStateEnum == GameStateEnum.HighScores)
                GraphicsDevice.Clear(Color.Purple);     //MAYBE: Pick another color (or stick with this one, *shrug*)
            else if(gameStateEnum == GameStateEnum.About)
                GraphicsDevice.Clear(new Color(64, 64, 64));  //Dark-ish gray
            else //for states yet to be defined (or used, *cough*Options*cough*)
                GraphicsDevice.Clear(Color.CornflowerBlue);

            //Do some drawing
            currentState.render(gameTime);
            currentState = states[gameStateEnum];

            //Then clear out the render list in preparation for the next loop
            renderer.ClearRenderList();

            base.Draw(gameTime);
        }
    }
}