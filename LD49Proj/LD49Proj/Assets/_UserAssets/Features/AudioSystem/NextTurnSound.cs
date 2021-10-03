using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip nextTurnSound;

    public void PlayNextTurnSound() {
        audioSource.clip = nextTurnSound;
        audioSource.loop = false;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
}
