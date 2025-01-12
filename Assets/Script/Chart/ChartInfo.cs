using UnityEngine;
using System;

namespace Megaton
{
    [Serializable]
    public class ChartInfo
    {
        public string Title = "Null";
        public string Composer = "Null";
        public string RootDir = "Null";
        public PlayMode PlayMode = PlayMode.L2R2;
        public int BPM = -50;

        public void SetProperty(string key, string value)
        {
            switch (key)
            {
                case "Title":
                    Title = value;
                    break;
                case "Composer":
                    Composer = value;
                    break;
                case "BPM":
                    BPM = int.Parse(value);
                    break;
                case "PlayMode":
                    PlayMode = (PlayMode) Enum.Parse(typeof(PlayMode), value);
                    break;
            }
        }
    }
}