using Megaton.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 游戏设置，玩家可以手动修改
    /// </summary>
    [SerializeField]
    public class Setting
    {
        #region 时间配置
        public RangeVarible Speed = RangeVarible.GetRangeVarible(0.5f, 10f, 5, 0.5f, 1);
        public RangeVarible Music_Offset = RangeVarible.GetRangeVarible(-100, 100, 0, 1, 0);
        public RangeVarible Input_Offset = RangeVarible.GetRangeVarible(-100, 100, 0, 1, 0);

        #endregion

        #region 场景配置
        public RangeVarible Board_Distance = RangeVarible.GetRangeVarible(-8f, 8f, 0, 1f, 0);
        #endregion
        
        #region 游玩显示配置
        public BoolVarible Distinguish_Critical = BoolVarible.GetBoolVarible(true);
        public BoolVarible Show_Fast_Late = BoolVarible.GetBoolVarible(true);
        public RangeVarible Judge_Feedback_Height = RangeVarible.GetRangeVarible(-3f, 3f, 0, 1f, 0);
        public DiscreteVarible<ScoreType> Float_Score_Type = new(new()
        {   new("不显示",ScoreType.None),
            new("101(-)",ScoreType.Minus101),
            new("100(-)",ScoreType.Minus100),
            new("距EX+",ScoreType.Gap1008),
            new("距EX",ScoreType.Gap1005),
            new("距FUL",ScoreType.Gap1000)
          });
        #endregion

        #region 音频设置
        public RangeVarible Effect_Volume = RangeVarible.GetRangeVarible(0, 120, 100, 10, 0);
        public RangeVarible Music_Volume = RangeVarible.GetRangeVarible(0, 120, 100, 10, 0);
        #endregion

        #region 不用管
        #region 生命周期
        private static Setting ins;
        private static string store_path = Path.Combine(GameVar.DataRootDir, "Setting", "setting.json");

        public static Setting Ins
        {
            get => (ins == null) ? ins = ReadFromFile() : ins;
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        public static void Reset()
        {
            ins = new Setting();
            SaveToFile();
        }
        #endregion

        #region IO
        private static Setting ReadFromFile()
        {
            Setting instance = new Setting();
            if (File.Exists(store_path))
            {
                using (StreamReader sr = new StreamReader(store_path))
                {
                    instance = JsonUtility.FromJson<Setting>(sr.ReadToEnd());
                    sr.Close();
                }

            }
            return instance;
        }

        public static void SaveToFile()
        {
            using (StreamWriter sr = new(store_path))
            {
                sr.Write(JsonUtility.ToJson(Ins, true));
                sr.Close();
            }
        }
        #endregion
        #endregion
    }
}