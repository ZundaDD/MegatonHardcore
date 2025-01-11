using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 游戏运行时的全局变量，需要有默认值
    /// </summary>
    public class GameVar
    {
        static GameVar ins = new();
        public static GameVar Ins => ins;



        public PlayMode PlayMode = PlayMode.NotPlaying;
        public Chart CurPlay = null;
    }
}