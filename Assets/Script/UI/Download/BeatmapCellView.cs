using System;
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
        [Header("裁剪尺寸")]
        [SerializeField] private int target_width;
        [SerializeField] private int target_height;
        [Header("UI组件")]
        [SerializeField] private Text sid;
        [SerializeField] private Text title;
        [SerializeField] private Text artist;
        [SerializeField] private Text bpm;
        [SerializeField] private Text creator;
        [SerializeField] private Text approved;
        [SerializeField] private Text bid_amounts;
        [SerializeField] private Image coverImage;
        [SerializeField] private Button downloadButton;

        private Texture2D ResizeCover(Texture2D cover)
        {
            Texture2D originalTexture = cover;
            Texture2D targetTexture =
                new Texture2D(target_width,
                target_height,
                TextureFormat.RGB24, false);
            try
            {
                int width = originalTexture.width;
                int height = originalTexture.height;

                int centerX = width / 2;
                int centerY = height / 2;

                //出厂设置
                targetTexture = new Texture2D(target_width, target_height, TextureFormat.RGB24, false);
                Color32[] whitePixels = new Color32[target_width * target_height];
                for (int i = 0; i < whitePixels.Length; i++)
                {
                    whitePixels[i] = Color.white;
                }
                targetTexture.SetPixels32(0, 0, target_width, target_height, whitePixels);

                //裁剪
                var pixels = originalTexture.GetPixels(
                    Math.Max(0, centerX - target_width / 2),
                    Math.Max(0, centerY - target_height / 2),
                    Math.Min(target_width, width),
                    Math.Min(target_height, height));

                targetTexture.SetPixels(
                    Math.Max(0, target_width / 2 - centerX),
                    Math.Max(0, target_height / 2 - centerY),
                    Math.Min(target_width, width),
                    Math.Min(target_height, height),
                    pixels);

                targetTexture.Apply();

                return targetTexture;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                UnityEngine.Object.Destroy(originalTexture);
            }
        }

        public void Bind(Web.Sayo.FullChart fullChart)
        {
            downloadButton.onClick.AddListener(() =>
            {
                foreach (var bm in fullChart.bid_data) 
                    DownloadSceneController.Ins.DownloadBeatmap(bm.bid);
            });
            var cover = ResizeCover(fullChart.cover);
            coverImage.sprite = Sprite.Create(cover, new Rect(0, 0, cover.width, cover.height), Vector2.zero);
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
