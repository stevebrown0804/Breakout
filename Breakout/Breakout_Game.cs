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
        /*Dictionary<string, Subsystems.Base.Subsystems> subsystems;  //Note for next time:  Don't use interfaces when the objects are this different.  Inherit a base class, maybe? TBD
        Subsystems.Base.Subsystems keyboard;
        Subsystems.Base.Subsystems renderer;*/
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
            /*//Initialize subsystems!
            subsystems = new Dictionary<string, Subsystems.Base.Subsystems>
            {
                { "keyboard", new BO_Keyboard() }, 
                { "renderer", new Renderer() },
                { "stringRenderer", new StringRenderer() }
            };*/
            subsystems = new();
            keyboard = subsystems.keyboard;
            keyboard.InitializePreviousState();
            renderer = subsystems.renderer;

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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.Clear(new Color(64, 64, 64));  //dark-ish gray...which I couldn't find a name for
                                                          //  Although I didn't look super-hard *thumbs up*
            //GraphicsDevice.Clear(Color.Black);
            //GraphicsDevice.Clear(Color.White);

            //Do some drawing
            currentState.render(gameTime);
            currentState = states[gameStateEnum];

            //Then clear out the render list in preparation for the next loop
            renderer.ClearRenderList();

            base.Draw(gameTime);
        }
    }
}