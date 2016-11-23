using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public interface IScoreRepository
    {
        void Write(List<Record> records, Difficulties difficulty);
        List<Record> Read(Difficulties difficulty);
    }
}
