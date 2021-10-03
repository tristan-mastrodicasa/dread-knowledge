using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScene : MonoBehaviour
{
    void Start () {
        Destroy(GameObject.Find("Game Manager"));
        Destroy(GameObject.Find("CharacterStatus"));
    }
}
