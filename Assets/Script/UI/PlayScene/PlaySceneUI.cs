using UnityEngine;

namespace Megaton.UI
{
    public class PlaySceneUI : BottomUI
    {
        [SerializeField] private Canvas canvasFar;
        [SerializeField] private PauseUI pauseUI;
        [SerializeField] private ScoreboardUI scoreboardUI;

        protected override void Open()
        {
            //场景配置
            canvasFar.planeDistance = 100 + Setting.Ins.Board_Distance.Value * 10;

            //UI显示
            ScoreBoard.Clear(GameVar.CurPlay.Quantity, GameVar.CurPlay.Weight);
            scoreboardUI.Bind();
        }

        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.Player.Escape.performed += ctx => pauseUI.SwitchState();
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.Player.Escape.performed -= ctx => pauseUI.SwitchState();
        }
    }
}
