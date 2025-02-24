using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 全局音效播放器
    /// </summary>
    public class GlobalEffectPlayer : MonoBehaviour
    {
        private GlobalEffectPlayer ins;
 
        private void Awake()
        {
            if(ins != null) Destroy(gameObject);
            else ins = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}