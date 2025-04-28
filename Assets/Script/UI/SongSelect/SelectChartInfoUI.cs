using DanielLochner.Assets.SimpleScrollSnap;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 控制选中歌曲的显示
    /// 由Contorller给出显示的歌曲信息
    /// </summary>
    public class SelectChartInfoUI : MonoBehaviour
    {
        // 场景引用
        [SerializeField] private InOutText title;
        [SerializeField] private InOutText composer;
        [SerializeField] private InOutText bpm;
        [SerializeField] private InOutImage cover;
        [SerializeField] private InOutText bestScore;
        [SerializeField] private InOutText bestRank;
        private AudioSource musicPlayer;

        void Start()
        {
            musicPlayer = GetComponent<AudioSource>();
        }

        public void SetToNull()
        {
            //初始默认，防止没有歌的时候什么也不显示
            title.ChangeTo("", false);
            composer.ChangeTo("", false);
            bpm.ChangeTo("", false);
            bestRank.ChangeTo("", false);
            bestScore.ChangeTo("", false);
        }

        public void ChangeSelected(ChartInfo info)
        {
            //改变显示信息
            title.ChangeTo(info.Title);
            composer.ChangeTo(info.Composer);
            bpm.ChangeTo("BPM:" + info.BPM);
            bestRank.ChangeTo(info.Score.BestRank);
            bestScore.ChangeTo(info.Score.BestRank == "" ? "" : $"{info.Score.BestScore.ToString().PadLeft(8,'0')}");
            cover.ChangeTo(info.GetCoverSprite());
            
            //重新播放音乐
            musicPlayer.Stop();
            musicPlayer.clip = MusicLoader.Path2Clip(info.RootDir, true);
            musicPlayer.Play();
        }

    }
}