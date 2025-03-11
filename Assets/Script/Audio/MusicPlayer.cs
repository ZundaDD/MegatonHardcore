using System;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 用于控制音乐播放的总对象
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        public static float ExactTime { get; private set; } = 0f;
        private AudioSource musicSource;

        #region 计时变量
        private int waitFrame = 0;
        private float pauseExactTime = 0;
        private float startDSP = 0f;
        private bool command = false;
        private bool formState = false;
        private float pausedTime = 0f;
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
            waitFrame = GameVar.PrepareFrame;
            musicSource.clip = clip;
            command = true;
        }

        /// <summary>
        /// 恢复指令
        /// </summary>
        public void Restore()
        {
            waitFrame = GameVar.RestoreFrame;
            GameCamera.Align(-waitFrame * Time.fixedDeltaTime);
            command = true;
        }

        /// <summary>
        /// 播放指令
        /// </summary>
        public void Play()
        {
            GameVar.IfPrepare = true;
            command = false;
            ExactTime -= Time.fixedDeltaTime * waitFrame;
            startDSP = (float)AudioSettings.dspTime;
            musicSource.time = pausedTime;
            musicSource.PlayScheduled(startDSP + Time.fixedDeltaTime * waitFrame);
        }

        /// <summary>
        /// 暂停指令
        /// </summary>
        public void Pause()
        {
            pausedTime = musicSource.time;
            pauseExactTime = ExactTime;
            musicSource.Stop();
        }

        /// <summary>
        /// 对齐时间
        /// </summary>
        public void Align()
        {
            
            float dsp = (float)AudioSettings.dspTime;
            float gap = dsp - startDSP - Time.fixedDeltaTime * waitFrame;
            if (gap < 0) return;
            
            //音乐时间与累计时间对比
            gap -= ExactTime - pauseExactTime;
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
            if (GameVar.IfStarted && ExactTime > musicSource.clip.length + 2f)
                PlayController.Ins.EndPlay();
        }
    }
}