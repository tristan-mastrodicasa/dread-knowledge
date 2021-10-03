using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoleSlot : MonoBehaviour, IDropHandler
{
    public DraggableName occupyingName = null;

    public Character character;

    public bool isSleepSlot = false;

    public RoleManager.Roles thisRole;


    public void OnDrop(PointerEventData eventData){
        if (eventData.pointerDrag != null){
            if (isSleepSlot && eventData.pointerDrag.GetComponent<DraggableName>().character != character){
                eventData.pointerDrag.GetComponent<DraggableName>().ReturnToSlot();
                return;
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            occupyingName = eventData.pointerDrag.GetComponent<DraggableName>();
            eventData.pointerDrag.GetComponent<DraggableName>().currentSlot = this;
        }

        
    }
}
