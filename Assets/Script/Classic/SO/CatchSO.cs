using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    public class CatchSO : NoteSO
    {
        public override void Judge(bool railState, bool formState)
        {
            if (railState || MusicPlayer.ExactTime - note.ExactTime > note.JudgeEnd)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

    }
}