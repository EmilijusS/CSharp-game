using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    class EasyGameFactory : IGameFactory
    {
        private static readonly Random Random = new Random();
        private readonly Color[] _colors = new Color[3] { Color.Red, Color.Green, Color.Blue};
        private readonly Vector2 _windowResolution;
        public double TimeSinceLastBlock { get; private set; }
        public int BlockInterval { get; private set; }

        public Difficulties Difficulty { get; } = Difficulties.Easy;

        public EasyGameFactory(Vector2 windowResolution) :this(windowResolution, 0, 1300){ }

        public EasyGameFactory(Vector2 windowResolution, double timeSinceLastBlock, int blockInterval)
        {
            _windowResolution = windowResolution;
            TimeSinceLastBlock = timeSinceLastBlock;
            BlockInterval = blockInterval;
        }

        public void AddBlock(List<Block> blocks, GameTime gameTime)
        {
            TimeSinceLastBlock += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (TimeSinceLastBlock < BlockInterval)
            {
                return;
            }

            blocks.Add(new Block(_colors[Random.Next(0, _colors.Length)],
                Random.Next(10, (int)_windowResolution.X - 20)));

            TimeSinceLastBlock = 0;
        }

        public void BlockHit(CollisionEventArgs e)
        {
            BlockInterval -= 2;
        }

        public Catcher GetCatcher()
        {
            return new Catcher(_colors, (int)_windowResolution.Y, (int)_windowResolution.X);
        }

    }
}

