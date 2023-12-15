using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Yunasawa.Utilities.UI
{
    [System.Serializable]
    public static class RaycastUI
    {
        /// <summary> Returns 'true' if we touched or hovering on Unity UI element. </summary>
        public static GameObject GetUIElement(int layer)
        {
            return IsPointerOverUI(GetEventSystemRaycastResults(), layer);
        }

        /// <summary> Returns 'true' if we touched or hovering on Unity UI element. </summary>
        public static GameObject IsPointerOverUI(List<RaycastResult> eventSystemRaysastResults, int layer)
        {
            foreach (var element in eventSystemRaysastResults)
            {
                if (element.gameObject.layer == layer) return element.gameObject;
            }
            return null;
        }

        /// <summary> Gets all event system raycast results of current mouse or touch position. </summary>
        public static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}