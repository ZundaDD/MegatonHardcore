using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 场景游玩控制器，是游玩场景中控制权最高的对象
    /// </summary>
    public class PlayController : MonoBehaviour
    {
        /// <summary>
        /// 输入控制器，由单例获取
        /// </summary>
        ProcessInput input;

        /// <summary>
        /// 全部轨道，由自身GO获取
        /// </summary>
        RailCollection rails;
        
        /// <summary>
        /// 当前播放的谱面，由GameVar单例获取
        /// </summary>
        Chart recording;

        /// <summary>
        /// 当前场景的摄像机，由场景获取
        /// </summary>
        Camera scenecamera;
        
        /// <summary>
        /// 只在创建的时候进行搜索，之后的运作逻辑不需要额外外部的调用
        /// </summary>
        void Awake()
        {
            input = ProcessInput.Ins;
            rails = GetComponent<RailCollection>();
            scenecamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            recording = GameVar.Ins.CurPlay;
        }

    }
}