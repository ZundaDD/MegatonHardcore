using UnityEngine;
using System;

namespace Megaton
{
    /// <summary>
    /// 谱面的信息，不包含游玩的部分
    /// </summary>
    [Serializable]
    public class ChartInfo
    {
        // 由路径得到的信息
        public string RootDir = "Null";
        public string Pack = "Null";
        public string Folder = "Null";
        public ChartScore Score = new ChartScore();

        // chart.txt文件中得到的信息
        public string Title = "Null";
        public string Composer = "Null";
        public float Level = 0;
        public string PlayMode = "";
        public int BPM = -50;

        public void SetProperty(string key, string value)
        {
            switch (key)
            {
                case "Level":
                    Level = float.Parse(value);
                    break;
                case "Title":
                    Title = value;
                    break;
                case "Composer":
                    Composer = value;
                    break;
                case "BPM":
                    BPM = int.Parse(value);
                    break;
                case "PlayMode":
                    PlayMode = value;
                    break;
            }
        }

        public string GetLevelString()
        {
            int round = (int)Level;
            float af = Level - round;
            return (af > .59f) ? round + "+" : round.ToString();
        }
    }
}