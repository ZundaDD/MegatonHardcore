using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Megaton.Abstract;

namespace Megaton
{
    [Serializable]
    public class ChartPlay
    {
        public ChartInfo Info;
        public AudioClip Music;
        public Dictionary<RailEnum, List<Command>> Content = new();
        public int Quantity;

        private float timeAcc = 0f;

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <param name="command">谱面单行内容</param>
        public void ParseCommand(string command)
        {
            string[] token = command.Split(' ');

            //解析时值
            int divide = int.Parse(token[0]);
            if(divide != 0) timeAcc += 60f / (Info.BPM * divide / 4);
            
            if (token.Length < 3) return;
            //解析内容
            RailEnum rail = GameVar.PlayMode.ParseRailRelection(token[1]);
            var commandObj = GameVar.PlayMode.ParseCommand(token, Info.BPM);
            
            //添加到列表
            if(commandObj != null && rail != RailEnum.Undefined)
            {
                commandObj.ExactTime = timeAcc;
                if (rail != RailEnum.Camera) Quantity++;
                if (!Content.ContainsKey(rail)) Content.Add(rail, new());
                Content[rail].Add(commandObj);
                //Debug.Log(commandObj.GetType().ToString() + commandObj.ExactTime.ToString());
            }
        }

        /// <summary>
        /// 获取摄像头指令
        /// </summary>
        /// <returns>指令列表</returns>
        public List<Command> GetCameraCommands()
        {
            if (Content.ContainsKey(RailEnum.Camera)) return Content[RailEnum.Camera];
            else return new();
        }

        /// <summary>
        /// 获取各个轨道指令
        /// </summary>
        /// <returns>指令列表字典</returns>
        public Dictionary<RailEnum, List<Command>> GetRailCommands() => Content;
    }
}