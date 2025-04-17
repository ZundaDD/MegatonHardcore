
using DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using Megaton.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    public class MainMenuController : MonoBehaviour
    {
        void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
        }

        public void Quit() => Application.Quit(); 
    }
}
