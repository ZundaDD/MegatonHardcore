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
        public override void Feedback(bool success,bool ifcontinue)
        {
            if (ifcontinue) mainModule.loop = true;
            else mainModule.loop = false;
            if (success) feedback.Play();
            if (ifcontinue && feedback.isPlaying == false) feedback.Play();
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