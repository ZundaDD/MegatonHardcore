using DG.Tweening;
using UnityEngine;

namespace Megaton.UI
{
    public class RotateImage : MonoBehaviour
    {
        [SerializeField] private bool circlewise = false;
        [SerializeField] private float cycleTime = 1f;
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>(); 
            rectTransform.DOLocalRotate(
            new Vector3(0, 0, 360f * (circlewise ? -1 : 1)),
            cycleTime,
            RotateMode.FastBeyond360
            )
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.InOutCubic);
        }

    }
}
