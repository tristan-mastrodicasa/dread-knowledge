using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    public int characterIndex;
    public GameManager gameManager;
    public Slider healthSlider;
    public Slider sanitySlider;
    public TextMeshProUGUI characterName;

    void Start() {
        StartCoroutine("UpdateDisplay");
    }

    IEnumerator UpdateDisplay() {
        while (true) {
            if(gameManager.characters.Count > characterIndex) {
                characterName.SetText(gameManager.characters[characterIndex].characterName);
                healthSlider.value = gameManager.characters[characterIndex].health;
                sanitySlider.value = gameManager.characters[characterIndex].sanity;
            } else {
                gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
