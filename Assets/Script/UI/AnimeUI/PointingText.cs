using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 在Text后面加上浮动的...的组件
    /// </summary>
    public class PointingText : MonoBehaviour
    {
        [SerializeField] private int pointingCount = 3;
        [SerializeField] private float floatingTime = 0.5f;

        private Text text;
        private string setText = "";
        private float timer = 0f;
        private int curCount = 0;
        private bool ifFloating = false;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        public void SetText(string newText, bool floating = true)
        {
            text.text = newText;
            setText = newText;
            ifFloating = floating;
            timer = 0;
        }

        public void Update()
        {
            if (ifFloating)
            {
                timer += Time.deltaTime;
                //过时重置
                if (timer > floatingTime)
                {
                    timer = 0;
                    if(curCount == pointingCount)
                    {
                        curCount = 0;
                        text.text = setText;
                    }
                    else
                    {
                        curCount++;
                        text.text = text.text + '.';
                    }
                    
                }
            }
        }
    }
}