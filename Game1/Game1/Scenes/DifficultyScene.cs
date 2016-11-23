using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class DifficultyScene : IScene
    {
        private MyGame _myGame;
        private readonly string _easyText;
        private readonly string _hardText;
        private readonly string _difficultyText;
        private SpriteFont _menuFont;
        private Button _easy;
        private Button _hard;
        private readonly int _buttonWidth = 300;
        private readonly int _buttonHeight = 80;
        private readonly int _buttonSpace = 150;
        private readonly int _difficultyTextPosition = 10;

        public DifficultyScene(MyGame myGame)
        {
            _myGame = myGame;
            _menuFont = myGame.Content.Load<SpriteFont>("MenuFont");
            _difficultyText = System.Configuration.ConfigurationManager.AppSettings["DifficultyText"];
            _easyText = System.Configuration.ConfigurationManager.AppSettings["EasyText"];
            _hardText = System.Configuration.ConfigurationManager.AppSettings["HardText"];

            _easy = new Button(new Rectangle((int)(myGame.WindowResolution.X - _buttonWidth) / 2,
                _buttonSpace, _buttonWidth, _buttonHeight),
                _menuFont, _easyText);
            _hard = new Button(new Rectangle((int)(myGame.WindowResolution.X - _buttonWidth) / 2,
                _buttonSpace * 2 + _buttonHeight, _buttonWidth, _buttonHeight),
                _menuFont, _hardText);

            _easy.Click += delegate { _myGame.CurrentScene = new GameScene(_myGame, new EasyGameFactory(_myGame.WindowResolution));};
            _hard.Click += delegate { _myGame.CurrentScene = new GameScene(_myGame, new HardGameFactory(myGame.WindowResolution));};
        }

        public void Update(GameTime gameTime)
        {
            _easy.CheckClick();
            _hard.CheckClick();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.DrawString(_menuFont, _difficultyText,
                new Vector2(_myGame.WindowResolution.X / 2, _difficultyTextPosition), Color.Black, 0f,
                new Vector2(_menuFont.MeasureString(_difficultyText).X / 2, 0), 1f, SpriteEffects.None, 0);
            _easy.Draw(spriteBatch, texture);
            _hard.Draw(spriteBatch, texture);
        }
    }
}
