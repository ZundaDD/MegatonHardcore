using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class PauseUI : PanelUI
    {
        [SerializeField] Button continueButton;
        [SerializeField] Button restartButton;
        [SerializeField] Button exitButton;
        [SerializeField] Button calculateButton;
        [SerializeField] RectTransform plane;

        private float contentHeight = 0;
        private float transTime = 0.5f;
        private bool ifSwitchable = true;

        void Start()
        {
            continueButton.onClick.AddListener(Pop);
            restartButton.onClick.AddListener(PlayController.Ins.Restart);
            exitButton.onClick.AddListener(PlayController.Ins.Exit);
            calculateButton.onClick.AddListener(PlayController.Ins.EndPlay);
        }

        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.UI.Escape.performed += Pop;
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.UI.Escape.performed -= Pop;
        }

        protected override bool Close()
        {
            if (!ifSwitchable) return false;

            ifSwitchable = false;
            PlayController.Ins.Restore();

            //动画
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSettingExit);
            plane.DOScale(new Vector3(1.2f, 1.2f, 1.2f), transTime).SetEase(Ease.InOutCubic);
            canvasGroup.DOFade(0, transTime).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    ifSwitchable = true;
                    gameObject.SetActive(false);
                });

            return true;
        }

        protected override bool Open()
        {
            if (!GameVar.IfStarted && !GameVar.IfPaused) return false;
            if (!ifSwitchable) return false;

            ifSwitchable = false;
            PlayController.Ins.Pause();

            //动画
            gameObject.SetActive(true);
            plane.DOScale(new Vector3(1f, 1f, 1f), transTime).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                ifSwitchable = true;
            });
            canvasGroup.DOFade(1, transTime).SetEase(Ease.InOutCubic);

            return true;
        }
    }
}