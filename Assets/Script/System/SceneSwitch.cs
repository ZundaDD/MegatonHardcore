using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Megaton
{
    /// <summary>
    /// 场景切换的逻辑类
    /// </summary>
    public class SceneSwitch : MonoBehaviour
    {
        public static Action OnSceneChange;

        public static float LoadProgress { get; private set; } = 0f;

        public static readonly float minSwitchTime = 1.5f;

        private static SceneSwitch Ins;
        private bool locked = false;

        private void Awake() => Ins = this;

        public static void Ending(int sceneIndex)
        {
            if (Ins.locked) return;
            Ins.locked = true;

            OnSceneChange.Invoke();
            Ins.StartCoroutine(Ins.loadScene(sceneIndex));
        }

        public static void Ending(string sceneName)  => Ending(NameToIndex(sceneName));

        private static int NameToIndex(string sceneName)
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
            float startTime = Time.time;
            LoadProgress = 0f;

            //启动磁盘加载
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;
            while (asyncLoad.progress < 0.9f)
            {
                LoadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                yield return null;
            }

            LoadProgress = 1f;

            //等待切换动画
            float waitTime = minSwitchTime - Time.time + startTime;
            if(waitTime > 0) yield return new WaitForSeconds(waitTime);

            //进行场景创建
            asyncLoad.allowSceneActivation = true;
        }
    }
}