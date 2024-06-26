using Core.Mouse3D;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MouseOverUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Mouse3D.Instance.DisableMouse();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Mouse3D.Instance.EnableMouse();
        }
    }
}
