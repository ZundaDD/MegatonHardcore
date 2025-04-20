using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class UnDragableRect : ScrollRect
    {
        public override void OnScroll(PointerEventData data) { }
        public override void OnBeginDrag(PointerEventData eventData) { }
        public override void OnDrag(PointerEventData eventData) { }
        public override void OnEndDrag(PointerEventData eventData) { }
    }
}