using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    public class HoldSO : NoteSO
    {
        [SerializeField] private Transform holdEnd;
        [SerializeField] private Transform holdBrigde;

        public override void Bind(Note note)
        {
            if (note is not Hold) throw new System.Exception("Note绑定类型错误，需要Hold！");
            Hold noteH = note as Hold;

            //配置结束和连接实物
            holdEnd.localPosition = new Vector3(0, 0, GameVar.Velocity * noteH.ExactLength);
            holdBrigde.localPosition = new Vector3(0, 0, GameVar.Velocity * noteH.ExactLength / 2);
            holdBrigde.localScale = new Vector3
                (holdBrigde.localScale.x, holdBrigde.localScale.y, GameVar.Velocity * noteH.ExactLength);

            base.Bind(note);
        }

        public override void Judge(bool railState, bool formState)
        {
            float offset = MusicPlayer.ExactTime - note.ExactTime;
            float exl = (note as Hold).ExactLength;

            //头判
            if (!formState && railState && !(note as Hold).ifStart && Mathf.Abs(offset) < note.JudgeStart) GetComponent<MeshRenderer>().enabled = false;

            //连接截取
            if (railState && offset > 0 && offset < exl)
            {
                float z = GameVar.Velocity * (exl - offset);
                holdBrigde.localScale = new Vector3
                (holdBrigde.localScale.x, holdBrigde.localScale.y, z);
                holdBrigde.localPosition = new Vector3
                    (holdBrigde.localPosition.x, holdBrigde.localPosition.y, holdEnd.localPosition.z - z / 2);
            }
        }

    }
}