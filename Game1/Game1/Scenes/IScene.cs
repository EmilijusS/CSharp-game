using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public interface IScene
    {

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture);
    }
}
