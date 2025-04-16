using UnityEngine;

namespace Megaton.UI
{
    /// <summary>
    /// 场景面板UI，实时打开关闭
    /// </summary>
    public abstract class PanelUI : UICollection
    {
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }
        
    }
}