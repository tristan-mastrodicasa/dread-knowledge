using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    public Text resultText;

    public float screenDuration = 10f;
    private float elapsedDuration = 0f;
    private void Start() {
        if (PersistCanvas.instance != null){
            Destroy(PersistCanvas.instance.gameObject);
        }

        if (GameManager.IsGameWon()){
            resultText.text = "At the edge of your wits, you have seen through the void and to the other side...";
        }
        else {
            resultText.text = "To the sound mind, the horrors of the inscrutable are too great...";
        }
    }

    private bool triggered = false;
    private void Update() {
        if (triggered){return;}

        elapsedDuration += Time.deltaTime;

        if (elapsedDuration >= screenDuration){
            triggered = true;
            FadeIn.FadeToScene(GameManager.IsGameWon() ? "EndGame" : "DefeatedScene");
        }
    }
}
