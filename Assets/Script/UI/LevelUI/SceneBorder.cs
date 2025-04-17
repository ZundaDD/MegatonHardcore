

using DG.Tweening;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 场景挡板，负责切换时的遮挡，原则上位于最低层级
    /// 但是遮挡层级最高,独立进行管理
    /// </summary>
    public class SceneBorder : MonoBehaviour
    {
        [SerializeField] private RectTransform leftMask;
        [SerializeField] private RectTransform rightMask;

        protected void Awake()
        {
            if (!GameVar.IfInitialed) return;

            SceneSwitch.OnSceneChange += Close;
            GetComponent<CanvasGroup>().alpha = 1;
            Open();
        }

        protected void Close()
        {
            SceneSwitch.OnSceneChange -= Close;
            leftMask.DOAnchorPosX(0, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(0, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneExit);

        }

        protected void Open()
        {
            leftMask.DOAnchorPosX(-1000f, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(1000f, SceneSwitch.minSwitchTime).SetEase(Ease.InOutCubic);
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneOpen);
        }
    }
}
