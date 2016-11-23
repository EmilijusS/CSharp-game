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
    class EscMenuScene : IScene
    {
        private MyGame _myGame;
        private IScene _gameScene;
        private SpriteFont _pauseTextFont;
        private KeyboardState _previousState;
        private readonly string _pauseText;

        public EscMenuScene(MyGame myGame, IScene gameScene)
        {
            _myGame = myGame;
            _gameScene = gameScene;
            _pauseTextFont = _myGame.Content.Load<SpriteFont>("ScoreFont");
            _previousState = Keyboard.GetState();
            _pauseText = System.Configuration.ConfigurationManager.AppSettings["PauseText"];
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _previousState.IsKeyUp(Keys.Escape))
                _myGame.CurrentScene = _gameScene;

            _previousState = Keyboard.GetState();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.DrawString(_pauseTextFont, _pauseText,
                        new Vector2(_myGame.WindowResolution.X / 2, _myGame.WindowResolution.Y / 2),
                        Color.Black, 0f,
                        _pauseTextFont.MeasureString(_pauseText) / 2, 1f, SpriteEffects.None, 0);
        }
    }
}
