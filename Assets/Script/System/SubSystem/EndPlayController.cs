using Megaton.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    public class EndPlayController : MonoBehaviour
    {
        private static EndPlayController ins;
        public static EndPlayController Ins => ins;


        [SerializeField] private Button exitButton;

        void Awake()
        {
            if (!GameVar.IfInitialed)
            {
                SceneManager.LoadScene(0);
                return;
            }

            ins = this;
        }

        private void Start()
        {
            exitButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
    }
}
