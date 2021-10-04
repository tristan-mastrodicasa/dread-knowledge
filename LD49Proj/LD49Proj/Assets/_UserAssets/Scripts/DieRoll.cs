using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TileSystem;

public class DieRoll : MonoBehaviour
{

    public List<Sprite> dieFaces;

    public Image dieCover;

    public Image mainDie;

    public Image diePips;



    public GameObject pushOnObject;
    public Image pushOnImage;
    public Text pushOnText;

    
    private void Start() {
        dieCover.color = new Color(dieCover.color.r, dieCover.color.g, dieCover.color.b, 0f);
        mainDie.enabled = false;
        diePips.enabled = false;
    }


    public void RollDie(){
        StartCoroutine(RollingDie());
    }


    public float rollDuration = 2f;
    
    private float elapsedTime;
    public IEnumerator RollingDie(){
        elapsedTime = 0f;
        mainDie.enabled = false;
        diePips.enabled = false;


        while (elapsedTime < rollDuration / 2f){
            elapsedTime += Time.deltaTime;

            dieCover.color = new Color(dieCover.color.r, dieCover.color.g, dieCover.color.b, elapsedTime / (rollDuration / 2f) );

            yield return null;
        }


        mainDie.enabled = true;
        diePips.enabled = true;

        int result = Random.Range(1, 7);
        mainDie.sprite = dieFaces[result-1];



        pushOnObject.SetActive(true);
        pushOnImage.color = new Color(pushOnImage.color.r, pushOnImage.color.g, pushOnImage.color.b, 0f);
        pushOnText.color  = new Color(pushOnText.color.r,  pushOnText.color.g,  pushOnText.color.b,  0f);

        
        while (elapsedTime < rollDuration){
            elapsedTime += Time.deltaTime;

            dieCover.color = new Color(dieCover.color.r, dieCover.color.g, dieCover.color.b, 1f - ( (elapsedTime - rollDuration / 2f) / (rollDuration / 2f) ) );

            pushOnImage.color = new Color(pushOnImage.color.r, pushOnImage.color.g, pushOnImage.color.b, (elapsedTime - rollDuration / 2f) / (rollDuration / 2f));
            pushOnText.color  = new Color(pushOnText.color.r,  pushOnText.color.g,  pushOnText.color.b,  (elapsedTime - rollDuration / 2f) / (rollDuration / 2f));


            yield return null;
        }

        dieCover.color = new Color(dieCover.color.r, dieCover.color.g, dieCover.color.b, 0f);
        

        InterpretRollResult(result);

    }

    public TileNavigator tileNav;

    public void InterpretRollResult(int result){
        tileNav.MoveForward(result);
    }


}
