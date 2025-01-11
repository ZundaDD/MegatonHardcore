using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Megaton
{
    [Serializable]
    public class Chart
    {
        public ChartInfo Info;
        public ChartMusic Music;
        public List<Command> Content;

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <param name="command">谱面单行内容</param>
        public void ParseCommand(string command)
        {

        }
    }
}