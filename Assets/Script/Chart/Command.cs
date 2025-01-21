using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 指令，是Note的父级概念，除了Note之外的谱面演出也属于Command比如变BPM
    /// </summary>
    public class Command
    {
        public float ExactTime;
    }
}
