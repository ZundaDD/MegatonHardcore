using UnityEngine;

namespace Megaton.Abstract
{
    public abstract class NoteSO : MonoBehaviour
    {
        protected Note note;

        public void Bind(Note note)
        {
            this.note = note;
            note.OnJudge += Judge;
        }

        public abstract void Judge(bool railState,bool formState);
    }
}