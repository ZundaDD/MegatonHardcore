using System.Collections.Generic;
using UnityEngine;

namespace Megaton
{
    public class RailCollection : MonoBehaviour
    {
        static RailCollection ins;
        public static RailCollection Ins => ins;

        public Dictionary<RailEnum, Rail> Rails;

        public void Awake()
        {
            Rails = new();
            foreach(var r in GameObject.FindGameObjectsWithTag("Rail"))
            {
                var rcompo = r.GetComponent<Rail>();
                if (rcompo == null)
                    Debug.LogError("Wrong GameObject With Tag \"Rail\" And No Rail Component");
                else
                    Rails.Add(rcompo.Id, rcompo);
            }
        }
    }
}