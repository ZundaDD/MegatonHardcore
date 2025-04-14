using Megaton.Abstract;
using Megaton.Classic;
using Megaton.UI;
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
        private static PlayController ins;
        public static PlayController Ins => ins;

        [SerializeField] private RailCollection rails;
        [SerializeField] private Canvas canvasFar;
        [SerializeField] private MusicPlayer musicPlayer;
        [SerializeField] private PauseUI pauseUI;
        [SerializeField] private ScoreboardUI scoreboardUI;

        /// <summary>
        /// 初始化场景
        /// </summary>
        void Awake()
        {
            if (!GameVar.IfInitialed)
            {
                SceneManager.LoadScene(0);
                return;
            }
            ins = this;
        }

        private void Start() => Initial();
        #region 流程控制
        /// <summary>
        /// 初始化游戏，加载资源和构建谱面
        /// </summary>
        public void Initial()
        {
            InputManager.Input.Player.Escape.performed += ctx => pauseUI.SwitchState();

            //输入设置
            rails.CollectRails();
            GameVar.PlayMode.InputBinding(InputManager.Input, rails);
            InputManager.SwitchInputMode(true);

            //场景配置
            canvasFar.planeDistance = 100 + Setting.Ins.Board_Distance.Value * 10;

            //UI显示
            ScoreBoard.Clear(GameVar.CurPlay.Quantity, GameVar.CurPlay.Weight);
            scoreboardUI.Bind();

            //给场景加载指令
            GameCamera.LoadCommands(GameVar.CurPlay.GetCameraCommands());
            rails.LoadNotes(GameVar.CurPlay.GetRailCommands());

            musicPlayer.CommandPlay(MusicLoader.Path2Clip(GameVar.CurPlay.Info.RootDir));
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
        }

        /// <summary>
        /// 恢复游玩
        /// </summary>
        public void Restore()
        {
            GameVar.IfPaused = false;
            musicPlayer.Restore();
        }

        /// <summary>
        /// 结束游玩，进行结算
        /// </summary>
        public void EndPlay()
        {
            ResetGlobalState();
            SceneSwitch.Ins.Ending(3);
        }

        /// <summary>
        /// 重新开始游玩，不进行结算
        /// </summary>
        public void Restart()
        {
            ResetGlobalState();
            SceneSwitch.Ins.Ending(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 退出游玩，不进行结算
        /// </summary>
        public void Exit()
        {
            ResetGlobalState();
            SceneSwitch.Ins.Ending(2);
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