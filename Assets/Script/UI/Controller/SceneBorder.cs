

using DG.Tweening;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 场景挡板，负责切换时的遮挡，原则上位于最低层级
    /// 但是遮挡层级最高,独立进行管理
    /// </summary>
    public class SceneBorder : UICollection
    {
        [SerializeField] private RectTransform leftMask;
        [SerializeField] private RectTransform rightMask;
        private bool locked = false;

        protected override void Awake()
        {
            SceneSwitch.OnSceneChange += Close;
            GetComponent<CanvasGroup>().alpha = 1;
            Open();
        }

        protected override void Close()
        {
            SceneSwitch.OnSceneChange -= Close;
            leftMask.DOAnchorPosX(0, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(0, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneExit);
        }

        protected override void Open()
        {
            leftMask.DOAnchorPosX(-1000f, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(1000f, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneOpen);
        }
    }
}
