using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class InOutText : MonoBehaviour
    {
        public static float limit = 0.1f;
        
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        public void ChangeTo(string newText,bool ifTween = true)
        {
            text.text = newText;

            if (!ifTween) return;

            //先变换
            var curColor = text.color;
            curColor.a = 0;
            text.color = curColor;
            text.transform.position = text.transform.position + new Vector3(10, 0, 0);
            
            //再恢复
            text.DOFade(1, limit).SetEase(Ease.InOutCubic);
            text.transform.DOMoveX(text.transform.position.x - 10f,limit).SetEase(Ease.InOutCubic);
        }

    }
}