using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    // Singleton pattern.
    public static TileManager instance;
    private void Awake() {
        instance = this;
    }

    public static List<TileSystem.Tile> GetTiles(){
        return instance.tiles;
    }

    // List of all tiles in order.
    public List<TileSystem.Tile> tiles;



    // Special tiles.
    public enum TileType {Normal, Forest, Treasure, Monsters, Aberration}


    public static void PopulateGameTiles(){
        instance.PopulateTiles();
    }

    public void PopulateTiles(){
        List<TileType> tileTypeList = GameManager.GetTileTypes();
        for(int i = 0; i < tiles.Count; i++){
            tiles[i].tileType = tileTypeList[i];
            
            GameObject iconPrefab = null;
            switch(tiles[i].tileType){
                case TileType.Forest:
                    iconPrefab = forestIcon;
                    break;
                case TileType.Treasure:
                    iconPrefab = treasureIcon;
                    break;
                case TileType.Monsters:
                    iconPrefab = monstersIcon;
                    break;
                case TileType.Aberration:
                    iconPrefab = aberrationIcon;
                    break;
                default:
                    break;
            }

            if (iconPrefab == null){continue;}

            GameObject.Instantiate<GameObject>(iconPrefab, tiles[i].tileTypeIconAnchor.transform.position, Quaternion.identity);


        }
    }

    public GameObject forestIcon;
    public GameObject treasureIcon;
    public GameObject monstersIcon;
    public GameObject aberrationIcon;

    

    
}
