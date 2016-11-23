using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class EndMenuScene : IScene
    {
        private MyGame _myGame;
        private int _score;
        private SpriteFont _menuFont;
        private SpriteFont _scoreFont;
        private SpriteFont _recordFont;
        private readonly string _restartGameText;
        private Button _restartGame;
        private List<Record> _records;
        private readonly int _buttonWidth = 300;
        private readonly int _buttonHeight = 80;
        private readonly int _scorePosition = 10;

        public EndMenuScene(MyGame myGame, int score, List<Record> records)
        {
            _myGame = myGame;
            _score = score;
            _records = records;

            _restartGameText = System.Configuration.ConfigurationManager.AppSettings["RestartGameText"];

            _menuFont = myGame.Content.Load<SpriteFont>("MenuFont");
            _scoreFont = _myGame.Content.Load<SpriteFont>("ScoreFont");
            _recordFont = _myGame.Content.Load<SpriteFont>("ErrorFont");

            _restartGame = new Button(new Rectangle((int)(myGame.WindowResolution.X - _buttonWidth) / 2,
                (int)myGame.WindowResolution.Y - _buttonHeight - 10, _buttonWidth, _buttonHeight),
                _menuFont, _restartGameText);

            _restartGame.Click += (b, e) => _myGame.CurrentScene = new DifficultyScene(_myGame);
        }

        public void Update(GameTime gameTime)
        {
            _restartGame.CheckClick();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            int Y, i;
            string record;

            spriteBatch.DrawString(_scoreFont, _score.ToString(),
                new Vector2(_myGame.WindowResolution.X / 2, _scorePosition), Color.Black, 0f,
                new Vector2(_scoreFont.MeasureString(_score.ToString()).X / 2, 0), 1f, SpriteEffects.None, 0);

            Y = _scorePosition + (int)_scoreFont.MeasureString(_score.ToString()).Y + 20;
            i = 1;

            foreach (var r in _records)
            {
                record = "";
                record += i++ + ". ";
                record += r._name;

                spriteBatch.DrawString(_recordFont, record, new Vector2(50, Y), Color.Black);
                spriteBatch.DrawString(_recordFont, r._score.ToString(), new Vector2(340, Y), Color.Black);
                Y += (int)_recordFont.MeasureString(record).Y + 10;
            }

            _restartGame.Draw(spriteBatch, texture);
        }
    }
}
