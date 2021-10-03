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


    private void Start(){
        PopulateSpots();
        SetUpNameAndSleepSlots();
    }

    


    public void ReturnToCampaign(){
        GameManager.LoadCampaignScene();
    }

    public void ExecuteDecisions(){
        // 1 in 3 chance of there being an attack on any given night.
        bool isAttacked = Random.Range(0, 3) == 0;
        bool isGuarded = GetIsGuarded();

        foreach(DraggableName d in draggableNames){
            if (!d.gameObject.activeInHierarchy){continue;}

            if (d.currentSlot.thisRole == Roles.Guard){
                d.character.ChangeSanity(-1);
                if (isAttacked){
                    d.character.ChangeHealth(-Random.Range(1,3));
                }
            }

            if (d.currentSlot.thisRole == Roles.Heal){
                d.character.ChangeSanity(-1);
                d.character.ChangeHealth(1);
            }

            if (d.currentSlot.thisRole == Roles.Sleep){
                d.character.ChangeSanity(1);
            }

            if (isAttacked && !isGuarded){
                d.character.ChangeSanity(-1);
                d.character.ChangeHealth(-Random.Range(1, 3));
            }
        }
    }


    private bool GetIsGuarded(){
        foreach(DraggableName d in draggableNames){
            if (!d.gameObject.activeInHierarchy){continue;}

            if (d.currentSlot.thisRole == Roles.Guard){
                return true;
            }
        }

        return false;
    }
    

    
}
