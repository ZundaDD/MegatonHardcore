

using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 控制生成判定反馈
    /// </summary>
    public class JudgeFeedBack : MonoBehaviour
    {
        private static JudgeFeedBack ins;
        public static JudgeFeedBack Ins => ins;

        private RectTransform rectT;
        [SerializeField] private Vector3 timeOffset;
        [Header("场景引用")]
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform worldPoint;
        [SerializeField] private RectTransform canvasPoint;
        [Header("预制件")]
        [SerializeField] private GameObject critical;
        [SerializeField] private GameObject perfect;
        [SerializeField] private GameObject great;
        [SerializeField] private GameObject good;
        [SerializeField] private GameObject miss;
        [SerializeField] private GameObject fast;
        [SerializeField] private GameObject late;

        private void Awake()
        {
            ins = this;
            rectT = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 在指定位置召唤判定反馈文字
        /// </summary>
        /// <param name="judge">判定结果</param>
        /// <param name="vector">世界位置</param>
        public void SummonAt(JudgeEnum judge, Vector3 vector)
        {
            vector = World2Canvas(vector);

            var judgeRect = InstantiateJudge(judge);
            judgeRect.transform.SetParent(rectT, false);
            judgeRect.anchoredPosition = vector;
            judgeRect.localScale = Vector3.one;

            var timeRect = InstantiateTime(judge);
            if (timeRect == null) return;
            timeRect.transform.SetParent(rectT, false);
            timeRect.anchoredPosition = vector - timeOffset;
            timeRect.localScale = Vector3.one;

        }

        public RectTransform InstantiateJudge(JudgeEnum judge)
        {
            switch (judge)
            {
                case JudgeEnum.MISS:
                    return Instantiate(miss).GetComponent<RectTransform>();
                case JudgeEnum.S_GOOD:
                case JudgeEnum.F_GOOD:
                    return Instantiate(good).GetComponent<RectTransform>();
                case JudgeEnum.S_GREAT:
                case JudgeEnum.F_GREAT:
                    return Instantiate(great).GetComponent<RectTransform>();
                case JudgeEnum.S_PERFECT:
                case JudgeEnum.F_PERFECT:
                    return Instantiate(perfect).GetComponent<RectTransform>();
                default:
                    return Instantiate(critical).GetComponent<RectTransform>();
            }
        }

        public RectTransform InstantiateTime(JudgeEnum judge)
        {
            if (judge > 0) return Instantiate(fast).GetComponent<RectTransform>();
            else if (judge < 0 && judge != JudgeEnum.MISS) return Instantiate(late).GetComponent<RectTransform>();
            else return null;
        }

        public Vector3 World2Canvas(Vector3 worldPos) => new Vector3(worldPos.x / worldPoint.position.x * canvasPoint.anchoredPosition.x, -120, 0);
        
    }
}
