using System.IO;
using UnityEngine;
using NLayer;

namespace Megaton
{
    /// <summary>
    /// 音乐加载器，默认MP3格式
    /// </summary>
    public static class MusicLoader
    {
        public static string MusicPath;
        public static string MusicName = "music.mp3";

        public static AudioClip Path2Clip(string path)
        {
            MusicPath = Path.Combine(path, MusicName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(MusicPath);

            MpegFile mpeg = new MpegFile(MusicPath);
            
            // assign samples into AudioClip
            AudioClip ac = AudioClip.Create(filename,
                                            (int)(mpeg.Length / sizeof(float) / mpeg.Channels),
                                            mpeg.Channels,
                                            mpeg.SampleRate,
                                            true,
                                            data => { int actualReadCount = mpeg.ReadSamples(data, 0, data.Length); },
                                            position => { mpeg.Position = position * 4 * mpeg.Channels;}
                                          );
            return ac;
        }

    }
}