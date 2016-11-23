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
    class Button
    {
        public EventHandler Click { get; set; }

        private Rectangle _rectangle;
        private Rectangle _outline;
        private string _text;
        private SpriteFont _font;
        private MouseState _lastMouseState;
        private bool clicked;

        public Button(Rectangle rectangle, SpriteFont font, string text)
        {
            _rectangle = rectangle;
            _font = font;
            _text = text;
            _outline = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 2, rectangle.Height + 2);
            _lastMouseState = Mouse.GetState();
            clicked = false;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (_rectangle.Contains(Mouse.GetState().Position))
            {
                spriteBatch.Draw(texture, _outline, Color.White);
                spriteBatch.Draw(texture, _rectangle, Color.Black);
                spriteBatch.DrawString(_font, _text, new Vector2(_rectangle.Center.X, _rectangle.Center.Y), Color.White, 0f, _font.MeasureString(_text) / 2, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, _outline, Color.Black);
                spriteBatch.Draw(texture, _rectangle, Color.White);
                spriteBatch.DrawString(_font, _text, new Vector2(_rectangle.Center.X, _rectangle.Center.Y), Color.Black, 0f, _font.MeasureString(_text) / 2, 1f, SpriteEffects.None, 0f);
            }
        }

        public void CheckClick()
        {
            if (_rectangle.Contains(Mouse.GetState().Position))
            {
                if (_lastMouseState.LeftButton == ButtonState.Released &&
                    Mouse.GetState().LeftButton == ButtonState.Pressed)
                    clicked = true;

                if(_lastMouseState.LeftButton == ButtonState.Pressed &&
                    Mouse.GetState().LeftButton == ButtonState.Released && clicked)
                    OnClick();
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                clicked = false;

            _lastMouseState = Mouse.GetState();
        }

        protected void OnClick()
        {
            Click?.Invoke(this, new EventArgs());
        }

    }
}
