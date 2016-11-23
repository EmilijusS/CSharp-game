using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    [Serializable]
    struct GameObjects
    {
        public readonly List<Block> _blocks;
        public readonly Catcher _catcher;
        public readonly int _score;
        public readonly double _timeSinceLastBlock;
        public readonly int _blockInterval;
        public readonly Difficulties _difficulty;

        public GameObjects(List<Block> blocks, Catcher catcher, int score, double timeSinceLastBlock, int blockInterval, Difficulties difficulty)
        {
            _blocks = blocks;
            _catcher = catcher;
            _score = score;
            _timeSinceLastBlock = timeSinceLastBlock;
            _blockInterval = blockInterval;
            _difficulty = difficulty;
        }
    }
}
