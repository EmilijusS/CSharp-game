using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class NewRecordScene : IScene
    {
        private MyGame _myGame;
        private int _score;
        private List<Record> _records;
        private string _newRecordText;
        private string _enterNameText;
        private SpriteFont _scoreFont;
        private SpriteFont _menuFont;
        private string _name;
        private readonly int _space = 10;
        private KeyboardState _previous;
        private Difficulties _difficulty;


        public NewRecordScene(MyGame myGame, int score, Difficulties difficulty)
        {
            _myGame = myGame;
            _score = score;
            _difficulty = difficulty;
            _records = difficulty == Difficulties.Easy ? _myGame.EasyRecords : _myGame.HardRecords;

            _scoreFont = _myGame.Content.Load<SpriteFont>("ScoreFont");
            _menuFont = _myGame.Content.Load<SpriteFont>("MenuFont");
            _newRecordText = System.Configuration.ConfigurationManager.AppSettings["NewRecordText"];
            _enterNameText = System.Configuration.ConfigurationManager.AppSettings["EnterNameText"];
            _name = "";
            _previous = Keyboard.GetState();

            if (_records.Count == 10 && _score <= _records[9]._score)
                _myGame.CurrentScene = new EndMenuScene(_myGame, _score, _records);
            else
                _myGame.CurrentScene = this;

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState current = Keyboard.GetState();

            if (current.IsKeyDown(Keys.Enter) && _previous.IsKeyUp(Keys.Enter))
                AddNewRecord();
            else if (current.IsKeyDown(Keys.Back) && _previous.IsKeyUp(Keys.Back) && _name.Length > 0)
                _name = _name.Substring(0, _name.Length - 1);
            else if(_name.Length < 10)
            {
                foreach (var k in current.GetPressedKeys())
                {
                    Console.WriteLine(k.ToString());

                    if (_previous.IsKeyUp(k) && k.ToString()[0] >= 'A' && k.ToString()[0] <= 'Z' && k.ToString().Length == 1)
                    {
                        _name += k.ToString();
                    }
                }
            }

            _previous = current;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            int Y = 0;

            spriteBatch.DrawString(_scoreFont, _score.ToString(),
                new Vector2(_myGame.WindowResolution.X / 2, _space), Color.Black, 0f,
                new Vector2(_scoreFont.MeasureString(_score.ToString()).X / 2, 0), 1f, SpriteEffects.None, 0);

            Y += (int) _scoreFont.MeasureString(_score.ToString()).Y + 20;

            spriteBatch.DrawString(_menuFont, _newRecordText,
                new Vector2(_myGame.WindowResolution.X / 2, Y), Color.Black, 0f,
                new Vector2(_menuFont.MeasureString(_newRecordText).X / 2, 0), 1f, SpriteEffects.None, 0);

            Y += (int)_menuFont.MeasureString(_newRecordText).Y + 20;

            spriteBatch.DrawString(_menuFont, _enterNameText,
                new Vector2(_myGame.WindowResolution.X / 2,Y), Color.Black, 0f,
                new Vector2(_menuFont.MeasureString(_enterNameText).X / 2, 0), 1f, SpriteEffects.None, 0);

            Y += (int)_menuFont.MeasureString(_enterNameText).Y + 20;

            spriteBatch.DrawString(_menuFont, _name,
                new Vector2(_myGame.WindowResolution.X / 2, Y), Color.Black, 0f,
                new Vector2(_menuFont.MeasureString(_name).X / 2, 0), 1f, SpriteEffects.None, 0);

        }

        private void AddNewRecord()
        {
            if(_records.Count == 10)
                _records.RemoveAt(9);
            _records.Add(new Record(_name, _score));
            _records.Sort((a, b) => b._score.CompareTo(a._score));
            _myGame.ScoreRepository.Write(_records, _difficulty);
            _myGame.CurrentScene = new EndMenuScene(_myGame, _score, _records);
        }
    }
}
