using System;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 用于控制音乐播放的总对象
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        /// <summary>
        /// 从开始播放计算的准确时间
        /// </summary>
        public static float ExactTime { get; private set; } = 0f;

        /// <summary>
        /// 结束播放时回调
        /// </summary>
        public Action OnEnd;
        private AudioSource musicSource;

        #region 计时变量
        //private int frameCount = -1;
        private float startDSP = 0f;
        private bool formState = false;
        #endregion

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
            ExactTime = 0;
        }

        private void Update()
        {
            if (!GameVar.IfStarted && GameVar.IfPrepare) Align();
            if (GameVar.IfPrepare && !GameVar.IfPaused) ExactTime += Time.deltaTime;
            EndCheck();
        }

        /// <summary>
        /// 播放指令
        /// </summary>
        public void Play(AudioClip clip)
        {
            GameVar.IfPrepare = true;
            startDSP = (float) AudioSettings.dspTime;
            musicSource.clip = clip;
            ExactTime = -Time.fixedDeltaTime * GameVar.PrepareFrame;
            musicSource.PlayScheduled(AudioSettings.dspTime + Time.fixedDeltaTime * GameVar.PrepareFrame);
        }

        /// <summary>
        /// 对齐时间
        /// </summary>
        public void Align()
        {
            float dsp = (float)AudioSettings.dspTime;
            float gap = dsp - startDSP - Time.fixedDeltaTime * GameVar.PrepareFrame;
            if (gap < 0) return;

            //与实际时间对比
            gap -= ExactTime;
            GameVar.IfStarted = true;
            GameCamera.Align(gap);
            ExactTime = gap;

            Debug.Log(string.Format("<color=#9aff99>Offset</color>:{0}ms", gap));
        }
        
        /// <summary>
        /// 音频播放结束
        /// </summary>
        public void EndCheck()
        {
            bool curState = musicSource.isPlaying;
            if (!curState && formState) OnEnd();
            formState = curState;
        }
    }
}