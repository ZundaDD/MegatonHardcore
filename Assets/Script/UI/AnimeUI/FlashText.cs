using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class FlashText : MonoBehaviour
    {
        private static float exist_time = 0.8f;
        private static float x_scale = 10f;
        private static float y_scale = 15f;
        [SerializeField] private AnimationCurve xCurve;
        [SerializeField] private AnimationCurve yCurve;
        private RectTransform rectT;
        private Vector2 origin;
        private float timer = 0;
        private Text text;

        void Start()
        {
            rectT = GetComponent<RectTransform>();
            text = GetComponent<Text>();
            origin = rectT.anchoredPosition;
            text.DOFade(0, exist_time).SetEase(Ease.InQuad);
        }

        void Update()
        {
            timer += Time.deltaTime;
            rectT.anchoredPosition = origin +
                new Vector2(xCurve.Evaluate(timer / exist_time) * x_scale,
                yCurve.Evaluate(timer / exist_time) * y_scale);
            if (timer > exist_time) Destroy(gameObject);
        }
    }
}