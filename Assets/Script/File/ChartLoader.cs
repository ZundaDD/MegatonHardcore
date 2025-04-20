using Megaton.Abstract;
using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 谱面加载器
    /// </summary>
    public static class ChartLoader
    {
        public static string ChartName = "chart.txt";

        public static ChartPlay Path2Play(string path,ChartInfo info)
        {
            GameVar.PlayMode = Mode.GetMode(info.PlayMode);

            string chartPath = Path.Combine(path, ChartName);
            ChartPlay chart = new ChartPlay();
            chart.Info = info;
            //chart.Music = MusicLoader.Path2Clip(path);

            using (StreamReader sr = new StreamReader(chartPath))
            {

                string line = sr.ReadLine();
                while (!sr.EndOfStream && line != "ST") line = sr.ReadLine();
                line = sr.ReadLine();
                while (!sr.EndOfStream && line != "ED")
                {
                    chart.ParseCommand(line);
                    line = sr.ReadLine();
                }

            }
            return chart;
        }

        public static ChartInfo Path2Info(string path)
        {
            string chartPath = Path.Combine(path, ChartName);
            ChartInfo info = new ChartInfo();
            
            using (StreamReader sr = new StreamReader(chartPath))
            {

                string line = sr.ReadLine();
                while (!sr.EndOfStream && line != "ST")
                {
                    string[] kvpair = line.Split('=');
                    if (kvpair.Length == 2 && kvpair[0] != string.Empty)
                    {
                        info.SetProperty(kvpair[0], kvpair[1]);
                    }
                    line = sr.ReadLine();
                }
                info.Folder = path.Split('\\')[^1];
                info.Pack = path.Split('\\')[^2];
                info.RootDir = path;
                if (GameVar.ChartScores.ContainsKey($"{info.Pack}/{info.Folder}"))
                    info.Score = GameVar.ChartScores[$"{info.Pack}/{info.Folder}"];
            }
            if (Mode.ValidMode(info.PlayMode)) return info;
            else return null;
        }
    }
}