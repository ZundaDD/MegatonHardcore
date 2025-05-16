using DG.Tweening;
using Megaton.Web;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class DownloadStateUI : MonoBehaviour
    {
        [Header("动画参数")]
        [SerializeField] private float yoffset = 120f;
        [SerializeField] private float duration = 0.5f;

        [Header("场景引用")]
        [SerializeField] private OSUHandler osuHandler;
        [SerializeField] private Slider progressBar;
        [SerializeField] private Text countText;
        [SerializeField] private Text statusText;
        [SerializeField] private Button invokeButton;

        private bool state = false;
        private RectTransform rectT;
        private float alignY;

        private void Start()
        {
            rectT = GetComponent<RectTransform>();
            alignY = rectT.anchoredPosition.y;

            invokeButton.onClick.AddListener(() =>
            {
                state = !state;
                if (state) EnableAnimation();
                else DisableAnimation();
                
            });
        }

        void Update()
        {
            progressBar.value = osuHandler.Progress;
            statusText.text = osuHandler.Status;
            countText.text = osuHandler.LeftTasks == 0 ? "" 
                : $"剩余任务数：{osuHandler.LeftTasks}";
        }

        public void EnableAnimation()
        {
            rectT.DOAnchorPosY(alignY + yoffset, duration).SetEase(Ease.OutSine);
        }

        public void DisableAnimation()
        {
            rectT.DOAnchorPosY(alignY, duration).SetEase(Ease.InSine);
        }
    }
}