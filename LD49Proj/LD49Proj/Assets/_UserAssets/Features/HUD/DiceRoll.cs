using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public List<Texture> diceFaces;
    public RawImage diceDisplay;
    int diceNumberToLandOn;

    void Awake() {
        diceDisplay.gameObject.SetActive(false);
    }

    //
    // Summary:
    //     Returns a dice roll and starts the animation
    public void RollDice() {
        int roll = Random.Range(1, 6);
        SimulateDiceRoll(roll);
        // return roll;
    }

    //
    // Summary:
    //     Simulate a dice roll landing on a predefined value
    //
    // Parameters:
    //   diceNumberToLandOn: Number from 1 to 6
    void SimulateDiceRoll(int diceNumberToLandOn) {
        diceDisplay.gameObject.SetActive(true);
        this.diceNumberToLandOn = diceNumberToLandOn;
        StartCoroutine("RollTheDice");
    }

    IEnumerator RollTheDice() {
        float rolls = 1;
        float numberOfRollsToShow = 45;
        int diceFaceIndex;

        while(rolls != numberOfRollsToShow) {
            float timeBetweenDiceFaceChanges = 0.0001f * (rolls * rolls);
            rolls++;

            if ((rolls) != numberOfRollsToShow) {
                diceFaceIndex = Random.Range(0, 5);
                diceDisplay.texture = diceFaces[diceFaceIndex];
            } else {
                diceDisplay.texture = diceFaces[diceNumberToLandOn - 1];
            }
            yield return new WaitForSeconds(timeBetweenDiceFaceChanges);
        }

        yield return new WaitForSeconds(1.5f);
        diceDisplay.gameObject.SetActive(false);
    }
    
}
