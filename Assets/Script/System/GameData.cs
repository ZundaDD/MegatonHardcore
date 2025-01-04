using Megaton;
using System.IO;
using UnityEngine;

namespace Megaton
{
    public class GameData
    {
        static GameData ins;
        static string store_path = Path.Combine(Application.dataPath, "Setting", "GameVar.json");

        public static GameData Ins
        {
            get => (ins == null) ? ins = ReadFromFile() : ins;
        }

        static GameData ReadFromFile()
        {
            GameData instance = new GameData();
            if (Directory.Exists(store_path))
            {
                using (StreamReader sr = new StreamReader(store_path))
                {
                    instance = JsonUtility.FromJson<GameData>(sr.ReadToEnd());
                }

            }
            return instance;
        }

        public static void SaveToFile()
        {
            if (!Directory.Exists(store_path)) Directory.CreateDirectory(store_path);
            using (StreamWriter sr = new(store_path))
            {
                sr.Write(JsonUtility.ToJson(Ins));
            }
        }

    }
}