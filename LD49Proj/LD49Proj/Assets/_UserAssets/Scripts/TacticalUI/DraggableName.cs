using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableName : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{


    public RoleSlot currentSlot;

    public Character character;





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
        
    }


    public void OnBeginDrag(PointerEventData eventData){
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
        else {
            // If no slot is involved, return to original slot.
            bool acceptable = false;
            foreach (RaycastResult r in results){
                if (r.gameObject.GetComponent<RoleSlot>() != null){
                    acceptable = true;
                    break;
                }
            }

            if (!acceptable){
                ReturnToSlot();
            }
        }


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
