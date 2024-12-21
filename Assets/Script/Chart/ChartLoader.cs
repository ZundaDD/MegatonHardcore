using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 谱面加载器
    /// </summary>
    public class ChartLoader
    {
        public string ChartPath;
        public string ChartName = "chart.txt";
        /// <summary>
        /// 构造一个谱面加载器
        /// </summary>
        /// <param name="path">从Data文件到谱面文件夹的路径</param>
        public ChartLoader(string path)
        {
            string rootPath = Path.Combine(Application.dataPath, "..");
            rootPath = Path.Combine(rootPath, "Data");
            ChartPath = Path.Combine(rootPath, path, ChartName);
            Debug.Log(ChartPath);
        }

        /// <summary>
        /// 给定路径加载谱面
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns>对应谱面</returns>
        public Chart Path2Chart()
        {
            Chart chart = new Chart();
            chart.Info = Path2Info();

            using (StreamReader sr = new StreamReader(ChartPath))
            {

                string line = sr.ReadLine();
                while (!sr.EndOfStream && line != "ST") line = sr.ReadLine();
                while (!sr.EndOfStream && line != "ED")
                {

                    line = sr.ReadLine();
                }

            }
            return chart;
        }

        /// <summary>
        /// 给定路径加载谱面信息
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns>对应的谱面信息</returns>
        public ChartInfo Path2Info()
        {
            ChartInfo info = new ChartInfo();
            using (StreamReader sr = new StreamReader(ChartPath))
            {

                string line = sr.ReadLine();
                while (sr.EndOfStream || line == "ST")
                {
                    string[] kvpair = line.Split('=');
                    if (kvpair.Length == 2 && kvpair[0] != string.Empty)
                    {
                        switch (kvpair[0])
                        {
                            case "Title":
                                info.Title = kvpair[1];
                                break;
                            case "Composer":
                                info.Composer = kvpair[1];
                                break;
                            case "BPM":
                                info.BPM = int.Parse(kvpair[1]);
                                break;
                        }
                    }
                    line = sr.ReadLine();
                }

            }
            return info;
        }
    }
}