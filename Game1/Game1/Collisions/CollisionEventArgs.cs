using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class CollisionEventArgs : EventArgs
    {
        public Vector2 Coordinates { get; }
        public Block Block { get; }

        public CollisionEventArgs(Vector2 coordinates, Block block)
        {
            Coordinates = coordinates;
            Block = block;
        }
    }
}
