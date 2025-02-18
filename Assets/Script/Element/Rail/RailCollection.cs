using System.Collections.Generic;
using UnityEngine;
using System;
using Megaton.Abstract;

namespace Megaton
{
    public class RailCollection : MonoBehaviour
    {

        private Dictionary<RailEnum, Rail> rails = new();

        public Rail this[RailEnum index]
        {
            get
            {
                if (rails.ContainsKey(index)) return rails[index];
                else throw new Exception($"Index {index} Not Found!");
            }
        }


        /// <summary>
        /// 绑定轨道
        /// </summary>
        public void CollectRails()
        {
            foreach (var r in GameObject.FindGameObjectsWithTag("Rail"))
            {
                var rcompo = r.GetComponent<Rail>();
                if (rcompo == null)
                    Debug.LogError("Wrong GameObject With Tag \"Rail\" And No Rail Component");
                else
                {
                    rails.Add(rcompo.Id, rcompo);
                    Debug.Log($"Rail {rcompo.Id} Loaded");
                }
            }
        }

        /// <summary>
        /// 为每一个轨道加载指令
        /// </summary>
        /// <param name="commands">指令字典</param>
        public void LoadNotes(Dictionary<RailEnum, List<Command>> commands)
        {
            foreach (var command in commands)
            {
                if (rails.ContainsKey(command.Key))
                {
                    rails[command.Key].Notes = command.Value.ConvertAll((x) => x as Note);
                    rails[command.Key].CalculateMax();
                }
            }
        }
    }
}