using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoleSlot : MonoBehaviour, IDropHandler
{
    public DraggableName occupyingName = null;

    public void OnDrop(PointerEventData eventData){
        if (eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            occupyingName = eventData.pointerDrag.GetComponent<DraggableName>();
            eventData.pointerDrag.GetComponent<DraggableName>().currentSlot = this;
        }

        
    }
}
