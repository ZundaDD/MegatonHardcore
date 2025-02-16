using Cysharp.Threading.Tasks.Triggers;
using EnhancedUI.EnhancedScroller;
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
            if (!GameVar.Ins.IfInitialed) SceneManager.LoadScene(0);
            ins = this;
            uiPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            exitButton.onClick.AddListener(Application.Quit);
        }

        public void PlayEffect(int index)
        {
            uiPlayer.PlayOneShot(clips[index]);
        }
    }
}