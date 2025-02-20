using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 用于控制音乐播放的总对象
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource musicSource;

        /// <summary>
        /// 从开始播放计算的准确时间
        /// </summary>
        public static float ExactTime { get; private set; } = 0f;

        #region 计时变量
        private int frameCount = -1;
        private float startDSP = 0f;
        #endregion

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
            ExactTime = 0;
        }

        private void FixedUpdate()
        {
            if (GameVar.IfPrepare) frameCount++;
            if (!GameVar.IfStarted && GameVar.IfPrepare) Align();
            if (GameVar.IfStarted && !GameVar.IfPaused) ExactTime += Time.fixedDeltaTime;
        }

        /// <summary>
        /// 播放指令
        /// </summary>
        public void Play(AudioClip clip)
        {
            GameVar.IfPrepare = true;
            startDSP = (float) AudioSettings.dspTime;
            musicSource.clip = clip;
            musicSource.PlayScheduled(AudioSettings.dspTime + Time.fixedDeltaTime * GameVar.PrepareFrame);
            //结束回调添加
            frameCount = 0;
        }

        /// <summary>
        /// 对齐时间
        /// </summary>
        public void Align()
        {
            float gap = (float)AudioSettings.dspTime - startDSP - Time.fixedDeltaTime * GameVar.PrepareFrame;
            if (gap < 0) return;

            GameVar.IfStarted = true;
            GameCamera.Align(gap);
            ExactTime = gap;

            Debug.Log(string.Format("<color=#9aff99>Offset</color>:{0}ms", gap));
        }
        
    }
}