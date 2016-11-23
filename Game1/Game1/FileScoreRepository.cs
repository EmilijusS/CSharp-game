using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class FileScoreRepository : IScoreRepository
    {
        public void Write(List<Record> records, Difficulties difficulty)
        {
            string filename = difficulty == Difficulties.Easy ? "EasyRecords.txt" : "HardRecords.txt";

            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Record>));

            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                serializer.WriteObject(stream, records);
            }
        }

        public List<Record> Read(Difficulties difficulty)
        {
            string filename = difficulty == Difficulties.Easy ? "EasyRecords.txt" : "HardRecords.txt";

            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Record>));

            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return (List<Record>)serializer.ReadObject(stream);
            }
        }
    }
}
