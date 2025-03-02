using MikanLab;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 全局音效播放器
    /// </summary>
    public class GlobalEffectPlayer : MonoBehaviour
    {
        private static GlobalEffectPlayer ins;
        [SerializeField] private EnumArray<AudioEffect,AudioClip> clips;
        private AudioSource player;

        private void Awake()
        {
            if(ins != null) Destroy(gameObject);
            else ins = this;
            DontDestroyOnLoad(gameObject);
            player = gameObject.GetComponent<AudioSource>();
        }

        public static void PlayEffect(AudioEffect index) => ins.player.PlayOneShot(ins.clips[index]);
    }
}