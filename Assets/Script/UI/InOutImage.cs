using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class InOutImage : MonoBehaviour
    {
        public static float limit = 0.1f;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ChangeTo(Sprite sprite, bool ifTween = true)
        {
            image.sprite = sprite;

            if (!ifTween) return;

            //先变换
            var curColor = image.color;
            curColor.a = 0;
            image.color = curColor;
            image.transform.Rotate(new Vector3(0, 0, 10));

            //再恢复
            image.DOFade(1, limit).SetEase(Ease.InOutCubic);
            image.transform.DORotate(image.transform.rotation.eulerAngles - new Vector3(0, 0, 10), limit).SetEase(Ease.InOutCubic);
        }
    }
}