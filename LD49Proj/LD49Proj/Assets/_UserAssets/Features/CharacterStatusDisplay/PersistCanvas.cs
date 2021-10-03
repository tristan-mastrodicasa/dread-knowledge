using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    // Don't Destroy On Load Singleton class. This class ensures continuity throughout the game.
    private static PersistCanvas instance;
    private void Awake() {
        if (instance == null){
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
            return;
        }
        
    }
}
