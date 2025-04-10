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
        private bool locked = false;

        private void Awake()
        {
            ins = this;
        }

        void Start() => Opening();

        /// <summary>
        /// 进入场景
        /// </summary>
        private void Opening()
        {
            leftMask.DOAnchorPosX(-1000f, maskTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(1000f, maskTime).SetEase(Ease.InOutCubic);
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneOpen);
        }

        public void Ending(int sceneIndex)
        {
            if (locked) return;
            locked = true;

            leftMask.DOAnchorPosX(0, maskTime).SetEase(Ease.InOutCubic);
            rightMask.DOAnchorPosX(0, maskTime).SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    GlobalEffectPlayer.PlayEffect(AudioEffect.OnSceneExit);
                    StartCoroutine(loadScene(sceneIndex));
                });
        }

        public void Ending(string sceneName)  => Ending(NameToIndex(sceneName));

        private int NameToIndex(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string nameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(path);

                if (nameInBuildSettings == sceneName) return i;
            }

            throw new System.Exception($"在 Build Settings 中找不到名为 '{sceneName}' 的场景！");
        }

        private IEnumerator loadScene(int sceneIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoad.isDone) yield return null;
        }
    }
}