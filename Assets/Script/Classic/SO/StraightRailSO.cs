using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    /// <summary>
    /// 直轨道的场景内物体
    /// </summary>
    public class StraightRailSO : RailSO
    {
        [SerializeField] private ParticleSystem feedback;
        [SerializeField] private ParticleSystem.MainModule mainModule;
        private float timer = 0;

        public override void Feedback(bool success,bool ifcontinue)
        {
            if(success) feedback.Play();
            //缓冲
            if(ifcontinue)
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

        void Update()
        {

        }
    }
}