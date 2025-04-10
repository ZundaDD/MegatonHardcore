
using DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using Megaton.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private SimpleScrollSnap scroller;
        [SerializeField] private RectTransform contentRect;
        

        private void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
        }

        private void Start()
        {
            DOTween.Sequence().AppendCallback(() => scroller.GoToNextPanel()).SetDelay(0.1f);
        }

        
    }
}
