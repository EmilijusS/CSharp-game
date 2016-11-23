using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public delegate void CollisionEventHandler(CollisionEventArgs e);

    class CollisionDetection
    {
        public event CollisionEventHandler Hit;
        public event CollisionEventHandler Miss;

        public void CheckCollision(List<Block> blocks, Catcher catcher)
        {
            if (blocks.Count == 0)
                return;

            // Only checks the lowest block
            Block b = blocks.First();

            if (b.Rectangle.Bottom > catcher.Rectangle.Top)
            {
                // Checks if whole block hits the right color
                if (b.Color.Equals(catcher.GetColorAtX(b.Rectangle.Left)) &&
                    b.Color.Equals(catcher.GetColorAtX(b.Rectangle.Right)))
                {
                    OnHit(new Vector2(b.Rectangle.Center.X, b.Rectangle.Bottom), b);
                }
                else
                {
                    OnMiss(new Vector2(b.Rectangle.Center.X, b.Rectangle.Bottom), b);
                }
            }
        }

        protected void OnHit(Vector2 coordinates, Block block)
        {
            Hit?.Invoke(new CollisionEventArgs(coordinates, block));

        }

        protected void OnMiss(Vector2 coordinates, Block block)
        {
            Miss?.Invoke(new CollisionEventArgs(coordinates, block));
        }

    }
}
