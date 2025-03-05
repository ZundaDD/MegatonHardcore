using UnityEngine;
using UnityEngine.Events;

namespace Megaton.UI
{
    /// <summary>
    /// 主界面每一个选项的单位视图
    /// </summary>
    public class PageCellView : MonoBehaviour
    {
        public UnityEvent DoPage;

        public void Quit() => Application.Quit();
    }
}
