using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 控制选中歌曲的显示
    /// </summary>
    public class SelectedDisplay : MonoBehaviour
    {
        private static SelectedDisplay ins;
        public static SelectedDisplay Ins => ins;

        private float changeSpace = InOutText.limit;
        private ChartInfo curSelection;
        private LoopFlash curHint;
        // 场景引用
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private InOutText title;
        [SerializeField] private InOutText composer;
        [SerializeField] private InOutText bpm;
        [SerializeField] private InOutImage cover;
        private AudioSource musicPlayer;

        private void Awake()
        {
            ins = this;
        }
        void Start()
        {
            musicPlayer = GetComponent<AudioSource>();
            ChangeSelected(GameVar.Ins.ChartInfos[0],
                scroller.GetCellViewAtDataIndex(0).GetComponent<SongCellView>().selectHint);
        }

        void Update()
        {
            if(changeSpace < InOutText.limit) changeSpace += Time.deltaTime;
        }

        public void ChangeSelected(ChartInfo info, LoopFlash newSelection)
        {
            if (curSelection == info) return;
            if (changeSpace < InOutText.limit) return;
            changeSpace = 0;

            //绑定最新的选择
            curSelection = info;
            curHint?.gameObject.SetActive(false);
            curHint = newSelection;
            curHint.gameObject.SetActive(true);

            //显示信息和音乐改变
            title.ChangeTo(info.Title);
            composer.ChangeTo(info.Composer);
            bpm.ChangeTo("BPM:" + info.BPM);
            cover.ChangeTo(CoverLoader.Path2Sprite(info.RootDir));
            musicPlayer.Pause();
            musicPlayer.clip = MusicLoader.Path2Clip(info.RootDir);
            musicPlayer.Play();
        }

        public bool IfSelected(ChartInfo info) => info == null ? false : info == curSelection;
    }
}