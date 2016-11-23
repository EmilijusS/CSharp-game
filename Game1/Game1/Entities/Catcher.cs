using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    [Serializable]
    class Catcher
    {
        Rectangle _rectangle;
        public Rectangle Rectangle { get { return _rectangle; } set { _rectangle = value; } }
        private double realX;
        private readonly Color[] _colors;
        private readonly int _speed = 5;
        private readonly int _sections;
        private readonly int _screenWidth;

        public Catcher(Color[] colors, int screenHeight, int screenWidth)
        {
            _colors = colors;
            _sections = _colors.Length;
            _rectangle = new Rectangle(0, screenHeight * 19 / 20, screenWidth / _sections, screenHeight / 20);
            realX = _rectangle.X;
            this._screenWidth = screenWidth;
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Rectangle rectangle = new Rectangle(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height);

            for(int i = 0; i < _sections; ++i)
            {
                spriteBatch.Draw(texture, rectangle, _colors[i]);

                if (rectangle.Right >= _screenWidth)
                {
                    rectangle.X -= _screenWidth;
                    spriteBatch.Draw(texture, rectangle, _colors[i]);
                }

                rectangle.X += rectangle.Width;
            }
        }

        public void MoveRight(GameTime gameTime)
        {
            realX += _speed * gameTime.ElapsedGameTime.TotalSeconds * 60;

            if (realX >= _screenWidth)
                realX -= _screenWidth;

            _rectangle.X = (int)realX;
        }

        public void MoveLeft(GameTime gameTime)
        {
            realX -= _speed * gameTime.ElapsedGameTime.TotalSeconds * 60;

            if (realX < 0)
                realX += _screenWidth;

            _rectangle.X = (int)realX;
        }

        public Color GetColorAtX(int x)
        {
            if (x < Rectangle.X)
                x += _screenWidth;

            // Magic
            return _colors[(x - Rectangle.X)/(Rectangle.Width)];
        }

        public void Reset()
        {
            _rectangle.X = 0;
            realX = 0;
        }


    }
}
