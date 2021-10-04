using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{

    public static FadeIn instance;
    private void Awake() {
        instance = this;
    }

    public static void FadeToScene(string nextScene){
        instance.FadeTransition(nextScene);
    }
    public void FadeTransition(string nextScene){
        StartCoroutine(FadeOutRoutine(nextScene));
    }


    public float fadeDuration;
    private Image image;
    private void Start() {
        image = GetComponent<Image>();
        image.enabled = true;
        StartCoroutine(FadeInRoutine());
    }

    public IEnumerator FadeInRoutine(){
        float fadeTime = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        while (fadeTime < fadeDuration){
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f - fadeTime/fadeDuration);

            fadeTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    public IEnumerator FadeOutRoutine(string nextScene){
        float fadeTime = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        while (fadeTime < fadeDuration){
            image.color = new Color(image.color.r, image.color.g, image.color.b, fadeTime/fadeDuration);

            fadeTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        SceneManager.LoadScene(nextScene);
        
    }

    
}
