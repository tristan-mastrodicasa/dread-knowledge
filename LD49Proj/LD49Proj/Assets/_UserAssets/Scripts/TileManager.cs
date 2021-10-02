using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    // Singleton pattern.
    private static TileManager instance;
    private void Awake() {
        instance = this;
    }

    public static List<Tile> GetTiles(){
        return instance.tiles;
    }



    // List of all tiles in order.
    public List<Tile> tiles;
}
