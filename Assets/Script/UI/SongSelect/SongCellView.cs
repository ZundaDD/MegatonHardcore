using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

namespace Megaton.UI
{
    /// <summary>
    /// 每首歌的显示子单元
    /// </summary>
    public class SongCellView : MonoBehaviour
    {
        
        [Header("UI组件")]
        [SerializeField] private Text levelText;
        [SerializeField] private Text titleText;
        [SerializeField] private Text rankText;
        [SerializeField] private Text modeText;
        [SerializeField] private Image coverImage;        
        [SerializeField] private Button selectedButton;
        [SerializeField] public Animator selectHint;
        
        public int PanelIndex { get; set; }
        public int RealIndex { get; set; }

        public void SetAnimation(bool on)
        {
            if (on) selectHint.SetTrigger("FadeIn");
            else selectHint.SetTrigger("FadeOut");
        }

        
        private void Start()
        {
            selectedButton.onClick.AddListener(() =>
            {
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
                SongList.Ins.ProcessPress(PanelIndex);
            });
        }

        public void Bind(ChartInfo chartInfo)
        {
            //显示数据
            levelText.text = chartInfo.GetLevelString();
            titleText.text = chartInfo.Title;
            rankText.text = chartInfo.Score.BestRank;
            modeText.text = chartInfo.PlayMode.ToString();
            coverImage.sprite = chartInfo.GetCoverSprite();
        }
    }
}