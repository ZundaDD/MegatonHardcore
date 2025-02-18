using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class PlayInfo : MonoBehaviour
    {
        [SerializeField] private Image cover;
        [SerializeField] private Text title;
        [SerializeField] private Text composer;
        [SerializeField] private Text level;

        void Start()
        {
            cover.sprite = CoverLoader.Path2Sprite(GameVar.Ins.CurPlay.Info.RootDir);
            title.text = GameVar.Ins.CurPlay.Info.Title;
            composer.text = GameVar.Ins.CurPlay.Info.Composer;
            level.text = GameVar.Ins.CurPlay.Info.GetLevelString();
        }

        
    }
}