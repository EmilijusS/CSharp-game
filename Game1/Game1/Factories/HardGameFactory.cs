using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    class HardGameFactory : IGameFactory
    {
        private static readonly Random Random = new Random();
        private readonly Color[] _colors = new Color[4] { Color.Red, Color.Green, Color.Blue, Color.Orange };
        private readonly Vector2 _windowResolution;
        private const int EventProbability = 25;
        public double TimeSinceLastBlock { get; private set; }
        public int BlockInterval { get; private set; }

        public Difficulties Difficulty { get; } = Difficulties.Hard;


        public HardGameFactory(Vector2 windowResolution) :this(windowResolution, 0, 1000){}

        public HardGameFactory(Vector2 windowResolution, double timeSinceLastBlock, int blockInterval)
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

            int x, colorNumber = Random.Next(0, _colors.Length);
            Color color;
            Block b = new Block(_colors[colorNumber],
                Random.Next(10, (int)_windowResolution.X - 20));
            

            blocks.Add(b);

            if (Random.Next(0, 99) < EventProbability)
            {
                // This works
                if (b.Rectangle.X < _windowResolution.X - _windowResolution.X / _colors.Length - 10)
                {
                    x = (int)(b.Rectangle.X + _windowResolution .X/ _colors.Length - 30 + Random.Next(60));
                    color = _colors[(colorNumber + 1)%_colors.Length];
                }
                else
                {
                    x = (int)(b.Rectangle.X - _windowResolution.X / _colors.Length + 30 - Random.Next(60));
                    color = _colors[(colorNumber - 1 + _colors.Length) % _colors.Length];
                }

                blocks.Add(new Block(color, x));
            }

            TimeSinceLastBlock = 0;
        }

        public void BlockHit(CollisionEventArgs e)
        {
            BlockInterval -= 3;
        }

        public Catcher GetCatcher()
        {
            return new Catcher(_colors, (int)_windowResolution.Y, (int)_windowResolution.X);
        }

    }
}
