using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    private void Awake() {
        instance = this;
    }

    public float popupDuration = 3.5f;
    public AnimationCurve opacityCurve;

    public Image popupImage;
    public Text popupText;

    public static void DisplayPopup(string message){
        instance.DisplayPopupMessage(message);
    }

    public void DisplayPopupMessage(string message){
        StartCoroutine(ShowPopup(message));
    }

    public IEnumerator ShowPopup(string message){
        float elapsedTime = 0f;
        SetOpacity(0f);
        popupText.text = message;

        while (elapsedTime < popupDuration){
            SetOpacity(opacityCurve.Evaluate(elapsedTime/popupDuration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetOpacity(0f);
    }

    void SetOpacity(float opacity){
        popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, opacity);
        popupText.color  = new Color(popupText.color.r, popupText.color.g, popupText.color.b, opacity);
    }

    

    
}
