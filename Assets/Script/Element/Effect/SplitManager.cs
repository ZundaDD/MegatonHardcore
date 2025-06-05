using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Megaton.Effect
{
    public class SplitManager : MonoBehaviour
    {
        [SerializeField] private int extraSplits = 5;
        [SerializeField] private GameObject splitPrefab;
        private List<GameObject> splits = new();
        private float splitDist;
        private int maxDist = 0;
        private float baseZ;
    
        public void GenerateSplits()
        {
            //Debug.Log(GameVar.Velocity);
            //实际差距0.01s
            float velocity = GameVar.Velocity;
            baseZ = GameVar.PrepareFrame * Time.fixedDeltaTime * velocity;
            
            float boardDist = 100 + Setting.Ins.Board_Distance.Value * 10;
            splitDist = 240f / GameVar.CurPlay.Info.BPM * velocity;

            for(int i = 0;i <= (int)(boardDist / splitDist) + extraSplits;i++)
            {
                var go = Instantiate(splitPrefab, transform);
                go.transform.localPosition = new Vector3(0, 0, baseZ + i * splitDist);
                splits.Add(go);
            }

            maxDist = (int)(boardDist / splitDist) + extraSplits;
        }

        public void Update() => UpdateSplits();
        
        public void UpdateSplits()
        {
            foreach(var split in splits)
            {
                if (split.transform.position.z < GameCamera.Ins.transform.position.z)
                {
                    maxDist += 1;
                    split.transform.localPosition = new(0, 0, baseZ + splitDist * maxDist);
                }
            }
        }
    }
}
