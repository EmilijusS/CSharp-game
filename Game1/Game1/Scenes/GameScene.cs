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
    class GameScene : IScene
    {
        private MyGame _myGame;
        private List<Block> _blocks;
        private Catcher _catcher;
        private IGameFactory _gameFactory;
        private CollisionDetection _collisionDetection;
        private int _score;
        private KeyboardState _previousState;
        private SpriteFont _scoreFont;
        private readonly int _scorePosition = 10;

        public GameScene(MyGame myGame, IGameFactory gameFactory)
            : this(myGame, gameFactory, new List<Block>(), gameFactory.GetCatcher(), 0){}

        public GameScene(MyGame myGame, IGameFactory gameFactory, List<Block> blocks, Catcher catcher, int score)
        {
            _blocks = blocks;
            _catcher = catcher;
            _score = score;
            _myGame = myGame;

            _gameFactory = gameFactory;

            _collisionDetection = new CollisionDetection();

            _collisionDetection.Hit += delegate (CollisionEventArgs e)
            {
                ++_score;
                _blocks.Remove(e.Block);
            };

            _collisionDetection.Hit += gameFactory.BlockHit;

            _collisionDetection.Miss += e => new NewRecordScene(_myGame, _score, _gameFactory.Difficulty);

            _scoreFont = _myGame.Content.Load<SpriteFont>("ScoreFont");

            _previousState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _gameFactory.AddBlock(_blocks, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _catcher.MoveLeft(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _catcher.MoveRight(gameTime);

            foreach (var b in _blocks)
                b.Update(gameTime);

            _collisionDetection.CheckCollision(_blocks, _catcher);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _previousState.IsKeyUp(Keys.Escape))
                _myGame.CurrentScene = new EscMenuScene(_myGame, this);

            if (Keyboard.GetState().IsKeyDown(Keys.F1) && _previousState.IsKeyUp(Keys.F1))
            {
                Task.Run(() => FileIO.WriteSave(new GameObjects(_blocks, _catcher, _score, _gameFactory.TimeSinceLastBlock, _gameFactory.BlockInterval, _gameFactory.Difficulty)));
            }

            _previousState = Keyboard.GetState();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            _catcher.Draw(spriteBatch, texture);

            foreach (var b in _blocks)
                b.Draw(spriteBatch, texture);

            spriteBatch.DrawString(_scoreFont, _score.ToString(),
                new Vector2(_myGame.WindowResolution.X / 2, _scorePosition), Color.Black, 0f,
                new Vector2(_scoreFont.MeasureString(_score.ToString()).X / 2, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
