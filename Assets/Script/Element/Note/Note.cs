using System;
using UnityEngine;

namespace Megaton.Abstract
{
    /// <summary>
    /// 最基本的音符
    /// </summary>
    public abstract class Note : Command
    {
        /// <summary>
        /// 判定事件
        /// </summary>
        public Action<bool, bool> OnJudge;

        /// <summary>
        /// 判定结束事件
        /// </summary>
        public Action<JudgeEnum> OnResult;

        /// <summary>
        /// 判定起始偏差
        /// </summary>
        public abstract float JudgeStart { get; }

        /// <summary>
        /// 判定结束偏差
        /// </summary>
        public abstract float JudgeEnd { get; }

        /// <summary>
        /// 尝试根据轨道状态进行判定
        /// </summary>
        /// <param name="railState">轨道的输入状态</param>
        /// <param name="formState">轨道上一次输入状态</param>
        /// <returns>是否得到判定结果</returns>
        public abstract bool Judge(bool railState,bool formState);

        /// <summary>
        /// 返回判定结果
        /// </summary>
        /// <returns>判定结果</returns>
        public abstract JudgeEnum GetResult();

        /// <summary>
        /// 生成对应SO
        /// </summary>
        /// <param name="prefab">对应预制件</param>
        /// <returns>生成物体</returns>
        public virtual GameObject GenerateSO(GameObject prefab)
        {
            var go = MonoBehaviour.Instantiate(prefab);
            go.GetComponent<NoteSO>().Bind(this);
            return go;
        }
    }

}