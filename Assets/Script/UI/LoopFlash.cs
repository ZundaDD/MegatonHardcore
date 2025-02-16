using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class LoopFlash : MonoBehaviour
    {
        public static float limit = 0.4f;
        private Image image;


        private void OnEnable()
        {
            if(image == null)image = GetComponent<Image>();
            image.DOFade(0, limit).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            image.DOKill();
        }
    }
}