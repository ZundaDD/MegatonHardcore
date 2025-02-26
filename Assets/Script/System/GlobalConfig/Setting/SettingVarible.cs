using System;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton.Generic
{
    /// <summary>
    /// 设置变量接口
    /// </summary>
    public interface ISettingVarible
    {
        /// <summary>
        /// 值自增
        /// </summary>
        public void Add();

        /// <summary>
        /// 值自减
        /// </summary>
        public void Minus();
    }

    [Serializable]
    public class RangeVarible<T>
    {
        [SerializeField] private T max;
        [SerializeField] private T min;
        [SerializeField] private T value;
        [SerializeField] private T step;

        public T Value => value;

        public RangeVarible(T max, T min, T value, T step)
        {
            this.max = max;
            this.min = min;
            this.value = value;
            this.step = step;
        }
    }

    [Serializable]
    public class DiscreteVarible<T>
    {
        [SerializeField] int CurIndex = 0;
        [SerializeField] List<string> Description = null;
        [SerializeField] List<T> Choices = new();

        public T Value => Choices[CurIndex];

        public string Display => Description == null ? Value.ToString() : Description[CurIndex];

        public DiscreteVarible(List<T> choices,int defaultIdx = 0,List<string> description = null)
        {
            Choices = choices;
            CurIndex = defaultIdx;
            Description = description;
        }
    }

    

}