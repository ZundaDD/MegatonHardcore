using Megaton.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Megaton
{
    public class DownloadSceneController : MonoBehaviour
    {
        [SerializeField] private OSUHandler osuHandler;

        public void Awake()
        {
            if (!GameVar.IfInitialed) SceneManager.LoadScene(0);
        }

        private void Start()
        {
            osuHandler.DownLoadChart(2342660);
            osuHandler.DownLoadChart(2340628);
            osuHandler.DownLoadChart(1872276);
            osuHandler.DownLoadChart(1107469);
        }
    }
}
