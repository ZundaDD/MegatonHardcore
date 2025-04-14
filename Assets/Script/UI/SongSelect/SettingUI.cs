using DG.Tweening;
using Megaton.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class SettingUI : MonoBehaviour
    {
        [Header("交互组件")]
        [SerializeField] Button exitButton;
        [SerializeField] Button resetButton;
        [Header("预制件")]
        [SerializeField] GameObject configPrefab;
        [SerializeField] GameObject headPrefab;
        [SerializeField] GameObject keybindingPrefab;
        [Header("引用")]
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform plane;
        [SerializeField] EasyKeyBindConfig targetKeys;

        private CanvasGroup canvasGroup;
        private float contentHeight = 0;
        private float transTime = 0.25f;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            exitButton.onClick.AddListener(DisableAnimation);
            resetButton.onClick.AddListener(() =>
            {
                Setting.Reset();
                Refresh();
            });
            AddConfigObject();
        }

        public void DisableAnimation()
        {
            InputManager.rebind?.Cancel();
            InputManager.Input.UI.Escape.performed -= ctx => DisableAnimation();
            GlobalEffectPlayer.PlayEffect(AudioEffect.OnSettingExit);
            plane.DOScale(new Vector3(1.2f, 1.2f, 1.2f), transTime).SetEase(Ease.InOutCubic);
            canvasGroup.DOFade(0, transTime).SetEase(Ease.InOutCubic).OnComplete(() => gameObject.SetActive(false));
        }

        public void EnableAnimation()
        {
            InputManager.Input.UI.Escape.performed += ctx => DisableAnimation();
            gameObject.SetActive(true);
            plane.DOScale(new Vector3(1f, 1f, 1f), transTime).SetEase(Ease.InOutCubic);
            canvasGroup.DOFade(1, transTime).SetEase(Ease.InOutCubic);
        }

        #region 设置内容
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

        /// <summary>
        /// 刷新为新的配置
        /// </summary>
        private void Refresh()
        {
            Debug.Log(content.childCount);
            List<GameObject> childs = new();
            for (int i = 0; i < content.childCount; i++) childs.Add(content.GetChild(i).gameObject);
            foreach(var child in childs) Destroy(child.gameObject);
            AddConfigObject();
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

        private void GenerateKeyBind(string name, InputActionReference actionRef)
        {
            var go = Instantiate(keybindingPrefab).GetComponent<Rebinder>();
            go.SelectTarget(name, actionRef);
            go.transform.SetParent(content, false);
            contentHeight += go.GetComponent<RectTransform>().rect.height;
        }

        /// <summary>
        /// 添加设置实物
        /// </summary>
        public void AddConfigObject()
        {
            contentHeight = 2 * content.GetComponent<LayoutGroup>().padding.top;

            GenerateHead("时间");
            GenerateObject(Setting.Ins.Speed, "流速");
            GenerateObject(Setting.Ins.Input_Offset, "输入延迟", "$ms");
            GenerateObject(Setting.Ins.Music_Offset, "音乐延迟", "$ms");

            GenerateHead("场景");
            GenerateObject(Setting.Ins.Board_Distance, "挡板距离");

            GenerateHead("游玩");
            GenerateObject(Setting.Ins.Distinguish_Critical, "区分Critical");
            GenerateObject(Setting.Ins.Show_Fast_Late, "显示快慢");
            GenerateObject(Setting.Ins.Judge_Feedback_Height, "判定显示高度");
            GenerateObject(Setting.Ins.Float_Score_Type, "分数显示类型");

            GenerateHead("音频");
            GenerateObject(Setting.Ins.Effect_Volume, "音效音量", "$%");
            GenerateObject(Setting.Ins.Music_Volume, "音乐音量", "$%");

            GenerateHead("键位");
            foreach(var key in targetKeys.Configs)
            {
                GenerateKeyBind(key.name, key.action);
            }

            GenerateHead("资料展示");

            content.sizeDelta = new Vector2(content.rect.width,contentHeight);
        }
        #endregion
    }
}
