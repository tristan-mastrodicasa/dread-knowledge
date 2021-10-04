using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart() {
        Destroy(GameManager.instance);
        FadeIn.FadeToScene("Overworld");
    }
}
