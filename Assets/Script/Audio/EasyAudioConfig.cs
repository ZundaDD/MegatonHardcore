using System;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton
{
    [CreateAssetMenu(fileName = "config",menuName = "Megaton/AudioConfig", order = 0)]
    public class EasyAudioConfig : ScriptableObject
    {
        public List<Config> Configs;
        [Serializable]
        public class Config
        {
            public AudioEffect name;
            public AudioClip clip;
        }
    }
}