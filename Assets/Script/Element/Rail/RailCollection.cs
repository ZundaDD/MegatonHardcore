using System.Collections.Generic;
using UnityEngine;
using System;
using MikanLab;
using Megaton.Abstract;

namespace Megaton
{
    public class RailCollection : MonoBehaviour
    {
        private static RailCollection ins;
        public static RailCollection Ins => ins;

        private Dictionary<RailEnum, Rail> rails = new();
        private Dictionary<string, GameObject> notePrefabs = new();
        
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
                if (rcompo == null)
                    Debug.LogError("Wrong GameObject With Tag \"Rail\" And No Rail Component");
                else
                {
                    rails.Add(rcompo.Id, rcompo);
                    //Debug.Log($"Rail {rcompo.Id} Loaded");
                }
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
                    rails[command.Key].Notes = command.Value.ConvertAll((x) => x as Note);
                    Debug.Log($"Rail:{command.Key}, Note:{rails[command.Key].Notes.Count}");
                }
            }
        }

        /// <summary>
        /// 生成Notes
        /// </summary>
        public void GenerateNotes()
        {
            foreach (var rail in rails)
            {
                foreach (var note in rail.Value.Notes)
                {
                    var go = Instantiate(notePrefabs[note.GetType().Name]);
                    go.GetComponent<NoteSO>().Bind(note);
                    go.transform.position += new Vector3(
                        rail.Value.transform.position.x,
                        rail.Value.transform.position.y,
                        GameCamera.Ins.JudgeLineZ);
                }
            }
        }
    }
}