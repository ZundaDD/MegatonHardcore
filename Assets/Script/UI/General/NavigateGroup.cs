using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Megaton.UI
{
    /// <summary>
    /// 选择回调接口，用于处理选择和取消选择事件
    /// </summary>
    public interface ISelectCallback
    {
        public GameObject gameobject { get; }
        public void OnSubmit(BaseEventData bed);
        public void OnSelect(BaseEventData bed);
        public void DeSelect(BaseEventData bed);
    }

    /// <summary>
    /// 导航组，用于管理一组可选择的UI元素
    /// </summary>
    public class NavigateGroup : MonoBehaviour
    {
        private GameObject curSelection;
        private EventTrigger eventTrigger;

        void Start()
        {
            BindEvents();
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void OnDestroy()
        {
            InputManager.Input.UI.Navigation.performed -= ReDirectToDefault;
        }

        public void BindEvents()
        {
            InputManager.Input.UI.Navigation.performed += ReDirectToDefault;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var go = transform.GetChild(i).gameObject;
                if (go.TryGetComponent(out ISelectCallback cbk))
                {
                    var eventTrigger = go.GetComponent<EventTrigger>();
                    if (eventTrigger == null) eventTrigger = go.gameObject.AddComponent<EventTrigger>();
                    if (eventTrigger.triggers == null) eventTrigger.triggers = new();


                    //绑定事件
                    BindEvent(eventTrigger, EventTriggerType.PointerEnter,
                        bed => ProcessHoverEnter(bed, cbk));
                    BindEvent(eventTrigger, EventTriggerType.PointerExit,
                        bed => ProcessHoverExit(bed, cbk));

                    BindEvent(eventTrigger, EventTriggerType.Submit, cbk.OnSubmit);
                    BindEvent(eventTrigger, EventTriggerType.PointerClick, cbk.OnSubmit);


                    BindEvent(eventTrigger, EventTriggerType.Select, cbk.OnSelect);
                    BindEvent(eventTrigger, EventTriggerType.Deselect, cbk.DeSelect);
                }
            }
        }


        public void ReDirectToDefault(InputAction.CallbackContext ctx)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                var fso = EventSystem.current.firstSelectedGameObject;
                EventSystem.current.SetSelectedGameObject(fso);
                curSelection = fso;
            }
        }



        /// <summary>
        /// 处理鼠标悬停事件
        /// </summary>
        private void ProcessHoverEnter(BaseEventData bed, ISelectCallback go)
        {
            if (curSelection != null)
            {
                if (curSelection != go.gameobject)
                {
                    var cbk = curSelection.GetComponent<ISelectCallback>();
                    cbk?.DeSelect(bed);
                }
            }
            EventSystem.current.SetSelectedGameObject(go.gameobject);
            curSelection = go.gameobject;
        }

        /// <summary>
        /// 处理鼠标离开事件
        /// </summary>
        private void ProcessHoverExit(BaseEventData bed, ISelectCallback go)
        {
            if (EventSystem.current == null) return;
            if (curSelection != null && go.gameobject != null && curSelection == go.gameobject)
            {
                go.DeSelect(bed);
                EventSystem.current.SetSelectedGameObject(null);
                curSelection = null;
            }
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="trigger">触发器</param>
        /// <param name="type">事件类型</param>
        /// <param name="callback">回调函数</param>
        public void BindEvent(EventTrigger trigger,EventTriggerType type, UnityAction<BaseEventData> callback)
        {
            //生成入口
            EventTrigger.Entry entry = new ();
            entry.eventID = type;

            //绑定回调
            if (entry.callback == null) entry.callback = new ();
            entry.callback.AddListener(callback);

            //添加到列表
            trigger.triggers.Add(entry);
        }
    }
}