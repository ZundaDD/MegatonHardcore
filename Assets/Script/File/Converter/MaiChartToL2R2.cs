using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Megaton.Chart
{
    /// <summary>
    /// 将majdata格式的谱面转化为l2r2模式的谱面
    /// </summary>
    public static class MaiChartToL2R2
    {
        private static int Division = 4;
        private static int BPM = 60;
        private static bool InChart = false;
        private static string Chart = string.Empty;

        private static Dictionary<int, int> RailReflection = 
            new() { { 3, 1 }, { 4, 2 }, { 5, 3 }, { 6, 4 } };
        private static Dictionary<string, string> KeyReflection =
            new() { { "title", "Title" }, { "artist", "Composer" }, { "wholebpm", "BPM" } };

        /// <summary>
        /// 流水化转换
        /// </summary>
        public static void DefaultProcessing()
        {
            string chartPath = "D:\\Charts\\L2R2";
            string savePath = Path.Combine(Application.persistentDataPath, "Data", "Charts");
            if (Directory.Exists(chartPath))
            {
                foreach (var chart in Directory.EnumerateDirectories(chartPath))
                {
                    string pack = chart.Split('\\')[^1].Split("_")[0];
                    string name = chart.Split('\\')[^1].Split('_')[1];
                    Directory.CreateDirectory(Path.Combine(savePath, pack));
                    Converting(chart, Path.Combine(savePath, pack, name));
                }
            }
            
        }

        /// <summary>
        /// 处理转换
        /// </summary>
        /// <param name="maiPath">majdata谱面文件夹</param>
        /// <param name="savePath">l2r2谱面文件夹</param>
        private static void Converting(string maiPath, string savePath)
        {
            Chart = string.Empty;
            InChart = false;

            Directory.CreateDirectory(savePath);
            //复制封面和音乐
            File.Copy(Path.Combine(maiPath, "bg.png"), Path.Combine(savePath, "cover.png"), true);
            File.Copy(Path.Combine(maiPath, "track.mp3"), Path.Combine(savePath, "music.mp3"), true);

            //谱面处理
            string line = "";
            using (StreamReader sr = new(Path.Combine(maiPath, "maidata.txt")))
            using (StreamWriter sw = File.CreateText(Path.Combine(savePath, "chart.txt")))
            {
                sw.WriteLine("PlayMode=L2R2"); 
                sw.WriteLine("Level=10.0");
                while ((line = sr.ReadLine()) != null) Parsing(sw, line);
                sw.WriteLine("ST");
                WriteChart(sw, Chart);
                sw.WriteLine("ED");
            }
        }

        /// <summary>
        /// 逐行解析文件
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="line"></param>
        private static void Parsing(StreamWriter sw, string line)
        {
            if (line == string.Empty) return;
            //config行
            if (line[0] == '&')
            {
                //预处理字符串
                string[] token = line.Split('=');
                if (token.Length != 2) return;
                
                token[0] = token[0].Remove(0, 1);
                token[1] = token[1].Replace("[DX]", "");
                token[1] = token[1].Replace("[SD]", "");
                
                //判断谱面内容
                if (token[0] == "inote_5")
                {
                    InChart = true;
                    Chart += token[1];
                }

                //写入config
                if (!KeyReflection.ContainsKey(token[0])) return;
                sw.WriteLine($"{KeyReflection[token[0]]}={token[1]}");
            }

            if (InChart)
            {
                if (line == "E")
                {
                    InChart = false;
                }
                else Chart += line;
            }
        }

        private static void WriteChart(StreamWriter sw, string text)
        {
            //去掉空格
            Chart = Chart.Replace(" ", "");

            //提取所有bpm片段
            var bpms = GetSplit(Chart, @"(\([0-9]+\))");

            //处理每一个bpm片段，无变bpm情况下只有一个，时值与标值无关
            foreach(var bpm in bpms)
            {
                if (bpm.content == "") continue;
                BPM = bpm.number;

                //提取所有切分音片段
                var divides = GetSplit(bpm.content, @"(\{[0-9]+\})");

                //处理每一个divide片段
                foreach(var divide in divides)
                {
                    if (divide.content == "") continue;
                    Division = divide.number;
                    sw.Write(Division);

                    var beats = GetBeat(divide.content, @"(?<=^|,)([^,]*)(?=,|$)");
                    
                    foreach(var beat in beats)
                    {
                        List<string> each = beat.Split('/').ToList();
                        
                        //写入检测
                        for (int i = 0; i < each.Count; i++)
                        {
                            //Debug.Log($"Mai: {each[i]}");
                            each[i] = Mai2L2R2(each[i]);
                            //Debug.Log($"L2R2: {each[i]}");
                        }
                        each.RemoveAll(x => x == ",");
                        if (each.Count > 0)
                        {
                            for(int i = 0;i < each.Count;++i)
                            {
                                if (i > 0) sw.Write($"/{each[i]}");
                                else sw.Write($" {each[i]}");
                            }
                        }
                        else sw.Write(" ,");

                    }

                    sw.WriteLine();
                }
            }
        
        }

        /// <summary>
        /// 将note的形式从mai转移到l2r2
        /// </summary>
        /// <param name="ele">mai note</param>
        /// <returns>l2r2 note</returns>
        private static string Mai2L2R2(string ele)
        {
            try
            {
                int rail = RailReflection[ele[0] - '0'];
                //按长度排列，不然会抛出异常
                if (ele.Length == 1) return $"{rail}T";
                if (ele[1] == 'x') return $"{rail}C";
                var match = Regex.Match(ele, @"h\[(\d+):(\d+)\]");
                if(match.Success)
                {
                    int div = int.Parse(match.Groups[1].Value);
                    int len = int.Parse(match.Groups[2].Value);
                    return $"{rail}H{4 / div * len}";
                }
            }
            catch
            {
                //Debug.Log("Exception");
                return ",";
            }
            return ",";
        }

        private static List<string> GetBeat(string input,string pattern)
        {
            var split = Regex.Matches(input, pattern);
            List<string> splits = new();
            foreach (Match match in split)
            {
                splits.Add(match.Groups[1].Value);
            }
            splits.RemoveAt(splits.Count - 1);
            return splits;
        }

        private static List<(int number,string content)> GetSplit(string input,string pattern)
        {
            var split = Regex.Split(input, pattern);
            List<(int number, string content)> splits = new();
            for (int i = 1; i < split.Length; i += 2)
            {
                int divide = 
                    int.Parse(Regex.Match(split[i], @"[0-9]+").Value);
                if (i + 1 < split.Length) splits.Add((divide, split[i + 1]));
            }
            return splits;
        }
    }
}