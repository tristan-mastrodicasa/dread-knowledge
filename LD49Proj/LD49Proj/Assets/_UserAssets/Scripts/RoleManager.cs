using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleManager : MonoBehaviour
{

    public enum Roles {Sleep, Guard, Heal}

    public List<DraggableName> draggableNames;
    public List<RoleSlot> roleSlots;
    public List<TacSpotManager> spots;


    public void PopulateSpots(){
        for(int i = 0; i < 6; i++){
            
            if ( i >= GameManager.GetCharacters().Count ){
                spots[i].ShowCharacter(null);
                continue;
            }

            spots[i].ShowCharacter(GameManager.GetCharacters()[i]);
            
        }
    }

    public void SetUpNameAndSleepSlots(){
        for(int i = 0; i < 6; i++){

            if ( i >= GameManager.GetCharacters().Count ){

                draggableNames[i].gameObject.SetActive(false);

                roleSlots[i].gameObject.SetActive(false);
                continue;
            }

            draggableNames[i].GetComponentInChildren<Text>().text = GameManager.GetCharacters()[i].characterName;
            draggableNames[i].character = GameManager.GetCharacters()[i];

            roleSlots[i].character = GameManager.GetCharacters()[i];

        }
    }


    private void Start() {
        PopulateSpots();
        SetUpNameAndSleepSlots();
    }

    


    public void ReturnToCampaign(){
        GameManager.LoadCampaignScene();
    }
    

    
}
