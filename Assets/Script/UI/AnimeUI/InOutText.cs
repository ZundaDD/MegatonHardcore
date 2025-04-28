using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class InOutText : MonoBehaviour
    {
        public static float limit = 0.1f;
        private RectTransform rectTransform;
        private Vector2 align;
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            rectTransform = GetComponent<RectTransform>();
            align = rectTransform.anchoredPosition;
        }

        public void ChangeTo(string newText,bool ifTween = true)
        {
            text.text = newText;

            if (!ifTween) return;

            //先变换
            var curColor = text.color;
            curColor.a = 0;
            text.color = curColor;
            rectTransform.anchoredPosition = align + new Vector2(10, 0);
            
            //再恢复
            text.DOFade(1, limit).SetEase(Ease.InOutCubic);
            rectTransform.DOAnchorPosX(align.x,limit).SetEase(Ease.InOutCubic);
        }

    }
}