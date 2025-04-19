using Cysharp.Threading.Tasks.Triggers;
using EnhancedUI.EnhancedScroller;
using Megaton.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    /// <summary>
    /// 选歌页面的总控制器
    /// </summary>
    public class SongSelectController : MonoBehaviour
    {
        private void Awake()
        {
            if(!GameVar.IfInitialed) SceneManager.LoadScene(0);
        }

        /// <summary>
        /// 开始游玩
        /// </summary>
        /// <param name="chartInfo">谱面信息</param>
        public static void StartPlay(ChartInfo chartInfo)
        {
            GameVar.CurPlay = ChartLoader.Path2Play(chartInfo.RootDir,chartInfo);
            SceneSwitch.Ending(chartInfo.PlayMode);
        }
    }
}