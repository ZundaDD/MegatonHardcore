
using DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private SimpleScrollSnap scroller;
        [SerializeField] private RectTransform contentRect;
        

        private void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
        }

        private void Start()
        {
            confirmButton.onClick.AddListener(() => SceneSwitch.Ins.Ending(2));
        }

        
    }
}
