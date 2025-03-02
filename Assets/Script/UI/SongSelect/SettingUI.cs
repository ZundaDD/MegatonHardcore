using DG.Tweening;
using Megaton.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class SettingUI : MonoBehaviour
    {
        
        [SerializeField] Button exitButton;
        [SerializeField] GameObject configPrefab;
        [SerializeField] GameObject headPrefab;
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform plane;

        private CanvasGroup canvasGroup;
        private float contentHeight = 0;
        private float transTime = 0.25f;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            exitButton.onClick.AddListener(() =>
            {
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSettingExit);
                plane.DOScale(new Vector3(1.2f, 1.2f, 1.2f), transTime).SetEase(Ease.InOutCubic);
                canvasGroup.DOFade(0, transTime).SetEase(Ease.InOutCubic).OnComplete(()=>gameObject.SetActive(false));
            });
            AddConfigObject();
        }

        public void EnableAnimation()
        {
            gameObject.SetActive(true);
            plane.DOScale(new Vector3(1f, 1f, 1f), transTime).SetEase(Ease.InOutCubic);
            canvasGroup.DOFade(1, transTime).SetEase(Ease.InOutCubic);
        }

        /// <summary>
        /// 关闭时自动保存
        /// </summary>
        private void OnDisable()
        {
            if (GameVar.IfInitialed)
            {
                Setting.SaveToFile();
                Debug.Log("Setting Saved");
            }
        }

        private void GenerateObject(SettingVarible config, string name, string syntax = "$")
        {
            var go = Instantiate(configPrefab).GetComponent<ConfigCellView>();
            go.Bind(config, name, syntax);
            go.transform.SetParent(content, false);
            contentHeight += go.GetComponent<RectTransform>().rect.height;
        }

        private void GenerateHead(string name)
        {
            var go = Instantiate(headPrefab).GetComponent<Text>();
            go.text = name;
            go.transform.SetParent(content, false);
            contentHeight += go.GetComponent<RectTransform>().rect.height;
        }

        /// <summary>
        /// 添加设置实物
        /// </summary>
        public void AddConfigObject()
        {
            GenerateHead("时间设置");
            GenerateObject(Setting.Ins.Speed, "流速");
            GenerateObject(Setting.Ins.Input_Offset, "输入延迟", "$ms");
            GenerateObject(Setting.Ins.Music_Offset, "音乐延迟", "$ms");

            GenerateHead("游玩设置");
            GenerateObject(Setting.Ins.Distinguish_Critical, "区分Critical");
            GenerateObject(Setting.Ins.Show_Fast_Late, "显示快慢");

            GenerateHead("音频设置");
            GenerateObject(Setting.Ins.Effect_Volume, "音效音量", "$%");
            GenerateObject(Setting.Ins.Music_Volume, "音乐音量", "$%");

            content.sizeDelta = new Vector2(content.rect.width,contentHeight);
        }
    }
}
