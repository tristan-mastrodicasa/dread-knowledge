using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{

    public enum Roles {Sleep, Guard, Heal}

    public List<DraggableName> draggableNames;


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


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Roles:");

            
        }
    }


    private void Start() {
        PopulateSpots();
    }
    

    
}
