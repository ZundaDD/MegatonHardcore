using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    /// <summary>
    /// 直轨道动效
    /// </summary>
    public class StraightRailFeedback : RailFeedback
    {
        [SerializeField] private ParticleSystem feedback;
        [SerializeField] private ParticleSystem.MainModule mainModule;
        private float timer = 0;

        public override void TapFeedback(bool holdDown)
        {
            
        }

        public override void JudgeFeedback(bool success,bool @continue)
        {
            return;
            if(success) feedback.Play();
            //缓冲
            if(@continue)
            {
                timer += Time.deltaTime;
                if(timer > 2 * Time.fixedDeltaTime)
                {
                    timer = 0;
                    feedback.Play();
                }
            }
        }

        void Start()
        {
            mainModule = feedback.main;
        }
    }
}