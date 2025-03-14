using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 选歌控制器
    /// </summary>
    public class SongListController : MonoBehaviour,IEnhancedScrollerDelegate
    {
        private EnhancedScroller scroller;
        private RectTransform prefabRect;
        [SerializeField] private SongCellView cellPrefab;

        void Start()
        {
            prefabRect = cellPrefab.GetComponent<RectTransform>();
            scroller = GetComponent<EnhancedScroller>();
            scroller.Delegate = this;
            scroller.ReloadData();
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            SongCellView cellView = scroller.GetCellView(cellPrefab) as SongCellView;
            cellView.Bind(GameVar.ChartInfos[dataIndex]);
            return cellView;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => prefabRect.rect.height;

        public int GetNumberOfCells(EnhancedScroller scroller) => GameVar.ChartInfos.Count;

    }
}