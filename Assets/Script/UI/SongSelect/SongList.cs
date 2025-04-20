using DanielLochner.Assets.SimpleScrollSnap;
using EnhancedUI.EnhancedScroller;
using System;
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
        [SerializeField] private int maxPanelCount = 20;
        [SerializeField] private int firstSelectPanel = 0;


        private void Start()
        {
            if(GameVar.ChartInfos.Count == 0) return;
            

            for(int i = 0;i < maxPanelCount ; ++i) 
            {
                var cell = Instantiate(cellPrefab, contentRect);
                //保证一定有20个cell以形成完整的视图
                cell.Bind(GameVar.ChartInfos[i % GameVar.ChartInfos.Count]);
            }

            //手动进行panel的排布
            scroller.ArrangePanel();
        }

        public void Update()
        {
            clearTime = Mathf.Max(reserveTime - Time.deltaTime, 0f);
            accTime += navigationInput == 0 ? 0 : Time.deltaTime;
            if (accTime > navigationTherehold)
            {
                if(navigationInput > 0) scroller.GoToNextPanel();
                else if(navigationInput < 0) scroller.GoToPreviousPanel();
    
                accTime -= navigationTherehold;
            }
            if(clearTime == 0) scrollInput = 0f;
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
                Debug.Log($"Next {over}");
                scrollInput -= over * scrollTherehold;

                //反映到Scroller上
                for(int i = 0; i < Math.Abs(over); ++i)
                {
                    if (over > 0) scroller.GoToNextPanel();
                    else scroller.GoToPreviousPanel();
                }
            }
        }

        /// <summary>
        /// 处理导航上下选歌事件
        /// </summary>
        public void OnNavigation(InputAction.CallbackContext ctx)
        {
            float delta = ctx.ReadValue<Vector2>().y;
            
            //保证每次按下一定切换一次
            if(delta > 0) scroller.GoToNextPanel();
            else if(delta < 0) scroller.GoToPreviousPanel();


            if (delta == 0) navigationInput = 0;
            else navigationInput = delta;
        }
    }
}