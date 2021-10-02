using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableName : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{


    public RoleSlot currentSlot;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public EventSystem eventSystem;

    private void Awake() {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }



    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Here we go.");
    }


    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Begin drag.");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData){

        List<RaycastResult> results = new List<RaycastResult>();

        eventSystem.RaycastAll(eventData, results);

        // If isn't over a slot, return to original slot.
        if (results.Count <= 0){
            ReturnToSlot();
        }

        Debug.Log("End drag.");
        canvasGroup.blocksRaycasts = true;
    }


    public void OnDrop(PointerEventData eventData){
        // If is over an occupied slot, return to original slot.
        if (eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<DraggableName>().ReturnToSlot();
        }
    }


    public void ReturnToSlot(){
        GetComponent<RectTransform>().anchoredPosition = currentSlot.GetComponent<RectTransform>().anchoredPosition;
    }
}
