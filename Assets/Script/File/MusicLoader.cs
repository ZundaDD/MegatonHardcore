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

            MpegFile mpegFile = new MpegFile(MusicPath);

            // assign samples into AudioClip
            AudioClip ac = AudioClip.Create(filename,
                                            (int)(mpegFile.Length / sizeof(float) / mpegFile.Channels),
                                            mpegFile.Channels,
                                            mpegFile.SampleRate,
                                            true,
                                            data => { int actualReadCount = mpegFile.ReadSamples(data, 0, data.Length); },
                                            position => { mpegFile = new MpegFile(MusicPath); }
                                          );
            return ac;
        }
    }
}