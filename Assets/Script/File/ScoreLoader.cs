using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Megaton
{
    public static class ScoreLoader
    {
        public static string PathName = Path.Combine(GameVar.DataRootDir, "Setting", "score.json");

        /// <summary>
        /// 更改后的序列化形式
        /// </summary>
        [Serializable]
        private class FullChartScore
        {
            public string key;
            public ChartScore value;
        }

        /// <summary>
        /// 封装
        /// </summary>
        [Serializable]
        private class ScoreCollection
        {
            [SerializeField] public List<FullChartScore> scores = new();
        }

        /// <summary>
        /// 读取分数
        /// </summary>
        /// <returns>返回分数字典</returns>
        public static Dictionary<string, ChartScore> Path2Score()
        {   
            //读取文件
            ScoreCollection scores;
            using (StreamReader sr = new StreamReader(PathName))
            {
                scores = JsonUtility.FromJson<ScoreCollection>(sr.ReadToEnd());
            }
            
            //转换实际形式
            Dictionary<string,ChartScore> dict = new Dictionary<string,ChartScore>();
            foreach(var score in scores.scores) dict.Add(score.key, score.value);
            return dict;
        }

        public static void SaveScore()
        {
            //转换序列化形式
            ScoreCollection scores = new();
            foreach(var score in GameVar.ChartScores)
            {
                scores.scores.Add(new FullChartScore() { key = score.Key, value = score.Value });
            }

            Debug.Log($"{scores.scores.Count} play logged!");
            //写入文件
            using (StreamWriter sw = new StreamWriter(PathName))
            {
                sw.Write(JsonUtility.ToJson(scores));
            }
        }
    }
}