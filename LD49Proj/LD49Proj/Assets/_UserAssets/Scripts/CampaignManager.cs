using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using UnityEngine.SceneManagement;

public class CampaignManager : MonoBehaviour
{
    public List<SpotManager> spots;

    public TileNavigator dais;

    public TileManager tileManager;

    public void PopulateDeis(){
        for(int i = 0; i < 6; i++){
            
            if ( i >= GameManager.GetCharacters().Count ){
                spots[i].ShowCharacter(null);
                continue;
            }

            spots[i].ShowCharacter(GameManager.GetCharacters()[i]);
            
        }
    }

    public void MoveDeisToCurrTile(){
        TileSystem.Tile currTile = tileManager.tiles[GameManager.GetTileIndex()];
        dais.transform.position = currTile.tileAnchor.transform.position;
        dais.currentTile = currTile;
    }


    private void Start() {
        PopulateDeis();
        MoveDeisToCurrTile();
    }

    public void ReturnToTactical(){
        SceneManager.LoadScene("TacticalMap");
    }
}
