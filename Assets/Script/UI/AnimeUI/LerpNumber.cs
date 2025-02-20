using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 插值数字实现平滑改变
    /// </summary>
    public class LerpNumber : MonoBehaviour
    {
        private Text text;
        [SerializeField] private int precision = 8;
        [SerializeField] private float transTime = 0.1f;
        private float timer = 0;
        private int preNumber = 0;
        private int fixedNumber = 0;

        void Start()
        {
            text = GetComponent<Text>();
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer < transTime) text.text = (preNumber + (fixedNumber - preNumber) * timer / transTime).ToString().PadLeft(precision,'0');
            else text.text = fixedNumber.ToString().PadLeft(precision, '0');
        }

        public void SetNumer(int value)
        {
            timer = 0;
            preNumber = fixedNumber;
            fixedNumber = value;
        }
    }
}
