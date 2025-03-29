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
        private Dictionary<AudioEffect,AudioClip> clips = new();
        [SerializeField] private EasyAudioConfig config;
        private AudioSource player;

        private void Awake()
        {
            if(ins != null) Destroy(gameObject);
            else ins = this;
            DontDestroyOnLoad(gameObject);

            foreach(var config in  config.Configs)
            {
                if (!clips.ContainsKey(config.name))
                    clips.Add(config.name, null);
                clips[config.name] = config.clip;
            }
            player = gameObject.GetComponent<AudioSource>();
        }

        public static void PlayEffect(AudioEffect index) => ins.player.PlayOneShot(ins.clips[index]);
    }
}