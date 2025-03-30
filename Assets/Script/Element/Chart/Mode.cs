using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Megaton.Abstract
{
    /// <summary>
    /// 游玩模式，负责进行场景的初始化和流程指引
    /// </summary>
    public abstract class Mode
    {
        #region 字符串对象转换
        /// <summary>
        /// 模式在谱面文件中的字符表示属性
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class IdentityStringAttribute : Attribute
        {
            public string Id;
            public IdentityStringAttribute(string id)
            {
                this.Id = id;
            }
        }

        /// <summary>
        /// 字符串和类型的字典
        /// </summary>
        private static Dictionary<string, Type> subMode = new();


        static Mode()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(Mode))) continue;
                var attribute = type.GetCustomAttribute<IdentityStringAttribute>();
                if (attribute != null)
                {
                    subMode.Add(attribute.Id, type);
                }
            }
        }

        public static bool ValidMode(string id) => subMode.ContainsKey(id);

        public static Mode GetMode(string id) => Activator.CreateInstance(subMode[id]) as Mode;

        #endregion


        /// <summary>
        /// 绑定输入
        /// </summary>
        /// <param name="inputActions"></param>
        /// <param name="rails"></param>
        public abstract void InputBinding(InputMap inputActions,RailCollection rails);

        /// <summary>
        /// 解除轨道输入绑定
        /// </summary>
        /// <param name="inputActions"></param>
        /// <param name="rails"></param>
        public abstract void InputRelease(InputMap inputActions, RailCollection rails);
        
        /// <summary>
        /// 解析轨道ID
        /// </summary>
        /// <param name="id">模型下字符</param>
        /// <returns>轨道ID</returns>
        public abstract RailEnum ParseRailRelection(string id);

        /// <summary>
        /// 解析指令,默认token[0]表示切分音类型,token[1]表示轨道,token[2]表示类型
        /// </summary>
        /// <param name="token">元素</param>
        /// <returns>指令</returns>
        public abstract Command ParseCommand(string token,int bpm, int divide);
    }
}