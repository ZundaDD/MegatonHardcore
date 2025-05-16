using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 将OSU谱面转化为通用谱面，最后保存到本地
    /// </summary>
    public class OSUConverter
    {
        /// <summary>
        /// 谱面文件的属性
        /// </summary>
        private class ChartFile
        {
            // 标志位
            public bool Valid = true;

            // info段
            public string CoverPath = "";
            public string AudioPath = "";
            public string Title = "";
            public string Artist = "";
            public int ID = -1;
            public int BPM = -1;
            public string Mode = "";
            public Dictionary<int, int> Keys = new();

            // note段
            public List<Note> Notes = new();

            /// <summary>
            /// 解析模式
            /// </summary>
            /// <returns>模式是否有效</returns>
            public bool ParseMode()
            {
                switch (Keys.Count)
                {
                    case 4:
                        Mode = "L2R2";
                        return true;
                }
                return false;
            }

            [Serializable]
            public struct Note
            {
                public int Time;
                public int Rail;
                public string End;

                public Note(int time,int rail, string end)
                {
                    Time = time;
                    Rail = rail;
                    End = end;
                }
            }

            /// <summary>
            /// 文件配置是否有效
            /// </summary>

            public bool IfValid() => 
                ParseMode() && 
                CoverPath != "" && 
                AudioPath != "" && 
                Title != "" && 
                Artist != "" &&
                ID != -1 &&
                BPM != -1;
        }

        private enum Section
        {
            NotIn,
            Undefined,
            General,
            Metadata,
            Events,
            TimePoints,
            HitObjects
        }

        /// <summary>
        /// 将压缩的谱面包转化到目标分类的文件夹下的谱面
        /// </summary>
        /// <param name="src">压缩包路径</param>
        /// <param name="dst">目标分类文件夹，即OSU</param>
        public static async UniTask Path2Path(string src,string dst)
        {
            if(!File.Exists(src))
            {
                Debug.LogError($"Source file does not exist: {src}");
                return;
            }

            //先解压缩
            string tempDirectory = src.Replace(".osz", "");
            Directory.CreateDirectory(tempDirectory);
            ZipFile.ExtractToDirectory(src, tempDirectory, true);

            File.Delete(src);

            //解析不同难度的谱面文件
            foreach(var file in Directory.GetFiles(tempDirectory, "*.osu"))
                await ParseOneDifficulty(file, dst);

            //生成完谱面后删除临时文件
            Directory.Delete(tempDirectory, true);
        }

        /// <summary>
        /// 解析谱面的一个难度
        /// </summary>
        /// <param name="filePath">谱面文件路径</param>
        /// <param name="dstPath">目标分类路径</param>
        async private static UniTask ParseOneDifficulty(string filePath,string dstPath)
        {
            //解析是否成功
            ChartFile file = new();

            //从文件中读取信息填入file
            using (StreamReader sr = new(filePath))
            {
                string line;

                //检验文件格式版本
                line = sr.ReadLine();
                if (!line.EndsWith("v14")) return;

                Section curSection = Section.NotIn;
                while ((line = sr.ReadLine()) != null)
                {
                    //跳过空行，退出当前块
                    if (curSection != Section.NotIn && line == string.Empty)
                    {
                        curSection = Section.NotIn;
                        continue;
                    }

                    //判断是否为节头
                    if (curSection == Section.NotIn)
                    {
                        var match = Regex.Match(line, @"\[(.*?)\]");
                        if (match.Success)
                        {
                            switch (match.Groups[1].Value)
                            {
                                case "General":
                                    curSection = Section.General;
                                    break;
                                case "Metadata":
                                    curSection = Section.Metadata;
                                    break;
                                case "TimingPoints":
                                    curSection = Section.TimePoints;
                                    break;
                                case "Events":
                                    curSection = Section.Events;
                                    break;
                                case "HitObjects":
                                    curSection = Section.HitObjects;
                                    break;
                            }
                        }
                    }
                    //如果不是节头且不在节中
                    else
                    {
                        switch (curSection)
                        {
                            case Section.General:
                                ParseGeneral(line, file);
                                break;
                            case Section.Metadata:
                                ParseMetadata(line, file);
                                break;
                            case Section.TimePoints:
                                ParseTimePoints(line, file);
                                break;
                            case Section.Events:
                                ParseEvents(line, file);
                                break;
                            case Section.HitObjects:
                                ParseHitObjects(line, file);
                                break;
                        }
                    }
                }
            }

            //验证谱面有效性
            if (!file.IfValid()) 
            { 
                Debug.Log($"Invalid File At {filePath}"); 
                return; 
            }
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(filePath), file.CoverPath))) 
            {
                Debug.Log($"Cover Not Found At {file.CoverPath}");
                return;
            }
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(filePath), file.AudioPath)))
            {
                Debug.Log($"Audio Not Found At {file.AudioPath}");
                return;
            }

            //有效则进行构建
            await SetDiffcultyFolder(
                Path.GetDirectoryName(filePath),
                Path.Combine(dstPath, file.ID.ToString()),
                file);
        }

        #region 解析每一个节
        private static void ParseGeneral(string line, ChartFile file)
        {
            string[] token = line.Split(':');
            if (token.Length == 2 && token[0] == "AudioFilename")
            {
                file.AudioPath = token[1].Trim();
            }
            else if (token.Length == 2 && token[0] == "Mode" && token[1].Trim() != "3")
            {
                file.Valid = false;
            }
        }

        private static void ParseTimePoints(string line, ChartFile file)
        {
            if (file.BPM != -1) return;

            string[] token = line.Split(',');
            if (token.Length > 2)
            {
                float beat = float.Parse(token[1]);
                file.BPM = (int)(60000 / beat);
            }
        }

        private static void ParseEvents(string line, ChartFile file)
        {
            if (file.CoverPath != "") return;
            string[] token = line.Split(',');
            if (token.Length > 2)
            {
                file.CoverPath = token[2].Substring(1, token[2].Length - 2);
            }
        }

        private static void ParseMetadata(string line, ChartFile file)
        {
            string[] token = line.Split(':');
            if (token.Length == 2)
            {
                switch (token[0])
                {
                    case "Title":
                        file.Title = token[1];
                        break;
                    case "Artist":
                        file.Artist = token[1];
                        break;
                    case "BeatmapID":
                        file.ID = int.Parse(token[1]);
                        break;
                }

            }
        }

        private static void ParseHitObjects(string line, ChartFile file)
        {
            string[] token = line.Split(',');
            if (token.Length != 6) return;

            //解析音符
            try
            {
                token[5] = token[5].Split(':')[0];
                int x = int.Parse(token[0]);
                
                int time = int.Parse(token[2]);
                int type = int.Parse(token[3]);
                int endtime = int.Parse(token[5]);

                file.Keys[x] = 1;

                //128(1<<7)表示Hold
                if (type == 128) file.Notes.Add(new(time, x, $"H${endtime - time}"));
                else file.Notes.Add(new(time, x, "T"));
            }
            catch { return; }
        }

        #endregion
        
        /// <summary>
        /// 构建一个难度的谱面
        /// </summary>
        /// <param name="srcPath">源谱面文件夹</param>
        /// <param name="dstPath">目标谱面文件夹</param>
        /// <param name="fileContent">谱面内容</param>
        private static async UniTask SetDiffcultyFolder(string srcPath, string dstPath, ChartFile fileContent)
        {
            //创建目标谱面文件夹
            if (Directory.Exists(dstPath)) Directory.Delete(dstPath, true);
            Directory.CreateDirectory(dstPath);

            await ResizeCover(Path.Combine(srcPath, fileContent.CoverPath));
            //复制封面音频
            File.Copy(Path.Combine(srcPath, fileContent.CoverPath), Path.Combine(dstPath, "cover.png"));
            File.Copy(Path.Combine(srcPath, fileContent.AudioPath), Path.Combine(dstPath, $"music.{fileContent.AudioPath.Split(".")[1]}"));

            //对轨道进行排序反向定位
            var ls = fileContent.Keys.Keys.ToList();
            ls.Sort();
            fileContent.Keys.Clear();
            for (int i = 0; i < ls.Count; i++) fileContent.Keys[ls[i]] = i + 1;

            //写谱面文件
            using (StreamWriter sw = new StreamWriter(Path.Combine(dstPath, "chart.txt")))
            {
                sw.WriteLine($"PlayMode={fileContent.Mode}");
                sw.WriteLine("Level=10.0");
                sw.WriteLine($"Title={fileContent.Title}");
                sw.WriteLine($"BPM={fileContent.BPM}");
                sw.WriteLine($"Composer={fileContent.Artist}");
                sw.WriteLine("ST");

                int pretime = -1;
                foreach(var t in fileContent.Notes)
                {
                    string note = $"{fileContent.Keys[t.Rail]}{t.End}";
                    //附加模式
                    if(t.Time == pretime)
                    {
                        sw.Write($"/{note}");
                    }
                    //换行模式
                    else
                    {
                        sw.WriteLine();
                        sw.Write($"${t.Time-pretime}");
                        pretime = t.Time;
                        sw.Write($" {note}");
                    }
                }
                sw.WriteLine();
                sw.WriteLine("ED");
            }
        }

        /// <summary>
        /// 重新裁剪封面
        /// </summary>
        /// <param name="filePath"></param>
        private static async UniTask ResizeCover(string filePath)
        {
            await UniTask.SwitchToMainThread();
            Texture2D originalTexture = new Texture2D(2, 2);
            Texture2D targetTexture = new Texture2D(400, 400, TextureFormat.RGB24, false);
            try
            {
                //读取
                byte[] bytes = File.ReadAllBytes(filePath);
                originalTexture = new Texture2D(2, 2);
                originalTexture.LoadImage(bytes);

                int width = originalTexture.width;
                int height = originalTexture.height;
                
                int centerX = width / 2;
                int centerY = height / 2;

                //出厂设置
                targetTexture = new Texture2D(400, 400, TextureFormat.RGB24, false);
                Color32[] whitePixels = new Color32[400 * 400];
                for (int i = 0; i < whitePixels.Length; i++)
                {
                    whitePixels[i] = Color.white;
                }
                targetTexture.SetPixels32(0, 0, 400, 400, whitePixels);

                //裁剪
                var pixels = originalTexture.GetPixels(
                    Math.Max(0, centerX -200),
                    Math.Max(0, centerY - 200),
                    Math.Min(400, width),
                    Math.Min(400, height));

                targetTexture.SetPixels(
                    Math.Max(0, 200 - centerX), 
                    Math.Max(0, 200 - centerY),
                    Math.Min(400, width),
                    Math.Min(400, height),
                    pixels);

                targetTexture.Apply();

                //写回
                byte[] writeBytes = targetTexture.EncodeToPNG();
                File.WriteAllBytes(filePath, writeBytes);
            }
            catch (Exception e)
            {
                Debug.Log($"{filePath} Resize Failed with {e.Message}!");
                return;
            }
            finally
            {
                UnityEngine.Object.Destroy(originalTexture);
                UnityEngine.Object.Destroy(targetTexture);
            }
        }
    }
}
