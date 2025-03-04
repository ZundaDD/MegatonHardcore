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
        [SerializeField] private Button pauseButton;
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

        private void Start()
        {
            pauseButton.onClick.AddListener(EndPlay);

            //输入设置
            rails.CollectRails();
            ProcessInput.BindRail(rails);

            //场景配置
            canvasFar.planeDistance = 100 + Setting.Ins.Board_Distance.Value * 10;

            //UI显示
            ScoreBoard.Clear(GameVar.CurPlay.Quantity);
            scoreboardUI.Bind();

            //给场景加载指令
            GameCamera.LoadCommands(GameVar.CurPlay.GetCameraCommands());
            rails.LoadNotes(GameVar.CurPlay.GetRailCommands());
            rails.GenerateNotes();

            //启动流程
            musicPlayer.OnEnd += EndPlay;
            musicPlayer.CommandPlay(GameVar.CurPlay.Music);
        }

        /// <summary>
        /// 结束游玩
        /// </summary>
        public void EndPlay()
        {
            ProcessInput.ReleaseRail();
            scoreboardUI.UnBind();
            SceneSwitch.Ins.Ending(3);
            GameVar.IfPrepare = false;
            GameVar.IfStarted = false;
        }

    }
}