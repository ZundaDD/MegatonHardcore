using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.UI
{
    public class PlaySceneUI : BottomUI
    {
        [SerializeField] private Canvas canvasFar;
        [SerializeField] private PauseUI pauseUI;
        [SerializeField] private ScoreboardUI scoreboardUI;

        protected override bool Open()
        {
            //场景配置
            canvasFar.planeDistance = 100 + Setting.Ins.Board_Distance.Value * 10;

            //UI显示
            ScoreBoard.Clear(GameVar.CurPlay.Quantity, GameVar.CurPlay.Weight);
            scoreboardUI.Bind();

            return true;
        }

        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.Player.Escape.performed += SummonPauseUI;
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.Player.Escape.performed -= SummonPauseUI;
        }

        private void SummonPauseUI(InputAction.CallbackContext ctx) => Push(pauseUI);
    }
}
