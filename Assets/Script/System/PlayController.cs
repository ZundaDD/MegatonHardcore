using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 场景游玩控制器，是游玩场景中控制权最高的对象
    /// </summary>
    public class PlayController : MonoBehaviour
    {


        
        /// <summary>
        /// 初始化场景
        /// </summary>
        void Awake()
        {

            ProcessInput.BindRail();
            ScoreBoard.Clear();

            GameCamera.LoadCommands(GameVar.Ins.CurPlay.GetCameraCommands());
            
            RailCollection.BindRails();
            RailCollection.LoadCommands(GameVar.Ins.CurPlay.GetRailCommands());
        }

    }
}