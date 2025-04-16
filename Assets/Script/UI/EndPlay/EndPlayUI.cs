using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class EndPlayUI : BottomUI
    {
        [SerializeField] private Button exitButton;

        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.UI.Escape.performed += ctx => SceneSwitch.Ending(2);
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.UI.Escape.performed -= ctx => SceneSwitch.Ending(2);
        }
        
        public void Start()
        {
            exitButton.onClick.AddListener(() => SceneSwitch.Ending(2));
        }
    }
}
