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

        [SerializeField] private AudioClip[] clips;
        [SerializeField] private AudioSource uiPlayer;
        [SerializeField] private RailCollection rails;
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
            //输入设置
            rails.CollectRails();
            ProcessInput.BindRail(rails);

            //UI显示
            ScoreBoard.Clear(GameVar.CurPlay.Quantity);
            scoreboardUI.Bind();
            pauseButton.onClick.AddListener(() => SceneManager.LoadScene(1));

            //给场景加载指令
            GameCamera.LoadCommands(GameVar.CurPlay.GetCameraCommands());
            rails.LoadNotes(GameVar.CurPlay.GetRailCommands());
            rails.GenerateNotes();

            //启动流程
            musicPlayer.Play(GameVar.CurPlay.Music);
        }

        /// <summary>
        /// 结束游玩
        /// </summary>
        public void EndPlay()
        {
            ProcessInput.ReleaseRail();
            scoreboardUI.UnBind();
            SceneManager.LoadScene(1);
        }

        public void PlayEffect(int index)
        {
            uiPlayer.PlayOneShot(clips[index]);
        }
    }
}