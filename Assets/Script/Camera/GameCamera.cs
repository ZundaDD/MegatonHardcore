using System.Collections.Generic;
using UnityEngine;
using Megaton.Abstract;

namespace Megaton
{
    /// <summary>
    /// 绑定在摄像机上，通过接口控制相机
    /// </summary>
    public class GameCamera : MonoBehaviour
    {
        public static GameCamera Ins { get; private set; }
        public List<Command> Commands;

        private void Awake()
        {
            Ins = this;
        }

        public void Update()
        {
            MoveForward(0.05f);    
        }

        public static void LoadCommands(List<Command> commands) => Ins.Commands = commands;

        public void MoveForward(float offset)
        {
            transform.position += new Vector3(0, 0, offset);
        }

    }
}