using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIndicator : MonoBehaviour
{
    private Light indicatorLight;
    private AudioSource indicatorAudio;


    private void Awake() {
        indicatorLight = GetComponent<Light>();
        indicatorAudio = GetComponent<AudioSource>();
    }


    public float lightFlashDuration = 1f;
    public float fadeAwayDuration = 1f;


    public Color damageColor;
    public Color healColor;
    public Color sanityColor;
    public Color insanityColor;

    public AudioClip damageSound;
    public AudioClip healSound;
    public AudioClip sanitySound;
    public AudioClip insanitySound;
    public AudioClip deathSound;


    public void IndicateDamage(){
        StopAllCoroutines();
        indicatorAudio.PlayOneShot(damageSound);
        StartCoroutine(FlashLight(damageColor));
    }
    public void IndicateHeal(){
        StopAllCoroutines();
        indicatorAudio.PlayOneShot(healSound);
        StartCoroutine(FlashLight(healColor));
    }
    public void IndicateSanity(){
        StopAllCoroutines();
        indicatorAudio.PlayOneShot(sanitySound);
        StartCoroutine(FlashLight(sanityColor));
    }
    public void IndicateInsanity(){
        StopAllCoroutines();
        indicatorAudio.PlayOneShot(insanitySound);
        StartCoroutine(FlashLight(insanityColor));
    }
    public void IndicateDeath(){
        StopAllCoroutines();
        indicatorAudio.PlayOneShot(deathSound);
        StartCoroutine(FlashLight(Color.white));
        StartCoroutine(FadeAway());
    }



    private IEnumerator FlashLight(Color col){
        float lightDuration = 0f;
        indicatorLight.enabled = true;
        indicatorLight.color = col;
        indicatorLight.intensity = 0f;

        while (lightDuration < lightFlashDuration / 2f){

            indicatorLight.intensity = lightDuration / (lightFlashDuration / 2f);

            lightDuration += Time.deltaTime;
            yield return null;
        }
        while (lightDuration < lightFlashDuration){
            indicatorLight.intensity = 1f - ( lightDuration - (lightFlashDuration / 2f) ) / (lightFlashDuration / 2f);
            
            lightDuration += Time.deltaTime;
            yield return null;
        }

        indicatorLight.enabled = false;
    }

    private IEnumerator FadeAway(){
        Material mat = transform.parent.GetComponentInChildren<Renderer>().material;
        float fadeDuration = 0;

        while (fadeDuration < fadeAwayDuration){
            float value = 1f - fadeDuration / fadeAwayDuration;
            mat.color = new Color(value, value, value, 1f);

            fadeDuration += Time.deltaTime;
            yield return null;
        }

        mat.color = new Color(0f, 0f, 0f, 1f);
    }
}
