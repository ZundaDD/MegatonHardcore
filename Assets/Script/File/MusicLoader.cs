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

        private static string lastLoadMusicPath;
        private static MpegFile lastLoadMusicFile;

        public static AudioClip Path2Clip(string path)
        {
            MusicPath = Path.Combine(path, MusicName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(MusicPath);

            lastLoadMusicPath = path;
            lastLoadMusicFile = new MpegFile(MusicPath);
            
            // assign samples into AudioClip
            AudioClip ac = AudioClip.Create(filename,
                                            (int)(lastLoadMusicFile.Length / sizeof(float) / lastLoadMusicFile.Channels),
                                            lastLoadMusicFile.Channels,
                                            lastLoadMusicFile.SampleRate,
                                            true,
                                            data => { int actualReadCount = lastLoadMusicFile.ReadSamples(data, 0, data.Length); },
                                            position => { lastLoadMusicFile.Position = position * 4 * lastLoadMusicFile.Channels;}
                                          );
            return ac;
        }

        public static AudioClip ReSizeClip(float startPos)
        {
           
            if(lastLoadMusicFile == null) lastLoadMusicFile = new MpegFile(lastLoadMusicPath);

            // assign samples into AudioClip
            AudioClip ac = AudioClip.Create(lastLoadMusicPath,
                                            (int)(lastLoadMusicFile.Length / sizeof(float) / lastLoadMusicFile.Channels),
                                            lastLoadMusicFile.Channels,
                                            lastLoadMusicFile.SampleRate,
                                            true,
                                            data => { int actualReadCount = lastLoadMusicFile.ReadSamples(data, 0, data.Length); },
                                            position => { lastLoadMusicFile = new MpegFile(MusicPath); }
                                          );

            return ac;
        }

        private static void PosCallback(int pos)
        {

        }
    }
}