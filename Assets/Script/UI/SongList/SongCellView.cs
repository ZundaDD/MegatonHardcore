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

        public void Bind(ChartInfo chartInfo)
        {
            levelText.text = chartInfo.Level.ToString();
            cellIdentifier = levelText.text;
            titleText.text = chartInfo.Title;
            rankText.text = "";
            modeText.text = chartInfo.PlayMode.ToString();
            //coverImage = 
        }
    }
}