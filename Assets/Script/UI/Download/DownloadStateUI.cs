using Megaton.Web;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class DownloadStateUI : MonoBehaviour
    {
        [SerializeField] private OSUHandler osuHandler;
        [SerializeField] private Slider progressBar;
        [SerializeField] private Text statusText;


        void Update()
        {
            progressBar.value = osuHandler.Progress;
            statusText.text = osuHandler.Status;
        }
    }
}