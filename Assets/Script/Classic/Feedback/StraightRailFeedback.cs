using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    /// <summary>
    /// 直轨道动效
    /// </summary>
    public class StraightRailFeedback : RailFeedback
    {
        [SerializeField] private MeshRenderer maskRenderer;
        
        [SerializeField] private ParticleSystem feedback;
        [SerializeField] private ParticleSystem.MainModule mainModule;
        private float timer = 0;
        private Material maskMaterial = null;

        public override void TapFeedback(bool holdDown)
        {
            if(maskMaterial == null) maskMaterial = maskRenderer.material;
            maskMaterial.SetFloat("_Hold", holdDown ? 1 : 0);
        }

        public override void JudgeFeedback(bool success,bool @continue)
        {
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