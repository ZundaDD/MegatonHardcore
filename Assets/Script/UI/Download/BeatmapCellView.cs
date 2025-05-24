using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 通过Sayo请求获取的谱面子视图
    /// </summary>
    public class BeatmapCellView : MonoBehaviour
    {
        [SerializeField] private Text sid;
        [SerializeField] private Text title;
        [SerializeField] private Text artist;
        [SerializeField] private Text bpm;
        [SerializeField] private Text creator;
        [SerializeField] private Text approved;
        [SerializeField] private Text bid_amounts;
        [SerializeField] private Image coverImage;
        [SerializeField] private Button downloadButton;

        public void Bind(Web.Sayo.FullChart fullChart)
        {
            downloadButton.onClick.AddListener(() =>
            {
                foreach (var bm in fullChart.bid_data) 
                    DownloadSceneController.Ins.DownloadBeatmap(bm.bid);
            });
            coverImage.sprite = Sprite.Create(fullChart.cover, new Rect(0, 0, fullChart.cover.width, fullChart.cover.height), Vector2.zero);
            sid.text = $"SID: {fullChart.info.sid}";
            bid_amounts.text = $"数量:{fullChart.bid_data.Count}";
            bpm.text = fullChart.bpm.ToString();
            title.text = fullChart.info.title;
            artist.text = fullChart.info.artist;
            creator.text = fullChart.info.creator;
            approved.text = fullChart.info.approved switch 
            {
                -2 => "Graveyard",
                -1 => "WIP",
                0 => "Pending",
                1 => "Ranked",
                2 => "Approved",
                3 => "Qualified",
                4 => "Loved",
                _ => "Unknown"
            };
        }

        public void OnDestroy()
        {
            if(coverImage.sprite != null) 
            {
                Destroy(coverImage.sprite.texture); // 释放纹理资源
                coverImage.sprite = null; // 清除引用，避免内存泄漏
            }
        }
    }
}
