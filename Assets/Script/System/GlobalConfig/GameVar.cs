using System.Collections.Generic;
using Megaton.Abstract;

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
        /// 游戏是否初始化
        /// </summary>
        public bool IfInitialed = false;

        /// <summary>
        /// 游玩模式
        /// </summary>
        public Mode PlayMode = null;
        
        /// <summary>
        /// 所有谱面的信息
        /// </summary>
        public List<ChartInfo> ChartInfos = new();
        
        /// <summary>
        /// 当前游玩的谱面
        /// </summary>
        public ChartPlay CurPlay = null;

    }
}