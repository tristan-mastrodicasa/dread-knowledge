using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart() {
        // todo: reset the game state
        SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
    }
}
