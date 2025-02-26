using Megaton;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 游戏数据，不包含设置，那是玩家可以调节的部分
    /// GameData的数据由游戏机制给出，玩家无法手动修改
    /// </summary>
    public class GameData
    {
        

        #region 生命周期
        static GameData ins;
        static string store_path = Path.Combine(Application.dataPath, "Environment", "GameVar.json");

        public static GameData Ins
        {
            get => (ins == null) ? ins = ReadFromFile() : ins;
        }
        #endregion

        #region IO
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
        #endregion
    }
}