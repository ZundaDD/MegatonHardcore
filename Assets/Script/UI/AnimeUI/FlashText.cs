using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class FlashText : MonoBehaviour
    {
        private static float exist_time = 0.6f;
        private float timer = 0;
        private Text text;

        void Start()
        {
            text = GetComponent<Text>();
            text.DOFade(0, exist_time);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer > exist_time) Destroy(gameObject);
        }
    }
}