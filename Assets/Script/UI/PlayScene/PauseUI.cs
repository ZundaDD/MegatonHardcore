using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] Button continueButton;
        [SerializeField] Button restartButton;
        [SerializeField] Button exitButton;
        [SerializeField] Button calculateButton;
        [SerializeField] RectTransform plane;

        private CanvasGroup canvasGroup;
        private float contentHeight = 0;
        private float transTime = 0.25f;
        private bool ifSwitchable = true;
        private bool state = false;
        
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            InputManager.Input.Player.Pause.performed += ctx => SwitchState();
            
            continueButton.onClick.AddListener(DisableAnimation);
            restartButton.onClick.AddListener(PlayController.Ins.Restart);
            exitButton.onClick.AddListener(PlayController.Ins.Exit);
            calculateButton.onClick.AddListener(PlayController.Ins.EndPlay);
        }

        private void OnDestroy()
        {
            InputManager.Input.Player.Pause.performed -= ctx => SwitchState();
        }

        public void SwitchState()
        {
            if(state) DisableAnimation();
            else EnableAnimation();
        }

        public void DisableAnimation()
        {
            if (!ifSwitchable) return;
            ifSwitchable = false;
            PlayController.Ins.Restore();
            state = false;

            //动画
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSettingExit);
            plane.DOScale(new Vector3(1.2f, 1.2f, 1.2f), transTime).SetEase(Ease.InOutCubic);
            canvasGroup.DOFade(0, transTime).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    ifSwitchable = true;
                    gameObject.SetActive(false);
                });
        }

        public void EnableAnimation()
        {
            if (!ifSwitchable) return;
            ifSwitchable = false;
            PlayController.Ins.Pause();
            state = true;

            //动画
            gameObject.SetActive(true);
            plane.DOScale(new Vector3(1f, 1f, 1f), transTime).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                ifSwitchable = true;
            });
            canvasGroup.DOFade(1, transTime).SetEase(Ease.InOutCubic);
        }
    }
}