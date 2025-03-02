using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 选歌控制器
    /// </summary>
    public class ScrollViewController : MonoBehaviour,IEnhancedScrollerDelegate
    {
        private EnhancedScroller scroller;
        [SerializeField] private SongCellView cellPrefab;

        void Start()
        {
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

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => 70f;

        public int GetNumberOfCells(EnhancedScroller scroller) => GameVar.ChartInfos.Count;

    }
}