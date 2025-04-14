using System.Collections.Generic;
using UnityEngine;
using System;
using MikanLab;
using Megaton.Abstract;

namespace Megaton
{
    /// <summary>
    /// 轨道统一管理器，提供对轨道的访问
    /// </summary>
    public class RailCollection : MonoBehaviour
    {
        private static RailCollection ins;
        public static RailCollection Ins => ins;

        private Dictionary<RailEnum, Rail> rails = new();
        public Dictionary<string, GameObject> notePrefabs = new();
        
        [Serializable]
        class StringPrefab
        {
            public string Id;
            public GameObject Prefab;
        }
        [SerializeField] private List<StringPrefab> stringPrefabs;

        private void Awake()
        {
            ins = this;
            foreach(var i in stringPrefabs) notePrefabs.Add(i.Id,i.Prefab);
            stringPrefabs = null;
        }

        public void TryJudge()
        {
            foreach(var rail in rails) rail.Value.TryJudge();
        }

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
                if (rcompo == null) Debug.LogError("Wrong GameObject With Tag \"Rail\" And No Rail Component");
                else rails.Add(rcompo.Id, rcompo);
            }
        }

        /// <summary>
        /// 为每一个轨道加载Note
        /// </summary>
        /// <param name="commands">指令字典</param>
        public void LoadNotes(Dictionary<RailEnum, List<Command>> commands)
        {
            foreach (var command in commands)
            {
                if (rails.ContainsKey(command.Key))
                {
                    rails[command.Key].LoadNote(command.Value.ConvertAll((x) => x as Note));
                }
            }
        }
    }
}