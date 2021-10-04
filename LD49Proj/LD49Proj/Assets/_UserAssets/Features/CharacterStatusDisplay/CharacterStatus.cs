using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    public int characterIndex;
    public Slider healthSlider;
    public Slider sanitySlider;
    public TextMeshProUGUI characterName;

    public float sliderSpeed = 2f;

    void Start() {
        StartCoroutine("UpdateDisplay");
    }

    private void Update() {
        UpdateDisplay();
    }

    void UpdateDisplay() {
        List<Character> charList = GameManager.GetCharacters();
        if(charList.Count > characterIndex) {
            characterName.SetText(charList[characterIndex].characterName);
            healthSlider.value = Mathf.MoveTowards(healthSlider.value, charList[characterIndex].health, sliderSpeed * Time.deltaTime);
            sanitySlider.value = Mathf.MoveTowards(sanitySlider.value, charList[characterIndex].sanity, sliderSpeed * Time.deltaTime);
        } else {
            gameObject.SetActive(false);
        }
    }
}
