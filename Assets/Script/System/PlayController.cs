using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 场景游玩控制器，是游玩场景中控制权最高的对象
    /// </summary>
    public class PlayController : MonoBehaviour
    {
        private static PlayController instance;
        public static PlayController Instance => instance;


        /// <summary>
        /// 精确的时间
        /// </summary>
        private float exactTime = 0;
        public float ExactTime { get; private set; }

        /// <summary>
        /// 初始化场景
        /// </summary>
        void Awake()
        {
            instance = this;

            ProcessInput.BindRail();
            ScoreBoard.Clear();

            //GameCamera.LoadCommands(GameVar.Ins.CurPlay.GetCameraCommands());
            
            RailCollection.BindRails();
            //RailCollection.LoadCommands(GameVar.Ins.CurPlay.GetRailCommands());
        }

    }
}