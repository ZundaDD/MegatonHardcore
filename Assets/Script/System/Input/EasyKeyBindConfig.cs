using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton
{
    [CreateAssetMenu(fileName = "config", menuName = "Megaton/KeyBindConfig", order = 0)]
    public class EasyKeyBindConfig : ScriptableObject
    {
        public List<Config> Configs;
        [Serializable]
        public class Config
        {
            public string name;
            public InputActionReference action;
        }

    }
}