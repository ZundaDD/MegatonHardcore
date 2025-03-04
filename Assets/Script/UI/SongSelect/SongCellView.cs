using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 每首歌的显示子单元
    /// </summary>
    public class SongCellView : EnhancedScrollerCellView
    {
        [SerializeField] Text levelText;
        [SerializeField] Text titleText;
        [SerializeField] Text rankText;
        [SerializeField] Text modeText;
        [SerializeField] Image coverImage;
        [SerializeField] Button selectedButton;
        [SerializeField] public Image selectHint;

        public void Bind(ChartInfo chartInfo)
        {
            //恢复闪
            if(SelectedDisplay.Ins.IfSelected(chartInfo))
            {
                SelectedDisplay.Ins.RealertSelected(selectHint);
            }

            //显示
            levelText.text = chartInfo.GetLevelString();
            cellIdentifier = levelText.text;
            titleText.text = chartInfo.Title;
            rankText.text = chartInfo.Score.BestRank;
            modeText.text = chartInfo.PlayMode.ToString();
            selectedButton.onClick.RemoveAllListeners();
            selectedButton.onClick.AddListener(() =>
            {
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
                SelectedDisplay.Ins.ChangeSelected(chartInfo,selectHint);
            });
            coverImage.sprite = CoverLoader.Path2Sprite(chartInfo.RootDir);
        }
    }
}