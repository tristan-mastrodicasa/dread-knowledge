using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public List<SpotManager> spots;

    public void PopulateDeis(){
        for(int i = 0; i < 6; i++){
            
            if ( i >= GameManager.GetCharacters().Count ){
                spots[i].ShowCharacter(null);
                continue;
            }

            spots[i].ShowCharacter(GameManager.GetCharacters()[i]);
            
        }
    }

    private void Start() {
        PopulateDeis();
    }
}
