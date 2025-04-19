using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Megaton.UI
{
    /// <summary>
    /// 主界面每一个选项的单位视图
    /// </summary>
    public class ModeChoice : MonoBehaviour,ISelectCallback
    {
        [SerializeField] private float animeTime = 0.2f;
        public UnityEvent DoPage;
        private RectTransform rectT;

        public GameObject gameobject => gameObject;

        public void OnSubmit(BaseEventData bed)
        {
            DoPage?.Invoke();
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
        }

        public void OnSelect(BaseEventData bed)
        {
            if(rectT == null) rectT = GetComponent<RectTransform>();
            rectT.DOScale(1.2f, animeTime).SetEase(Ease.InOutCubic);
        }

        public void DeSelect(BaseEventData bed)
        {
            if (rectT == null) rectT = GetComponent<RectTransform>();
            rectT.DOScale(1f, animeTime).SetEase(Ease.InOutCubic);
        }
    }
}
