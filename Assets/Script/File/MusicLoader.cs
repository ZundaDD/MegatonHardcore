using System.IO;
using UnityEngine;
using NLayer;
using NVorbis;
using System.IO.Enumeration;

namespace Megaton
{
    /// <summary>
    /// 音乐加载器，默认MP3格式
    /// </summary>
    public static class MusicLoader
    {
        public static string MusicName = "music";
        public static VorbisReader ReadStream = null;

        public static AudioClip Path2Clip(string path,bool stream = true)
        {
            string filename = Path.Combine(path, MusicName);
            AudioClip ac = null;
            if (File.Exists($"{filename}.mp3"))
            {
                filename = $"{filename}.mp3";
                MpegFile mpeg = new MpegFile(filename);

                // assign samples into AudioClip
                ac = AudioClip.Create(filename,
                                                (int)(mpeg.Length / sizeof(float) / mpeg.Channels),
                                                mpeg.Channels,
                                                mpeg.SampleRate,
                                                stream,
                                                data => { int actualReadCount = mpeg.ReadSamples(data, 0, data.Length); }
                                                );
            }
            else
            {
                filename = $"{filename}.ogg";
                var vorbis = new VorbisReader(filename);
                ReadStream = vorbis;
                ac = AudioClip.Create(filename,
                                            (int)(vorbis.SampleRate * vorbis.TotalTime.TotalSeconds),
                                            vorbis.Channels,
                                            vorbis.SampleRate,
                                            stream,
                                            data => { int actualReadCount = vorbis.ReadSamples(data, 0, data.Length); }
                                            );
            }
            

            return ac;
        }

    }
}