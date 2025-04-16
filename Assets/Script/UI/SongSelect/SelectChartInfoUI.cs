using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 控制选中歌曲的显示
    /// </summary>
    public class SelectChartInfoUI : MonoBehaviour
    {
        private static SelectChartInfoUI ins;
        public static SelectChartInfoUI Ins => ins;

        private float changeSpace = InOutText.limit;
        private ChartInfo curSelection;
        private Image curHint;
        // 场景引用
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private InOutText title;
        [SerializeField] private InOutText composer;
        [SerializeField] private InOutText bpm;
        [SerializeField] private InOutImage cover;
        [SerializeField] private InOutText bestScore;
        [SerializeField] private InOutText bestRank;
        private AudioSource musicPlayer;

        private void Awake() => ins = this;

        void Start()
        {
            musicPlayer = GetComponent<AudioSource>();
            if (GameVar.ChartInfos.Count != 0)
                ChangeSelected(GameVar.ChartInfos[0],
                    scroller.GetCellViewAtDataIndex(0).GetComponent<SongCellView>().selectHint);
            else
            {
                title.ChangeTo("Empty", false);
                composer.ChangeTo("Empty", false);
                bpm.ChangeTo("Empty", false);
            }
        }

        void Update()
        {
            if(changeSpace < InOutText.limit) changeSpace += Time.deltaTime;
        }

        public void RealertSelected(Image reSelection)
        {
            curHint?.gameObject.SetActive(false);
            curHint = reSelection;
            curHint.gameObject.SetActive(true);
        }

        public void ChangeSelected(ChartInfo info, Image newSelection)
        {
            //点击冷却
            if (changeSpace < InOutText.limit) return;
            changeSpace = 0;
            
            //再次点击则开始游玩
            if (curSelection == info)
            {
                SongSelectController.StartPlay(info);
                return;
            }

            //绑定最新的选择
            curSelection = info;
            curHint?.gameObject.SetActive(false);
            curHint = newSelection;
            curHint.gameObject.SetActive(true);

            //显示信息和音乐改变
            title.ChangeTo(info.Title);
            composer.ChangeTo(info.Composer);
            bpm.ChangeTo("BPM:" + info.BPM);
            bestRank.ChangeTo(info.Score.BestRank);
            bestScore.ChangeTo(info.Score.BestRank == "" ? "" : $"{info.Score.BestScore.ToString().PadLeft(8,'0')}");
            cover.ChangeTo(CoverLoader.Path2Sprite(info.RootDir));
            musicPlayer.Pause();
            musicPlayer.clip = MusicLoader.Path2Clip(info.RootDir, true);
            musicPlayer.Play();
        }

        public bool IfSelected(ChartInfo info) => info == null ? false : info == curSelection;
    }
}