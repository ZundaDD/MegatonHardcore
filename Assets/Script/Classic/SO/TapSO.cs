using Megaton.Abstract;

namespace Megaton.Classic
{
    public class TapSO : NoteSO
    {
        public override void Judge(bool railState, bool formState)
        {
            if(!formState && railState || MusicPlayer.ExactTime - note.ExactTime > note.JudgeEnd)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}