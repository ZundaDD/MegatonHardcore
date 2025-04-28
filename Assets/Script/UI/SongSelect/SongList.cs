using DanielLochner.Assets.SimpleScrollSnap;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.UI
{
    /// <summary>
    /// 选歌控制器
    /// </summary>
    public class SongList : MonoBehaviour
    {
        private float scrollInput = 0f;
        private float clearTime = 0f;

        private List<SongCellView> cells = new();
        private float navigationInput = 0f;
        private float accTime = 0f;

        [Header("GO引用")]
        [SerializeField] private RectTransform contentRect;
        [SerializeField] private SongCellView cellPrefab;
        [SerializeField] private SimpleScrollSnap scroller;

        [Header("参数设置")]
        [SerializeField] private float reserveTime = 0.2f;
        [SerializeField,Range(0.51f,1.0f)] private float scrollTherehold = 0.75f;
        [SerializeField,Range(0.1f,1f)] private float navigationTherehold = 0.2f;
        [SerializeField] private int maxPanelCount = 21;
        public int firstSelectPanel = 10;

        private void Start()
        {
            if(GameVar.ChartInfos.Count == 0) return;
            
            for(int i = 0;i < maxPanelCount ; ++i) 
            {
                var cell = Instantiate(cellPrefab, contentRect);
                cell.name = $"Song {i}";
                cells.Add(cell.GetComponent<SongCellView>());
                //保证一定有足够的cell以形成完整的视图
                cell.Bind(GameVar.ChartInfos[i % GameVar.ChartInfos.Count]);
            }

            //手动进行panel的排布
            scroller.Bind += 
                (cellidx, realidx) => cells[cellidx].Bind(GameVar.ChartInfos[realidx]);
            scroller.StartingPanel = firstSelectPanel;
            scroller.RealIndex = firstSelectPanel % GameVar.ChartInfos.Count;
            scroller.RealPanels = GameVar.ChartInfos.Count;
            scroller.VirtualPanels = maxPanelCount;
            scroller.ArrangePanel();
        }

        public void Update()
        {
            clearTime = Mathf.Max(reserveTime - Time.deltaTime, 0f);
            if(clearTime == 0) scrollInput = 0f;

            ProcessNavigation();
        }

        private void ProcessNavigation()
        {
            if (navigationInput == 0) accTime = 0;
            else
            {
                accTime += Time.deltaTime;
                int over = Mathf.FloorToInt(Mathf.Abs(accTime / navigationTherehold));
                if (over != 0)
                {
                    if (navigationInput > 0) scroller.GoUp(over);
                    else scroller.GoDown(over);
                }
                accTime -= over * navigationTherehold;
            }
        }

        /// <summary>
        /// 处理鼠标滚轮选歌事件
        /// </summary>
        public void OnScroll(InputAction.CallbackContext ctx)
        {
            //读取滚动值
            float delta = ctx.ReadValue<Vector2>().y;
            if (delta != 0) clearTime = reserveTime;

            //计算偏移值
            scrollInput += delta;
            int over = (scrollInput > 0 ? 1 : -1) * Mathf.FloorToInt(Mathf.Abs(scrollInput / scrollTherehold));
            if (over != 0)
            {
                //Debug.Log($"Next {over}");
                scrollInput -= over * scrollTherehold;

                //反映到Scroller上
                if (over > 0) scroller.GoUp(Math.Abs(over));
                else scroller.GoDown(Math.Abs(over));
            }
        }

        /// <summary>
        /// 处理导航上下选歌事件
        /// </summary>
        public void OnNavigation(InputAction.CallbackContext ctx)
        {
            float delta = ctx.ReadValue<Vector2>().y;
            
            //保证每次按下一定切换一次
            if(delta > 0) scroller.GoUp();
            else if(delta < 0) scroller.GoDown();

            navigationInput = delta;
        }

    }
}