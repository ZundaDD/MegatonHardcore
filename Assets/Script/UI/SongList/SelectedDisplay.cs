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
        // 场景引用
        [SerializeField] private InOutText title;
        [SerializeField] private InOutText composer;
        [SerializeField] private InOutText bpm;
        [SerializeField] private InOutImage cover;
        [SerializeField] private AudioSource musicPlayer;

        void Start()
        {
            ins = this;

            musicPlayer = GetComponent<AudioSource>();
            ChangeSelected(GameVar.Ins.ChartInfos[1]);
        }

        void Update()
        {
            if(changeSpace < InOutText.limit) changeSpace += Time.deltaTime;
        }

        public void ChangeSelected(ChartInfo info)
        {
            if (curSelection == info) return;
            if (changeSpace < InOutText.limit) return;
            changeSpace = 0;

            curSelection = info;

            title.ChangeTo(info.Title);
            composer.ChangeTo(info.Composer);
            bpm.ChangeTo("BPM:" + info.BPM);
            cover.ChangeTo(CoverLoader.Path2Sprite(info.RootDir));
            musicPlayer.Pause();
            musicPlayer.clip = MusicLoader.Path2Clip(info.RootDir);
            musicPlayer.Play();
        }

        
    }
}