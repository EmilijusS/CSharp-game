using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    interface IGameFactory
    {
        double TimeSinceLastBlock { get; }
        int BlockInterval { get; }
        Difficulties Difficulty { get; }

        void AddBlock(List<Block> blocks, GameTime gameTime);
        Catcher GetCatcher();
        void BlockHit(CollisionEventArgs e);
    }
}
