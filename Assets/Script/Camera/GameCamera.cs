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
        public Camera Camera { get; private set; }

        [SerializeField] private float step = 0.001f;
        private int curBpm;

        private void Awake()
        {
            Ins = this;
        }

        private void Start()
        {
            Camera = GetComponent<Camera>();
            curBpm = GameVar.Ins.CurPlay.Info.BPM;
        }

        public void FixedUpdate()
        {
            MoveForward(curBpm * step);    
        }

        public static void LoadCommands(List<Command> commands) => Ins.Commands = commands;

        public void MoveForward(float offset)
        {
            transform.position += new Vector3(0, 0, offset);
        }

    }
}