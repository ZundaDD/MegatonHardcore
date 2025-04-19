using Megaton.Abstract;
using Megaton.Classic;
using Megaton.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    /// <summary>
    /// 场景游玩控制器，是游玩场景中控制权最高的对象
    /// </summary>
    public class PlayController : MonoBehaviour
    {
        public static PlayController Ins { get; private set; }

        [SerializeField] private RailCollection rails;
        [SerializeField] private MusicPlayer musicPlayer;

        /// <summary>
        /// 初始化场景
        /// </summary>
        void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
            Ins = this;
        }

        private void Start() => Initial();

        #region 流程控制
        /// <summary>
        /// 初始化游戏，加载资源和构建谱面
        /// </summary>
        public void Initial()
        {
            //输入设置
            rails.CollectRails();
            GameVar.PlayMode.InputBinding(InputManager.Input, rails);
            InputManager.SwitchInputMode(true);

            //给场景加载指令
            GameCamera.LoadCommands(GameVar.CurPlay.GetCameraCommands());
            rails.LoadNotes(GameVar.CurPlay.GetRailCommands());

            //开始播放音乐，即开始游玩
            musicPlayer.CommandPlay(MusicLoader.Path2Clip(GameVar.CurPlay.Info.RootDir, false));
        }

        /// <summary>
        /// 暂停游玩
        /// </summary>
        public void Pause()
        {
            GameVar.IfPaused = true;
            GameVar.IfPrepare = false;
            GameVar.IfStarted = false;
            musicPlayer.Pause();
            InputManager.SwitchInputMode(false);
        }

        /// <summary>
        /// 恢复游玩
        /// </summary>
        public void Restore()
        {
            GameVar.IfPaused = false;
            musicPlayer.Restore();
            InputManager.SwitchInputMode(true);
        }

        /// <summary>
        /// 结束游玩，进行结算
        /// </summary>
        public void EndPlay()
        {
            ResetGlobalState();
            SceneSwitch.Ending(3);
        }

        /// <summary>
        /// 重新开始游玩，不进行结算
        /// </summary>
        public void Restart()
        {
            ResetGlobalState();
            GameVar.CurPlay = ChartLoader.Path2Play(GameVar.CurPlay.Info.RootDir, GameVar.CurPlay.Info);
            SceneSwitch.Ending(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 退出游玩，不进行结算
        /// </summary>
        public void Exit()
        {
            ResetGlobalState();
            SceneSwitch.Ending(2);
        }

        private void ResetGlobalState()
        {
            InputManager.SwitchInputMode(false);
            musicPlayer.EndPlay();
            GameVar.PlayMode.InputRelease(InputManager.Input, rails);
            GameVar.IfPrepare = false;
            GameVar.IfStarted = false;
            GameVar.IfPaused = false;
        }
        #endregion
    }
}