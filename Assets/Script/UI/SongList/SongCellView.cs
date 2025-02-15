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

        public void Bind(ChartInfo chartInfo)
        {
            levelText.text = chartInfo.GetLevelString();
            cellIdentifier = levelText.text;
            titleText.text = chartInfo.Title;
            rankText.text = "";
            modeText.text = chartInfo.PlayMode.ToString();
            selectedButton.onClick.RemoveAllListeners();
            selectedButton.onClick.AddListener(() =>
            {
                SelectController.Ins.PlayEffect(0);
                SelectedDisplay.Ins.ChangeSelected(chartInfo);
            });
            coverImage.sprite = CoverLoader.Path2Sprite(chartInfo.RootDir);
        }
    }
}