using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Megaton.Generic
{
    /// <summary>
    /// 设置变量抽象基类
    /// </summary>
    [Serializable]
    public abstract class SettingVarible
    {
        /// <summary>
        /// 可否循环操作
        /// </summary>
        [SerializeField]protected bool cycleable;
        
        /// <summary>
        /// 返回加减的状态
        /// </summary>
        /// <returns>[是否可加，是否可减]</returns>
        public abstract bool[] SwitchState(); 
        
        /// <summary>
        /// 给出的描述
        /// </summary>
        public abstract string Display { get; }

        /// <summary>
        /// 值自增
        /// </summary>
        public abstract void Add();

        /// <summary>
        /// 值自减
        /// </summary>
        public abstract void Minus();
    }

    [Serializable]
    public class BoolVarible : DiscreteVarible<bool>
    {
        public BoolVarible(List<Choice> choices, int defaultIdx = 0, bool cycleable = true) : base(choices, defaultIdx, cycleable) { }

        public static BoolVarible GetBoolVarible(bool value, bool cycleable = true)
        {
            return new BoolVarible(new()
            {
                new (){description = "否",value= false},
                new (){description = "是",value = true}
            },
            value ? 1 : 0,
            cycleable);
        }
    }

    [Serializable]
    public class RangeVarible : DiscreteVarible<float>
    {
        [SerializeField] public float precision;

        public override string Display => Value.ToString($"N{precision}");

        public RangeVarible(List<Choice> choices, int defaultIdx = 0, bool cycleable = true) : base(choices, defaultIdx, cycleable) { }

        public static RangeVarible GetRangeVarible(float min, float max, float value, float step,uint precision)
        {
            int length = (int)((max - min) / step);
            int index = (int)((value - min) / (max - min) * length);
            List<Choice> choices = new List<Choice>();
            for (int i = 0; i < length; i++)
            {
                choices.Add(new() { description = "", value = min + i * step });
            }

            var rv = new RangeVarible(choices, index, false);
            rv.precision = precision;
            return rv;
        }

    }

    [Serializable]
    public class DiscreteVarible<T> : SettingVarible
    {
        [Serializable]
        public class Choice
        {
            public string description;
            public T value;
        }

        [SerializeField] private int curIndex = 0;
        [SerializeField] private List<Choice> choices = new();

        public T Value => choices[curIndex].value;

        public override string Display => choices[curIndex].description == "" ?
            choices[curIndex].value.ToString() : choices[curIndex].description;

        public DiscreteVarible(List<Choice> choices,int defaultIdx = 0,bool cycleable = true)
        {
            this.choices = choices;
            curIndex = defaultIdx;
            this.cycleable = cycleable;
            this.choices = choices;
        }

        public override bool[] SwitchState()
        {
            return cycleable ? new bool[2] { true, true } :
                new bool[2] { curIndex < choices.Count - 1, curIndex > 0 };
        }

        public override void Add() => curIndex = (curIndex + 1) % choices.Count;

        public override void Minus() => curIndex = (curIndex - 1 + choices.Count) % choices.Count;

    }
}