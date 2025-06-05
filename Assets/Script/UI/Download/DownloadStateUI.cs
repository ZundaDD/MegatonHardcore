using DG.Tweening;
using Megaton.Web;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class DownloadStateUI : MonoBehaviour
    {
        [Header("动画参数")]
        [SerializeField] private float xoffset = 120f;
        [SerializeField] private float duration = 0.5f;

        [Header("场景引用")]
        [SerializeField] private OSUHandler osuHandler;
        [SerializeField] private Slider progressBar;
        [SerializeField] private Text countText;
        [SerializeField] private Text statusText;

        private bool state = false;
        private RectTransform rectT;
        private float alignX;

        private void Start()
        {
            rectT = GetComponent<RectTransform>();
            alignX = rectT.anchoredPosition.x;
        }

        void Update()
        {
            progressBar.value = osuHandler.Progress;
            statusText.text = osuHandler.Status;
            countText.text = osuHandler.LeftTasks == 0 ? "" 
                : $"剩余任务数：{osuHandler.LeftTasks}";
            progressBar.gameObject.SetActive(osuHandler.LeftTasks > 0);
        }
    }
}