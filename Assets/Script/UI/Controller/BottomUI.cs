using System;

namespace Megaton.UI
{
    /// <summary>
    /// 场景最底层UI，具有唯一性
    /// 与流程控制逻辑分离
    /// </summary>
    public abstract class BottomUI : UICollection
    {
        public static BottomUI Instance { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            if (Instance != null) throw new Exception("场景最底层UI重复");
            Instance = this;
            
            BottomPush(this);
        }

        protected override void Open() { }
        protected override void Close() { }
    }
}