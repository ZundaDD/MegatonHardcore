using Cysharp.Threading.Tasks;
using Megaton.Web;
using System;
using Megaton.Web.Sayo;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Megaton.UI;

namespace Megaton
{
    public class DownloadSceneController : MonoBehaviour
    {
        #region public部分
        public static Action<int> OnStateChanged;

        public static DownloadSceneController Ins { get; private set; } = null;
        
        /// <summary>
        /// 0表示正在获取谱面列表，1表示获取成功，2表示获取失败
        /// </summary>
        public int State { 
            get => state;
            private set
            {
                state = value;
                OnStateChanged?.Invoke(value);
            }
        }
        private int state = 0;
        #endregion

        [SerializeField] private ResultList resultList;
        [SerializeField] private OSUHandler osuHandler;
        [SerializeField] private int pageCount = 10;

        private BeatmapListResponse curResponse = null;
        private SayoHandler sayoHandler = null;
        private List<FullChart> chartInfos = new();
        private int maxOffset = 0;
        private int curOffset = 0;

        public void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
            Ins = this;
        }

        private async void Start()
        {
            sayoHandler = new SayoHandler();

            /*osuHandler.DownLoadChart(2342660);
            osuHandler.DownLoadChart(2340628);
            osuHandler.DownLoadChart(1872276);
            osuHandler.DownLoadChart(1107469);*/

            await RefreshList(0);
        }
        
        /// <summary>
        /// 下载某个难度的谱面
        /// </summary>
        public void DownloadBeatmap(int bid)
        {
            osuHandler.DownLoadChart(bid);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="page">页面</param>
        /// <returns></returns>
        private async UniTask RefreshList(int page)
        {
            State = 0;
            resultList.Clear();
            chartInfos.Clear();

            curResponse = await sayoHandler.GetManiaList(page, pageCount);
            if (curResponse != null)
            {
                foreach (var chartInfo in curResponse.data)
                {
                    //获取谱面信息
                    var chart = await GetChartInfo(chartInfo.sid, chartInfo);
                    if (chart == null) continue;
                    else chartInfos.Add(chart);
                }

                //更新offset
                curOffset = page;
                if(curResponse.endid == 0) maxOffset = curOffset + chartInfos.Count;
                else maxOffset = curOffset + curResponse.endid;
            }
            
            //构建列表
            resultList.Construct(chartInfos);

            //设置状态位
            if (curResponse == null || chartInfos.Count == 0) State = 2;
            else State = 1;
        }

        /// <summary>
        /// 得到完整的谱面信息
        /// </summary>
        /// <param name="sid">song id</param>
        /// <returns></returns>
        private async UniTask<FullChart> GetChartInfo(int sid, Web.Sayo.ChartInfo chartInfo)
        {
            var info = await sayoHandler.GetManiaInfo(sid);
            var cover = await sayoHandler.GetCover(sid);
            //cover = await ResizeCover(cover);
            if (info == null || cover == null) return null;

            FullChart chart = new();
            chart.bid_data = new();
            chart.cover = cover;
            //筛选有效的难度
            foreach (var bid in info.data.bid_data) chart.bid_data.Add(bid);
            
            chart.bpm = info.bpm;
            chart.bids_amounts = chart.bid_data.Count;
            chart.info = chartInfo;

            return chart;
        }
    }
}
