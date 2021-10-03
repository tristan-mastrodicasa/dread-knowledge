using System.Collections;
using UnityEngine;

public class MovingDiasSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footStepSound;
    Vector3 lastPosition;

    void Start() {
        StartCoroutine("UpdateLocation");
        StartCoroutine("MovingDias");
    }

    IEnumerator MovingDias() {
        while (true) {
            if (lastPosition != transform.position) {
                audioSource.clip = footStepSound;
                audioSource.loop = false;
                audioSource.volume = 0.1f;
                audioSource.Play();
            }

            yield return new WaitForSeconds(0.6f);
        }
    }

    IEnumerator UpdateLocation() {
        while (true) {
            lastPosition = transform.position;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
