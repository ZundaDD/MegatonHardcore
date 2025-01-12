using System.Collections.Generic;
using UnityEngine;

namespace Megaton
{
    public class RailCollection : MonoBehaviour
    {
        static RailCollection ins;
        public static RailCollection Ins => ins;

        public Dictionary<RailEnum, Rail> Rails = new();

        private void Awake()
        {
            ins = this;
        }

        /// <summary>
        /// 绑定轨道
        /// </summary>
        public static void BindRails()
        {
            foreach (var r in GameObject.FindGameObjectsWithTag("Rail"))
            {
                var rcompo = r.GetComponent<Rail>();
                if (rcompo == null)
                    Debug.LogError("Wrong GameObject With Tag \"Rail\" And No Rail Component");
                else
                    Ins.Rails.Add(rcompo.Id, rcompo);
            }
        }

        /// <summary>
        /// 为每一个轨道加载指令
        /// </summary>
        /// <param name="commands">指令字典</param>
        public static void LoadCommands(Dictionary<RailEnum, List<Command>> commands)
        {
            foreach (var command in commands)
            {
                if (Ins.Rails.ContainsKey(command.Key))
                {
                    Ins.Rails[command.Key].Commands = command.Value;
                }
            }
        }
    }
}