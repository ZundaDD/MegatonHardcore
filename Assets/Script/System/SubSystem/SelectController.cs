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
    public class SelectController : MonoBehaviour
    {
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            if (!GameVar.Ins.IfInitialed) SceneManager.LoadScene(0); 
        }

        private void Start()
        {
            exitButton.onClick.AddListener(Application.Quit);
        }


    }
}