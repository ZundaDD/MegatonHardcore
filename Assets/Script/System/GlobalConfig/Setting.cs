using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 游戏设置，玩家可以手动修改
    /// </summary>
    public class Setting
    {
        #region 生命周期
        static Setting ins;
        static string store_path = Path.Combine(Application.dataPath, "Environment", "Setting.json");

        public static Setting Ins
        {
            get => (ins == null) ? ins = ReadFromFile() : ins;
        }
        #endregion

        #region IO
        static Setting ReadFromFile()
        {
            Setting instance = new Setting();
            if (Directory.Exists(store_path))
            {
                using (StreamReader sr = new StreamReader(store_path))
                {
                    instance = JsonUtility.FromJson<Setting>(sr.ReadToEnd());
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