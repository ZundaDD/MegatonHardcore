using System;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 响应结果列表
    /// </summary>
    public class ResultList : MonoBehaviour
    {
        [SerializeField] private GameObject cellviewPrefab;
        [SerializeField] private RectTransform content;

        /// <summary>
        /// 清空当前列表
        /// </summary>
        public void Clear()
        {
            List<GameObject> childs = new();
            for (int i = 0; i < content.childCount; i++)
            {
                childs.Add(content.GetChild(i).gameObject);
                content.GetChild(i).SetParent(null);
            }
            foreach (var child in childs) Destroy(child);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// 构建列表
        /// </summary>
        /// <param name="charts">完整谱面列表</param>
        public void Construct(List<Web.Sayo.FullChart> charts)
        {
            gameObject.SetActive(true);
            foreach(var chart in charts)
            {
                Instantiate(cellviewPrefab, content).GetComponent<BeatmapCellView>().Bind(chart);
            }
        }
    }
}
