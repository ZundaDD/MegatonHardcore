using Megaton.Abstract;

namespace Megaton.Classic
{
    public class TapSO : NoteSO
    {
        public override void Judge(bool railState, bool formState)
        {
            if(!formState && railState || note.ExactTime - MusicPlayer.ExactTime > note.JudgeEnd)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}