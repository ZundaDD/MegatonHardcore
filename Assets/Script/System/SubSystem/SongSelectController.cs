using Cysharp.Threading.Tasks.Triggers;
using EnhancedUI.EnhancedScroller;
using Megaton.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 选歌页面的总控制器
    /// </summary>
    public class SongSelectController : MonoBehaviour
    {
        private static SongSelectController ins;
        public static SongSelectController Ins => ins;

        [SerializeField] private AudioClip[] clips;
        private AudioSource uiPlayer;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private SelectedDisplay selectedHandler;

        private void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
            ins = this;
            uiPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            exitButton.onClick.AddListener(Application.Quit);
        }

        /// <summary>
        /// 开始游玩
        /// </summary>
        /// <param name="chartInfo">谱面信息</param>
        public void StartPlay(ChartInfo chartInfo)
        {
            GameVar.CurPlay = ChartLoader.Path2Play(chartInfo.RootDir,chartInfo);
            SceneManager.LoadScene(Mode.GetSceneIndex(chartInfo.PlayMode));
        }

        public void PlayEffect(int index)
        {
            uiPlayer.PlayOneShot(clips[index]);
        }
    }
}