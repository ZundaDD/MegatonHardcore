using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 每首歌的显示子单元
    /// </summary>
    public class SongCellView : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text titleText;
        [SerializeField] private Text rankText;
        [SerializeField] private Text modeText;
        [SerializeField] private Image coverImage;
        
        [SerializeField] private Button selectedButton;

        private bool isSelected = false;
        //[SerializeField] public Image selectHint;

        
        public void Bind(ChartInfo chartInfo)
        {
            /*恢复闪
            if(SelectChartInfoUI.Ins.IfSelected(chartInfo))
            {
                SelectChartInfoUI.Ins.RealertSelected(selectHint);
            }*/

            //显示数据
            levelText.text = chartInfo.GetLevelString();
            titleText.text = chartInfo.Title;
            rankText.text = chartInfo.Score.BestRank;
            modeText.text = chartInfo.PlayMode.ToString();
            coverImage.sprite = chartInfo.GetCoverSprite();

            //绑定点击事件
            selectedButton.onClick.RemoveAllListeners();
            selectedButton.onClick.AddListener(() => 
            {
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
                SongSelectController.StartPlay(chartInfo);
            });
            
        }
    }
}