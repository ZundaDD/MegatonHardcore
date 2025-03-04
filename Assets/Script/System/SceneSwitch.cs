using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Megaton
{
    public class SceneSwitch : MonoBehaviour
    {
        private static SceneSwitch ins;
        public static SceneSwitch Ins => ins;

        [SerializeField] private float maskTime = 1.5f;
        [SerializeField] private RectTransform leftMask;
        [SerializeField] private RectTransform rightMask;

        private void Awake() => ins = this;

        void Start() => Opening();

        /// <summary>
        /// 进入场景
        /// </summary>
        private void Opening()
        {
            leftMask.DOAnchorPosX(leftMask.anchoredPosition.x - 1000f, maskTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(rightMask.anchoredPosition.x + 1000f, maskTime).SetEase(Ease.InOutCubic);
        }

        public void Ending(int sceneIndex)
        {
            leftMask.DOAnchorPosX(leftMask.anchoredPosition.x + 1000f, maskTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(rightMask.anchoredPosition.x - 1000f, maskTime).SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    StartCoroutine(loadScene(sceneIndex));
                });
        }

        public void Ending(string sceneName)
        {
            leftMask.DOAnchorPosX(leftMask.anchoredPosition.x + 1000f, maskTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(rightMask.anchoredPosition.x - 1000f, maskTime).SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    StartCoroutine(loadScene(sceneName));
                });
        }


        private IEnumerator loadScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone) yield return null;
        }

        private IEnumerator loadScene(int sceneIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoad.isDone) yield return null;
        }
    }
}