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

        [SerializeField] private SettingUI settingCanvas;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private SelectedDisplay selectedHandler;
        [SerializeField] private GameObject blockSelect;

        private void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
            ins = this;
        }

        private void Start()
        {
            settingCanvas.gameObject.SetActive(false);
            settingButton.onClick.AddListener(() => settingCanvas.EnableAnimation());
            exitButton.onClick.AddListener(() => SceneSwitch.Ins.Ending(1));
        }

        /// <summary>
        /// 开始游玩
        /// </summary>
        /// <param name="chartInfo">谱面信息</param>
        public void StartPlay(ChartInfo chartInfo)
        {
            blockSelect.SetActive(true);
            GameVar.CurPlay = ChartLoader.Path2Play(chartInfo.RootDir,chartInfo);
            SceneSwitch.Ins.Ending(chartInfo.PlayMode);
        }

    }
}