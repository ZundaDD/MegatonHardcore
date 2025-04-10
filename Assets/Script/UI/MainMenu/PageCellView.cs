using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Megaton.UI
{
    /// <summary>
    /// 主界面每一个选项的单位视图
    /// </summary>
    public class PageCellView : MonoBehaviour
    {
        public UnityEvent DoPage;
        private RectTransform rectT;
        
        public void Invoke()
        {
            DoPage?.Invoke();
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
        }

        public void OnHoverStart()
        {
            if(rectT == null) rectT = GetComponent<RectTransform>();
            rectT.DOScale(1.2f, 0.2f).SetEase(Ease.InOutCubic);
        }

        public void OnHoverEnd()
        {
            if (rectT == null) rectT = GetComponent<RectTransform>();
            rectT.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic);
        }

        public void Quit() => Application.Quit();
    }
}
