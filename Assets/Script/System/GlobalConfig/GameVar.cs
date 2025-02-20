using System.Collections.Generic;
using Megaton.Abstract;

namespace Megaton
{
    /// <summary>
    /// 游戏运行时的全局变量，需要有默认值
    /// </summary>
    public static class GameVar
    {
        /// <summary>
        /// 游戏是否初始化
        /// </summary>
        public static bool IfInitialed = false;

        /// <summary>
        /// 游玩是否开始准备
        /// </summary>
        public static bool IfPrepare = false;

        /// <summary>
        /// 游玩是否开始
        /// </summary>
        public static bool IfStarted = false;

        /// <summary>
        /// 游玩是否暂停
        /// </summary>
        public static bool IfPaused = false;
        
        /// <summary>
        /// 游玩模式
        /// </summary>
        public static Mode PlayMode = null;
        
        /// <summary>
        /// 所有谱面的信息
        /// </summary>
        public static List<ChartInfo> ChartInfos = new();
        
        /// <summary>
        /// 当前游玩的谱面
        /// </summary>
        public static ChartPlay CurPlay = null;

        /// <summary>
        /// 标准摄像头移动速度
        /// </summary>
        public static float Velocity = 0f;

        /// <summary>
        /// 标准帧率
        /// </summary>
        public static int FrameRate = 60;

        /// <summary>
        /// 准备帧
        /// </summary>
        public static int PrepareFrame = 120;
    }
}