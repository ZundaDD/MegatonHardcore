using System;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton.Web.Sayo
{
    /// <summary>
    /// Sayo镜像站的谱面完整信息，用于Cellview展示
    /// 也是通过API访问得到的最终形式
    /// </summary>
    [Serializable]
    public class FullChart
    {
        public ChartInfo info;
        public int bids_amounts;
        public int bpm;
        public Texture2D cover;
        public List<Beatmapid_Info> bid_data;
    }

    /// <summary>
    /// Sayo镜像站的谱面信息
    /// </summary>
    [Serializable]
    public class ChartInfo
    {
        public string artist;
        public int approved;
        public string title;
        public string creator;
        public int sid;
    }

    /// <summary>
    /// Sayo镜像站的beatmaplist响应
    /// </summary>
    [Serializable]
    public class BeatmapListResponse
    {
        public ChartInfo[] data;
        public int endid;
        public int results;
    }

    /// <summary>
    /// Sayo镜像站的beatmapinfo响应
    /// </summary>
    [Serializable]
    public class BeatmapInfoResponse
    {
        [Serializable]
        public class Sid_Data
        {
            public Beatmapid_Info[] bid_data;
        }
        public int bids_amounts;
        public int bpm;
        public Sid_Data data;
    }

    /// <summary>
    /// 一个难度的信息
    /// </summary>
    [Serializable]
    public class Beatmapid_Info
    {
        public int bid;
        public int mode;
        public float star;
        public string audio;
        public string bg;
        public string length;
        public string version;
    }
}
