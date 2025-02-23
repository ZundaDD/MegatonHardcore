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

        //公共部分
        public List<CameraEffect> Commands { get; private set; }
        public Camera Camera { get; private set; }
        [HideInInspector] public float JudgeLineZ { get; private set; } = 0f;

        //私有部分
        [SerializeField] private GameObject judgeLine;
        [SerializeField] private float step = 0.003f;

        private void Awake()
        {
            if (!GameVar.IfInitialed) return;

            Ins = this;
            JudgeLineZ = judgeLine.transform.position.z;
            GameVar.Velocity = GameVar.CurPlay.Info.BPM * step;
            Camera = GetComponent<Camera>();
        }

        public void Update()
        {
            if(GameVar.IfPrepare && !GameVar.IfPaused) MoveForward(GameVar.Velocity * Time.deltaTime);    
        }

        public static void LoadCommands(List<Command> commands) => Ins.Commands = commands.ConvertAll(x => x as CameraEffect);

        public static void Align(float timeOffset) => Ins.MoveForward(timeOffset * GameVar.Velocity);
        
        public void MoveForward(float physicalOffset) => transform.position += new Vector3(0, 0, physicalOffset);

    }
}