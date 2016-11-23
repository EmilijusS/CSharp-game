using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    [Serializable]
    public struct Record
    {
        public readonly string _name;
        public readonly int _score;

        public Record(string name, int score)
        {
            _name = name;
            _score = score;
        }
    }
}
