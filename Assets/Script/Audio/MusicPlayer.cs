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
        public Action OnEnd {  get; set; }


        private AudioSource musicSource;

        #region 计时变量
        private float startDSP = 0f;
        private bool command = false;
        private bool formState = false;
        #endregion

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
            ExactTime = 0;
        }

        private void Update()
        {
            //这里的顺序是，command置状态，再下一次Update时
            //实际地执行Play的指令，但此次攻击中不累计时间，也不移动摄像头
            //从下一次Update开始，每次都要累计时间，并移动摄像头
            //直到实际dsp时间找过了预定的dsp时间，此时进行一次音乐时间对累计时间校正
            if (GameVar.IfPrepare && !GameVar.IfPaused)
            {
                GameCamera.Ins.MoveForward(GameVar.Velocity * Time.deltaTime);
                ExactTime += Time.deltaTime;
                RailCollection.Ins.TryJudge();
            }
            if (!GameVar.IfStarted && GameVar.IfPrepare) Align();
            if (command) Play();
            EndCheck();
        }

        public void CommandPlay(AudioClip clip)
        {
            musicSource.clip = clip;
            command = true;
        }

        /// <summary>
        /// 播放指令
        /// </summary>
        public void Play()
        {
            GameVar.IfPrepare = true;
            command = false;
            ExactTime = -Time.fixedDeltaTime * GameVar.PrepareFrame;
            startDSP = (float) AudioSettings.dspTime;
            musicSource.PlayScheduled(startDSP + Time.fixedDeltaTime * GameVar.PrepareFrame);
        }

        /// <summary>
        /// 对齐时间
        /// </summary>
        public void Align()
        {
            float dsp = (float)AudioSettings.dspTime;
            float gap = dsp - startDSP - Time.fixedDeltaTime * GameVar.PrepareFrame;
            if (gap < 0) return;

            //音乐时间与累计时间对比
            gap -= ExactTime;
            GameVar.IfStarted = true;
            GameCamera.Align(gap);
            ExactTime += gap;

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