using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void Start() {
        if (PersistCanvas.instance != null){
            Destroy(PersistCanvas.instance.gameObject);
        }
    }

    public void Restart() {
        Destroy(GameManager.instance);
        FadeIn.FadeToScene("Overworld");
    }
}
