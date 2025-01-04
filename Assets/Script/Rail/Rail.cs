using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton
{
    public class Rail : MonoBehaviour
    {
        [NonSerialized] public List<Note> Notes;
        
        [SerializeField] private bool on;
        public bool On => on;
        
        [SerializeField] public RailEnum Id;

        /// <summary>
        /// 按下
        /// </summary>
        public void Tap(InputAction.CallbackContext ctx)
        {
            on = true;
        }

        /// <summary>
        /// 松开
        /// </summary>
        public void Release(InputAction.CallbackContext ctx)
        {
            on = false;
        }
    }
}