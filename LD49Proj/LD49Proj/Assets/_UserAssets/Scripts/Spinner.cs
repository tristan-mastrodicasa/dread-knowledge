using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 30f;
    void Update() {
        transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f), Space.World);
    }
}
