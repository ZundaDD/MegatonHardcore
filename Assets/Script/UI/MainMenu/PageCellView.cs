using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Megaton.UI
{
    /// <summary>
    /// 主界面每一个选项的单位视图
    /// </summary>
    public class PageCellView : MonoBehaviour,ISelectCallback
    {
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
            rectT.DOScale(1.2f, 0.2f).SetEase(Ease.InOutCubic);
        }

        public void DeSelect(BaseEventData bed)
        {
            if (rectT == null) rectT = GetComponent<RectTransform>();
            rectT.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic);
        }

        public void Quit() => Application.Quit();
    }
}
