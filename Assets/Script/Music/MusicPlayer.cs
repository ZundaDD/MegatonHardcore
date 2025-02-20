using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 用于控制音乐播放的总对象
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource musicSource;
        private bool ifCommand = false;
        private bool ifStarted = false;
        private bool ifPaused = false;

        /// <summary>
        /// 从开始播放计算的准确时间
        /// </summary>
        public static float ExactTime { get; private set; } = 0f;

        #region 计时变量
        [SerializeField] private int prepareFrame = 120;
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
            if (ifCommand) frameCount++;
            if (!ifStarted && ifCommand) Align();
            if (ifStarted && !ifPaused) ExactTime += Time.fixedDeltaTime;
        }

        /// <summary>
        /// 播放指令
        /// </summary>
        public void Play(AudioClip clip)
        {
            ifCommand = true;
            startDSP = (float) AudioSettings.dspTime;
            musicSource.clip = clip;
            musicSource.PlayScheduled(AudioSettings.dspTime + Time.fixedDeltaTime * prepareFrame);
            frameCount = 0;
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            ifPaused = true;
        }

        /// <summary>
        /// 对齐时间
        /// </summary>
        public void Align()
        {
            
            float gap = (float)AudioSettings.dspTime - startDSP;
            if (gap < Time.fixedDeltaTime * prepareFrame) return;
            ifStarted = true;
            Debug.Log(string.Format("<color=#9aff99>Offset</color>:{0}ms", gap - Time.fixedDeltaTime * prepareFrame));
            ExactTime = gap - Time.fixedDeltaTime * prepareFrame;
        }
        
        /// <summary>
        /// 恢复播放
        /// </summary>
        public void Restore()
        {
            ifPaused = false;
        }
    }
}