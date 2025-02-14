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
        public ChartMusic Music;
        public List<Command> Content;

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <param name="command">谱面单行内容</param>
        public void ParseCommand(string command)
        {

        }

        /// <summary>
        /// 获取摄像头指令
        /// </summary>
        /// <returns>指令列表</returns>
        public List<Command> GetCameraCommands()
        {
            return Content;
        }

        /// <summary>
        /// 获取各个轨道指令
        /// </summary>
        /// <returns>指令列表字典</returns>
        public Dictionary<RailEnum,List<Command>> GetRailCommands()
        {
            return new();
        }
    }
}