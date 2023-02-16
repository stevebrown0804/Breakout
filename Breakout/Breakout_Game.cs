using Breakout.Game_states;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Game state stuff
        GameStateEnum gameStateEnum;
        IGameState gameState;

        //Subsystems
        InputProcessor inputProcessor;
        BO_Keyboard keyboard;
        Sprites sprites;  //TODO: See if we need to use this AND ContentManager
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
            inputProcessor = new();
            keyboard = new();
            renderer = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            sprites = new(this, spriteBatch);
            //...then use sprites' dictionar(y/ies) to get Texture2Ds
        }

        protected override void Update(GameTime gameTime)
        {
            inputProcessor.ProcessInput(gameTime, keyboard);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //  Retort: Ok!
            renderer.DoRendering();
          

            base.Draw(gameTime);
        }
    }
}