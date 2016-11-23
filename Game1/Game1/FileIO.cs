using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    static class FileIO
    {
        public static void WriteSave(GameObjects gameObjects)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(GameObjects));

            using (Stream stream = new FileStream("SavedGame.txt", FileMode.Create, FileAccess.Write))
            {
                serializer.WriteObject(stream, gameObjects);
            }
        }

        public static GameObjects ReadSave()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(GameObjects));

            using (Stream stream = new FileStream("SavedGame.txt", FileMode.Open, FileAccess.Read))
            {
                return (GameObjects) serializer.ReadObject(stream);
            }
        }
    }
}
