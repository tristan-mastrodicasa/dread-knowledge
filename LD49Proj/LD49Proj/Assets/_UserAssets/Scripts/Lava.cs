using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    
    private Material[] lavaMat;

    public Vector2 flowSpeed;


    private void Awake() {
        lavaMat = GetComponent<Renderer>().materials;
    }


    void Update() {
        foreach (Material m in lavaMat){
            m.mainTextureOffset = Time.time * flowSpeed;
        }
    }
        
}
