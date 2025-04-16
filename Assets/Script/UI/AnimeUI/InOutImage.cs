using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class InOutImage : MonoBehaviour
    {
        public static float limit = 0.2f;
        public static float loop = 20f;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ChangeTo(Sprite sprite, bool ifTween = true)
        {
            image.sprite = sprite;

            if (!ifTween) return;

            //变换初状态
            DOTween.Kill(image.transform);
            var curColor = image.color;
            curColor.a = 0;
            image.color = curColor;
            image.transform.rotation = Quaternion.Euler(0, 0, 0);


            //初始状态
            image.DOFade(1, limit).SetEase(Ease.InOutCubic);
            image.transform
                .DORotate(new Vector3(0, 0, -5), limit)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    image.transform
                    .DORotate(new Vector3(0, 0, 5), loop)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo);
                }
                );
        }
    }
}