using Megaton.Classic;
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
        private static PlayController instance;
        public static PlayController Instance => instance;

        [SerializeField] private RailCollection rails;
        [SerializeField] private MusicPlayer musicPlayer;
        [SerializeField] private Button pauseButton;

        /// <summary>
        /// 初始化场景
        /// </summary>
        void Awake()
        {
            if (!GameVar.Ins.IfInitialed)
            {
                SceneManager.LoadScene(0);
                return;
            }

            instance = this;

            //输入设置
            rails.CollectRails();
            GameVar.Ins.PlayMode = new L2R2(); //需要变为一般化的表示
            ProcessInput.BindRail(rails);
            
            //UI显示
            ScoreBoard.Clear(GameVar.Ins.CurPlay.Quantity);
            pauseButton.onClick.AddListener(() => SceneManager.LoadScene(1));

            //给场景加载指令
            GameCamera.LoadCommands(GameVar.Ins.CurPlay.GetCameraCommands());
            rails.LoadNotes(GameVar.Ins.CurPlay.GetRailCommands());
        }

        private void Start()
        {
            musicPlayer.Play(GameVar.Ins.CurPlay.Music);
        }

        /// <summary>
        /// 结束游玩
        /// </summary>
        public void EndPlay()
        {
            ProcessInput.ReleaseRail();
            SceneManager.LoadScene(1);
        }
    }
}