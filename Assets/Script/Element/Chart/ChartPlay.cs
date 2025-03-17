using UnityEngine;
using System;
using System.Collections.Generic;
using Megaton.Abstract;
using MikanLab;
using Unity.VisualScripting;

namespace Megaton
{
    [Serializable]
    public class ChartPlay
    {
        public ChartInfo Info;
        public AudioClip Music;
        public Dictionary<RailEnum, List<Command>> Content = new();
        public int Quantity;
        public int Weight;

        private float timeAcc = 0f;

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <param name="command">谱面单行内容</param>
        public void ParseCommand(string command)
        {
            string[] token = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int divide = int.Parse(token[0]);
            for (int i = 1; i < token.Length; i++)
            {
                //一个子音符组加时一次
                if(divide != 0) timeAcc += 60f / (Info.BPM * divide / 4);
                
                //拆分音符组
                string[] subToken = token[i].Split("/");
                foreach (string s in subToken)
                {
                    string[] split = SplitNumber(s);
                    
                    //解析内容
                    RailEnum rail = GameVar.PlayMode.ParseRailRelection(split[0]);
                    var item = GameVar.PlayMode.ParseCommand(split[1], Info.BPM, divide);

                    //添加到列表
                    if (item != null && rail != RailEnum.Undefined)
                    {
                        item.ExactTime = timeAcc;
                        if (rail != RailEnum.Camera)
                        {
                            Weight += (item as Note).Weight;
                            Quantity++;
                        }
                        if (!Content.ContainsKey(rail)) Content.Add(rail, new());
                        Content[rail].Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 分割字符串中的第一个数字
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string[] SplitNumber(string token)
        {
            int i = 0;
            while (i < token.Length && token[i] >= '0' && token[i] <= '9') i++;

            if (i == token.Length) return new string[2] { token, "" };
            else return new string[2] { token.Substring(0, i), token.Substring(i) };
        }

        /// <summary>
        /// 获取摄像头指令
        /// </summary>
        /// <returns>指令列表</returns>
        public List<CameraEffect> GetCameraCommands()
        {
            if (Content.ContainsKey(RailEnum.Camera)) return Content[RailEnum.Camera].ConvertAll(x => x as CameraEffect);
            else return new();
        }

        /// <summary>
        /// 获取各个轨道指令
        /// </summary>
        /// <returns>指令列表字典</returns>
        public Dictionary<RailEnum, List<Command>> GetRailCommands() => Content;
    }
}