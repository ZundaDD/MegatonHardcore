using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 响应状态UI
    /// </summary>
    public class ResponseStateUI : MonoBehaviour
    {

        [SerializeField] private PointingText text;
        [SerializeField] private Image loadingImage;
        [SerializeField] private Image errorImage;

        private void Start()
        {
            loadingImage.gameObject.SetActive(true);
            errorImage.gameObject.SetActive(false);
        }

        public void Hide() => gameObject.SetActive(false);

        public void LoadingState()
        {
            gameObject.SetActive(true);
            loadingImage.gameObject.SetActive(true);
            errorImage.gameObject.SetActive(false);
            text.SetText("正在获取谱面列表", true);
        }

        public void ErrorState()
        {
            gameObject.SetActive(true);
            loadingImage.gameObject.SetActive(false);
            errorImage.gameObject.SetActive(true);
            text.SetText("获取谱面列表失败", false);
        }

    }
}