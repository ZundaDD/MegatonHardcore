using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// UI集合，是所有层级UI的基类
    /// </summary>
    public abstract class UICollection : MonoBehaviour
    {   
        protected CanvasGroup canvasGroup;

        protected static Stack<UICollection> level = new();

        #region 层操作
        static UICollection()
        {
            SceneSwitch.OnSceneChange += AllPop;
        }

        /// <summary>
        /// 进入新场景后，清空栈并将底层UI入栈
        /// </summary>
        /// <param name="bottomUI">最底层UI</param>
        public static void BottomPush(BottomUI ui)
        {
            if (level.Count > 0) throw new Exception("重复添加最底层UI");

            level.Push(ui);
            ui.Open();
            ui.EnableInteract();
        }

        /// <summary>
        /// 当场景切换时，关闭所有UI的交互
        /// </summary>
        private static void AllPop()
        {
            if (level.Count < 1) throw new Exception("层级栈为空");
            while (level.Count > 0)
            {
                //只关闭交互，不关闭UI
                level.Peek().DisableInteract();
                level.Pop();
            }
        }

        /// <summary>
        /// 弹出顶层UI，
        /// </summary>
        public static void Pop()
        {
            if (level.Count < 2) throw new Exception("层级栈不满足最底层约束");

            //弹出顶层元素
            var ui = level.Pop();
            ui.Close();
            ui.DisableInteract();

            //恢复交互性
            level.Peek().EnableInteract();
        }

        public void Push(UICollection ui)
        {
            if (level.Count < 1) throw new Exception("层级栈为空");
            if (ui is BottomUI) throw new Exception("尝试插入底层UI，请使用BottomPush"); 
            
            //上级元素沉默
            level.Peek().DisableInteract();
            
            //新元素入栈
            level.Push(ui);
            ui.Open();
            ui.EnableInteract();
        }
        #endregion

        #region 交互设置
        protected virtual void EnableInteract()
        {
            canvasGroup.interactable = false;
        }

        protected virtual void DisableInteract()
        {
            canvasGroup.interactable = true;
        }

        #endregion

        #region 生命周期

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) Debug.LogError("面板UI缺少CanvasGroup组件，无法管理");
        }

        /// <summary>
        /// 打开层
        /// </summary>
        protected abstract void Open();
        
        /// <summary>
        /// 关闭层
        /// </summary>
        protected abstract void Close();
        #endregion
    }
}
