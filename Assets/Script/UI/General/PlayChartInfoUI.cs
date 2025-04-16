using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class PlayChartInfoUI : MonoBehaviour
    {
        [SerializeField] private Image cover;
        [SerializeField] private Text title;
        [SerializeField] private Text composer;
        [SerializeField] private Text level;

        void Start()
        {
            cover.sprite = CoverLoader.Path2Sprite(GameVar.CurPlay.Info.RootDir);
            title.text = GameVar.CurPlay.Info.Title;
            composer.text = GameVar.CurPlay.Info.Composer;
            level.text = GameVar.CurPlay.Info.GetLevelString();
        }
    }
}