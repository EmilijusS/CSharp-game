using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game
    {
        public Vector2 WindowResolution { get; private set; }
        public IScene CurrentScene { get; set; }
        public List<Record> EasyRecords { get; private set; }
        public List<Record> HardRecords { get; private set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        public IScoreRepository ScoreRepository;

        public MyGame(IScoreRepository scoreRepository)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ScoreRepository = scoreRepository;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int windowWidth, windowHeight;

            // Setting window resolution from app config file
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["WindowWidth"], out windowWidth);
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["WindowHeight"], out windowHeight);

            WindowResolution = new Vector2(windowWidth, windowHeight);

            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;           

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new[] {Color.White});

            try { EasyRecords = ScoreRepository.Read(Difficulties.Easy);}
            catch { EasyRecords = new List<Record>();}
           
            try { HardRecords = ScoreRepository.Read(Difficulties.Hard);}
            catch { HardRecords = new List<Record>();}

            CurrentScene = new MainMenuScene(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            CurrentScene.Draw(gameTime, _spriteBatch, _texture);

            _spriteBatch.End();


            base.Draw(gameTime);
        }

    }



    
}
