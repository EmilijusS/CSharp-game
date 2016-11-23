using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    [Serializable]
    public class Block
    {
        public Color Color { get; set; }
        private Rectangle _rectangle;
        public Rectangle Rectangle { get { return _rectangle; } set { _rectangle = value; } }
        private double realY;
        private readonly int _speed = 3;
        private readonly int _height = 10;
        private readonly int _width = 10;

        public Block(Color color, int x)
        {
            Color = color;
            Rectangle = new Rectangle(x, -30, _height, _width);
            realY = Rectangle.Y;
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Rectangle, Color);
        }

        public void Update(GameTime gameTime)
        {
            realY += _speed*gameTime.ElapsedGameTime.TotalSeconds*60;
            _rectangle.Y = (int)realY;
        }
    }
}
