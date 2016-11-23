using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class MainMenuScene : IScene
    {
        private MyGame _myGame;

        private readonly Button _startGame;
        private readonly Button _loadGame;
        private SpriteFont _menuFont;
        private string _unsuccesfulLoadText;
        private readonly int _buttonWidth = 300;
        private readonly int _buttonHeight = 80;
        private readonly int _buttonSpace = 100;
        private bool unsuccesfulLoad;

        public MainMenuScene(MyGame myGame)
        {
            _myGame = myGame;

            _menuFont = _myGame.Content.Load<SpriteFont>("MenuFont");

            _startGame = new Button(new Rectangle((int)(_myGame.WindowResolution.X - _buttonWidth) / 2,
               _buttonSpace, _buttonWidth, _buttonHeight), _myGame.Content.Load<SpriteFont>("MenuFont"),
               System.Configuration.ConfigurationManager.AppSettings["StartGameText"]);
            _loadGame = new Button(new Rectangle((int)(_myGame.WindowResolution.X - _buttonWidth) / 2,
               _buttonSpace * 2 + _buttonHeight, _buttonWidth, _buttonHeight), _myGame.Content.Load<SpriteFont>("MenuFont"),
               System.Configuration.ConfigurationManager.AppSettings["LoadGameText"]);

            _unsuccesfulLoadText = System.Configuration.ConfigurationManager.AppSettings["UnsuccesfulLoadText"];

            _startGame.Click += (b, e) => myGame.CurrentScene = new DifficultyScene(_myGame);

            _loadGame.Click += Load;

            unsuccesfulLoad = false;

        }

        public void Update(GameTime gameTime)
        {
            _startGame.CheckClick();
            _loadGame.CheckClick();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            _startGame.Draw(spriteBatch, texture);
            _loadGame.Draw(spriteBatch, texture);

            if(unsuccesfulLoad)
                spriteBatch.DrawString(_menuFont, _unsuccesfulLoadText,
                new Vector2(_myGame.WindowResolution.X / 2, _buttonHeight * 2 + _buttonSpace * 2 + 10), Color.Red, 0f,
                new Vector2(_menuFont.MeasureString(_unsuccesfulLoadText).X / 2, 0), 1f, SpriteEffects.None, 0);
        }

        private void Load(object b, EventArgs e)
        {
            GameObjects gameObjects;

            try
            {
                gameObjects = FileIO.ReadSave();
            }
            catch (Exception)
            {
                unsuccesfulLoad = true;
                return;
            }


            _myGame.CurrentScene = new GameScene(_myGame,  gameObjects._difficulty == Difficulties.Easy?
                (IGameFactory)new EasyGameFactory(_myGame.WindowResolution) : new HardGameFactory(_myGame.WindowResolution),
                gameObjects._blocks, gameObjects._catcher, gameObjects._score);
        }


    }
}
