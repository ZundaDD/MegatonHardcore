using System.Collections.Generic;
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


        /// <summary>
        /// 游玩模式
        /// </summary>
        public Mode PlayMode = null;
        
        /// <summary>
        /// 所有谱面的信息
        /// </summary>
        public List<ChartInfo> ChartInfos = null;
        
        /// <summary>
        /// 当前游玩的谱面
        /// </summary>
        public Chart CurPlay = null;

    }
}